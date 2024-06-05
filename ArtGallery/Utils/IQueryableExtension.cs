using ArtGallery.Utils;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery;

public static class IQueryableExtension {
	public static async Task<PaginatedResponse<T>> Paginate<T>(this IQueryable<T> query, int page, int page_size = 15) {
		if (page < 1) page = 1;
		var paginate = await query.Skip((page - 1) * page_size).Take(page_size).ToListAsync();
		int count = await query.CountAsync();
		int total_pages = (int)Math.Ceiling(count / (double)page_size);
		var response = new PaginatedResponse<T>(paginate, page, total_pages);
		return response;
	}
}
