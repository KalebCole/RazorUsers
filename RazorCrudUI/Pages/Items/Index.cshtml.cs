using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace UI.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly IItemRepository _repo;

        public IndexModel(IItemRepository repo)
        {
            _repo = repo;
        }

        // we do not bind the property here, because we are not updating anything --> it is read only
        public IList<ItemModel> ItemModel { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Prices { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? ItemPriceMin { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? ItemPriceMax { get; set; }

        // this is getting called on a different thread
        // when your interface is locked up, things are working on the same thread
        // with async, it won't lock up the interface, it will just freeze
        public async Task OnGetAsync()
        {
            var items = await _repo.GetItemsAsync();
            var itemList = items.ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                itemList = (List<ItemModel>)await _repo.GetItemsBySearchAsync(SearchString);
            }

            if (ItemPriceMin.HasValue)
            {
                itemList = itemList.Where(x => x.Price >= ItemPriceMin).ToList();
            }
            if (ItemPriceMax.HasValue)
            {
                itemList = itemList.Where(x => x.Price <= ItemPriceMax).ToList();
            }

            ItemModel = await Task.Run(() => itemList.ToList());
        }
    }
}
