using DAL.Reservas.DAL;
using Data.Reservas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Reservas.BLL
{
    public class HotelService
    {
        static HotelDAO hotelDAO = new HotelDAO();
        static QuartoDAO quartoDAO = new QuartoDAO();
        static CepDAO cepDAO = new CepDAO();
        static ViaCepDAO viaCepDAO = new ViaCepDAO();

        public List<Hotel> ListarHoteis()
        {
            return hotelDAO.Listar();
        }

        public Hotel ObterHotel(int id)
        {

            return hotelDAO.Detalhar(id);

        }

        public int IncluirHotel(Hotel hotel)
        {

           return hotelDAO.Incluir(hotel);

        }

        public void AlterarHotel(Hotel hotel)
        {
            hotelDAO.Alterar(hotel);
        }

        public List<Quarto> ObterQuartos(int id)
        {
            return quartoDAO.Listar().Where(q => q.HotelId == id).ToList();
        }

        public void DeletarHotel(Hotel hotel)
        {
            hotelDAO.Deletar(hotel);
        }

        public List<Quarto> ObterQuartosDisponiveis(int id)
        {
            return quartoDAO.ListarDisponiveis().Where(q => q.HotelId == id).ToList();
        }

        public Logradouros ObterEndereco(int cep)
        {
            Logradouros logradouros = cepDAO.ObterEndereco(cep);
            if (logradouros == null)
            {
                logradouros = viaCepDAO.BuscarEndereco(cep);
                if (logradouros != null)
                {
                    cepDAO.IncluirLogradouroComDependencias(logradouros);
                    return logradouros;
                }
                return null;
            }
            return logradouros;
        }
    }
}
