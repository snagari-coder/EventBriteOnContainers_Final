using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

enum Format
{
    //AnyFormat,
    Class,
    Conference,
    Festival,
    Party,
    Appearence,
    Attraction,
    Game,
    Networking,
    Tournament,
    Tour,
  

}
namespace EventCatalog.Domain
{
    public class EventFormat  //Format is like confernece ,cultural ,festival,Tour,Party
    {
        public int Id { get; set; }
        public string Event_Format { get; set; }
    }
}
