using Microsoft.AspNetCore.Mvc;
using TelegramLib;

namespace TelegramProject.Controllers;

public class TelegramController : Controller
{
    private readonly Mytelbot _mytelbot;

    public TelegramController(Mytelbot mytelbot)
    {
        _mytelbot = mytelbot;
    }
    public IActionResult Index()
    {
        return View(_mytelbot.Mytelbol);
    }
}
