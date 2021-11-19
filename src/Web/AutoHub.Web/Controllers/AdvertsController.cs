namespace AutoHub.Web.Controllers
{
    using AutoHub.Common;
    using AutoHub.Common.Exceptions;
    using AutoHub.Data.Common.Repositories;
    using AutoHub.Data.Models;
    using AutoHub.Services.Data;
    using AutoHub.Web.ViewModels.Adverts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AdvertsController : Controller
    {
        private readonly IAdvertsService advertsService;
        private readonly IDeletableRepository<Engine> enginesRepository;

        public AdvertsController(IAdvertsService advertsService, IDeletableRepository<Engine> enginesRepository)
        {
            this.advertsService = advertsService;
            this.enginesRepository = enginesRepository;
        }

        public IActionResult ById(string id)
        {
            throw new NotImplementedException(nameof(this.ById));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var inputModel = new CreateAdvertInputModel
            {
                Engines = this.enginesRepository.All()
                    .ToList().Select(e => new SelectListItem(e.Type, e.Id.ToString())),
            };

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateAdvertInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string advertId;

            try
            {
                Advert advert = await this.advertsService.CreateAdvertAsync(input, this.User);
                advertId = advert.Id.ToString();
            }
            catch (InvalidModelStateException ex)
            {
                this.ModelState.AddErrors(ex.ErrorsByPropertyName);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.ById), advertId);
        }
    }
}
