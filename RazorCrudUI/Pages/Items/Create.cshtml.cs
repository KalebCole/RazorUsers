using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Models;
using Domain.Interfaces;
using UI.Utilities;

namespace UI.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly IItemRepository _repo;
        private readonly IWebHostEnvironment _env;

        public CreateModel(IItemRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        // we use bind property when we want to update things
        [BindProperty]
        public ItemModel ItemModel { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // HttpContext represents the incoming request from the client
            if(HttpContext.Request.Form.Files.Count > 0)
            {
                ItemModel.ImageURL = FileHelper.UploadImage(_env, HttpContext.Request.Form.Files[0]);
            }
            //ItemModel.ImageURL = await ItemModel.UploadImageAsync(_env);
            await _repo.InsertItemAsync(ItemModel);
            return RedirectToPage("./Index");
        }

            }
}
