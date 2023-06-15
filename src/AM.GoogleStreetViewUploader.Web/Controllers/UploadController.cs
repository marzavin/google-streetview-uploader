using Google.Apis.Auth.AspNetCore;
using Google.Apis.Services;
using Google.Apis.StreetViewPublish.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AM.GoogleStreetViewUploader.Web.Controllers
{
    public class UploadController : Controller
    {
        public const string Name = "Upload";

        public const string HomeAction = "Index";

        [AllowAnonymous]
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [GoogleScopedAuthorize(StreetViewPublishService.ScopeConstants.Streetviewpublish)]
        public async Task<IActionResult> Upload([FromServices] IGoogleAuthProvider authProvider)
        {
            var credentials = await authProvider.GetCredentialAsync();
            var service = new StreetViewPublishService(new BaseClientService.Initializer { HttpClientInitializer = credentials });
            return View();         
        }
    }
}