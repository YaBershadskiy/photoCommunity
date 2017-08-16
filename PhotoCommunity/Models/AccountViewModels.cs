using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PhotoCommunity.Models
{   
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resources.Resource))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]        
        [DataType(DataType.Text)]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Surname", ResourceType = typeof(Resources.Resource))]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Avatar", ResourceType = typeof(Resources.Resource))]
        public HttpPostedFileBase Avatar { get; set; }

        [Required]
        [Phone/*(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorPhoneFormat")*/]
        [Display(Name = "Tel", ResourceType = typeof(Resources.Resource))]
        public string Tel { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordSequrity", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfrimation", ResourceType = typeof(Resources.Resource))]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordsNotEqual")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "About", ResourceType = typeof(Resources.Resource))]
        public string About { get; set; }
    }

    public class AdminUsersListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] Avatar { get; set; }
        public string About { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
    }   

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }
    }
}
