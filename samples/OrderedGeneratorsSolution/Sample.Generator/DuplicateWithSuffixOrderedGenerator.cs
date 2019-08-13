// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.txt file in the project root for full license information.

namespace Sample.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using CodeGeneration.Roslyn;
    using Validation;

    public class DuplicateWithSuffixOrderedGenerator : IRichCodeGenerator
    {
        private readonly AttributeData attributeData;
        private readonly ImmutableDictionary<string, TypedConstant> data;
        private readonly string suffix;

        public DuplicateWithSuffixOrderedGenerator(AttributeData attributeData)
        {
            Requires.NotNull(attributeData, nameof(attributeData));

            this.suffix = (string)attributeData.ConstructorArguments[0].Value;
            this.attributeData = attributeData;
            this.data = this.attributeData.NamedArguments.ToImmutableDictionary(kv => kv.Key, kv => kv.Value);
        }

        public Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();

            MemberDeclarationSyntax copy = null;
            var applyToClass = context.ProcessingNode as ClassDeclarationSyntax;
            if (applyToClass != null)
            {
                copy = applyToClass
                    .WithIdentifier(SyntaxFactory.Identifier(applyToClass.Identifier.ValueText + this.suffix));
                results = results.Add(copy);
            }

            var applyToField = context.ProcessingNode as FieldDeclarationSyntax;
            if (applyToField != null)
            {
                var oldDeclaration = applyToField.Declaration;
                copy = applyToField.WithDeclaration(SyntaxFactory.VariableDeclaration(oldDeclaration.Type).WithVariables(SyntaxFactory.SeparatedList<VariableDeclaratorSyntax>(
                    oldDeclaration.Variables.Select(v =>
                    {
                        if (v is VariableDeclaratorSyntax variableDeclaratorSyntax)
                        {
                            return SyntaxFactory.VariableDeclarator(variableDeclaratorSyntax.Identifier.Text + suffix);
                        }
                        return v;
                    })
                    )));
                results = results.Add(copy);
            }

            return Task.FromResult(results);
        }


        public async Task<RichGenerationResult> GenerateRichAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {

            var results = new List<ChangeMember>();

            MemberDeclarationSyntax copy = null;
            var applyToClass = context.ProcessingNode as ClassDeclarationSyntax;
            if (applyToClass != null)
            {
                copy = applyToClass
                    .WithIdentifier(SyntaxFactory.Identifier(applyToClass.Identifier.ValueText + this.suffix));
                results.Add(ChangeMember.AddMember(context.ProcessingNode,  copy));
                //results.Add(ChangeMember.ReplaceMember(context.ProcessingNode, copy));
            }

            var applyToField = context.ProcessingNode as FieldDeclarationSyntax;
            if (applyToField != null)
            {
                var oldDeclaration = applyToField.Declaration;
                copy = applyToField.WithDeclaration(SyntaxFactory.VariableDeclaration(oldDeclaration.Type).WithVariables(SyntaxFactory.SeparatedList<VariableDeclaratorSyntax>(
                    oldDeclaration.Variables.Select(v =>
                    {
                        if (v is VariableDeclaratorSyntax variableDeclaratorSyntax)
                        {
                            return SyntaxFactory.VariableDeclarator(variableDeclaratorSyntax.Identifier.Text + suffix);
                        }
                        return v;
                    })
                    )));
                results.Add(ChangeMember.AddMember(context.ProcessingNode, copy));
            }

            return new RichGenerationResult
            {
                Members = results,
                Usings = SyntaxFactory.List<UsingDirectiveSyntax>(),
                AttributeLists = SyntaxFactory.List<AttributeListSyntax>(),
                Externs = SyntaxFactory.List<ExternAliasDirectiveSyntax>()
            };
        }
    }    
}
