using Microsoft.AspNetCore.Mvc;
using Lr15.Net.Services.Email;
using System.Threading.Tasks;

namespace Lr15.Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmailSenderService _emailSenderService;

        public HomeController(EmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string to, string subject, string body)
        {
            await _emailSenderService.SendEmailAsync(to, subject, body);
            ViewBag.Message = "Email sent successfully!";
            return View("Index");
        }
    }
}
