using System.Diagnostics;
using AspNetCoreIdentityApp.Web.Models;
using AspNetCoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentityApp.Web.Extensions;
using AspNetCoreIdentityApp.Web.Services;

namespace AspNetCoreIdentityApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if(!ModelState.IsValid) return View();
           
            var identityResult = await _userManager.CreateAsync(new() { UserName = request.UserName, PhoneNumber = request.Phone, Email = request.Email }, request.PasswordConfirm);

            
            if (identityResult.Succeeded) 
            {
                TempData["SucceedMessage"] = "�yelik kay�t i�lemi ba�ar�yla ger�ekle�mi�tir.";
                return RedirectToAction(nameof(HomeController.SignUp));
            }
            ModelState.AddModelErrorList(identityResult.Errors.Select(x=>x.Description).ToList());
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {
            var hasUser= await _userManager.FindByEmailAsync(request.Email);
            if(hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Bu email adresine sahip kullan�c� bulunamam��t�r.");
                return View();
            }
            //�rneklink
            //https://localhost:7203?userId=112233&token=aasdfasfsbdf
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
            var passwordResetLink = Url.Action("ResetPassword", "Home", new { userId = hasUser.Id, Token = passwordResetToken },HttpContext.Request.Scheme);
            //Emailservice
            await _emailService.SendResetPasswordEmail(passwordResetLink, hasUser.Email);

            TempData["SucceedMessage"] = "�ifre yenileme linki eposta adresinize g�nderilmi�tir";
            return RedirectToAction(nameof(ForgetPassword));
        }
        public async Task<IActionResult> ResetPassword(string userId,string token)
        {
            TempData["userId"] = userId;
            TempData["token"]=token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request)
        {
            string userId = TempData["userId"].ToString();
            string token = TempData["token"].ToString();
            if(userId == null || token==null)
                throw new Exception("Bir hata meydana geldi.");

            var hasUser = await _userManager.FindByIdAsync(userId);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Kullan�c� bulunamam��t�r.");
                return View();
            }

            var result= await _userManager.ResetPasswordAsync(hasUser,token,request.Password);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "�ifreniz ba�ar�yla yenilenmi�tir.";
            }
            else
            {
                ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
            }
            return View();
        }



        public async Task<IActionResult> SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model,string? returnUrl=null)
        {
            returnUrl=returnUrl ?? Url.Action("Index","Home");
            var hasUser = await _userManager.FindByEmailAsync(model.Email);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya �ifre yanl��");
                return View();
            }
            var signInResult =await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);

            if (signInResult.Succeeded)
                return Redirect(returnUrl);

            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "3 dakika sonra tekrar deneyiniz." });
                return View();
            }

            ModelState.AddModelErrorList(new List<string>() { $"Email veya �ifre hatal�",$"Ba�ar�s�z giri� say�s�:{await _userManager.GetAccessFailedCountAsync(hasUser)}" });

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
