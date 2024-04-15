using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMem
{
    public class ItemRepositoryMem : IItemRepository
    {

        IList<ItemModel> _list;

        public ItemRepositoryMem()
        {
            _list = new List<ItemModel> {
            new ItemModel { Id = 1, Name = "Item 1",
            Description = "Description 1", Price = 1.99m },

            new ItemModel { Id = 2, Name = "Item 2",
            Description = "Description 2", Price = 2.99m },

            new ItemModel { Id = 3, Name = "Item 3",
            Description = "Description 3", Price = 3.99m },

            new ItemModel { Id = 4, Name = "Item 4",
            Description = "Description 4", Price = 4.99m },

            new ItemModel { Id = 5, Name = "Item 5",
            Description = "Description 5", Price = 5.99m }

            };
        }
        public Task<bool> DeleteItemAsync(int id)
        {
            // find the item
		    // return false if you can not
        var item = _list.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return Task.FromResult(false);
            // we found item so delete it            		    
            _list.Remove(item);
            return Task.FromResult(true);

        }

        public Task<ItemModel?> GetItemByIDAsync(int id)
        {
            return Task.FromResult(_list.FirstOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<ItemModel?>> GetItemsAsync()
        {
            return Task.FromResult(_list.AsEnumerable());
        }

        public Task<IEnumerable<ItemModel>> GetItemsByPriceAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemModel>> GetItemsBySearchAsync(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return GetItemsAsync();
            return Task.FromResult(_list.Where(i => i.Name.Contains(filter)));
        }

        public Task InsertItemAsync(ItemModel item)
        {
            item.Id = _list.Max(x => x.Id) + 1;

            _list.Add(item);
            return Task.CompletedTask;
        }

        public Task<bool> UpdateItemAsync(ItemModel item)
        {
            // find the item
            // return false if you can not
            var existingItem = _list.FirstOrDefault(x => x.Id == item.Id);
            if (existingItem == null)
                return Task.FromResult(false);

            // we found existing item       
            // existingItem is a reference type
            // so making changes to it here WILL change it in the list as well
            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.Price = item.Price;
            return Task.FromResult(true);
        }
    }
}
