using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace EventCatalog.Domain
{
    public class EventInputData
    {
        public string Inp_Event_Name { get; set; }
        public string Inp_Event_Desc { get; set; }
     
        public DateTime Inp_Start_Time { get; set; }
        public DateTime Inp_End_Time { get; set; }
      
        public decimal Inp_Price { get; set; } 
        public string Inp_Event_Category { get; set; }
        public string Inp_Event_Location { get; set; }     
        public string Inp_Event_Language { get; set; }
        public string Inp_Event_Kind { get; set; }
        public string Inp_Event_ZipCode { get; set; }
        public string Inp_Event_Audience { get; set; }
        public string Inp_Event_Format { get; set; }
        public string Inp_Event_Organiser { get; set; }

    }
}