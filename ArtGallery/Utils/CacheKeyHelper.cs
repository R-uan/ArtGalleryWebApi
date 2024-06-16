using System.Reflection;
namespace ArtGallery.Utils.Caching
{
	public static class CacheKeyHelper
	{
		public static string GenerateCacheKey(MethodBase method)
		{
			var className = method.DeclaringType!.FullName;
			return $"{className}";
		}

		public static string GenerateCacheKey(MethodBase method, int pageIndex)
		{
			var className = method.DeclaringType!.FullName;
			return $"{className}:{pageIndex}";
		}
	}
}