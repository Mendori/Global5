using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesLesson.Models;

namespace RazorPagesLesson.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesLesson.Models.StudentsContext _context;

        public CreateModel(RazorPagesLesson.Models.StudentsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            GenderItems = Enum.GetValues(typeof(GenderType))
            .Cast<GenderType>()
            .Select(o => new SelectListItem
            {
                Value = ((int)o).ToString(),
                Text = o.ToString()
            })
            .ToList();
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Student.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public List<SelectListItem> GenderItems
        {
            get; set;
        }
    }



}