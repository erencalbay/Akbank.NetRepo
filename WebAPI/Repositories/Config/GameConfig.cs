using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Models;

namespace WebAPI.Repositories.Config
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            //seed data
            builder.HasData(
                new Game { Id = 1, Title = "Counter Strike 2", Price = 30 },
                new Game { Id = 2, Title = "Cities Skylines", Price = 45 },
                new Game { Id = 3, Title = "Euro Truck Simulator 2", Price = 60 }
            );
        }
    }
}
