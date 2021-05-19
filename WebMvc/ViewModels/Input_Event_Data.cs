using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebMvc.ViewModels
{
    public class Input_Event_Data
    {

        public string Inp_Event_Name { get; set; }
        public string Inp_Event_Desc { get; set; }    
        public string Inp_Event_Organiser { get; set; }
        public DateTime Inp_Start_Time { get; set; }
        public DateTime Inp_End_Time { get; set; }
        public decimal Inp_Price { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Audiences { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
        public IEnumerable<SelectListItem> Kinds { get; set; }
        public IEnumerable<SelectListItem> ZipCodes { get; set; }

        public IEnumerable<SelectListItem> Languages { get; set; }

        public IEnumerable<SelectListItem> Formats { get; set; }

        public int? categoryFilterApplied { get; set; }

        public int? audienceFilterApplied { get; set; }

        public int? formatFilterApplied { get; set; }

        public int? kindFilterApplied { get; set; }

        public int? languageFilterApplied { get; set; }

        public int? locationFilterApplied { get; set; }

        public int? zipCodeFilterApplied { get; set; }

        public IFormFile File { get; set; }

    }
}
