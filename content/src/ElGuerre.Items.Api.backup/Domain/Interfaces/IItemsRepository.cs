using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElGuerre.Items.Api.Domain.Interfaces
{
    public interface IItemsRepository
    {
        ItemEntity GetByKey(int id);
        List<ItemEntity> GetAll();
        Task<int> UpdateAsync(ItemEntity entity);
    }
}