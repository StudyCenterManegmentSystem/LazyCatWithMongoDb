using Application.Commens.Exceptions;
using Application.Dtos.FanDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace StudyCenterManegmentSystem.Front_end.Controllers;

public class FansController(IFanService fanService) : Controller
{
    private readonly IFanService _fanService = fanService;

    public IActionResult Index()
    {
        var fans = _fanService.GetAllAsync();
        return View(fans);
    }

    [HttpGet]
    public IActionResult AddFan()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(AddFanDto dto)
    {
        _fanService.AddAsync(dto);
        return RedirectToAction("Index");
    }

    public IActionResult Update(string id)
    {
        try
        {
            var fan = _fanService.GetByIdAsync(id);
            return View(fan);
        }
        catch(CustomException ex)
        {
            return RedirectToAction($"eror {ex.Message}", "home", new { url = "/fans/index" });
        }
    }

    [HttpPut]
    public IActionResult Update(FanDto dto)
    {
        try
        {
            _fanService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }
        catch (CustomException ex)
        {
            return RedirectToAction($"eror {ex.Message}", "home", new { url = "/fans/index" });
        }
    }

    [HttpDelete]
    public IActionResult Delete(string id)
    {
        _fanService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
