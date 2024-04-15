using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Interfaces;

namespace Data
{
	public class ItemRepositoryEf : IItemRepository
	{
		private readonly ItemsContext _context;

		public ItemRepositoryEf(ItemsContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<ItemModel?>> GetItemsAsync()
		{
			return await _context.Items.Where(x => x.isDeleted == false).ToListAsync();
		}

		public async Task<ItemModel?> GetItemByIDAsync(int id)
		{
			return await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<IEnumerable<ItemModel>> GetItemsBySearchAsync(string filter)
		{
			return await _context.Items.Where(x => x.Name.Contains(filter) && x.isDeleted == false).ToListAsync();
		}

		public async Task InsertItemAsync(ItemModel item)
		{
			item.CreatedAt = DateTime.UtcNow;
			item.CreatedBy = Environment.UserName;
			_context.Items.Add(item);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> UpdateItemAsync(ItemModel item)
		{
			var dbItem = await GetItemByIDAsync(item.Id);
			if (dbItem == null)
			{
				return false;
			}

			if (dbItem.isDeleted)
			{
				return false;
			}

			if (!(item.Name.Equals(dbItem.Name) && item.Description.Equals(dbItem.Description) && item.Price.Equals(dbItem.Price) && item.ImageURL.Equals(dbItem.ImageURL)))
			{
				if (!item.Name.Equals(dbItem.Name))
				{
					dbItem.Name = item.Name;
				}
				if (!item.Description.Equals(dbItem.Description))
				{
					dbItem.Description = item.Description;
				}
				if (item.Price != dbItem.Price)
				{
					dbItem.Price = item.Price;
				}
				if (!item.ImageURL.Equals(dbItem.ImageURL))
				{
					dbItem.ImageURL = item.ImageURL;
				}
			}

			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<bool> DeleteItemAsync(int id)
		{
			var itemModel = await GetItemByIDAsync(id);
			if (itemModel == null)
			{
				return false;
			}

			var dbItem = await GetItemByIDAsync(id);
			if (!itemModel.Equals(dbItem))
			{
				return false;
			}

			itemModel.isDeleted = true;
			_context.Items.Update(itemModel);
			await _context.SaveChangesAsync();
			return true;
		}

		public Task<IEnumerable<ItemModel>> GetItemsByPriceAsync(decimal minPrice, decimal maxPrice)
		{
			throw new NotImplementedException();
		}
	}
}
