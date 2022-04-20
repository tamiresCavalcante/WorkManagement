using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.Models;

namespace WorkManagement.Services
{
    public interface IOficinaService
    {
        Task<IEnumerable<Oficina>> GetOficinas();
        Task<IEnumerable<Oficina>> GetCargaDisponivel();

        Task<Oficina> GetOficinaPorId(int id);
        Task<IEnumerable<Oficina>> GetOficinaPorData(DateTime data);
        Task CreateOficina(Oficina oficina);

        Task UpdateOficina(Oficina oficina);
        Task UpdateCargaDisponivel(Oficina oficina);
        Task DeleteOficina(Oficina oficina);
        
    }
}
