using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Homies.Data.Common.ValidationConstants;

namespace Homies.Data.Models
{
    public class Event
    {
        public Event()
        {
            EventsParticipants = new List<EventParticipant>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(EventDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string OrganaiserId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(OrganaiserId))]
        public IdentityUser Organaiser { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        [ForeignKey(nameof(TypeId))]
        public Data.Models.Type Type { get; set; } = null!;

        public IList<EventParticipant> EventsParticipants { get; set; }
    }
} 
