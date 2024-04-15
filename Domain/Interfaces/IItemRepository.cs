using Domain.Models;

namespace Domain.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<ItemModel?>> GetItemsAsync();
        // we wrap this in a task because we are going to be calling this from a different thread
        Task<ItemModel?> GetItemByIDAsync(int id);
        Task<IEnumerable<ItemModel>> GetItemsBySearchAsync(string filter);

        Task InsertItemAsync(ItemModel item);
        Task<bool> DeleteItemAsync(int id);

        Task<IEnumerable<ItemModel>> GetItemsByPriceAsync(decimal minPrice, decimal maxPrice);

        Task<bool> UpdateItemAsync(ItemModel item);
    }
}
