source 'https://github.com/CocoaPods/Specs.git'
source 'https://github.com/appodeal/CocoaPods.git'
source 'https://github.com/bidon-io/CocoaPods_Specs.git'
platform :ios, '13.0'

target 'UnityFramework' do
    pod 'Appodeal', '3.5.0.0'
    pod 'APDAdjustAdapter', '3.5.0.0'
    pod 'APDAmazonAdapter', '3.5.0.0'
    pod 'APDAppLovinAdapter', '3.5.0.0'
    pod 'APDAppLovinMAXAdapter', '3.5.0.0'
    pod 'APDAppsFlyerAdapter', '3.5.0.0'
    pod 'APDBidMachineAdapter', '3.5.0.0'
    pod 'APDBidonAdapter', '3.5.0.0'
    pod 'APDBigoAdsAdapter', '3.5.0.0'
    pod 'APDDTExchangeAdapter', '3.5.0.0'
    pod 'APDFacebookAdapter', '3.5.0.0'
    pod 'APDFirebaseAdapter', '3.5.0.0'
    pod 'APDGoogleAdMobAdapter', '3.5.0.0'
    pod 'APDIABAdapter', '3.5.0.0'
    pod 'APDInMobiAdapter', '3.5.0.0'
    pod 'APDIronSourceAdapter', '3.5.0.0'
    pod 'APDMetaAudienceNetworkAdapter', '3.5.0.0'
    pod 'APDMintegralAdapter', '3.5.0.0'
    pod 'APDMyTargetAdapter', '3.5.0.0'
    pod 'APDPangleAdapter', '3.5.0.0'
    pod 'APDSentryAdapter', '3.5.0.0'
    pod 'APDUnityAdapter', '3.5.0.0'
    pod 'APDVungleAdapter', '3.5.0.0'
    pod 'APDYandexAdapter', '3.5.0.0'
    pod 'AppLovinMediationAmazonAdMarketplaceAdapter', '5.0.1.0'
    pod 'AppLovinMediationBidMachineAdapter', '3.2.0.0.0'
    pod 'AppLovinMediationFacebookAdapter', '6.16.0.0'
    pod 'AppLovinMediationFyberAdapter', '8.3.5.0'
    pod 'AppLovinMediationGoogleAdManagerAdapter', '11.13.0.0'
    pod 'AppLovinMediationGoogleAdapter', '11.13.0.0'
    pod 'AppLovinMediationInMobiAdapter', '10.8.0.0'
    pod 'AppLovinMediationIronSourceAdapter', '8.7.0.0.0'
    pod 'AppLovinMediationMintegralAdapter', '7.7.6.0.0'
    pod 'AppLovinMediationMyTargetAdapter', '5.25.1.0'
    pod 'AppLovinMediationUnityAdsAdapter', '4.13.1.0'
    pod 'AppLovinMediationVungleAdapter', '7.4.4.0'
    pod 'AppLovinMediationYandexAdapter', '5.2.1.0'
    pod 'BidMachineAmazonAdapter', '3.2.0.0'
    pod 'BidMachineMetaAudienceAdapter', '3.2.0.0'
    pod 'BidMachineMintegralAdapter', '3.2.0.0'
    pod 'BidMachineMyTargetAdapter', '3.2.0.0'
    pod 'BidMachinePangleAdapter', '3.2.0.0'
    pod 'BidMachineVungleAdapter', '3.2.0.0'
    pod 'BidonAdapterAppLovin', '0.7.12.0'
    pod 'BidonAdapterBidMachine', '0.7.12.0'
    pod 'BidonAdapterBigoAds', '0.7.12.0'
    pod 'BidonAdapterDTExchange', '0.7.12.0'
    pod 'BidonAdapterGoogleAdManager', '0.7.12.0'
    pod 'BidonAdapterGoogleMobileAds', '0.7.12.0'
    pod 'BidonAdapterInMobi', '0.7.12.0'
    pod 'BidonAdapterMetaAudienceNetwork', '0.7.12.0'
    pod 'BidonAdapterMintegral', '0.7.12.0'
    pod 'BidonAdapterUnityAds', '0.7.12.0'
    pod 'BidonAdapterVungle', '0.7.12.0'
  
    pod 'YandexMobileMetrica', '4.4.0'

    pod 'FBSDKCoreKit_Basics', '~> 17.0.1'
    pod 'FBSDKCoreKit', '~> 17.0.1'
    pod 'FBSDKLoginKit', '~> 17.0.1'
    pod 'FBSDKShareKit', '~> 17.0.1'
    pod 'FBSDKGamingServicesKit', '~> 17.0.1'

end
target 'Unity-iPhone' do
end
use_frameworks!

post_install do |installer|
  installer.pods_project.targets.each do |target|
    target.build_configurations.each do |config|
      config.build_settings['IPHONEOS_DEPLOYMENT_TARGET'] = '13.0'
    end
  end
end