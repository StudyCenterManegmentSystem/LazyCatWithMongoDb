using Application.Dtos.FanDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FrontendCRM.Controllers
{
    public class FanController(IFanService fanService) : Controller
    {
        private readonly IFanService _fanService = fanService;

        public async Task<IActionResult> Index()
        {
            List<FanDto> allFans = await fanService.GetAllAsync();

            return View(allFans);
        }
    }
}
