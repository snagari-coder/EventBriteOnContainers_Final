using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;
using WebMvc.ViewModels;

namespace WebMvc.Services
{
    public interface IEventCatalogService
    {

        Task<EventCatalog> GetEventCatalogItemsAsync(int? category, int? audience, int? format, int? kind, int? language, int? location, int? ZipCode, int page, int take);

        Task<IEnumerable<SelectListItem>> GetEventFormatsAsync();

        Task<IEnumerable<SelectListItem>> GetEventLocationsAsync();

        Task<IEnumerable<SelectListItem>> GetEventKindsAsync();

        Task<IEnumerable<SelectListItem>> GetEventLanguagesAsync();

        Task<IEnumerable<SelectListItem>> GetEventCategoriesAsync();

        Task<IEnumerable<SelectListItem>> GetEventZipcodesAsync();

        Task<IEnumerable<SelectListItem>> GetEventAudiencesAsync();

        Task<String> PostEventDataAsync(Input_Event_Data data);
        Task<EventCatalog> GetEventItemsAsync(int page, int size, string searchString);
    }
}
