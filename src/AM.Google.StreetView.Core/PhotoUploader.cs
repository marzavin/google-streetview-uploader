using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.StreetViewPublish.v1;
using Google.Apis.StreetViewPublish.v1.Data;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AM.Google.StreetView.Core
{
    public class PhotoUploader
    {
        protected PublisherInfo Publisher { get; }

        public PhotoUploader(PublisherInfo publisher)
        {
            Publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public async Task<OperationResult> UploadAsync(string filePath)
        {
            var result = new OperationResult();

            var fileValidator = new PhotoFileValidator();
            var validationResult = await fileValidator.ValidateAsync(filePath);

            await result.MergeAsync(validationResult);
            if (!result.Success)
            {
                return result;
            }

            var metadataResolver = new PhotoMetadataResolver();
            var metadataResult = await metadataResolver.GetMetadataAsync(filePath);

            await result.MergeAsync(metadataResult);
            if (!result.Success)
            {
                return result;
            }

            var secrets = GoogleClientSecrets.FromFile(Publisher.ClientConfigurationPath);
            var scopes = new[] { StreetViewPublishService.Scope.Streetviewpublish };
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets.Secrets, scopes, Publisher.User, CancellationToken.None);

            var initializer = new BaseClientService.Initializer { HttpClientInitializer = credential, ApplicationName = Publisher.Application };
            var service = new StreetViewPublishService(initializer);

            var uploadUrl = service.Photo.StartUpload(null).Execute().UploadUrl;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + credential.Token.AccessToken);
            var content = new StreamContent(File.Open(filePath, FileMode.Open));
            client.PostAsync(uploadUrl, content).Wait();

            var uploadRequest = new Photo { UploadReference = new UploadRef { UploadUrl = uploadUrl } };
            var photo = service.Photo.Create(uploadRequest).Execute();

            Console.WriteLine(photo.ETag);
            Console.WriteLine(photo.UploadTime);
            Console.WriteLine(photo.ShareLink);
            Console.WriteLine(photo.CaptureTime);
            Console.WriteLine(photo.UploadReference);
            Console.WriteLine(photo.DownloadUrl);

            return result;
        }
    }
}
