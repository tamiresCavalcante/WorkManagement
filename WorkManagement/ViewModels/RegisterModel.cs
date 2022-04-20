using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.Models;

namespace WorkManagement.ViewModels
{
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string LoginCNPJ { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirma senha")]
        [Compare("Password", ErrorMessage = "Senhas não conferem")]
        public string ConfirmPassword { get; set; }

        
    }
}
