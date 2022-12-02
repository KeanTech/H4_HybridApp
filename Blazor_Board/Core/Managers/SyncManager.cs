using Blazor_Board.Core.Services;
using Blazor_Board.Core.Services.Cache;
using Blazor_Board.Models.Data;

namespace Blazor_Board.Core.Managers
{
    /// <summary>
    /// This class has the responseability for keeping data in sync from local storage to db and the other way around
    /// 
    /// <para>
    ///     Did not have time to finish the because i valued other functionality,
    ///     had some trouble with the ui so now i have added a way to turn offline mode on and off
    ///     
    ///     theres no sync as it is now so either you run your data from local storage or from database
    /// </para>
    ///  
    /// </summary>
    public class SyncManager
    {
        private readonly ICacheService<Section> _sectionCacheService;
        private readonly ICacheService<SectionTask> _sectionTaskCacheService;
        private readonly ICacheService<User> _userCacheService;
        private readonly IDataService<Section> _sectionDataService;
        private readonly IDataService<SectionTask> _sectionTaskDataService;
        private readonly IDataService<User> _userDataService;

        public SyncManager(
            ICacheService<Section> sectionCacheService,
            ICacheService<SectionTask> sectionTaskCacheService,
            ICacheService<User> userCacheService,
            IDataService<Section> sectionDataService,
            IDataService<SectionTask> sectionTaskDataService,
            IDataService<User> userDataService) 
        {
            _sectionCacheService = sectionCacheService;
            _sectionTaskCacheService = sectionTaskCacheService;
            _userCacheService = userCacheService;
            _sectionDataService = sectionDataService;
            _sectionTaskDataService = sectionTaskDataService;
            _userDataService = userDataService;
        }


        public void SectionSync(List<Section> sections, List<Section> sectionCache) 
        {
            List<Section> sectionsToUpdate = new List<Section>();
            List<Section> sectionsInCacheToUpdate = new List<Section>();

            foreach (var section in sections)
            {
                var exist = sectionCache.FirstOrDefault(x => x.Id == section.Id);
                if(exist is not null)
                    _sectionCacheService.Update(exist);

            }
        }
    }
}
