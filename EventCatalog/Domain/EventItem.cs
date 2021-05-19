using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalog.Domain
{
   
    public class EventItem
    {
        public int Id { get; set; }

        public string Event_Name { get; set; }

        public string Event_Desc { get; set; }

        //public int Event_Capacity { get; set; }


        public DateTime Event_Start_Time { get; set; }

        public DateTime Event_End_Time { get; set; }

        public string Event_Pictureurl { get; set; }

       // public string Event_Online_Link { get; set; } //Link for event if online  //Change the name online_lnk

        //public string Event_Address { get; set; } //Address of the venue optional if online

        public decimal Event_Price { get; set; } 

        public int Event_LocationId { get; set; } //Foreign Key for Location online or venue based

        public EventLocation Event_Location { get; set; } //EventLocation type to ensure no junk values entered for foreign key

        public int Event_CategoryId { get; set; } //Foreign key for category 

        public EventCategory Event_Category{ get; set; }  //EventCategory type to ensure no junk values are entered for foreign key

  

        public int Event_LanguageId{ get; set; }//EventLanguage foreign key

        public EventLanguage Event_Language { get; set; }

        public int Event_KindId { get; set; }

        public EventKind Event_Kind { get; set; }

        public EventZipCode Event_ZipCode { get; set; }

        public int Event_ZipCodeId { get; set; }
        public string Event_Organiser { get; set; }
        
        public int Event_AudienceId{ get; set; }

        public EventAudience Event_Audience{ get; set; }

        public int Event_FormatId { get; set; }

        public EventFormat Event_Format { get; set; }

     
        
        }

    
}
