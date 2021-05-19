using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host.Mef;
using WebMvc.Models;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class EventCatalogController : Controller
    {

        private readonly IEventCatalogService _service;
        public EventCatalogController(IEventCatalogService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int? page, int? categoryFilterApplied, int? audienceFilterApplied,int? formatFilterApplied,int? kindFilterApplied,int? languageFilterApplied,int? locationFilterApplied,int? zipCodeFilterApplied)
        {

            var items_on_page = 6;
            
            
            
            
            
            var catalog = await _service.GetEventCatalogItemsAsync(categoryFilterApplied, audienceFilterApplied, formatFilterApplied, kindFilterApplied, languageFilterApplied, locationFilterApplied, zipCodeFilterApplied, page ?? 0, items_on_page);

            var vm = new EventCatalogIndexViewModel
            {

                EventItems = catalog.Data,
                Categories = await _service.GetEventCategoriesAsync(),
                Audiences = await _service.GetEventAudiencesAsync(),
                Locations = await _service.GetEventLocationsAsync(),
                Languages = await _service.GetEventLanguagesAsync(),
                Formats = await _service.GetEventFormatsAsync(),
                Kinds = await _service.GetEventKindsAsync(),
                ZipCodes = await _service.GetEventZipcodesAsync(),



                PaginationInfo = new PaginationInfo
                {
                    TotalItems =  catalog.Count,
                    ItemsPerPage = catalog.PageSize,
                    ActualPage = page ?? 0,
                    TotalPages = (int)Math.Ceiling((decimal)catalog.Count / items_on_page),
                },


                categoryFilterApplied = categoryFilterApplied ?? 0,
                audienceFilterApplied = audienceFilterApplied ?? 0,
                formatFilterApplied = formatFilterApplied ?? 0,
                languageFilterApplied = languageFilterApplied ?? 0,
                locationFilterApplied = locationFilterApplied ?? 0,
                kindFilterApplied = kindFilterApplied ?? 0,
                zipCodeFilterApplied = zipCodeFilterApplied?? 0,
            };

            int i = 0;
            return View(vm);
            
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";


            return View();
        }

        public async Task<IActionResult> EventItemView(EventCatalogItem data)
        {


            var languages =  await _service.GetEventLanguagesAsync();
            foreach (var language in languages)
            {
                if (language.Value == data.Event_LanguageId.ToString())
                {
                    data.Event_Language = language.Text;
                }
            }
            return View(data);

        }

       


        public async Task<IActionResult> SearchEvent(int? page, string? searchstring)
        {
            var items_on_page =10 ;
            var catalog = await _service.GetEventItemsAsync(page ?? 0, items_on_page, searchstring);
            var vm = new EventCatalogIndexViewModel
            {

                EventItems = catalog.Data,
                Categories = await _service.GetEventCategoriesAsync(),
                Audiences = await _service.GetEventAudiencesAsync(),
                Locations = await _service.GetEventLocationsAsync(),
                Languages = await _service.GetEventLanguagesAsync(),
                Formats = await _service.GetEventFormatsAsync(),
                Kinds = await _service.GetEventKindsAsync(),
                ZipCodes = await _service.GetEventZipcodesAsync(),



                PaginationInfo = new PaginationInfo
                {
                    TotalItems = catalog.Count,
                    ItemsPerPage = catalog.PageSize,
                    ActualPage = page ?? 0,
                    TotalPages = (int)Math.Ceiling((decimal)catalog.Count / items_on_page),
                },



                categoryFilterApplied =  0,
                audienceFilterApplied =  0,
                formatFilterApplied =  0,
                languageFilterApplied =  0,
                locationFilterApplied =  0,
                kindFilterApplied = 0,
                zipCodeFilterApplied =  0,
            };


            return View(vm);
        }

    }
}