using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blazor_Board.Models.Data
{
    public class SectionTask
    {
        public int Id { get; set; } 
        public string Status { get; set; } 
        public string Text { get; set; }
        public int UserId { get; set; }
        public int SectionId { get; set; }
        
    }
}
