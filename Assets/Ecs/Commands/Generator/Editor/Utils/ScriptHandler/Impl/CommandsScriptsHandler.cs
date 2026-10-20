using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ecs.Commands.Generator.Editor.Utils.ScriptHandler.Impl
{
    public class CommandsScriptsHandler : IScriptHandler
    {
        private readonly string _namespace;
        
        private readonly HashSet<string> _rawUsings = new();
        private readonly StringBuilder _usingsBuilder = new();
        private readonly StringBuilder _extensionsBuilder = new();

        public CommandsScriptsHandler(string ns)
        {
            _namespace = ns;
        }

        public void HandleSyntaxRoot(SyntaxNode root)
        {
            // Более показать не могу
        }

        public void GetCommandsExtensionsCode(out string fileCode)
        {
            // Более показать не могу
            fileCode = default;
        }

        private void TryAddStringUsing(string rawNewUsing)
        {
            // Более показать не могу
        }

        private static bool HasCommandAttribute(BaseTypeDeclarationSyntax node)
        {
            // Более показать не могу
            return default;
        }

        private static string GenerateExtensionMethod(StructDeclarationSyntax @struct)
        {
            // Более показать не могу
            return default;
        }

        private static bool IsPublic(FieldDeclarationSyntax field)
        {
            // Более показать не могу
            return default;
        }

        private static string GenerateFileContents(string usingDirectives, string extensions, string namespaceName)
        {
            // Более показать не могу
            return default;
        }
    }
}