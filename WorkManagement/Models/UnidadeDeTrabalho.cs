using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagement.Models
{
    public enum UnidadeDeTrabalho : int
    {
        AlinhamentoDeRodas = 1,
        Lavacao = 2,
        TrocaDeOleo = 3,
        RevisaoBasica = 5,
        RevisaoCompleta = 8
    }
}
