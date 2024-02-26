using Application.Dtos.FanDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace StudyCenterManegmentSystem.Front_end.Controllers;

public class FansController(IFanService fanService) : Controller
{
    public readonly IFanService _fanService = fanService;

    public async Task<IActionResult> Index() 
    {
        var fans = await _fanService.GetAllAsync(); 
        var returnFans = fans.Select(f => (FanDto)f).ToList(); 
        return View(returnFans); 
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    public IActionResult Save(AddFanDto dto)
    {

        _fanService.AddAsync(dto);
        return RedirectToAction("Index");
    }
}



