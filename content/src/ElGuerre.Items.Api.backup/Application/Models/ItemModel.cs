using System.ComponentModel.DataAnnotations;

namespace ElGuerre.Items.Api.Application.Models
{
    public class ItemModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
