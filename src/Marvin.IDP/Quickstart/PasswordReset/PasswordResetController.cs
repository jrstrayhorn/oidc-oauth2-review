using System.Threading.Tasks;
using Marvin.IDP.PasswordReset;
using Marvin.IDP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Marvin.IDP.PasswordReset
{
    public class PasswordResetController : Controller
    {
        private readonly ILocalUserService _localUserService;
        private readonly ILogger<PasswordResetController> _logger;
        public PasswordResetController(ILocalUserService localUserService, ILogger<PasswordResetController> logger)
        {
            _logger = logger;
            _localUserService = localUserService;
        }

        [HttpGet]
        public IActionResult RequestPassword()
        {
            var vm = new RequestPasswordViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestPassword(RequestPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var securityCode = await _localUserService
                .InitiatePasswordResetRequest(model.Email);
            await _localUserService.SaveChangesAsync();

            // create an activation link
            var link = Url.ActionLink("ResetPassword", "PasswordReset", new { securityCode });

            _logger.LogInformation(link);

            return View("PasswordResetRequestSent");
        }
    }
}