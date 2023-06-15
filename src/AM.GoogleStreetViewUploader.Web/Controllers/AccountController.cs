using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AM.GoogleStreetViewUploader.Web.Controllers
{
    public class AccountController : Controller
    {
        public const string Name = "Account";

        public const string SignInAction = "StartSession";
        public const string SignOutAction = "FinishSession";

        [Authorize]
        [Route("signin")]
        public IActionResult StartSession()
        {
            return RedirectToAction(UploadController.HomeAction, UploadController.Name);
        }

        [Authorize]
        [Route("signout")]
        public async Task<IActionResult> FinishSession()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(UploadController.HomeAction, UploadController.Name);
        }
    }
}
