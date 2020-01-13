using System.ComponentModel.DataAnnotations.Schema;

namespace ElGuerre.Items.Api.Domain
{  
    public class ItemEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
