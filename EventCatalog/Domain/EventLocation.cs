using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

enum Location
{
    [Description("Online")]Online,
    [Description("In Person")]InPerson,
    //[Description("Online or InPerson")]OnlineorInPerson,
}

namespace EventCatalog.Domain
{
    public class EventLocation //online or venue 
    {
        public int Id { get; set; }
    
        public string Event_Location { get; set; }

    }
}
