using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UI.Utilities;

namespace UI.Pages.Items
{
    public class EditModel : PageModel
    {
        private readonly IItemRepository _repo;
		private readonly IWebHostEnvironment _env;


		public EditModel(IItemRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        [BindProperty]
        public ItemModel ItemModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemmodel = await _repo.GetItemByIDAsync(id.Value);
            if (itemmodel == null)
            {
                return NotFound();
            }
            ItemModel = itemmodel;
            return Page(); //has references to the properties of the ItemModel
            // this is the same as return View(ItemModel);
            // View is a method that returns a view result
            // 
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
			// HttpContext represents the incoming request from the client
			if (HttpContext.Request.Form.Files.Count > 0)
			{
				//delete the old image
                FileHelper.DeleteOldImage(_env, ItemModel.ImageURL);                
                //set the new image
                ItemModel.ImageURL = FileHelper.UploadImage(_env, HttpContext.Request.Form.Files[0]);
			}
			var dbItem = await _repo.UpdateItemAsync(ItemModel);
            if (dbItem == false)
            {
                ModelState.AddModelError(string.Empty, "Failed to update item.");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
