﻿// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.txt file in the project root for full license information.

namespace CodeGeneration.Roslyn
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System.Collections.Generic;

    /// <summary>
    /// Contains <see cref="CompilationUnitSyntax"/> additions generated by the <see cref="IRichCodeGenerator"/>.
    /// </summary>
    public class RichGenerationResult
    {
        //public SyntaxNode ReplacedMemberNode { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MemberDeclarationSyntax"/> to add to generated <see cref="CompilationUnitSyntax"/>.
        /// </summary>
        public List<ChangeMember> Members { get; set; } = new List<ChangeMember>();

        /// <summary>
        /// Gets or sets the <see cref="UsingDirectiveSyntax"/> to add to generated <see cref="CompilationUnitSyntax"/>.
        /// </summary>
        public SyntaxList<UsingDirectiveSyntax> Usings { get; set; } = new SyntaxList<UsingDirectiveSyntax>();

        /// <summary>
        /// Gets or sets the <see cref="ExternAliasDirectiveSyntax"/> to add to generated <see cref="CompilationUnitSyntax"/>.
        /// </summary>
        public SyntaxList<ExternAliasDirectiveSyntax> Externs { get; set; } = new SyntaxList<ExternAliasDirectiveSyntax>();

        /// <summary>
        /// Gets or sets the <see cref="AttributeListSyntax"/> to add to generated <see cref="CompilationUnitSyntax"/>.
        /// </summary>
        public SyntaxList<AttributeListSyntax> AttributeLists { get; set; } = new SyntaxList<AttributeListSyntax>();
    }

    public class ChangeMember
    {
        public SyntaxNode OldMember { get; private set; }
        public SyntaxNode Parent { get; private set; }
        public SyntaxNode BaseMember { get; private set; }
        public SyntaxNode NewMember { get; private set; }

        private ChangeMember(SyntaxNode oldMember, SyntaxNode parent, SyntaxNode baseMember, SyntaxNode newMember)
        {
            OldMember = oldMember;
            Parent = parent;
            BaseMember = baseMember;
            NewMember = newMember;
        }

        public static ChangeMember AddMember(SyntaxNode parent, SyntaxNode baseMember, SyntaxNode newMember)
        {
            return new ChangeMember(null, parent, baseMember, newMember);
        }

        public static ChangeMember ReplaceMember(SyntaxNode oldMember, SyntaxNode parent, SyntaxNode newMember)
        {
            return new ChangeMember(oldMember, parent, null, newMember);
        }

        public static ChangeMember RemoveMember(SyntaxNode oldMember, SyntaxNode parent)
        {
            return new ChangeMember(oldMember, parent, null, null);
        }


    }
}
