// Возможно, в будущем будем использовать. Привет ) (c)
// using System;
// using System.Collections.Generic;
// using Core;
// using Libs.Analytics;
// using Proxifier;
// using Purchase.Services.Signals;
// using UnityEngine;
//
// #if TEST_ANALYTICS && UNITY_EDITOR
// using System.Text;
// #endif
//
// namespace Libs.AnalyticsAppMetrica
// {
//     public class AppMetricaRevenueHandler : IAnalyticHandler
//     {
//         private readonly IYandexAppMetrica _appMetrica;
//
//         public AppMetricaRevenueHandler(
//             IYandexAppMetrica appMetrica
//         )
//         {
//             _appMetrica = appMetrica;
//         }
//
//
//         public void HandleSignalProxyBefore(IMethodMessage methodMessage)
//         {
//         }
//
//         public void HandleSignalProxyAfter(IReturnMessage methodMessage)
//         {
//         }
//
//         public void HandleAnalyticables(IEnumerable<IAnalyticable> analyticables)
//         {
//         }
//
//         public void HandleSignal(object signal)
//         {
//             if (signal is SignalInAppPurchase revenue)
//             {
//                 if (!revenue.IsValidated)
//                     return;
//                 
//                 HandleSignalAdRevenue(revenue);
//             }
//         }
//
//         private void HandleSignalAdRevenue(SignalInAppPurchase revenueSignal)
//         {
//             var product = revenueSignal.Product;
//             var currency = product.metadata.isoCurrencyCode;
//             var price = product.metadata.localizedPrice;
//
//             var revenue = new YandexAppMetricaRevenue(price, currency)
//             {
//                 Quantity = 1, ProductID = product.definition.id,
//             };
//             
//             if (product.receipt != null) 
//             {
//                 var yaReceipt = new YandexAppMetricaReceipt();
//                 var receipt = JsonUtility.FromJson<Receipt>(product.receipt);
//                 
//                 if (!PlatformUtils.IsEditor())
//                 {
//                     if (PlatformUtils.IsAndroid())
//                     {
//                         var payloadAndroid = JsonUtility.FromJson<PayloadAndroid>(receipt.Payload);
//                         yaReceipt.Signature = payloadAndroid.Signature;
//                         yaReceipt.Data = payloadAndroid.Json;
//                     }
//
//                     if (PlatformUtils.IsIPhone())
//                     {
//                         yaReceipt.TransactionID = receipt.TransactionID;
//                         yaReceipt.Data = receipt.Payload;
//                     }
//                 }
//                 
//                 revenue.Receipt = yaReceipt;
//             }
//             
//             _appMetrica.ReportRevenue(revenue);
//             _appMetrica.SendEventsBuffer();
//             
// #if TEST_ANALYTICS && UNITY_EDITOR
//             Debug.Log($"[TestAnalytics] | Sent (ReportRevenue).\nRevenue params: \n{GetStringParams(revenue)}");
// #endif
//         }
//         
//         [Serializable]
//         private struct Receipt
//         {
//             public string Store;
//             public string TransactionID;
//             public string Payload;
//         }
//
//         [Serializable]
//         private struct PayloadAndroid
//         {
//             public string Json;
//             public string Signature;
//         }
//       
// #if TEST_ANALYTICS && UNITY_EDITOR
//         private static string GetStringParams(YandexAppMetricaRevenue revenue)
//         {
//             var stringBuilder = new StringBuilder();
//
//             stringBuilder.Append("\n");
//             AppendObject(nameof(revenue.PriceDecimal), revenue.PriceDecimal);
//             AppendObject(nameof(revenue.Currency), revenue.Currency);
//             AppendObject(nameof(revenue.Quantity), revenue.Quantity);
//             AppendObject(nameof(revenue.ProductID), revenue.ProductID);
//             AppendObject(nameof(revenue.Payload), revenue.Payload);
//             
//             return stringBuilder.ToString();
//
//             void AppendObject(string name, object o)
//             {
//                 stringBuilder.Append(name);
//                 stringBuilder.Append(": ");
//                 stringBuilder.Append(o);
//                 stringBuilder.Append("\n");
//             }
//         }
// #endif
//     }
// }