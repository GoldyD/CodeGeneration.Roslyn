namespace CodeGeneration.Roslyn
{
    public interface IOrderedCodeGeneration
    {
        /// <summary>
        /// Gets or sets execution order
        /// </summary>
        int ExecutionOrder { get; set; }
    }
}
