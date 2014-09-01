using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cobrowse.demo.Helpers
{
    public static class SessionHelper
    {
        public static T Get<T>(string key)
        {
            object value = HttpContext.Current.Session[key];

            return value == null ? default(T) : (T)value;
        }

        public static void Set<T>(T t, string key)
        {
            HttpContext.Current.Session[key] = t;
        }
    }
}