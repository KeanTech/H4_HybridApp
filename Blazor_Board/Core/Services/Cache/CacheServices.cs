using Blazor_Board.Models.Data;
using System.Runtime.CompilerServices;

namespace Blazor_Board.Core.Services.Cache
{
    /// <summary>
    /// This class is a container for all cache services to make it more compact when injecting it on the page 
    /// </summary>
    public class CacheServices
    {
        public readonly ICacheService<SectionTask> SectionTaskCache;
        public readonly ICacheService<Section> SectionCache;
        public readonly ICacheService<User> UserCache;
        
        /// <summary>
        /// This constructor should be use with dependency injection
        /// </summary>
        /// <param name="sectionTaskCache">Its defined in <see cref="SectionTask"/>  </param>
        /// <param name="sectionCache"></param>
        /// <param name="userCache"></param>
        public CacheServices(ICacheService<SectionTask> sectionTaskCache,
            ICacheService<Section> sectionCache,
            ICacheService<User> userCache
            )
        {
            SectionTaskCache = sectionTaskCache;
            SectionCache = sectionCache;
            UserCache = userCache;
        }

        /// <summary>
        /// Makes a call to the <see cref="ICacheService{T}.GetAll"/> to get the sections from the local storage
        /// <para>Then it iterates through the sections to find all the projects names</para>
        /// <para>
        /// Makes a check to make sure that there is no duplications 
        /// </para>
        /// </summary>
        /// <returns>A list of all project names</returns>
        public async Task<List<string>> GetProjects() 
        {
            var projects = new List<string>();
            var sections = await SectionCache.GetAll();

            foreach (var section in sections)
            {
                if (projects.Contains(section.ProjectName))
                    continue;

                projects.Add(section.ProjectName);
            }

            return projects;
        }

        /// <summary>
        /// Makes a call to <see cref="ICacheService{T}.GetAll"/> to get all sections
        /// <para>Then it iterates through the list and finds all the sections that matches the criteria</para>
        /// </summary>
        /// <param name="projectName">Is the criteria for the search in the section list</param>
        /// <returns>All the sections with the project name of the parameter</returns>
        public async Task<List<Section>> GetProjectSections(string projectName) 
        {
            var sections = await SectionCache.GetAll();
            
            var projectSections = sections.Where(x => x.ProjectName == projectName);

            return projectSections.ToList();
        }

        /// <summary>
        /// Uses the <see cref="GetProjectSections(string)"/> to make a list of SectionTasks
        /// 
        /// <para>Uses <see cref="Section.Id"/> to match the <see cref="SectionTask.SectionId"/></para>
        /// </summary>
        /// <param name="projectName">Used get data from a specifik project</param>
        /// <returns>A list of SectionTask that matches the sections with the project name</returns>
        public async Task<List<SectionTask>> GetSectionTasks(string projectName)
        {
            var sectionTasks = new List<SectionTask>();
            var sections = await GetProjectSections(projectName);
            var savedSectionTasks = await SectionTaskCache.GetAll();

            foreach (var section in sections)
            {
                var tasks = savedSectionTasks.FindAll(x => x.SectionId == section.Id);
                if (tasks is not null && tasks.Count > 0)
                    sectionTasks.AddRange(tasks);
            }

            return sectionTasks;
        }
    }
}
