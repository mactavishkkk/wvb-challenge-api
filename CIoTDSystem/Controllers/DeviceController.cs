using Microsoft.AspNetCore.Mvc;

namespace CIoTDSystem.Controllers
{
    public class DeviceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
