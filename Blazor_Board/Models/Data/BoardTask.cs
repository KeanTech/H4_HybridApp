using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blazor_Board.Models.Data
{
	public class BoardTask
	{
		public int Id { get; set; } // int
		 
		public string Name { get; set; } // varchar(25)
		public string Text { get; set; } // varchar(max)
		public int? PriorityId { get; set; } // int
		public int? SectionId { get; set; } // int
		public int? StatusId { get; set; } // int

		#region Associations
		
		public Priority Priority { get; set; }
		public Section Section { get; set; }
		public Status Status { get; set; }
		
		#endregion
	}
}
