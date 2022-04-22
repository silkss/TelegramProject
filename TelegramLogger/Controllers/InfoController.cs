using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TelegramLib;
using TelegramLib.Interfaces;
using TelegramLib.Models;

namespace TelegramProject.Controllers;

public class InfoController : Controller
{
    private readonly Mytelbot _mytelbot;
    private readonly IRepository<UserModel> _userRepository;

    public InfoController(Mytelbot mytelbot, IRepository<UserModel> userRepository)
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

}
