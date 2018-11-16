using DAL.Reservas.DAL;
using Data.Reservas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Reservas.BLL
{
    public class ReservaService
    {
        static ReservaDAO reservaDAO = new ReservaDAO();
        static QuartoDAO quartoDAO = new QuartoDAO();

        public List<Reserva> ListarReservas()
        {
            return reservaDAO.Listar();
        }

        public Reserva ObterReserva(int id)
        {

            return reservaDAO.Detalhar(id);

        }

        private void ReservarQuarto(Reserva reserva, string acao)
        {
            if(acao == "alterar")
            {
                var quartoAntigo = ObterReserva(reserva.ReservaId).Quarto;
                var quartoNovo = quartoDAO.Detalhar(reserva.QuartoId);
                if (quartoAntigo.QuartoId != quartoNovo.QuartoId)
                {
                    quartoAntigo.Disponiveis += 1;
                    quartoDAO.Alterar(quartoAntigo);
                    quartoNovo.Disponiveis -= 1;
                    quartoDAO.Alterar(quartoNovo);
                }
            }
            if (acao == "adicionar")
            {
                var quartoModificar = quartoDAO.Detalhar(reserva.QuartoId);
                quartoModificar.Disponiveis -= 1;
                quartoDAO.Alterar(quartoModificar);
            }
            if (acao == "deletar")
            {
                var quartoAntigo = ObterReserva(reserva.ReservaId).Quarto;
                quartoAntigo.Disponiveis += 1;
                quartoDAO.Alterar(quartoAntigo);
            }
        }

        public int IncluirReserva(Reserva reserva)
        {
            ReservarQuarto(reserva,"adicionar");
            reserva.DataReserva = DateTime.Now;
            return reservaDAO.Incluir(reserva);
        }

        public void AlterarReserva(Reserva reserva)
        {
            ReservarQuarto(reserva, "alterar");
            reserva.DataReserva = ObterReserva(reserva.ReservaId).DataReserva;
            reservaDAO.Alterar(reserva);
        }

        public void DeletarReserva(Reserva reserva)
        {
            ReservarQuarto(reserva, "deletar");
            reservaDAO.Deletar(reserva);
        }
    }
}
