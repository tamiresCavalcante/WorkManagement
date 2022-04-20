using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagement.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Login com CNPJ é obrigatório")]
        [StringLength(11, ErrorMessage = "O login não pode exceder 11 caracteres numéricos")]
        [DataType(DataType.Text)]
        public string LoginCNPJ { get; set; }


        [Required(ErrorMessage = "Email é obrigatório")]
        [StringLength(12, ErrorMessage = "A senha deve ter no mínimo 8 e no máximo 12 caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string NomeOficina { get; set; } 
    }
}
