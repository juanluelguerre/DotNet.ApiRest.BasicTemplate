using ElGuerre.Items.Api.Application.Models;
using ElGuerre.Items.Api.Domain;
using ElGuerre.Items.Api.Domain.Interfaces;
using ElGuerre.Items.Api.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElGuerre.Items.Api.Application.Services
{
    public class ItemsService : IItemsService
    {
        private readonly ILogger _logger;
        private readonly IItemsRepository _repository;

        public ItemsService(IItemsRepository repository, ILogger<ItemsService> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        public ItemModel GetItem(int id)
        {
            if (id <= 0)
                throw new ArgumentNullException(nameof(id), "Item Id must be a possitive number.");

            var entity = _repository.GetByKey(id);

            // We can use Automapper instead 
            return new ItemModel
            {
                Id = entity.Id,
                Name = entity.Name

                // Description no assigned
            };
        }

        public List<ItemModel> GetItems()
        {
            var entities = _repository.GetAll();

            // We can use Automapper instead
            var model = new List<ItemModel>();
            foreach (var item in entities)
            {
                model.Add(new ItemModel { Id = item.Id, Name = item.Name });
            }

            return model;
        }

        public async Task<int> UpdateAsync(ItemModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Input model cannot be null.");

            if (model.Id <= 0)
                throw new ArgumentException("Item Id cannot be null or empty.", nameof(model.Id));

            var entity = new ItemEntity
            {
                Id = model.Id,
                Name = model.Name,
                Description = $"The id '{model.Id}' is linked to item '{model.Name}'."
            };

            var result = await _repository.UpdateAsync(entity);
            return result;
        }
    }
}
