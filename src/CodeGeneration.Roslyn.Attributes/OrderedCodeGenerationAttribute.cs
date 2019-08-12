namespace CodeGeneration.Roslyn
{
    using System;

    public abstract class OrderedCodeGenerationAttribute : Attribute, IOrderedCodeGeneration
    {
        /// <summary>
        /// Gets or sets execution order
        /// </summary>
        public int ExecutionOrder { get; set; }
    }
}
