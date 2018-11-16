using Data.Reservas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Reservas.DAL
{
    public class TuristaDAO
    {

        public List<Turista> Listar()
        {
            using (var contexto = new ReservasModelDb())
            {
                return contexto.Turistas.ToList();
            }
        }

        public Turista Detalhar(int id)
        {
            using (var contexto = new ReservasModelDb())
            {
                return contexto.Turistas.Find(id);
            }
        }

        public int Incluir(Turista turista)
        {
            using (var contexto = new ReservasModelDb())
            {
                contexto.Turistas.Add(turista);
                contexto.SaveChanges();
                return turista.TuristaId;
            }
        }

        public void Alterar(Turista turista)
        {
            using (var contexto = new ReservasModelDb())
            {
                contexto.Entry(turista).State = System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();
            }
        }

        public void Deletar(Turista turista)
        {
            using (var contexto = new ReservasModelDb())
            {
                contexto.Entry(turista).State = System.Data.Entity.EntityState.Deleted;
                contexto.SaveChanges();
            }
        }

        public Turista DetalharPorCpf(string turistaCpf)
        {
            using (var contexto = new ReservasModelDb())
            {
                return contexto.Turistas.Where(t=>t.Cpf == turistaCpf).FirstOrDefault();
            }
        }
    }
}
