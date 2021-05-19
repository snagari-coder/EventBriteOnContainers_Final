using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
enum Kind
{
    //AnyPrice,
    Free,
    Paid
}


namespace EventCatalog.Domain
{
    public class EventKind  //free or paid
    {
       public  int Id { get; set; }

       public  string Event_Kind { get; set; }
    }
}
