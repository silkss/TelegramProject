using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TelegramLib;
using TelegramLib.Interfaces;
using TelegramLib.Models;
using TelegramProject.Models;

namespace TelegramProject.Controllers;

public class TelegramController : Controller
{
    private readonly Mytelbot _mytelbot;
    private readonly IRepository<UserModel> _userRepository;

    public TelegramController(Mytelbot mytelbot, IRepository<UserModel> userRepository)
    {
        _mytelbot = mytelbot;
        _userRepository = userRepository;
    }
    public IActionResult Index()
    {
        return View(_mytelbot.Mytelbol);
    }
    public async Task<IActionResult> Users()
    {
        var users = await _userRepository.GetAllAsync();
        return View(users);
    }
    [HttpGet]
    public IActionResult SendMessage()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SendMessage(MessageModel message)
    {
        var recievers = await _userRepository.GetAllAsync();
        await _mytelbot.SendMessageAsync(message.Text, recievers);
        return RedirectToAction("Users");
    }
}
