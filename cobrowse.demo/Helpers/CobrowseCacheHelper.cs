using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cobrowse.demo.Helpers
{
    public static class CobrowseCacheHelper
    {
        private static object _cacheLock = new object();
        private const string CACHE_KEY = "COBROWSE_KEY";

        public static void EnableCobrowse(string userName)
        {
            lock (_cacheLock)
            {
                var list = new List<string>();
                if (CacheHelper.Check(CACHE_KEY))
                {
                    list = CacheHelper.Get<List<string>>(CACHE_KEY);    
                }

                if (!list.Contains(userName.ToLower()))
                {
                    list.Add(userName.ToLower());
                    CacheHelper.Set<List<string>>(list, CACHE_KEY);
                }

            }
        }
        public static void DisableCobrowse(string userName)
        {
            lock (_cacheLock)
            {
                if (CacheHelper.Check(CACHE_KEY))
                {
                    var list = CacheHelper.Get<List<string>>(CACHE_KEY);
                    if (list.Contains(userName.ToLower()))
                    {
                        list.Remove(userName);
                    }

                    CacheHelper.Set<List<string>>(list, CACHE_KEY);
                }
            }
        }

        public static bool CheckCobrowse(string userName)
        {
            try
            {
                var result = CacheHelper.Check(CACHE_KEY) && CacheHelper.Get<List<string>>(CACHE_KEY).Contains(userName.ToLower());
                return result;
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }
    }
}