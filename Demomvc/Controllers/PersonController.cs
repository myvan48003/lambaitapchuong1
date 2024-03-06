using Microsoft.AspNetCore.Mvc;
using Demomvc.Models;

namespace Demomvc.Controllers;
public class PersonController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Index(Person ps)
    {
        string strOutput = "Xin chào" + ps.PersonId + "-" + ps.Fullname + "-" + ps.Address;
        ViewBag.infoPerson = strOutput;
        return View();
    }
}

