using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ElGuerre.Items.Api.Application.Models;
using ElGuerre.Items.Api.Infrastructure;
using ElGuerre.Items.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ElGuerre.Items.Api.Controllers
{
    [SwaggerTag("Items")]
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        /// <summary>
        /// Get an item
        /// </summary>
        /// <param name="id">Idetifier for a item to look for</param>
        /// <returns>Item</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemModel), (int)HttpStatusCode.OK)]
        public IActionResult GetItem(int id)
        {
            return Ok(_itemsService.GetItem(id));
        }

        /// <summary>
        /// Get full list of items
        /// </summary>        
        /// <returns>List of items found</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ItemModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetItems()
        {
            return Ok(_itemsService.GetItems());
        }
    }
}
