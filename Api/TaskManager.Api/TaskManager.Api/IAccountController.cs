using Microsoft.AspNetCore.Mvc;
using Refit;

namespace TaskManager.Api.IControllers
{
    public interface IAccountController
    {
        [Post("api/account/info")]
        public ActionResult GetCurreutUserInfo();

        [Post("api/account/token")]
        public IActionResult GetToken();
    }
}