using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalog.Domain
{

    public enum AgeGroup
    {
        [Description("Kid(2-9)")] Kid,
        [Description("Preteen(10-12)")] Preteen,
        [Description("Teenager(13-19)")] Teenager,
        [Description("YoungAdult(19-25)")] YoungAdult,
        [Description("Adult (25-65)")] Adult,
        [Description("Retiree (66-80)")] Retiree,
        //[Description("All")]All,
    }
    public class EventAudience
    {
        public int Id { get; set; }
        public string Event_AgeGroup{ get; set; }
      
    }
}
