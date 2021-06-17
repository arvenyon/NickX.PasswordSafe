using System.ComponentModel.DataAnnotations;

namespace NickX.PasswordSafe.WebAPI.Resources
{
    public class SaveCategoryResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
