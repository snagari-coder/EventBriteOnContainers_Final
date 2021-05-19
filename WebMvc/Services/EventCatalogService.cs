using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Infrastructure;
using WebMvc.Models;
using WebMvc.ViewModels;

namespace WebMvc.Services
{
    public class EventCatalogService : IEventCatalogService
    {

        private readonly string baseuri;
        private readonly IHttpClient _client;
        private IHttpContextAccessor _httpContextAccesor;


        public EventCatalogService(IConfiguration config, IHttpClient client, IHttpContextAccessor httpContextAccesor)
        {
            baseuri = $"{config["CatalogUrl"]}/api/EventItem/";
            _client = client;
            _httpContextAccesor = httpContextAccesor;
        }


        public async Task<String> PostEventDataAsync([FromForm]Input_Event_Data data)
        {
            EventItem item;
            IFormFile file;
            file = data.File; 
            item = Process_Data(data);
            var token = await GetUserTokenAsync();
            var PostEventUri = ApiPaths.EventCatalog.PostNewEventItem(baseuri);
            var ret_string = await _client.PostEventAsync(PostEventUri,item,file,token);
            return ret_string;

        }


        public EventItem Process_Data(Input_Event_Data data)
        {
           
            EventItem item = new EventItem();
            item.Inp_Event_Name = data.Inp_Event_Name;
            item.Inp_Event_Desc = data.Inp_Event_Desc;
            item.Inp_Start_Time = data.Inp_Start_Time;
            item.Inp_End_Time = data.Inp_End_Time;
            item.Inp_Price = data.Inp_Price;
            item.Inp_Event_Organiser = data.Inp_Event_Organiser;

            foreach (var audience in data.Audiences)
            {
                if(audience.Value==data.audienceFilterApplied.ToString())
                {
                    item.Inp_Event_Audience = audience.Text;
                }
            }
            foreach (var category in data.Categories)
            {
                if (category.Value == data.categoryFilterApplied.ToString())
                {
                    item.Inp_Event_Category = category.Text;
                }
            }

            foreach (var kind in data.Kinds)
            {
                if (kind.Value == data.kindFilterApplied.ToString())
                {
                    item.Inp_Event_Kind = kind.Text;
                }
            }

            foreach (var format in data.Formats)
            {
                if (format.Value == data.formatFilterApplied.ToString())
                {
                    item.Inp_Event_Format = format.Text;
                }
            }


            foreach (var location in data.Locations)
            {
                if (location.Value == data.locationFilterApplied.ToString())
                {
                    item.Inp_Event_Location = location.Text;
                }
            }

            foreach (var language in data.Languages)
            {
                if (language.Value == data.languageFilterApplied.ToString())
                {
                    item.Inp_Event_Language = language.Text;
                }
            }

            foreach (var zipcode in data.ZipCodes)
            {
                if (zipcode.Value == data.zipCodeFilterApplied.ToString())
                {
                    item.Inp_Event_ZipCode= zipcode.Text;
                }
            }
          
            return item;
        }
        public async Task<IEnumerable<SelectListItem>> GetEventAudiencesAsync()
        {
            var EventAudiencesUri = ApiPaths.EventCatalog.GetallEventAudiences(baseuri);
            var datastring = await _client.GetStringAsync(EventAudiencesUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value=null,
                    Text="All",
                    Selected=true
                }
            };

            var audiences = JArray.Parse(datastring);

            foreach (var audience in audiences)
            {
                items.Add(
                  new SelectListItem()
                  {
                      Value = audience.Value<string>("id"),
                      Text = audience.Value<string>("event_AgeGroup")
                  }
                  );
            }

            return items;
        }

        public async Task<EventCatalog> GetEventCatalogItemsAsync(int? category, int? audience, int? format, int? kind, int? language, int? location, int? zipCode, int page, int take)
        {
            var eventCatalogItemsUri = ApiPaths.EventCatalog.GetAllEventItems(baseuri, category, audience, format, kind, language, location, zipCode, page, take);
            var datastring = await _client.GetStringAsync(eventCatalogItemsUri);
            return JsonConvert.DeserializeObject<EventCatalog>(datastring);
        }

        public async Task<IEnumerable<SelectListItem>> GetEventCategoriesAsync()
        {
            var EventCategoriesUri = ApiPaths.EventCatalog.GetAllEventCategories(baseuri);
            var datastring = await _client.GetStringAsync(EventCategoriesUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value=null,
                    Text="All",
                    Selected=true
                }
            };

            var categories = JArray.Parse(datastring);

            foreach (var category in categories)
            {
                items.Add(
                  new SelectListItem()
                  {
                      Value = category.Value<string>("id"),
                      Text = category.Value<string>("event_Category")
                  }
                  );
            }

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetEventFormatsAsync()
        {
            var EventFormatsUri = ApiPaths.EventCatalog.GetallEventFormats(baseuri);
            var datastring = await _client.GetStringAsync(EventFormatsUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value=null,
                    Text="All",
                    Selected=true
                }
            };

            var formats = JArray.Parse(datastring);

            foreach (var format in formats)
            {
                items.Add(
                  new SelectListItem()
                  {
                      Value = format.Value<string>("id"),
                      Text = format.Value<string>("event_Format")
                  }
                  );
            }
            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetEventKindsAsync()
        {
            var EventKindsUri = ApiPaths.EventCatalog.GetallEventKinds(baseuri);
            var datastring = await _client.GetStringAsync(EventKindsUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value=null,
                    Text="All",
                    Selected=true
                }
            };

            var kinds = JArray.Parse(datastring);

            foreach (var kind in kinds)
            {
                items.Add(
                  new SelectListItem()
                  {
                      Value = kind.Value<string>("id"),
                      Text = kind.Value<string>("event_Kind")
                  }
                  );
            }
            return items;

        }

        public async Task<IEnumerable<SelectListItem>> GetEventLanguagesAsync()
        {
            var EventLanguagesUri = ApiPaths.EventCatalog.GetAllEventLanguages(baseuri);
            var datastring = await _client.GetStringAsync(EventLanguagesUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value=null,
                    Text="All",
                    Selected=true
                }
            };

            var languages = JArray.Parse(datastring);

            foreach (var language in languages)
            {
                items.Add(
                  new SelectListItem()
                  {
                      Value = language.Value<string>("id"),
                      Text = language.Value<string>("event_Language")
                  }
                  );
            }
            return items;

        }

        public async Task<IEnumerable<SelectListItem>> GetEventLocationsAsync()
        {
            var EventLocationsUri = ApiPaths.EventCatalog.GetAllEventLocations(baseuri);
            var datastring = await _client.GetStringAsync(EventLocationsUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value=null,
                    Text="All",
                    Selected=true
                }
            };

            var locations = JArray.Parse(datastring);

            foreach (var location in locations)
            {
                items.Add(
                  new SelectListItem()
                  {
                      Value = location.Value<string>("id"),
                      Text = location.Value<string>("event_Location")
                  }
                  );
            }
            return items;

        }

        public async Task<IEnumerable<SelectListItem>> GetEventZipcodesAsync()
        {
            var EventZipCodesUri = ApiPaths.EventCatalog.GetAllEventZipcodes(baseuri);
            var datastring = await _client.GetStringAsync(EventZipCodesUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value=null,
                    Text="All",
                    Selected=true
                }
            };

            var zipcodes = JArray.Parse(datastring);

            foreach (var zipcode in zipcodes)
            {
                items.Add(
                  new SelectListItem()
                  {
                      Value = zipcode.Value<string>("id"),
                      Text = zipcode.Value<string>("event_Zipcode")
                  }
                  );
            }
            return items;

        }

        public async Task<EventCatalog> GetEventItemsAsync(int page, int size, string? searchstring)
        {
            var EventCatalogUri = ApiPaths.EventCatalog.GetSearchedEvents(baseuri, page, size, searchstring);
            var dataString = await _client.GetStringAsync(EventCatalogUri);
            return JsonConvert.DeserializeObject<EventCatalog>(dataString);

        }

        async Task<string> GetUserTokenAsync()
        {
            var context = _httpContextAccesor.HttpContext;

            return await context.GetTokenAsync("access_token");

        }


    }

}