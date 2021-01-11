using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ims.api.Filter
{
    public static class SessionExtensions
    {
        public static void SetSession(this ISession session, string key, object value)
        {
            //session.SetSession)le
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetSession<T>(this ISession session, string key)
        {
            var s = session.GetString(key);
            return JsonConvert.DeserializeObject<T>(s);
        }

    }
}
