using AM.Google.StreetView.Core;

namespace AM.GoogleStreetViewUploader.CLI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var file = new FileInfo("");

            var publisher = new PublisherInfo
            {
                ClientConfigurationPath = "",
                Application = "AM StreetView Uploader Desktop",
                User = ""
            };

            var uploader = new PhotoUploader(publisher);
            var uploadResult = await uploader.UploadAsync(file.FullName);

            if (uploadResult.Success)
            {
                Console.WriteLine($"Your image '{file.FullName}' has been uploaded successfully!");
            }
            else
            {
                Console.WriteLine($"An error ocured during file {file.FullName} upload:");
                foreach (var error in uploadResult.Errors)
                {
                    Console.WriteLine($"\t{error}");
                }
            }

            Console.ReadKey();
        }
    }
}
