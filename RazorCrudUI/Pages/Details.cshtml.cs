using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IItemRepository _repo;

        public ItemModel Item { get; set; } = default!; //! is a nullable reference type
        
        public DetailsModel(IItemRepository repo)
        {
            _repo = repo;
        }

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
                Item = itemmodel;
            }
            return Page();

        }
    }
}
