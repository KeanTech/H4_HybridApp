namespace Blazor_Board.Models.Data
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProjectBoardId { get; set; }
        public IEnumerable<BoardTask> BoardTasks { get; set; }
        public ProjectBoard ProjectBoard { get; set; }

    }
}
