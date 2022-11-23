namespace Blazor_Board.Models.Kanban
{
    public class KanbanTaskItem
    {
        public string Name { get; init; }
        public string Status { get; set; }

        public KanbanTaskItem(string name, string status)
        {
            Name = name;
            Status = status;
        }
    }
}
