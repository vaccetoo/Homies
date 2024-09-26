using System.ComponentModel.DataAnnotations;
using static Homies.Data.Common.ValidationConstants;

namespace Homies.Models
{
    public class EventFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(EventNameMaxLength, MinimumLength = EventNameMinLength, ErrorMessage = LengthErrorMessage)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(EventDescriptionMaxLength, MinimumLength = EventDescriptionMinLength, ErrorMessage = LengthErrorMessage)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Start { get; set; } = null!;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string End { get; set; } = null!;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int TypeId { get; set; } 

        [Required]
        public IEnumerable<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();

        public string? OrganaiserId { get; set; }
    }
}
