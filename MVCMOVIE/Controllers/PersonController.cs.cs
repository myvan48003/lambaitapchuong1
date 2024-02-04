using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.Person.ToListAsync();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address")] Person person)
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
            if (id == null || _context.Person == null)
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
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address")] Person person)
        {
            if (id != person.PersonId)
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
                    if (!await PersonExistsAsync(person.PersonId))
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
private bool PersonExists(string id)
{
    return (_context.Person?.Any(e => e.PersonId == id)).GetValueOrDefault ();
}

public async Task<IActionResult> Delete(string id)
{
    // If the id is null or the DbSet<Person> in the context is null, the method returns a NotFound result.
    if (id == null || _context.Person == null)
    {
        return NotFound();
    }

    // Retrieves the first person that matches the given id asynchronously.
    var person = await _context.Person.FirstOrDefaultAsync(m => m.PersonId == id);

    // If no person is found, return a NotFound result.
    if (person == null)
    {
        return NotFound();
    }

    // If a person is found, return the view for that person, likely for confirmation of deletion.
    return View(person);
}

// This HTTP POST action method confirms the deletion of a person.
// It also has an anti-forgery token validation to prevent CSRF attacks.
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(string id)
{
    // If the DbSet<Person> in the context is null, return a problem detail.
    if (_context.Person == null)
    {
        return Problem("Entity set 'ApplicationDbContext.Person' is null.");
    }

    // Finds the person by the given id asynchronously.
    var person = await _context.Person.FindAsync(id);

    // If the person is found, remove the person from the DbSet<Person>.
    if (person != null)
    {
        _context.Person.Remove(person);
    }

    // Save the changes made to the context asynchronously.
    await _context.SaveChangesAsync();

    // Redirect to the Index action method after deletion.
    return RedirectToAction(nameof(Index));
}

    }
}


