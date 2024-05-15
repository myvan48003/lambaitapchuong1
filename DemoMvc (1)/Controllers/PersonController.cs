using System.Net;
using Microsoft.AspNetCore.Mvc;
using DemoMvc.Models;
using DemoMvc.Data;
using Microsoft.EntityFrameworkCore;
using DemoMvc.Models.Process;
using OfficeOpenXml;
using OfficeOpenXml.Table;


namespace DemoMvc.Controllers;
public class PersonController : Controller
{
    private readonly ApplicationDbContext _context;
    private ExcelProcess _excelProcess = new ExcelProcess();
    public PersonController (ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var model = await _context.Person.ToListAsync();
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Index(string KeySearch)
    {
        if(_context.Person == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Person' is null .");
        }
        var person = from m in _context.Person
                    select m;
        
        if(!string.IsNullOrEmpty(KeySearch))
        {
            person = person.Where(s => s.Fullname.Contains(KeySearch));
        }
        return View(await person.ToListAsync());
    }
    public IActionResult Create()
    {
        return  View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PersonId,Fullname,Address,Workat")] Person person)
    {
        if (ModelState.IsValid)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(person);
    }
    public async Task<IActionResult> Edit(string id)
    {
        if(id == null || _context.Person == null)
        {
            return NotFound();
        }
        var person = await _context.Person.FindAsync(id);
        if (person == null)
        {
            return NotFound();
        }
        return View(person);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id,[Bind("PersonId,Fullname,Address,Workat")]Person person)
    {
        if (id !=person.PersonId)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(person);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(person.PersonId))
                {
                    return NotFound();
                }
            else
            {
                throw;
            }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(person);
    }
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null || _context.Person == null)
        {
            return NotFound();
        }
        var person = await _context.Person
.FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null )
            {
                return NotFound();
            }
            return View(person);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Person' is null.");
            }
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upload(IFormFile file)
        {
            if (file!=null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "please choose excel file to upload!");
                }
                else
                {
                    var personList =await  _context.Person.ToListAsync();
                   _context.RemoveRange(personList);
                   await _context.SaveChangesAsync();
                    //rename file when upload to server
                    var fileName = file.FileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory()+ "/Upload/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to server
                        await file.CopyToAsync(stream);
                        //read data from excel fill DataTable
                        var dt = _excelProcess.ExcelToDataTable (fileLocation);
                        //using for loop to read data from dt
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //create new Person object
                            var ps = new Person();
                            //set value to attributes
                            ps.PersonId = dt.Rows[i][0].ToString();
                            ps.Fullname = dt.Rows[i][1].ToString();
                            ps.Address = dt.Rows[i][2].ToString();
                            ps.Workat = dt.Rows[i][3].ToString();
                            //Add object to context
                            _context.Add(ps);    
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
public async Task<IActionResult> Download()
{
    var fileName = "YourFileName" + ".xlsx";

    using (var excelPackage = new ExcelPackage())
    {
        var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
        worksheet.Cells["A1"].Value = "PersonID";
        worksheet.Cells["B1"].Value = "FullName";
        worksheet.Cells["C1"].Value = "Address";

        var personList = await _context.Person.ToListAsync(); // Using ToListAsync for async

        worksheet.Cells["A2"].LoadFromCollection(personList, PrintHeaders: true, TableStyle: OfficeOpenXml.Table.TableStyles.Light1);
        var stream = new MemoryStream(excelPackage.GetAsByteArray());

        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
}
        private bool PersonExists(string id)
        {
            return (_context.Person?.Any(e => e.PersonId ==id)).GetValueOrDefault();
        }
    }