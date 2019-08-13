namespace Sample.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using CodeGeneration.Roslyn;

    public class RemoveOrderedGenerator : IRichCodeGenerator
    {
        public RemoveOrderedGenerator(AttributeData attributeData)
        {
        }

        public async Task<RichGenerationResult> GenerateRichAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            var results = new List<ChangeMember>();
            results.Add(ChangeMember.RemoveMember(context.ProcessingNode));            

            return new RichGenerationResult
            {
                Members = results,
                Usings = SyntaxFactory.List<UsingDirectiveSyntax>(),
                AttributeLists = SyntaxFactory.List<AttributeListSyntax>(),
                Externs = SyntaxFactory.List<ExternAliasDirectiveSyntax>()
            };
        }

        public Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
