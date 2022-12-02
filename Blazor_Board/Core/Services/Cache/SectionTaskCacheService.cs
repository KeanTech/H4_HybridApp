using Blazor_Board.Models.Data;
using Blazored.LocalStorage;
using System.Security.Cryptography.X509Certificates;

namespace Blazor_Board.Core.Services.Cache
{
    public class SectionTaskCacheService : ICacheService<SectionTask>
    {
        private readonly string _sectionTaskKey = "SectionTask";
        ILocalStorageService _storageService;

        public SectionTaskCacheService(ILocalStorageService storageService)
        {
            _storageService = storageService;
        }
        /// <summary>
        /// Uses the <see cref="_sectionTaskKey"/> to get the data
        /// <para><see cref="_sectionTaskKey"/> needs to be defined to use this method</para>
        /// </summary>
        /// <returns>A list of Sectiontasks</returns>
        public async Task<List<SectionTask>> GetAll()
        {
            return await _storageService.GetItemAsync<List<SectionTask>>(_sectionTaskKey);
        }

        /// <summary>
        /// Calls <see cref="GetAll"/> to iterate through 
        /// 
        /// </summary>
        /// <param name="id">Is the Sectiontask id</param>
        /// <returns>The section with a macthing Id or null if nothing is found</returns>
        public async Task<SectionTask?> Get(int id)
        {
            List<SectionTask> SectionTasks = await GetAll();

            if (SectionTasks is null)
                return null;

            SectionTask? exist = SectionTasks.FirstOrDefault(x => x.Id == id);

            if (exist is null)
                return null;

            return exist;
        }

        /// <summary>
        /// Uses the <see cref="GetAll"/> to get all the data stored
        /// <para>Then adds a sectiontask if the section does not exist in storage</para>
        /// <para>And lastly it will call <see cref="UpdateAll(List{SectionTask})"/> to save the changed list to the storage</para>
        /// </summary>
        /// <param name="section">Is the section that is saved in storage</param>
        public async void Add(SectionTask sectionTask)
        {
            List<SectionTask> sectionTasks = await GetAll();

            var exist = sectionTasks.FirstOrDefault(x => x.Id == sectionTask.Id);
            if (exist is not null)
                sectionTasks.Add(exist);

            UpdateAll(sectionTasks);
        }

        /// <summary>
        /// If the storage is empty it will add the list to storage as bulk insert
        /// <para>If not it will use <see cref="Add(SectionTask)"/> to insert one by one</para>
        /// </summary>
        /// <param name="sectionTasks">List of sectionTasks to insert</param>
        public async void AddAll(List<SectionTask> sectionTasks)
        {
            var sectionTaskCache = await GetAll();
            if (sectionTaskCache is null)
            {
                await _storageService.SetItemAsync(_sectionTaskKey, sectionTasks);
                return;
            }

            foreach (var sectionTask in sectionTasks)
            {
                if (sectionTaskCache.Contains(sectionTask))
                    continue;

                Add(sectionTask);
            }
        }

        /// <summary>
        /// Uses the <see cref="UpdateAll(List{SectionTask})"/> to update the storage with the edited sectiontask
        /// 
        /// <para>Checks the list from <see cref="GetAll"/> for the section that should be updated returns if nothing is found</para>
        /// </summary>
        /// <param name="section">Sectiontask to be updated</param>
        public async void Update(SectionTask sectionTask)
        {
            var sectionTasks = await GetAll();
            if(sectionTasks is null)
                return;

            var exist = sectionTasks.FirstOrDefault(x => x.Id == sectionTask.Id);
            if (exist is null)
                return;

            sectionTasks.Remove(exist);
            sectionTasks.Add(sectionTask);
        }

        /// <summary>
        /// It will only use the bulk feature if storage is null
        /// <para>If not it will use <see cref="Update(SectionTask)"/> in a foreach</para>
        /// </summary>
        /// <param name="sections">Sections to update</param>
        public async void UpdateAll(List<SectionTask> sectionTasks)
        { 
            var sectiontasks = await GetAll();
            if (sectiontasks is null)
                await _storageService.SetItemAsync(_sectionTaskKey, sectionTasks);

            foreach (var sectionTask in sectionTasks)
            {
                Update(sectionTask);
            }
        }

        /// <summary>
        /// Uses the <see cref="GetAll"/> to get all the data from storage
        /// <para>If the storage is null it will return</para>
        /// <para>Iterates through the data to find a match on sectiontask id</para>
        /// <para>When it finds the section it will remove it from the data list and the use <see cref="UpdateAll(List{SectionTask})"/> to save the changed list</para>
        /// </summary>
        /// <param name="section">Sectiontask to delete</param>
        public async void Remove(SectionTask sectionTask)
        {
            var sectionTasks = await GetAll();
            if (sectionTasks is null)
                return;

            var exist = sectionTasks.FirstOrDefault(x => x.Id == sectionTask.Id);
            if (exist is null)
                return;

            sectionTasks.Remove(exist);

            await _storageService.SetItemAsync<List<SectionTask>>(_sectionTaskKey, sectionTasks);
        }

        /// <summary>
        /// Gets all section data to find out if the sectiontask exist in the storage
        /// 
        /// </summary>
        /// <param name="sectionTask">Sectiontask to look for</param>
        /// <returns>True if exist</returns>
        public async Task<bool> Exist(SectionTask sectionTask)
        {
            var sectionTasks = await GetAll();
            return sectionTasks.Exists(x => x.Id == sectionTask.Id);
        }

        /// <summary>
        /// Used to clear the whole storage 
        /// <para>Be careful to use this because it will delete all data not only Sectiontask data</para>
        /// </summary>
        public async void Clear()
        {
            await _storageService.ClearAsync();
        }
    }
}
