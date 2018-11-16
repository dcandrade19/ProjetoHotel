using Data.Reservas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Reservas.DAL
{
    public class QuartoDAO
    {

        public List<Quarto> Listar()
        {
            using (var contexto = new ReservasModelDb())
            {
                return contexto.Quartos
                    .Include("Hotel")
                    .ToList();
            }
        }

        public List<Quarto> ListarDisponiveis()
        {
            using (var contexto = new ReservasModelDb())
            {
                return contexto.Quartos
                    .Include("Hotel")
                    .Where(q => q.Disponiveis > 0)
                    .ToList();
            }
        }

        public Quarto Detalhar(int id)
        {
            using (var contexto = new ReservasModelDb())
            {
                return contexto.Quartos
                    .Include("Hotel")
                    .SingleOrDefault(q => q.QuartoId == id);
            }
        }

        public int Incluir(Quarto quarto)
        {
            using (var contexto = new ReservasModelDb())
            {
                contexto.Quartos.Add(quarto);
                contexto.SaveChanges();
                return quarto.QuartoId;
            }
        }

        public void Alterar(Quarto quarto)
        {
            using (var contexto = new ReservasModelDb())
            {
                contexto.Entry(quarto).State = System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();
            }
        }

        public void Deletar(Quarto quarto)
        {
            using (var contexto = new ReservasModelDb())
            {
                contexto.Entry(quarto).State = System.Data.Entity.EntityState.Deleted;
                contexto.SaveChanges();
            }
        }
    }
}
