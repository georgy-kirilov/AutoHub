namespace AutoHub.Services.Data
{
    using System.Security.Claims;
    using AutoHub.Data.Models;
    using AutoHub.Web.ViewModels.Adverts;

    public interface IAdvertsService
    {
        Task<Advert> CreateAdvertAsync(CreateAdvertInputModel input, ClaimsPrincipal authorClaims);
    }
}
