using System.ComponentModel.DataAnnotations;
namespace Utils
{
    public class CreatePlaylist
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public CreatePlaylist()
        {

        }
    }
}
