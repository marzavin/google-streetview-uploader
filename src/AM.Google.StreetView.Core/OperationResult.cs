using System.Collections.Generic;
using System.Threading.Tasks;

namespace AM.Google.StreetView.Core
{
    public class OperationResult
    {
        public List<string> Errors { get; } = new List<string>();

        public bool Success => Errors.Count == 0;

        public Task MergeAsync(OperationResult result)
        {
            Errors.AddRange(result.Errors);
            return Task.CompletedTask;
        }
    }
}
