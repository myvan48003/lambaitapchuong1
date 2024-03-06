using Microsoft.AspNetCore.Mvc;
using Demomvc.Models;

namespace Demomvc.Controllers;
public class EmployeeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Index(Employee ps)
    {
        string strOutput = "Xin chào" + ps.PersonId + "-" + ps.Fullname + "-" + ps.Address+ "-"+ ps.EmployeeId + "-" + ps.Age;
        ViewBag.infoDaica = strOutput;
        return View();
    }
}