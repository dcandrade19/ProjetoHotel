using Data.Reservas.Model;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Reservas.DAL
{
    public class HotelDAO
    {

        public List<Hotel> Listar()
        {
            using (var contexto = new ReservasModelDb())
            {
                return contexto.Hoteis.ToList();
            }
        }

        public Hotel Detalhar(int id)
        {
            using (var contexto = new ReservasModelDb())
            {
                return contexto.Hoteis.Find(id);
            }
        }

        public int Incluir(Hotel hotel)
        {
            using (var contexto = new ReservasModelDb())
            {
                contexto.Hoteis.Add(hotel);
                contexto.SaveChanges();
                return hotel.HotelId;
            }
        }

        public void Alterar(Hotel hotel)
        {
            using (var contexto = new ReservasModelDb())
            {
                contexto.Entry(hotel).State = System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();
            }
        }

        public void Deletar(Hotel hotel)
        {
            using (var contexto = new ReservasModelDb())
            {
                contexto.Entry(hotel).State = System.Data.Entity.EntityState.Deleted;
                contexto.SaveChanges();
            }
        }
    }
}
