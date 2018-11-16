using BLL.Reservas.BLL;
using ClosedXML.Excel;
using Data.Reservas.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelWeb.Controllers
{
    public class ReservaController : Controller
    {
        static ReservaService reservaService = new ReservaService();
        static HotelService hotelService = new HotelService();
        static QuartoService quartoService = new QuartoService();
        static TuristaService turistaService = new TuristaService();
        // GET: Reserva
        public ActionResult Index(int? pagina)
        {
            var model = reservaService.ListarReservas();
            int paginaTamanho = 6;
            int paginaNumero = (pagina ?? 1);

            return View(model.ToPagedList(paginaNumero, paginaTamanho));
        }

        // GET: Reserva/Details/5
        [ActionName("Detalhes")]
        public ActionResult Details(int id)
        {
            var model = reservaService.ObterReserva(id);
            return View(model);
        }

        // GET: Reserva/Create
        [ActionName("CriarNovo")]
        public ActionResult Create()
        {
            var hoteis = hotelService.ListarHoteis();

            var model = new Reserva();
            ViewBag.Hoteis = hoteis;
            return View(model);
        }

        // POST: Reserva/Create
        [HttpPost]
        [ActionName("CriarNovo")]
        public ActionResult Create(Reserva reserva)
        {
            var hoteis = hotelService.ListarHoteis();
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var id = reservaService.IncluirReserva(reserva);
                    TempData["resultado"] = "Salvo";
                    TempData["id"] = id;
                    TempData["nome"] = id;
                    TempData["controller"] = "Reserva";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Hoteis = hoteis;
                    return View(reserva);
                }
            }
            catch
            {
                TempData["resultado"] = "Erro";
                ViewBag.Hoteis = hoteis;
                return View(reserva);
            }
        }

        // GET: Reserva/Edit/5
        [ActionName("Editar")]
        public ActionResult Edit(int id)
        {
            var hoteis = hotelService.ListarHoteis();
            var model = reservaService.ObterReserva(id);

            ViewBag.Hoteis = hoteis;
            return View(model);
        }

        // POST: Reserva/Edit/5
        [HttpPost]
        [ActionName("Editar")]
        public ActionResult Edit(int id, Reserva reserva)
        {
            var hoteis = hotelService.ListarHoteis();
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    reservaService.AlterarReserva(reserva);
                    TempData["resultado"] = "Editado";
                    TempData["id"] = reserva.ReservaId;
                    TempData["nome"] = reserva.ReservaId;
                    TempData["controller"] = "Reserva";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Hoteis = hoteis;
                    return View(reserva);
                }
            }
            catch
            {
                TempData["resultado"] = "Erro";

                ViewBag.Hoteis = hoteis;
                return View(reserva);
            }
        }

        // GET: Reserva/Delete/5
        [ActionName("Deletar")]
        public ActionResult Delete(int id)
        {
            var model = reservaService.ObterReserva(id);
            return View(model);
        }

        // POST: Reserva/Delete/5
        [HttpPost]
        [ActionName("Deletar")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var reserva = reservaService.ObterReserva(id);
            try
            {
                // TODO: Add delete logic here
                reservaService.DeletarReserva(reserva);
                TempData["resultado"] = "Deletado";
                TempData["nome"] = reserva.ReservaId;
                TempData["controller"] = "Reserva";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["resultado"] = "Erro";
                return RedirectToAction("Index");
            }
        }
        // Passar esta função para o QuartoService??
        public JsonResult GetOcupantes(int id)
        {
            var quarto = quartoService.ObterQuarto(id);
            return Json(quarto.MaximoOcupantes, JsonRequestBehavior.AllowGet);
        }
        // Passar esta função para o QuartoService??
        public JsonResult GerarDiaria(int id, int qtdOcupantes, bool crianca)
        {
            var diaria = quartoService.GerarDiaria(id, qtdOcupantes, crianca);
            return Json(diaria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportarExcel(string nome = "Relatorio")
        {
            var wb = GerarXml(nome);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + nome + ".xlsx");

            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return View();
        }

        public XLWorkbook GerarXml(string nome)
        {
            var reservas = reservaService.ListarReservas();

            // Cria o arquivo xml
            var workbook = new XLWorkbook();
            // Cria a planilha
            var worksheet = workbook.Worksheets.Add(nome);
            int x = 3;
            // Adicionando os nomes das colunas
            worksheet.Cell("A1").Value = nome + " - Gerado em " + String.Format("{0:dd/MM/yyyy} as {0:HH:mm:ss}", DateTime.Now);
            worksheet.Cell("A2").Value = "#";
            worksheet.Cell("B2").Value = "Data da Reserva";
            worksheet.Cell("C2").Value = "Turista";
            worksheet.Cell("D2").Value = "Chegada";
            worksheet.Cell("E2").Value = "Partida";
            worksheet.Cell("F2").Value = "Quarto";
            worksheet.Cell("G2").Value = "Diaria";
            // Adicionando os valores as celulas vindos da lista de vendas
            foreach (Reserva item in reservas)
            {
                worksheet.Cell("A" + x).Value = item.ReservaId;
                worksheet.Cell("B" + x).Value = item.DataReserva;
                worksheet.Cell("C" + x).Value = item.Turista.Nome;
                worksheet.Cell("D" + x).Value = item.Chegada;
                worksheet.Cell("E" + x).Value = item.Partida;
                worksheet.Cell("F" + x).Value = item.Quarto.Titulo;
                worksheet.Cell("G" + x).Value = item.ValorDiaria;
                x++;
            }

            // Definindo a "range" da tabela, celula inicial e final
            var rngTable = worksheet.Range("A1:G" + (x));
            // Formatando fonts e cores da tabela
            rngTable.Cell(1, 1).Style.Font.Bold = true;
            rngTable.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.Gray;
            rngTable.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            // Mesclando as celulas de titulo
            rngTable.Row(1).Merge();
            // Definindo bordas da tabela
            rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            rngTable.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            // Definindo e formatando o cabeçalho/headers da tabela
            var rngHeaders = rngTable.Range("A2:G2");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;
            // Defininto e formatando footer da tabela
            var rngFooter = rngTable.Range(string.Format("A{0}:G{0}", x));
            rngFooter.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            rngFooter.Style.Font.Bold = true;
            rngFooter.Style.Fill.BackgroundColor = XLColor.LightGray;
            // Ajusta o tamanho das colunas de acordo com os valores
            worksheet.Columns(1, 7).AdjustToContents();

            return workbook;
        }
    }
}
