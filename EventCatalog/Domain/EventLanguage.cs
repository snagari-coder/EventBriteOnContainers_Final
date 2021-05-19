using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


enum Language
{
   // AnyLanguage,
    English,
    German,
    Spanish,
    French,
    Hindi,
   
}

namespace EventCatalog.Domain
{
    public class EventLanguage
    {
        public int Id { get; set; }
        public string Event_Language { get; set; }
    }
}
