
using EventCatalog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using NuGet.Frameworks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EventCatalog.Data
{
    public static class EventCatalogSeed
    {

        public static void Seed(EventCatalogContext EventCatalogContext, IWebHostEnvironment env)
        {
            EventCatalogContext.Database.Migrate();

            if (!EventCatalogContext.EventAudiences.Any())
            {
                EventCatalogContext.EventAudiences.AddRange(GetEventAudiences());
                EventCatalogContext.SaveChanges();
            }

            if (!EventCatalogContext.EventCategories.Any())
            {
                EventCatalogContext.EventCategories.AddRange(GetEventCategories());
                EventCatalogContext.SaveChanges();
            }
            if (!EventCatalogContext.EventKinds.Any())
            {
                EventCatalogContext.EventKinds.AddRange(GetEventKinds());
                EventCatalogContext.SaveChanges();
            }

            if (!EventCatalogContext.EventLanguages.Any())
            {
                EventCatalogContext.EventLanguages.AddRange(GetEventLanguages());
                EventCatalogContext.SaveChanges();
            }

            if (!EventCatalogContext.EventFormats.Any())
            {
                EventCatalogContext.EventFormats.AddRange(GetEventFormats());
                EventCatalogContext.SaveChanges();
            }

            if (!EventCatalogContext.EventLocations.Any())
            {
                EventCatalogContext.EventLocations.AddRange(GetEventLocations());
                EventCatalogContext.SaveChanges();
            }


            if (!EventCatalogContext.EventZipCodes.Any())
            {
                EventCatalogContext.EventZipCodes.AddRange(GetEventZipCodes());
                EventCatalogContext.SaveChanges();
            }



        }

        private static IEnumerable<EventAudience> GetEventAudiences()
        {


            List<EventAudience> event_audience = new List<EventAudience>();
            string desc = "default";
            foreach (int i in Enum.GetValues(typeof(AgeGroup)))
            {
                var val = (AgeGroup)i;

                var enumMember = val.GetType().GetMember(val.ToString()).FirstOrDefault();
                var attr = enumMember.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attr.Length > 0)
                {
                    var da = attr[0] as DescriptionAttribute;
                    desc = da.Description;
                }

                event_audience.Add(new EventAudience
                {
                    Event_AgeGroup = desc

                }

               );

            }
            return event_audience;

        }
        private static IEnumerable<EventCategory> GetEventCategories()
        {


            List<EventCategory> event_category = new List<EventCategory>();
            string desc = "default";
            foreach (int i in Enum.GetValues(typeof(Category)))
            {
                var val = (Category)i;

                var enumMember = val.GetType().GetMember(val.ToString()).FirstOrDefault();
                var attr = enumMember.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attr.Length > 0)
                {
                    var da = attr[0] as DescriptionAttribute;
                    desc = da.Description;
                }

                event_category.Add(new EventCategory
                {
                    Event_Category = desc

                }

               ); ;

            }
            return event_category;
        }


        private static IEnumerable<EventKind> GetEventKinds()
        {
            List<EventKind> event_kind = new List<EventKind>();
            foreach (string s in Enum.GetNames(typeof(Kind)))
            {
                event_kind.Add(new EventKind
                {
                    Event_Kind = s
                });

            }
            return event_kind;
        }


        private static IEnumerable<EventLanguage> GetEventLanguages()
        {
            List<EventLanguage> event_language = new List<EventLanguage>();
            foreach (string s in Enum.GetNames(typeof(Language)))
            {
                event_language.Add(new EventLanguage
                {
                    Event_Language = s
                });

            }
            return event_language;

        }

        private static IEnumerable<EventFormat> GetEventFormats()
        {
            List<EventFormat> event_format = new List<EventFormat>();
            foreach (string s in Enum.GetNames(typeof(Format)))
            {
                event_format.Add(new EventFormat
                {
                    Event_Format = s
                });

            }
            return event_format;

        }

        private static IEnumerable<EventLocation> GetEventLocations()
        {

            List<EventLocation> event_locations = new List<EventLocation>();
            string desc = "default";
            foreach (int i in Enum.GetValues(typeof(Location)))
            {
                var val = (Location)i;

                var enumMember = val.GetType().GetMember(val.ToString()).FirstOrDefault();
                var attr = enumMember.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attr.Length > 0)
                {
                    var da = attr[0] as DescriptionAttribute;
                    desc = da.Description;
                }

                event_locations.Add(new EventLocation
                {
                    Event_Location = desc
                   

                }

               );

            }
            return event_locations;
        }


       
      
        private static IEnumerable<EventZipCode> GetEventZipCodes()
        {

            List<EventZipCode> event_zipcodes = new List<EventZipCode>();
            string desc = "default";
            foreach (int i in Enum.GetValues(typeof(ZipCode)))
            {
                var val = (ZipCode)i;

                var enumMember = val.GetType().GetMember(val.ToString()).FirstOrDefault();
                var attr = enumMember.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attr.Length > 0)
                {
                    var da = attr[0] as DescriptionAttribute;
                    desc = da.Description;
                }

                event_zipcodes.Add(new EventZipCode
                {
                    Event_Zipcode = desc

                }

               );

            }
            return event_zipcodes;
        }


        

        public static async Task<String> UpdateEvent(EventInputData eventInput, EventCatalogContext catalogContext,IFormFile File,IWebHostEnvironment env)
        {
            EventItem eventItem = new EventItem();
            try
            {

                eventItem.Event_Name = eventInput.Inp_Event_Name;
                eventItem.Event_Desc = eventInput.Inp_Event_Desc;
                eventItem.Event_Organiser = eventInput.Inp_Event_Organiser;
                eventItem.Event_Start_Time = eventInput.Inp_Start_Time;
                eventItem.Event_End_Time = eventInput.Inp_End_Time;
                eventItem.Event_Price = eventInput.Inp_Price;
                
                if(File != null)
                {
                    eventItem.Event_Pictureurl = File.FileName;
                    var result = await upload_file(File, env);

                    if (result != "Ok")
                    {
                        return result;
                    }
                }
                else
                {
                    eventItem.Event_Pictureurl = "Picture Not Provided";
                }

                int key=0;


                if (eventInput.Inp_Event_Category != null)
                {
                    key = await FindForeignKey(eventInput.Inp_Event_Category, catalogContext, "Category");
                    eventItem.Event_CategoryId = key;
                }
                if (key==0 || eventInput.Inp_Event_Category==null)
                {

                    throw new ArgumentNullException(String.Format("Inp_Event_Category is null"));
                }


                if (eventInput.Inp_Event_Audience != null)
                {
                    key = await FindForeignKey(eventInput.Inp_Event_Audience, catalogContext, "Audiences");
                    eventItem.Event_AudienceId = key;
                }
                if (key == 0 || eventInput.Inp_Event_Audience == null)
                {
                    throw new ArgumentNullException(String.Format("Inp_Event_Audience is null"));

                }


                if (eventInput.Inp_Event_Format != null)
                {
                    key = await FindForeignKey(eventInput.Inp_Event_Format, catalogContext, "Format");
                    eventItem.Event_FormatId = key;
                }
                if (key == 0 || eventInput.Inp_Event_Format== null)
                {
                    throw new ArgumentNullException(String.Format("Inp_Event_Format is null"));
                }


                if (eventInput.Inp_Event_Kind != null)
                {
                    key = await FindForeignKey(eventInput.Inp_Event_Kind, catalogContext, "Kind");
                    eventItem.Event_KindId = key;
                }
                if (key == 0 || eventInput.Inp_Event_Kind == null)
                {
                    throw new ArgumentNullException(String.Format("Inp_Event_Kind is null"));
                }

                if (eventInput.Inp_Event_Location != null)
                {
                   

                    key = await FindForeignKey(eventInput.Inp_Event_Location, catalogContext, "Location");
                    
                    eventItem.Event_LocationId = key;
                }

                if (key == 0 || eventInput.Inp_Event_Location== null)
                {
                    throw new ArgumentNullException(String.Format("Inp_Event_Location is null"));

                }


                if (eventInput.Inp_Event_Language != null)
                {

                    key = await FindForeignKey(eventInput.Inp_Event_Language, catalogContext, "Language");
                    eventItem.Event_LanguageId = key;
                }
                if (key == 0 || eventInput.Inp_Event_Language == null)
                {

                    throw new ArgumentNullException(String.Format("Inp_Event_Language is null"));

                }

                if (eventInput.Inp_Event_ZipCode != null)
                {
                    key = await FindForeignKey(eventInput.Inp_Event_ZipCode, catalogContext, "ZipCode");
                    eventItem.Event_ZipCodeId = key;
                }

                if (key == 0 || eventInput.Inp_Event_ZipCode == null)
                {
                    throw new ArgumentNullException(String.Format("Inp_Event_ZipCode is null"));
                }

               

                try
                {
                    await catalogContext.EventItems.AddAsync(eventItem);
                    catalogContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {

                    return "DB UPDATE FAILED";
                }
                 
                return "Event Addition Succesful";
            }

            catch(ArgumentNullException ex)
            {

                return  ex.GetType().Name + ex.Message;


            }

          
        }

        private static async Task<string> upload_file(IFormFile file,IWebHostEnvironment env)
        {
            var webRoot = env.WebRootPath;
            var path = Path.Combine($"{webRoot}/Uploads/");
            try
            { 
               
                if ( file!=null )
                {

                    if (!Directory.Exists(env.WebRootPath + "/Uploads/"))
                    {
                        Directory.CreateDirectory(env.WebRootPath + "/Uploads/");
                    }

                    await using (FileStream fileStream = System.IO.File.Create(path + file.FileName))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                        return "Ok";
                    }
                }
                else
                {
                    throw new Exception("No File Uploaded");
                }
              
            }
            catch(Exception ex)
            {
                return ex.GetType().Name + ex.Message;
            }
            
           
           
        }


        private static async Task<int> FindForeignKey(String search_str,EventCatalogContext  catalogContext, string table_type)
        {
           try
            {
                if (table_type == "Category")
                {
                    var category =await catalogContext.EventCategories
                       .Where(b => b.Event_Category == search_str)
                       .FirstOrDefaultAsync();
                    if(category != null)
                        return category.Id;
                }

                if (table_type == "Location")
                {
                    var location = await catalogContext.EventLocations
                      .Where(b => b.Event_Location == search_str)
                      .FirstOrDefaultAsync();
                    if (location != null)
                        return location.Id;
                }
                if (table_type == "Format")
                {
                    var format = await catalogContext.EventFormats
                       .Where(b => b.Event_Format == search_str)
                       .FirstOrDefaultAsync();
                    if (format != null)
                        return format.Id;
                }
                if (table_type == "Kind")
                {
                    var kind = await catalogContext.EventKinds
                       .Where(b => b.Event_Kind == search_str)
                       .FirstOrDefaultAsync();
                    if (kind != null)
                    return kind.Id;
                }

                

                if (table_type == "Audiences")
                {
                    var Audience = await catalogContext.EventAudiences
                       .Where(b => b.Event_AgeGroup == search_str)
                       .FirstOrDefaultAsync();
                    if (Audience != null)
                        return Audience.Id;
                }
                if (table_type == "Language")
                {
                    var Language = await catalogContext.EventLanguages
                       .Where(b => b.Event_Language == search_str)
                       .FirstOrDefaultAsync();
                    if (Language!= null)
                        return Language.Id;
                }

        


                if (table_type == "ZipCode")
                {
                    var Zipcode = await catalogContext.EventZipCodes
                      .Where(b => b.Event_Zipcode == search_str)
                      .FirstOrDefaultAsync();
                    if (Zipcode != null)
                        return Zipcode.Id;

                }
                return 0;
            }
            catch(Exception)
            {
                return 0;
            }
            
        }
            


        

    }
}
