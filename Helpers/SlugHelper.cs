using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Helpers
{
    public static class SlugHelper
    {
        public static async Task<string> GenerateUniqueSlugAsync<T>(
            string baseName,
            DbSet<T> dbSet) where T : class, ISluggable
        {
            string slug = baseName.ToLower().Replace(" ", "-");
            string uniqueSlug = slug;
            int counter = 1;

            while (await dbSet.AnyAsync(e => e.Slug == uniqueSlug))
            {
                uniqueSlug = $"{slug}-{counter}";
                counter++;
            }

            return uniqueSlug;
        }
    }
}
