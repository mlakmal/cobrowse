using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace cobrowse.demo.Helpers
{
    public static class CacheHelper
    {
        public static bool Check(string key)
        {
            return System.Web.HttpContext.Current.Cache[key] != null;
        }

        public static T Get<T>(string key)
        {
            object value = System.Web.HttpContext.Current.Cache[key];

            return value == null ? default(T) : (T)value;
        }

        public static void Set<T>(T t, string key, CacheDependency cacheDependency = null, DateTime? cacheExpireTime = null, TimeSpan? slidingTimeSpan = null, CacheItemRemovedCallback callback = null)
        {
            System.Web.HttpContext.Current.Cache.Insert(key, t, cacheDependency, cacheExpireTime ?? DateTime.MaxValue, slidingTimeSpan ?? Cache.NoSlidingExpiration, CacheItemPriority.Normal, callback);
        }
    }
}