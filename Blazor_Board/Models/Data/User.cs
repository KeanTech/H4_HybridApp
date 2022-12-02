using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor_Board.Models.Data
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Contact { get; set; } 
        public string? Info { get; set; } 
        public string? Sauce { get; set; } 
    }
}
