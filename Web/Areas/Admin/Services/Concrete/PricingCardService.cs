using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.PricingCard;

namespace Web.Areas.Admin.Services.Concrete
{
    public class PricingCardService : IPricingCardService
    {
        private readonly IPricingCardRepository _pricingCardRepository;
        private readonly ModelStateDictionary _modelState;
        private readonly IFileService _fileService;

        public PricingCardService(IActionContextAccessor actionContextAccessor,
                                  IPricingCardRepository pricingCardRepository,
                                  IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _pricingCardRepository = pricingCardRepository;
            _fileService = fileService;
        }

        public async Task<PricingCardIndexVM> GetAsync()
        {
            var model = new PricingCardIndexVM()
            {
                PricingCards = await _pricingCardRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<PricingCardUpdateVM> GetUpdateModelAsync(int id)
        {
            var pricingCard = await _pricingCardRepository.GetAsync(id);
            if (pricingCard == null) return null;
            var model = new PricingCardUpdateVM()
            {
                Title = pricingCard.Title,
                SubTitle = pricingCard.SubTitle,
                Features = pricingCard.Features,
                PricePer = pricingCard.PricePer,
                Status = pricingCard.Status,
                PriceValue = pricingCard.PriceValue,
                PriceUnit = pricingCard.PriceUnit,
            };
            return model;
        }

        public async Task<PricingCardDetailsVM> DetailsAsync(int id)
        {
            var pricingCard = await _pricingCardRepository.GetAsync(id);
            if (pricingCard == null) return null;
            var model = new PricingCardDetailsVM()
            {
                Title = pricingCard.Title,
                SubTitle = pricingCard.SubTitle,
                Features = pricingCard.Features,
                PriceUnit = pricingCard.PriceUnit,
                PricePer = pricingCard.PricePer,
                PriceValue = pricingCard.PriceValue,
                Status = pricingCard.Status,
                CreatedAt = pricingCard.CreatedAt,
                ModifiedAt = pricingCard.ModifiedAt,
            };
            return model;
        }

        public async Task<bool> CreateAsync(PricingCardCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var pricingCard = new PricingCard
            {
                Title = model.Title,
                SubTitle = model.SubTitle,
                Features = model.Features,
                PricePer = model.PricePer,
                PriceUnit = model.PriceUnit,
                PriceValue = model.PriceValue,
                Status = model.Status,
                CreatedAt = DateTime.Now,
            };
            await _pricingCardRepository.CreateAsync(pricingCard);
            return true;
        }

        public async Task<bool> UpdateAsync(PricingCardUpdateVM model, int id)
        {
            var pricingCard = await _pricingCardRepository.GetAsync(id);
            if (pricingCard == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;

            pricingCard.Title = model.Title;
            pricingCard.SubTitle = model.SubTitle;
            pricingCard.Status = model.Status;
            pricingCard.PricePer = model.PricePer;
            pricingCard.PriceValue = model.PriceValue;
            pricingCard.ModifiedAt = DateTime.Now;
            pricingCard.PriceUnit = model.PriceUnit;
            pricingCard.Features = model.Features;
            await _pricingCardRepository.UpdateAsync(pricingCard);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pricingCard = await _pricingCardRepository.GetAsync(id);
            if (pricingCard == null) return false;
            await _pricingCardRepository.DeleteAsync(pricingCard);
            return true;
        }
    }
}
