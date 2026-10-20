using Microsoft.CodeAnalysis;

namespace Ecs.Commands.Generator.Editor.Utils.ScriptHandler
{
    public interface IScriptHandler
    {
        void HandleSyntaxRoot(SyntaxNode syntaxNode);
    }
}