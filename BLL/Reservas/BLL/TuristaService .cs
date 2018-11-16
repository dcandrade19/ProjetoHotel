using DAL.Reservas.DAL;
using Data.Reservas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Reservas.BLL
{
    public class TuristaService
    {
        static TuristaDAO turistaDAO = new TuristaDAO();
        static ReservaDAO reservaDAO = new ReservaDAO();

        public List<Turista> ListarTuristas()
        {
            return turistaDAO.Listar();
        }

        public Turista ObterTurista(int id)
        {

            return turistaDAO.Detalhar(id);

        }

        public int IncluirTurista(Turista turista)
        {

           return turistaDAO.Incluir(turista);

        }

        public void AlterarTurista(Turista turista)
        {
            turistaDAO.Alterar(turista);
        }

        public List<Reserva> ObterReservas(int id)
        {
            return reservaDAO.Listar().Where(q => q.TuristaId == id).ToList();
        }

        public void DeletarTurista(Turista turista)
        {
            turistaDAO.Deletar(turista);
        }

        public Turista ObterTuristaPorCpf(string turistaCpf)
        {
            return turistaDAO.DetalharPorCpf(turistaCpf);
        }
    }
}
