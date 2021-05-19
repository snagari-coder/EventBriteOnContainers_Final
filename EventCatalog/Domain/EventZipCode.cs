using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

enum ZipCode
{
    [Description("Bellevue,WA,98005")] Bellevue_1,
    [Description("Bellevue,WA,98006")] Bellevue_2,
    [Description("Bellevue,WA,98007")] Bellevue_3,
    [Description("Bellevue,WA,98008")] Bellevue_4,
    [Description("Redmond,WA,98052")] Redmond_5,
    [Description("Redmond,WA,98053")] Redmond_6,
    [Description("Sammamish,WA,98072")] Sammamish_7,
    [Description("Sammamish,WA,98073")] Sammamish_8,
    //[Description("Others")]Others,


}

namespace EventCatalog.Domain
{
    public class EventZipCode
    {
       public int Id { get; set; }
       public string Event_Zipcode { get; set; }
    }
}
