using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesLesson.Models;

namespace RazorPagesLesson.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesLesson.Models.StudentsContext _context;

        public EditModel(RazorPagesLesson.Models.StudentsContext context)
        {
            _context = context;

            GenderItems = Enum.GetValues(typeof(GenderType))
            .Cast<GenderType>()
            .Select(o => new SelectListItem
            {
                Value = ((int)o).ToString(),
                Text = o.ToString()
            })
            .ToList();
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student.SingleOrDefaultAsync(m => m.Id == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(Student.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }

        public List<SelectListItem> GenderItems
        {
            get; set;
        }
    }
}
