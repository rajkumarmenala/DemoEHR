using System.Collections.Generic;

namespace Monad.EHR.Common.StateManagement
{
   
    public interface IObjectStore
    {
        T Get<T>(string key);

        void Set<T>(string key, T value);

        void Set<T>(string key, T value, int duration);

        void Remove(string key);

        void Clear();
        
        bool Contains(string key);

        IEnumerable<KeyValuePair<string, object>> GetAll();
    }
}