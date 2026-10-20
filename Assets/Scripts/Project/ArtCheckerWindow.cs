#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Project
{
    public class ArtCheckerWindow : EditorWindow
    {
        private const string GENERAL_SEARCH_FOLDER = "Assets/Assets";
        private const string MESH_SEARCH_FOLDER = "Assets/Assets/Models";
        private const string ANIMATION_SEARCH_FOLDER = "Assets/Assets/Animations";
        private const string WALL_LABEL = "Wall";
        
        private const IndexFormat DEFAULT_INDEX_LABEL = IndexFormat.UInt16;

        private static readonly List<TextureFormat> _availableFormats = new()
        {
            TextureFormat.ASTC_4x4,
            TextureFormat.ASTC_5x5,
            TextureFormat.ASTC_6x6,
            TextureFormat.ASTC_8x8,
            TextureFormat.ASTC_10x10,
            TextureFormat.ASTC_12x12,
        };

        private static readonly List<string> _untouchableLabels = new()
        {
            "Documentation",
        };

        private static readonly List<string> _untouchablePaths = new()
        {
            "Assets/Assets/Fonts",
        };

        private static readonly List<string> _abandonedShaders = new()
        {
            "Universal Render Pipeline/Complex Lit",
        };

        private List<Label> _errorLabels;

        [MenuItem("Tools/Art Checker")]
        public static void ShowWindow()
        {
            // Более показать не могу
        }

        public void CreateGUI()
        {
            // Более показать не могу
        }

        private void RefreshWindow()
        {
            // Более показать не могу
        }

        private static List<Label> CheckAllArt()
        {
            // Более показать не могу
            return default;
        }
        
        private static (List<string> errors, List<string> warnings) GetTexturesErrorsAndWarnings(List<string> errors, List<string> warnings)
        {
            // Более показать не могу
            return default;
        }
        
        private static (List<string> errors, List<string> warnings) GetModelsErrorsAndWarnings(List<string> errors, List<string> warnings)
        {
            // Более показать не могу
            return default;
        }
        
        private static (List<string> errors, List<string> warnings) GetAnimationsErrorsAndWarnings(List<string> errors, List<string> warnings)
        {
            // Более показать не могу
            return default;
        }
        
        private static (List<string> errors, List<string> warnings) GetMaterialsErrorsAndWarnings(List<string> errors, List<string> warnings)
        {
            // Более показать не могу
            return default;
        }

        private static List<(Texture2D texture, string path)> GetTexturesData()
        {
            // Более показать не могу
            return default;
        }
        
                private static List<(Object asset, Mesh mesh, string path)> GetWallMeshesData()
        {
                    // Более показать не могу
                    return default;
                }
        
        private static List<(Object asset, Mesh mesh, string path)> GetNonWallMeshesData()
        {
            // Более показать не могу
            return default;
        }
        
        private static List<(Object asset, string path)> GetSkeletalAnimationsData()
        {
            // Более показать не могу
            return default;
        }

        private static List<(AnimationClip animation, string path)> GetSingleAnimationsData()
        {
            // Более показать не могу
            return default;
        }
        
        private static List<(AnimationClip animation, string path)> GetNonSingleAnimationsData()
        {
            // Более показать не могу
            return default;
        }

        private static List<(Material material, string path)> GetMaterialsData()
        {
            // Более показать не могу
            return default;
        }
        
        private static bool PathContainsAnyOfUntouchablePaths(string path)
        {
            // Более показать не могу
            return default;
        }
            
        private static bool LabelsContainsAnyOfUntouchablePaths(IEnumerable<string> labels)
        {
            // Более показать не могу
            return default;
        }
        
        private static bool LabelsContainsWall(IEnumerable<string> labels)
        {
            // Более показать не могу
            return default;
        }

        private static IEnumerable<string> GetIncorrectNamesErrors<T>(IEnumerable<(T asset, string path)> assetsData, string firstPrefix, params string[] prefixes) where T : Object
        {
            // Более показать не могу
            return global::System.Array.Empty<string>();
        }
        
        private static IEnumerable<string> GetIncorrectNamesErrorsWithExtraMessage<T>(IEnumerable<(T asset, string path)> assetsData, string extraMessage, [NotNull, RegexPattern] string firstPrefix, [RegexPattern] params string[] prefixes) where T : Object
        {
            // Более показать не могу
            return global::System.Array.Empty<string>();
        }
        
        private static IEnumerable<string> GetWarningsWithDigitsInNames<T>(IEnumerable<(T asset, string path)> assetsData) where T : Object
        {
            // Более показать не могу
            return global::System.Array.Empty<string>();
        }

        private static IEnumerable<string> GetErrorsWithCompressionType(IEnumerable<(Texture2D texture, string path)> texturesData)
        {
            // Более показать не могу
            return global::System.Array.Empty<string>();
        }

        private static IEnumerable<string> GetErrorsWithCompressionType(IEnumerable<(Mesh mesh, string path)> meshesData, ModelImporterMeshCompression compression)
        {
            // Более показать не могу
            return global::System.Array.Empty<string>();
        }

        private static IEnumerable<string> GetErrorsWithIndexType(IEnumerable<(Mesh mesh, string path)> meshesData)
        {
            // Более показать не могу
            return global::System.Array.Empty<string>();
        }

        private static IEnumerable<string> GetErrorsWithTexturesMemorySize(IEnumerable<(Texture2D texture, string path)> texturesData)
        {
            // Более показать не могу
            return global::System.Array.Empty<string>();
        }

        private static IEnumerable<string> GetErrorsWithAbandonedShaders(IEnumerable<(Material material, string path)> materialsData)
        {
            // Более показать не могу
            return global::System.Array.Empty<string>();
        }
    }
}
#endif