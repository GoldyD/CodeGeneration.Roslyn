namespace Sample.Generator
{
    using System;
    using CodeGeneration.Roslyn;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    [CodeGenerationAttribute(typeof(TryCatchOrderedGenerator))]
    public class TryCatchOrderedAttribute : OrderedCodeGenerationAttribute
    {
        public TryCatchOrderedAttribute()
        {
        }
    }
}
