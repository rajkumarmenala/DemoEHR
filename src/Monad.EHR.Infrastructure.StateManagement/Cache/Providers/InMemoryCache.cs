using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace Monad.EHR.Infrastructure.StateManagement.Cache.Providers
{
    public class InMemoryCache : CacheProviderBase<IMemoryCache>
    {
        public InMemoryCache():base(1200) // default cache duration is 20 minutes
        {
        }
        protected override IMemoryCache InitCache()
        {
            return (new MemoryCache(new MemoryCacheOptions()));
        }

        public override void Clear()
        {
            var keyList = this.GetAll().Select(x => x.Key).ToList();
            foreach (var key in keyList) 
            {
                Cache.Remove(key);
            }
        }

        public override bool Contains(string key)
        {
            // assuming we dont store null for a  given key
            return (Cache.Get(key)!=null);
        }

        public override T Get<T>(string key)
        {
            T result;
            Cache.TryGetValue<T>(key, out result);
            return ((result != null) ? result : default(T));
        }

        public override IEnumerable<KeyValuePair<string, object>> GetAll()
        {
            throw new NotImplementedException();
            //foreach (DictionaryEntry item in Cache)
            //{
            //    yield return new KeyValuePair<string, object>(Convert.ToString(item.Key), item.Value);
            //}
        }

        public override void Remove(string key)
        {
            Cache.Remove(key);
        }

        public override void Set<T>(string key, T value)
        {
            this.Set(key, value, CacheDuration);
        }

        public override void Set<T>(string key, T value, int duration)
        {
            Cache.Set(
                  key,
                  value,
                  new MemoryCacheEntryOptions()
                  .SetSlidingExpiration(TimeSpan.FromSeconds(duration))
                  .SetAbsoluteExpiration(DateTimeOffset.UtcNow.AddSeconds(duration)));
        }

    }
}
