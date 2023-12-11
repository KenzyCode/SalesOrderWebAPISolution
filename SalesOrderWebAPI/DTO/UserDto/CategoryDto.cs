using System.ComponentModel.DataAnnotations;

namespace SalesOrderWebAPI.DTO.UserDto
{
    public class CategoryDto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
