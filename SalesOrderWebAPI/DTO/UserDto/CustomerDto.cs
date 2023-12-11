using System.ComponentModel.DataAnnotations;

namespace SalesOrderWebAPI.DTO.UserDto
{
    public class CustomerDto
    {
        [Key]
        [StringLength(20)]
        public string Code { get; set; } = null!;

        [StringLength(200)]
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public string Address { get; set; }


        [StringLength(50)]
        public string? Phoneno { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        public string? StatusName { get; set; }



    }
}
