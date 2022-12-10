using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Statistics;

namespace Web.Areas.Admin.Services.Concrete
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticRepository;
        private readonly ModelStateDictionary _modelState;

        public StatisticsService(IStatisticsRepository statisticRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _statisticRepository = statisticRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<StatisticsIndexVM> GetAllAsync()
        {
            var statistic = await _statisticRepository.GetAllAsync();
            var model = new StatisticsIndexVM
            {
                Statistics = statistic
            };
            return model;
        }

        public async Task<StatisticsUpdateVM> GetUpdateModelAsync(int id)
        {
            var statistic = await _statisticRepository.GetAsync(id);
            var model = new StatisticsUpdateVM
            {
                Id = statistic.Id,
                Quantity = statistic.Quantity,
                Title = statistic.Title
            };
            return model;
        }

        public async Task<bool> CreateAsync(StatisticsCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _statisticRepository.AnyAsync(st => st.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This content already created");
                return false;
            }
            var statistic = new Statistics
            {
                CreatedAt = DateTime.Now,
                Quantity = model.Quantity,
                Title = model.Title
            };
            await _statisticRepository.CreateAsync(statistic);
            return true;
        }

        public async Task<bool> UpdateAsync(StatisticsUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var statistic = await _statisticRepository.GetAsync(model.Id);
            var isExist = await _statisticRepository.AnyAsync(st => st.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && model.Id != st.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This content already created");
                return false;
            }
            statistic.Quantity = model.Quantity;
            statistic.Title = model.Title;
            statistic.ModifiedAt = DateTime.Now;

            await _statisticRepository.UpdateAsync(statistic);
            return true;
        }
        
        public async Task DeleteAsync(int id)
        {
            var statistic = await _statisticRepository.GetAsync(id);
            await _statisticRepository.DeleteAsync(statistic);
        }
    }
}
