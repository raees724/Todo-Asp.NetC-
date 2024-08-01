using FirstProjAsp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProjAsp.Pages
{
    public class PeopleModel : PageModel
    {
        private readonly MyDbContext _context;

        public List<Person> People { get; set; } = new List<Person>();

        [BindProperty]
        public Person NewPerson { get; set; }

        [BindProperty]
        public Person EditPerson { get; set; }

        public PeopleModel(MyDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            People = _context.People.ToList();
        }

        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.People.Add(NewPerson);
            _context.SaveChanges();
            return RedirectToPage();
        }

        public IActionResult OnPostEdit()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var person = _context.People.Find(EditPerson.Id);
            if (person == null)
            {
                return NotFound();
            }

            person.Name = EditPerson.Name;
            person.Age = EditPerson.Age;

            _context.People.Update(person);
            _context.SaveChanges();
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var person = _context.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            _context.SaveChanges();
            return RedirectToPage();
        }
    }
}
