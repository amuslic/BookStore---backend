using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace BookStoreApi.Processor
{
    internal class ExternalApiOperationProcessor : IOperationProcessor
    {
        public bool Process(OperationProcessorContext context)
        {
            return true;
        }
    }
}