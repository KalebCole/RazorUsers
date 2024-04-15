using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace UI.Pages.Items
{
    public class DetailsModel : PageModel
    {
        private readonly IItemRepository _repo;

        public DetailsModel(IItemRepository repo)
        {
            _repo = repo;
        }

        public ItemModel ItemModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // IEnumerable passing a collection of items to make a list
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
    }
}
