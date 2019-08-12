namespace Sample.Generator
{
    using System;
    using System.Diagnostics;
    using CodeGeneration.Roslyn;

    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    [CodeGenerationAttribute(typeof(TryCatchOrderedGenerator))]
    public class TryCatchOrderedAttribute : OrderedCodeGenerationAttribute
    {
        public TryCatchOrderedAttribute()
        {
        }
    }
}
