using DAL.Reservas.DAL;
using Data.Reservas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Reservas.BLL
{
    public class QuartoService
    {
        static QuartoDAO quartoDAO = new QuartoDAO();
        static ReservaDAO reservaDAO = new ReservaDAO();

        public List<Quarto> ListarQuartos()
        {
            return quartoDAO.Listar();
        }

        public List<Quarto> ListarQuartosDisponiveis()
        {
            return quartoDAO.ListarDisponiveis();
        }

        public Quarto ObterQuarto(int id)
        {

            return quartoDAO.Detalhar(id);

        }

        public int IncluirQuarto(Quarto quarto)
        {
            quarto.Disponiveis = quarto.Quantidade;
            return quartoDAO.Incluir(quarto);
        }

        public List<Reserva> ObterReservas(int id)
        {
            return reservaDAO.Listar().Where(q => q.QuartoId == id).ToList();
        }

        public void AlterarQuarto(Quarto quarto)
        {
            var qtdAntiga = quartoDAO.Detalhar(quarto.QuartoId).Quantidade;
            var qtdNova = quarto.Quantidade;
            var diferenca = 0;
            if (qtdAntiga != qtdNova)
            {
                diferenca = qtdNova - qtdAntiga;
            }
            quarto.Disponiveis += diferenca;
            quartoDAO.Alterar(quarto);
        }

        public void DeletarQuarto(Quarto quarto)
        {
            quartoDAO.Deletar(quarto);
        }

        public decimal GerarDiaria(int id, int qtdOcupantes, bool crianca)
        {
            var quarto = quartoDAO.Detalhar(id);
            var diaria = (decimal)quarto.ValorDiaria;
            if (quarto.DiariaPorOcupante == true)
            {
                diaria = qtdOcupantes * diaria;
            }
            if (crianca)
            {
                diaria += (decimal)quarto.ValorDiariaCrianca;
            }

            return diaria;
        }
    }
}
