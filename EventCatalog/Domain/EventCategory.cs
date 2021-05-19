using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalog.Domain
{
    public enum Category
    {
        //[Description("Any Category")] AnyCategory,
        [Description("Community & Culture")] Community,
        [Description("Food & Drink")] FoodDrink,
        [Description("Business & Professional")] BusinessProfessional,
        [Description("Family & Education")] FamilyEducation,
        [Description("Fashion & Beauty")] FashionBeauty,
        [Description("Health & Wellness")] HealthWellness,
        [Description("Home & LifeStyle")] HomeLifestyle,
        [Description("Science & Technology")] ScienceTechnology,
        [Description("Music & Entertainment")] MusicEntertainment,
        [Description("Other")] Other,
    }
         




         



    
    public class EventCategory //Various categories
    {
        public int Id { get; set;}
        public string Event_Category { get; set; }
    }
}
