using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;
using WebMvc.ViewModels;

namespace WebMvc.Infrastructure
{
    public class ApiPaths
    {
        public static class Basket
        {
            public static string GetBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }

            public static string UpdateBasket(string baseUri)
            {
                return baseUri;
            }

            public static string CleanBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }
        }


        public static class Order
        {
            public static string GetOrder(string baseUri, string orderId)
            {
                return $"{baseUri}/{orderId}";
            }

            public static string GetOrdersByUser(string baseUri, string userName)
            {
                return $"{baseUri}/userOrders?userName={userName}";
            }
            public static string GetOrders(string baseUri)
            {
                return baseUri;
            }
            public static string AddNewOrder(string baseUri)
            {
                return $"{baseUri}/new";
            }
        }
        public static class EventCatalog
        {
            public static string GetallEventFormats(string baseUri)
            {
                return $"{baseUri}EventFormats";
            }

            public static string GetallEventAudiences(string baseUri)
            {
                return $"{baseUri}EventAudiences";
            }

            public static string GetallEventKinds(string baseUri)
            {
                return $"{baseUri}EventKinds";
            }

            public static string GetAllEventLanguages(string baseUri)
            {
                return $"{baseUri}EventLanguages";
            }

            public static string GetAllEventLocations(string baseUri)
            {
                return $"{baseUri}EventLocations";
            }

            public static string GetAllEventZipcodes(string baseUri)
            {
                return $"{baseUri}EventZipcodes";
            }

            public static string GetAllEventCategories(string baseUri)
            {
                return $"{baseUri}EventCategories";
            }

            public static string GetAllEventItems(string baseUri,int? category,int? audience,int? format,int? kind,int? language,int? location,int? ZipCode,int page,int take)
            {
                var filterQs = string.Empty;
                if (category.HasValue || audience.HasValue||format.HasValue||kind.HasValue||language.HasValue||location.HasValue||ZipCode.HasValue)
                {
                    var catQs = (category.HasValue) ? category.Value.ToString() : " ";
                    var audQs = (audience.HasValue) ? audience.Value.ToString() : " ";
                    var forQs = (format.HasValue) ? format.Value.ToString() : " ";
                    var kinQs = (kind.HasValue) ? kind.Value.ToString() : " ";
                    var lanQs = (language.HasValue) ? language.Value.ToString() : " ";
                    var locQs = (location.HasValue) ? location.Value.ToString() : " ";
                    var ZipQs = (ZipCode.HasValue) ? ZipCode.Value.ToString() : " ";
                    filterQs = $"/category/{catQs}/audience/{audQs}/format/{forQs}/kind/{kinQs}/language/{lanQs}/location/{locQs}/zipcode/{ZipQs}";
                }
                return $"{baseUri}Items{filterQs}?pageIndex={page}&pageSize={take}";

                
            }

            public static string PostNewEventItem(string baseUri)
            {
                return $"{baseUri}EventDetails";

            }


            public static string GetSearchedEvents(string baseUri, int page, int take, string searchstring)
            {
                var filterQs = string.Empty;
                if (searchstring != null)
                {
                    var searchQs = (searchstring != null) ? searchstring : " ";
                    filterQs = $"/searchstring/{searchstring}";

                }

                return $"{baseUri}Items{filterQs}?pageIndex={page}&pageSize={take}";
            }


           





        }
    }
}
