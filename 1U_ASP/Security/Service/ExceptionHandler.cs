using System;
using System.Collections;
using _1U_ASP.Models.Abstract;
using Newtonsoft.Json;

namespace _1U_ASP.Security.Service
{
    public class ExceptionHandler
    {
        public static Exception MyCustomException(Exception ex, object o)
        {
            throw new NotificationException(
                ex.Message,
                ex.InnerException,
                o);
        }

        public static object SerializeForException(IBaseDto dto)
        {
            return JsonConvert.SerializeObject(dto, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }

        public static object SerializeForException(IEnumerable list)
        {
            return JsonConvert.SerializeObject(list, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }
    }
}
