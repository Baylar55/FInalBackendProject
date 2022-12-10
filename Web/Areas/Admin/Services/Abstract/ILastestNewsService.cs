using Web.Areas.Admin.ViewModels.LastestNews;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ILastestNewsService
    {
        Task<LastestNewsIndexVM> GetAsync();
        Task<LastestNewsUpdateVM> GetUpdateModelAsync(int id);
        Task<LastestNewsDetailsVM> DetailsAsync(int id);
        Task<bool> CreateAsync(LastestNewsCreateVM model);
        Task<bool> UpdateAsync(LastestNewsUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
