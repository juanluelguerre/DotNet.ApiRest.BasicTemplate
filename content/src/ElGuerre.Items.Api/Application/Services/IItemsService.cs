using ElGuerre.Items.Api.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElGuerre.Items.Api.Application.Services
{
    public interface IItemsService
    {
        ItemModel GetItem(int id);
        List<ItemModel> GetItems();
        Task<int> UpdateAsync(ItemModel model);
    }
}
