using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.Context;
using WorkManagement.Models;

namespace WorkManagement.Services
{
    public class OficinaService : IOficinaService
    {
        private readonly AppDbContext _context;

        public OficinaService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Oficina>> GetOficinas()
        {
            return await _context.Oficinas.ToListAsync();
        }

        public async Task<IEnumerable<Oficina>> GetCargaDisponivel()
        {
            return _context.Oficinas.Where(x => x.CargaDisponivel != 0).ToList();
        }

        public async Task<IEnumerable<Oficina>> GetOficinaPorData(DateTime data)
        {
            IEnumerable<Oficina> agendamentos;
            agendamentos = await _context.Oficinas.Where(d => d.Data.Equals(data)).ToListAsync();

            return agendamentos;
        }
        public async Task<Oficina> GetOficinaPorId(int id)
        {
            var oficina = await _context.Oficinas.FindAsync(id);
            return oficina;
        }
        public async Task CreateOficina(Oficina oficina)
        {
            if(oficina.Data.DayOfWeek == DayOfWeek.Thursday || oficina.Data.DayOfWeek == DayOfWeek.Friday)
            {
                var cargaTotalServico = Math.Truncate((oficina.CargaDeTrabalho) * 1.3);
                if(((uint)(oficina.UnidadeDeTrabalho)) <= cargaTotalServico)
                {
                    await UpdateCargaDisponivel(oficina);
                    _context.Oficinas.Add(oficina);
                    await _context.SaveChangesAsync();
                } 
                              
            } else
            {
                if(((uint)(oficina.UnidadeDeTrabalho)) <= oficina.CargaDeTrabalho)
                {
                    await UpdateCargaDisponivel(oficina);
                    _context.Oficinas.Add(oficina);
                    await _context.SaveChangesAsync();
                }
            }
        }

        

        public async Task UpdateOficina(Oficina oficina)
        {
            _context.Entry(oficina).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCargaDisponivel(Oficina oficina)
        {
            var cargaDisponivel = ((int)oficina.CargaDeTrabalho) - ((int)oficina.UnidadeDeTrabalho);
            oficina.CargaDisponivel = cargaDisponivel;
            _context.Entry(oficina).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteOficina(Oficina oficina)
        {
            _context.Oficinas.Remove(oficina);
            await _context.SaveChangesAsync();
        }

        
    }
}
