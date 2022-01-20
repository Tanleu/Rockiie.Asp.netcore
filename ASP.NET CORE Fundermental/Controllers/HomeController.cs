﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Second_Lesson_ASP.Core_MVC.Models;

namespace Second_Lesson_ASP.Core_MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Route("hihi/hehe/haha?a=1&b=2&c=3")]
    public IActionResult Index()
    {
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

    public string BasicAction(){
        return "Ha lo! It is basic action";
    }
}
