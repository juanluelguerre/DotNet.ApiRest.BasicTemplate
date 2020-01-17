using ElGuerre.Items.Api.Domain;
using ElGuerre.Items.Api.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElGuerre.Items.Api.Infrastructure.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly ItemsContext _context;

        public ItemsRepository(ItemsContext context, ILogger<ItemsRepository> logger)
        {
            _context = context;
        }

        public List<ItemEntity> GetAll()
        {
            return _context.Items.ToList();
        }

        public Task<List<ItemEntity>> GetAllAsync()
        {
            return _context.Items.ToAsyncEnumerable().ToList();
        }

        public ItemEntity GetByKey(int id)
        {
            var item = _context.Items.Find(id);
            return item;
        }

        public Task<ItemEntity> GetByKeyAsync(int id)
        {
            var item = _context.Items.FindAsync(id);
            return item;
        }

        public async Task<int> UpdateAsync(ItemEntity entity)
        {
            if (!_context.Items.Any(item => item.Id == entity.Id))
                _context.Items.Add(entity);
            else
                _context.Items.Update(entity);            

            return await _context.SaveChangesAsync();
        }
    }
}
