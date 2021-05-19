using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebMvc.Services;
using WebMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace WebMvc.Controllers
{
    public class PostEventController : Controller
    {
        private readonly IEventCatalogService _service;
        public PostEventController(IEventCatalogService service)
        {
            _service = service;
        }

        [Authorize]
        public async Task<IActionResult> CreateEvent(Input_Event_Data event_Data)
        {    

            var vm = new Input_Event_Data();
            vm.Categories = await _service.GetEventCategoriesAsync();
            vm.Audiences = await _service.GetEventAudiencesAsync();
            vm.Locations = await _service.GetEventLocationsAsync();
            vm.Languages = await _service.GetEventLanguagesAsync();
            vm.Formats = await _service.GetEventFormatsAsync();
            vm.Kinds = await _service.GetEventKindsAsync();
            vm.ZipCodes = await _service.GetEventZipcodesAsync();
            vm.categoryFilterApplied = event_Data.categoryFilterApplied ?? 0;
            vm.audienceFilterApplied = event_Data.audienceFilterApplied ?? 0;
            vm.formatFilterApplied = event_Data.formatFilterApplied ?? 0;
            vm.languageFilterApplied = event_Data.languageFilterApplied ?? 0;
            vm.locationFilterApplied = event_Data.locationFilterApplied ?? 0;
            vm.kindFilterApplied = event_Data.kindFilterApplied ?? 0;
            vm.zipCodeFilterApplied = event_Data.zipCodeFilterApplied ?? 0;

            return View(vm);

        }



        public async Task<IActionResult> ProcessEvent ([FromForm]Input_Event_Data event_Data)
        {
          

            int a = 0;

            event_Data.Inp_Event_Organiser = User.Identity.Name;



            event_Data.Categories = await _service.GetEventCategoriesAsync();
            event_Data.Audiences = await _service.GetEventAudiencesAsync();
            event_Data.Locations = await _service.GetEventLocationsAsync();
            event_Data.Languages = await _service.GetEventLanguagesAsync();
            event_Data.Formats = await _service.GetEventFormatsAsync();
            event_Data.Kinds = await _service.GetEventKindsAsync();
            event_Data.ZipCodes = await _service.GetEventZipcodesAsync();
            var ret_string = await _service.PostEventDataAsync(event_Data);
            
            ViewData["Message"] = ret_string;

            return View();
        }
    }
}