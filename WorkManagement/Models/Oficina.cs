using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.ViewModels;

namespace WorkManagement.Models
{
    public class Oficina
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargaDeTrabalho { get; set; }
        public DateTime Data { get; set; }
        public UnidadeDeTrabalho UnidadeDeTrabalho { get; set; }

        public int CargaDisponivel { get; set; }
        


    }
}
