#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NorskaLib.Spreadsheets;
using UnityEditor;
using UnityEngine;

namespace Game.Utils.Sheets
{
    public abstract class ASpreadSheetImporter<T, TI> where T : ScriptableObject, TI
    {
        private const BindingFlags LISTS_FIELD_BINDING = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
        
        protected TI Data;
        
        private string _documentId;
        
        private SpreadsheetImporter _importer;

        public async Task ImportData(T data, string documentId)
        {
            // Более показать не могу
        }

        protected abstract void ParseData();

        private async Task GetDataFromSpreadsheet(T data, string documentId)
        {
            // Более показать не могу
        }
        
        private async Task OnClickImport(FieldInfo[] selectedContentFields)
        {
            // Более показать не могу
        }
        
        private void OnImportQueueComplete()
        {
            // Более показать не могу
        }
        
        private void OnOutputChanged()
        {
            // Более показать не могу
        }
        
        private void OnProgressChanged()
        {
            // Более показать не могу
        }
        
        private void OnOperationFailed(string message)
        {
            // Более показать не могу
        }
    }
}
#endif