// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.txt file in the project root for full license information.

namespace Sample.Generator
{
    using System;
    using System.Diagnostics;
    using CodeGeneration.Roslyn;
    using Validation;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    [CodeGenerationAttribute(typeof(DuplicateWithSuffixOrderedGenerator))]    
    public class DuplicateWithSuffixByNameOrderedAttribute : OrderedCodeGenerationAttribute
    {
        public DuplicateWithSuffixByNameOrderedAttribute(string suffix)
        {
            Requires.NotNullOrEmpty(suffix, nameof(suffix));

            this.Suffix = suffix;
        }

        public string Suffix { get; }
    }
}
