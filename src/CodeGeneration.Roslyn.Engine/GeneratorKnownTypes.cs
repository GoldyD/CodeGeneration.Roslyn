namespace CodeGeneration.Roslyn.Engine
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class GeneratorKnownTypes
    {
        public INamedTypeSymbol IOrderCodeGenerationTypeSymbol { get; private set; }

        public INamedTypeSymbol CodeGenerationAttributeAttributeTypeSymbol { get; private set; }

        public GeneratorKnownTypes(CSharpCompilation compilation)
        {
            IOrderCodeGenerationTypeSymbol = compilation.GetTypeByMetadataName("CodeGeneration.Roslyn.IOrderedCodeGeneration");
            CodeGenerationAttributeAttributeTypeSymbol = compilation.GetTypeByMetadataName("CodeGeneration.Roslyn.CodeGenerationAttributeAttribute");
        }
    }
}
