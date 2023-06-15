using Microsoft.AspNetCore.Authentication;

namespace AM.GoogleStreetViewUploader.Web.Models
{
    public class UploadViewModel
    {
        public List<AuthenticationScheme> AuthenticationSchemes { get; set; }
    }
}
