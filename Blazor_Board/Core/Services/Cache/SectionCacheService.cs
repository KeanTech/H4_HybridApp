using Blazor_Board.Models.Data;
using Blazored.LocalStorage;

namespace Blazor_Board.Core.Services.Cache
{
    public class SectionCacheService : ICacheService<Section>
    {
        private readonly string _sectionKey = "Sections";
        ILocalStorageService _storageService;

        public SectionCacheService(ILocalStorageService storageService)
        {
            _storageService = storageService;
        }

        /// <summary>
        /// Uses the <see cref="_sectionKey"/> to get the data
        /// <para><see cref="_sectionKey"/> needs to be defined to use this method</para>
        /// </summary>
        /// <returns>A list of Sections</returns>
        public async Task<List<Section>> GetAll()
        {
            return await _storageService.GetItemAsync<List<Section>>(_sectionKey);
        }

        /// <summary>
        /// Calls <see cref="GetAll"/> to iterate through 
        /// 
        /// </summary>
        /// <param name="id">Is the section id</param>
        /// <returns>The section with a macthing Id or null if nothing is found</returns>
        public async Task<Section?> Get(int id)
        {
            var sections = await GetAll();

            if (sections is null)
                return null;

            Section? exist = sections.FirstOrDefault(x => x.Id == id);

            if (exist is null)
                return null;

            return exist;
        }

        /// <summary>
        /// Uses the <see cref="GetAll"/> to get all the data stored
        /// <para>Then adds a section if the section does not exist in storage</para>
        /// <para>And lastly it will call <see cref="UpdateAll(List{Section})"/> to save the changed list to the storage</para>
        /// </summary>
        /// <param name="section">Is the section that is saved in storage</param>
        public async void Add(Section section)
        {
            var sections = await GetAll();
            var idIndex = sections.MaxBy(x => x.Id).Id;
            if (idIndex is not 0 && section.Id == 0)
                section.Id = idIndex + 1;

            var exist = sections.FirstOrDefault(x => x.Id == section.Id);
            if (exist is not null)
                sections.Add(exist);

            UpdateAll(sections);
        }

        /// <summary>
        /// If the storage is empty it will add the list to storage as bulk insert
        /// <para>If not it will use <see cref="Add(Section)"/> to insert one by one</para>
        /// </summary>
        /// <param name="sections">List of sections to insert</param>
        public async void AddAll(List<Section> sections)
        {
            var sectionCache = await GetAll();
            if (sectionCache is null)
            {
                await _storageService.SetItemAsync(_sectionKey, sections);
                return;
            }

            foreach (var section in sections)
            {
                if (sectionCache.Contains(section))
                    continue;

                Add(section);
            }
        }

        /// <summary>
        /// Uses the <see cref="UpdateAll(List{Section})"/> to update the storage with the edited section
        /// 
        /// <para>Checks the list from <see cref="GetAll"/> for the section that should be updated returns if nothing is found</para>
        /// </summary>
        /// <param name="section">Section to be updated</param>
        public async void Update(Section section)
        {
            var sections = await GetAll();
            if (sections is null)
                return;

            var exist = sections.FirstOrDefault(x => x.Id == section.Id);
            if (exist is null)
                return;

            sections.Remove(exist);
            sections.Add(section);
            UpdateAll(sections);
        }

        /// <summary>
        /// It will only use the bulk feature if storage is null
        /// <para>If not it will use <see cref="Update(Section)"/> in a foreach</para>
        /// </summary>
        /// <param name="sections">Sections to update</param>
        public async void UpdateAll(List<Section> sections)
        {
            var sectionCache = await GetAll();
            if (sectionCache is null)
                await _storageService.SetItemAsync(_sectionKey, sections);

            foreach (var section in sections)
            {
                Update(section);
            }
        }

        /// <summary>
        /// Uses the <see cref="GetAll"/> to get all the data from storage
        /// <para>If the storage is null it will return</para>
        /// <para>Iterates through the data to find a match on section id</para>
        /// <para>When it finds the section it will remove it from the data list and the use <see cref="UpdateAll(List{Section})"/> to save the changed list</para>
        /// </summary>
        /// <param name="section">Section to delete</param>
        public async void Remove(Section section)
        {
            var sectionCache = await GetAll();
            if (sectionCache is null)
                return;

            var project = sectionCache.FirstOrDefault(x => x.Id == section.Id);
            if (project is null)
                return;

            sectionCache.Remove(project);

            UpdateAll(sectionCache);
        }

        /// <summary>
        /// Gets all section data to find out if the section exist in the storage
        /// 
        /// </summary>
        /// <param name="section">Section to look for</param>
        /// <returns>True if exist</returns>
        public async Task<bool> Exist(Section section)
        {
            var sectionCache = await GetAll();
            return sectionCache.Exists(x => x.Id == section.Id);
        }

        /// <summary>
        /// Used to clear the whole storage 
        /// <para>Be careful to use this because it will delete all data not only Section data</para>
        /// </summary>
        public async void Clear()
        {
            await _storageService.ClearAsync();
        }
    }
}
