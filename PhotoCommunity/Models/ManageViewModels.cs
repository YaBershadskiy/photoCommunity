using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace PhotoCommunity.Models
{
    public class EditProfileViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Surname", ResourceType = typeof(Resources.Resource))]
        public string Surname { get; set; }

        [Display(Name = "Avatar", ResourceType = typeof(Resources.Resource))]
        public byte[] Avatar { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Tel", ResourceType = typeof(Resources.Resource))]
        public string Tel { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "About", ResourceType = typeof(Resources.Resource))]
        public string About { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(Resources.Resource))]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordSequrity", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Resources.Resource))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "NewPasswordConfrim", ResourceType = typeof(Resources.Resource))]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordsNotEqual")]
        public string ConfirmPassword { get; set; }
    }
}