using Homies.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Homies.Data.Configuration
{
    public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
    {
        public void Configure(EntityTypeBuilder<EventParticipant> builder)
        {
            builder
                .HasKey(ep => new
                {
                    ep.EventId,
                    ep.HelperId
                });

            builder
                .HasOne(e => e.Event)
                .WithMany(e => e.EventsParticipants)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
