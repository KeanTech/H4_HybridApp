namespace Blazor_Board.Models.Data
{
    public class Section
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string ProjectName { get; set; }
        public string NewProjectName { get; set; }
        public bool NewTaskOpen { get; set; }
		public string NewTaskName { get; set; }
        public bool Edit { get; set; }

	}
}
