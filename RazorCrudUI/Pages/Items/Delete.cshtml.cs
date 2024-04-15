using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Interfaces;

namespace UI.Pages.Items
{
    public class DeleteModel : PageModel
    {
        private readonly IItemRepository _repo;

        public DeleteModel(IItemRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public ItemModel ItemModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemmodel = await _repo.GetItemByIDAsync(id);

            if (itemmodel == null)
            {
                return NotFound();
            }
            else
            {
                ItemModel = itemmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isDeleted = await _repo.DeleteItemAsync(id);
            if (isDeleted == false)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete item.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
