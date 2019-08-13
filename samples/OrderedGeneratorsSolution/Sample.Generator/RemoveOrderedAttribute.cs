namespace Sample.Generator
{
    using System;
    using CodeGeneration.Roslyn;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    [CodeGenerationAttribute(typeof(RemoveOrderedGenerator))]
    public class RemoveOrderedAttribute : OrderedCodeGenerationAttribute
    {
        public RemoveOrderedAttribute()
        {        
        }
    }
}
