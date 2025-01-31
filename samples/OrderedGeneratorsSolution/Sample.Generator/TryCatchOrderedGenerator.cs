﻿// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.

namespace Sample.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using CodeGeneration.Roslyn;

    public class TryCatchOrderedGenerator : IRichCodeGenerator
    {
        public TryCatchOrderedGenerator(AttributeData attributeData)
        {
        }

        public async Task<RichGenerationResult> GenerateRichAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            var processMemberNode = context.ProcessingNode;
            var newProcessMemberNode = context.ProcessingNode;

            var semanticModelRoot = context.SemanticModel.SyntaxTree.GetRoot();

            var typeInfo = context.SemanticModel.GetDeclaredSymbol(processMemberNode);

            Dictionary<MethodDeclarationSyntax, MethodDeclarationSyntax> replacedMethods =
                new Dictionary<MethodDeclarationSyntax, MethodDeclarationSyntax>();
            foreach (var method in processMemberNode.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                var newMethod = method.WithBody(SyntaxFactory.Block(
                                                    SyntaxFactory.SingletonList<StatementSyntax>(
                                                          SyntaxFactory.TryStatement(
                                                             SyntaxFactory.SingletonList<CatchClauseSyntax>(
                                                                SyntaxFactory.CatchClause()
                                                                  .WithDeclaration(SyntaxFactory.CatchDeclaration(SyntaxFactory.IdentifierName(SyntaxFactory.Identifier(SyntaxFactory.TriviaList(), "Exception", SyntaxFactory.TriviaList(SyntaxFactory.Space))))
                                                                  .WithIdentifier(SyntaxFactory.Identifier("ex")))
                                                                  //.WithBlock(SyntaxFactory.Block(SyntaxFactory.SingletonList(SyntaxFactory.ExpressionStatement(SyntaxFactory.IdentifierName("throw")))))
                                                                  ))
                                                     .WithBlock(method.Body)))).NormalizeWhitespace();
                replacedMethods.Add(method, newMethod);
            }
           
            return new RichGenerationResult
            {
                Members = replacedMethods.Select(p => ChangeMember.ReplaceMember(p.Key, p.Value)).ToList(),
                Usings = SyntaxFactory.List<UsingDirectiveSyntax>().Add(SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System"))),
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
