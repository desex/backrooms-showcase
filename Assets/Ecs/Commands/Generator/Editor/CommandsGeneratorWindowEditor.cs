using System.IO;
using Ecs.Commands.Generator.Editor.Utils.ScriptHandler.Impl;
using Ecs.Commands.Generator.Editor.Utils.ScriptsParser;
using UnityEditor;
using UnityEngine;

namespace Ecs.Commands.Generator.Editor
{
	public class CommandsGeneratorWindow : EditorWindow
	{
		private const string DEFAULT_PATH = "Generated/Commands";
		private const string GENERATION_PATH_KEY = "CommandsGenerationPath";
		private const string GENERATED_EXTENSIONS_CLASS_NAME = "CommandBufferExtensions";

		private static string _generationPathKey;
		private static string _defaultPath;
		private static string _generationPath;

		private static void InitGenerators()
		{
			// Более показать не могу
		}
		
		[MenuItem("Tools/Commands/Settings")]
		public static void MiOpenWindow()
		{
			// Более показать не могу
		}

		[MenuItem("Tools/Commands/Generate")]
		public static void GenerateProperties()
		{
			// Более показать не могу
		}

		private void OnEnable()
		{
			// Более показать не могу
		}

		private void OnGUI()
		{
			// Более показать не могу
		}

		private static void DrawGenerationPath()
		{
			// Более показать не могу
		}

		private static void DrawGenerateButton()
		{
			// Более показать не могу
		}

		private void OnDisable()
		{
			// Более показать не могу
		}

		private static void Save(FileInfo fileInfo, in string generatedData)
		{
			// Более показать не могу
		}
	}
}