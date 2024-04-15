using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IItemRepository _repo;


        
        public IEnumerable<ItemModel> Items { get; set; }


        [BindProperty(SupportsGet = true)]
        public string? Filter { get; set; }

        public IndexModel(IItemRepository repo, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task OnGetAsync()
        {
            if(Filter != null)
            {
                Items = await _repo.GetItemsBySearchAsync(Filter);
            }
            else
            {
                Items = await _repo.GetItemsAsync();
            }

        }
    }
}
