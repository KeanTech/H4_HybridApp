using Blazor_Board.Models.Data;

namespace Blazor_Board.Core.Services
{
    public interface ISectionService
    {
        public Task<Section> GetSection(int id);
        public Task<List<Section>> GetSections();
        public void CreateSection(Section section);
        public void UpdateSection(Section section);
        public void DeleteSection(int id);
    }
}
