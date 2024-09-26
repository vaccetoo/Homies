using Homies.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homies.Data.Configuration
{
    public class TypeConfiguration : IEntityTypeConfiguration<Data.Models.Type>
    {
        public void Configure(EntityTypeBuilder<Data.Models.Type> builder)
        {
            builder.HasData(SeedType());
        }

        private static IEnumerable<Data.Models.Type> SeedType()
        {
            return new List<Data.Models.Type>()
            {
                new Data.Models.Type()
                {
                    Id = 1,
                    Name = "Animals"
                },
                new Data.Models.Type()
                {
                    Id = 2,
                    Name = "Fun"
                },
                new Data.Models.Type()
                {
                    Id = 3,
                    Name = "Discussion"
                },
                new Data.Models.Type()
                {
                    Id = 4,
                    Name = "Work"
                }
            };
        
        }
    }
}
