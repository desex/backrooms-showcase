using System;
using System.IO;
using Ecs.Commands.Generator.Editor.Utils.ScriptHandler;
using Microsoft.CodeAnalysis.CSharp;

namespace Ecs.Commands.Generator.Editor.Utils.ScriptsParser
{
    public static class ScriptsTraveler
    {
        private const string SEARCH_PATTERN = "*.cs";

        public static void Run(string directoryPath, IScriptHandler handler)
        {
            // Более показать не могу
        }
    }
}