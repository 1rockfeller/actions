using Stats.API.Models;
using System.Diagnostics;

namespace Stats.API.Data
{
    public class DbInitializer
    {
        public static void Initialize(StatContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Platforms.Any())
            {
                return;   // DB has been seeded
            }

            var platforms = new Platform[]
            {
            new Platform{Name="Youtube",LastModifiedDt = DateTime.Now},
            new Platform{Name="Facebook",LastModifiedDt = DateTime.Now}
            };
            foreach (Platform s in platforms)
            {
                context.Platforms.Add(s);
            }
            context.SaveChanges();           
        }
    }
}
