using Web.Areas.Admin.ViewModels.Statistics;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IStatisticsService
    {
        Task<StatisticsIndexVM> GetAllAsync();
        Task<StatisticsUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(StatisticsCreateVM model);
        Task<bool> UpdateAsync(StatisticsUpdateVM model);
        Task DeleteAsync(int id);
    }
}
