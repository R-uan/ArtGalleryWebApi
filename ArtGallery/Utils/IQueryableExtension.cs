using ArtGallery.Utils;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery;

public static class IQueryableExtension {
	public static async Task<PaginatedResponse<T>> Paginate<T>(this IQueryable<T> query, int page, int pageSize = 15) {
		if (page < 1) page = 1;
		var paginate = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
		int total_pages = await query.CountAsync();
		var response = new PaginatedResponse<T>(paginate, page, total_pages);
		return response;
	}
}
