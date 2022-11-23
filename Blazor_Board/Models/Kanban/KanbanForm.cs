using System.ComponentModel.DataAnnotations;

namespace Blazor_Board.Models.Kanban
{
	public class KanbanForm
	{
        [Required]
        [StringLength(10, ErrorMessage = "Name length can't be more than 10.")]
        public string Name { get; set; }
    }
}
