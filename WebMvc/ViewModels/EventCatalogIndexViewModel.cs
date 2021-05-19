using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class EventCatalogIndexViewModel
    {
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Audiences{ get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
        public IEnumerable<SelectListItem> Kinds { get; set; }
        public IEnumerable<SelectListItem> ZipCodes { get; set; }

        public IEnumerable<SelectListItem> Languages{ get; set; }

        public IEnumerable<SelectListItem> Formats  { get; set; }
        
        public IEnumerable<EventCatalogItem> EventItems { get; set; }

        public PaginationInfo PaginationInfo { get; set; }
       
        public int? categoryFilterApplied { get; set; }

        public int? audienceFilterApplied { get; set; }

        public int? formatFilterApplied { get; set; }

        public int? kindFilterApplied { get; set; }

        public int? languageFilterApplied { get; set; }

        public int? locationFilterApplied { get; set; }

        public int? zipCodeFilterApplied { get; set; }

        public string searchstring { get; set; }

    }
}
