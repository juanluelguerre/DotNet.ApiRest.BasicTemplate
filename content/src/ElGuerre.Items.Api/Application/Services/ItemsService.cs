using ElGuerre.Items.Api.Application.Models;
using ElGuerre.Items.Api.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Get the item for teh id passed by param
        /// </summary>
        /// <param name="id">The id to look for</param>
        /// <returns>Item found</returns>
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

        /// <summary>
        /// Get all items for <see cref="ItemModel"/> type.
        /// </summary>
        /// <returns>List of all intems</returns>
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

        public Task<int> UpdateAsync(ItemModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Input model cannot be null.");

            if (model.Id <= 0)
                throw new InvalidOperationException($"{nameof(ItemModel.Id)} cannot be null or empty.");

            return UpdateInternalAsync(model);
        }

        /// <summary>
        /// Internal metod to update a model. 
        /// Avoid sonarqube QA rule: 'Parameter validation in "async"/"await" methods should be wrapped'
        /// </summary>
        /// <param name="model">Model to be updated</param>
        /// <returns>Operation result. 1 or 0 to indicate if update really has change somethig.</returns>        
        private async Task<int> UpdateInternalAsync(ItemModel model)
        {
            var entity = new ItemEntity
            {
                Id = model.Id,
                Name = model.Name,
                Description = $"The id '{model.Id}' is linked to item '{model.Name}'."
            };            

            var result = await _repository.UpdateAsync(entity);

            _logger.LogInformation($"Update completed with id: '{entity.Id}' has been updated with name: {entity.Name}.");

            return result;
        }
    }
}
