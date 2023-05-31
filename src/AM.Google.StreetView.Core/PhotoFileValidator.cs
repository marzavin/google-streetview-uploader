using System.Threading.Tasks;

namespace AM.Google.StreetView.Core
{
    internal class PhotoFileValidator
    {
        public Task<OperationResult> ValidateAsync(string filePath)
        {
            return Task.FromResult(new OperationResult());
        }
    }
}
