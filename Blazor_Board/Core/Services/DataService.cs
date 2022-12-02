using Blazor_Board.Models.Data;

namespace Blazor_Board.Core.Services
{
    public class DataService
    {
        public readonly IDataService<Section> SectionService;
        public readonly IDataService<SectionTask> SectionTaskService;
        public readonly IDataService<User> UserService;

        public DataService(
            IDataService<Section> sectionService,
            IDataService<SectionTask> sectionTaskService,
            IDataService<User> userService)
        {
            SectionService = sectionService;
            SectionTaskService = sectionTaskService;
            UserService = userService;
        }


        public async Task<List<string>> GetProjects() 
        {
            var projects = new List<string>();
            var sections = await SectionService.Get();

            foreach (var section in sections)
            {
                if (projects.Contains(section.ProjectName))
                    continue;

                projects.Add(section.ProjectName);
            }

            return projects;
        }

        public async Task<List<Section>> GetProjectSections(string projectName) 
        {
            var sections = await SectionService.Get();
            var projectSections = sections.Where(x => x.ProjectName == projectName);
            
            return projectSections.ToList();
        }

        public async Task<List<SectionTask>> GetSectionTasks(string projectName) 
        { 
            var sectionTasks = new List<SectionTask>();
            List<Section> sections = await GetProjectSections(projectName);
            List<SectionTask> savedSectionTasks = await SectionTaskService.Get();
            
            foreach (var section in sections)
            {
                var tasks = savedSectionTasks.FindAll(x => x.SectionId == section.Id);
                
                if(tasks is not null && tasks.Count > 0)
                    sectionTasks.AddRange(tasks);
            }

            return sectionTasks;
        }

    }
}
