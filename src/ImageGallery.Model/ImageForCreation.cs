using System.ComponentModel.DataAnnotations;

namespace ImageGallery.Model
{
    // using a separate DTO for various actions
    // that are different from database entity!!
    public class ImageForCreation
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public byte[] Bytes { get; set; }
    }
}
