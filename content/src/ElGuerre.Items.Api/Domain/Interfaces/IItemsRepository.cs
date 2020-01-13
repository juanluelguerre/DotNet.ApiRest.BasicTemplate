using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElGuerre.Items.Api.Domain.Interfaces
{
    public interface IItemsRepository
    {
        Task<ItemEntity> GetByKeyAsync(int id);
        ItemEntity GetByKey(int id);
        List<ItemEntity> GetAll();
        Task<List<ItemEntity>> GetAllAsync();
    }
}