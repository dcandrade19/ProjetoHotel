using Data.Reservas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Reservas.DAL
{
    public class ReservaDAO
    {

        public List<Reserva> Listar()
        {
            using (var contexto = new ReservasModelDb())
            {
                return contexto.Reservas
                    .Include("Turista")
                    .Include("Quarto")
                    .ToList();
            }
        }

        public Reserva Detalhar(int id)
        {
            using (var contexto = new ReservasModelDb())
            {
                return contexto.Reservas
                    .Include("Turista")
                    .Include("Quarto.Hotel")
                    .SingleOrDefault(r => r.ReservaId == id);
            }
        }

        public int Incluir(Reserva reserva)
        {
            using (var contexto = new ReservasModelDb())
            {
                contexto.Reservas.Add(reserva);
                contexto.SaveChanges();
                return reserva.ReservaId;
            }
        }

        public void Alterar(Reserva reserva)
        {
            using (var contexto = new ReservasModelDb())
            {
                contexto.Entry(reserva).State = System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();
            }
        }

        public void Deletar(Reserva reserva)
        {
            using (var contexto = new ReservasModelDb())
            {
                contexto.Entry(reserva).State = System.Data.Entity.EntityState.Deleted;
                contexto.SaveChanges();
            }
        }
    }
}
