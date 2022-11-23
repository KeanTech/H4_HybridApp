namespace Blazor_Board.Models.Kanban
{
	public class KanbanSection
	{
        public string Name { get; init; }
        public bool NewTaskOpen { get; set; }
        public string NewTaskName { get; set; }

        public KanbanSection(string name, bool newTaskOpen, string newTaskName)
        {
            Name = name;
            NewTaskOpen = newTaskOpen;
            NewTaskName = newTaskName;
        }
    }
}
