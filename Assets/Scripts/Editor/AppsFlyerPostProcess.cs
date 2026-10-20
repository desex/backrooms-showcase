using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace Editor
{
    public class AppsFlyerPostProcess
    {
        [PostProcessBuild(43)]
        public static void UpdateInfoPlist(BuildTarget buildTarget, string buildPath)
        {
            // Более показать не могу
        }
        
        private static void AddNSAdvertisingAttributionReportEndpoint(string path)
        {
            // Более показать не могу
        }
        
        private static void AddKeyToPlist(string path, string key, string value)
        {
            // Более показать не могу
        }
    }
}