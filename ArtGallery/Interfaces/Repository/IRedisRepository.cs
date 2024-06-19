using System.Collections;
using System.Reflection;

namespace ArtGallery.Interfaces.Repository
{
    public interface IRedisRepository
    {
        Task<bool> Store<T>(string key, T values);
        Task<T?> Get<T>(string key);
        public void ClearThisKeys(MethodBase method);

    }
}
