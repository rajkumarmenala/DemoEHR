using System.Collections.Generic;
using Monad.EHR.Common.StateManagement;

namespace Monad.EHR.Infrastructure.StateManagement.Cache
{
    public abstract class CacheProviderBase<TCache> : ICacheProvider
    {
        public CacheProviderBase()
            : this(60) // 60 is default duration
        {
        }
        
        public CacheProviderBase(int durationInSeconds)
        {
            CacheDuration = durationInSeconds;
            Cache = InitCache();
        }

        protected TCache Cache { get; set; }

        public int CacheDuration { get; set; }

        #region Abstract Methods

        protected abstract TCache InitCache();

        
        public abstract T Get<T>(string key);

        
        public abstract void Set<T>(string key, T value);

        
        public abstract void Set<T>(string key, T value, int duration);

        
        public abstract void Remove(string key);

        public abstract void Clear();

        
        public abstract bool Contains(string key);

        public abstract IEnumerable<KeyValuePair<string, object>> GetAll();

        #endregion Abstract Methods
    }
}