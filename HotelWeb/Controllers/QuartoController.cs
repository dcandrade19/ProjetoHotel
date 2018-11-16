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
    public class QuartoController : Controller
    {
        static QuartoService quartoService = new QuartoService();
        static HotelService hotelService = new HotelService();
        // GET: Quarto
        public ActionResult Index(int? pagina)
        {
            var model = quartoService.ListarQuartos();
            int paginaTamanho = 6;
            int paginaNumero = (pagina ?? 1);

            return View(model.ToPagedList(paginaNumero, paginaTamanho));
        }

        // GET: Quarto/Details/5
        [ActionName("Detalhes")]
        public ActionResult Details(int id)
        {
            var model = quartoService.ObterQuarto(id);
            ViewBag.Reservas = quartoService.ObterReservas(id);
            return View(model);
        }

        // GET: Quarto/Create
        [ActionName("CriarNovo")]
        public ActionResult Create(int hotelId = 0)
        {
            var hoteis = hotelService.ListarHoteis();
            if (hotelId > 0)
            {
                var hotel = hoteis.Where(q => q.HotelId == hotelId).FirstOrDefault();
                if (hotel != null)
                {
                    hoteis.Remove(hotel);
                    hoteis.Insert(0, hotel);
                }
            }
            var model = new Quarto();
            ViewBag.Hoteis = hoteis;
            return View(model);
        }

        // POST: Quarto/Create
        [HttpPost]
        [ActionName("CriarNovo")]
        public ActionResult Create(Quarto quarto)
        {
            var hoteis = hotelService.ListarHoteis();
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var id = quartoService.IncluirQuarto(quarto);
                    TempData["resultado"] = "Salvo";
                    TempData["id"] = id;
                    TempData["nome"] = quarto.Titulo;
                    TempData["controller"] = "Quarto";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Hoteis = hoteis;
                    return View(quarto);
                }
            }
            catch
            {
                TempData["resultado"] = "Erro";
                ViewBag.Hoteis = hoteis;
                return View(quarto);
            }
        }

        // GET: Quarto/Edit/5
        [ActionName("Editar")]
        public ActionResult Edit(int id)
        {
            var hoteis = hotelService.ListarHoteis();
            var model = quartoService.ObterQuarto(id);
            var result = hoteis.Find(q => q.HotelId == model.HotelId);
            if (result == null)
            {
                hoteis.Add(model.Hotel);
            }
            ViewBag.Hoteis = hoteis;
            return View(model);
        }

        // POST: Quarto/Edit/5
        [HttpPost]
        [ActionName("Editar")]
        public ActionResult Edit(int id, Quarto quarto)
        {
            var hoteis = hotelService.ListarHoteis();
            quarto.Disponiveis = quartoService.ObterQuarto(quarto.QuartoId).Disponiveis;
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    quartoService.AlterarQuarto(quarto);
                    TempData["resultado"] = "Editado";
                    TempData["id"] = quarto.QuartoId;
                    TempData["nome"] = quarto.Titulo;
                    TempData["controller"] = "Quarto";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Hoteis = hoteis;
                    return View(quarto);
                }
            }
            catch
            {
                TempData["resultado"] = "Erro";
                ViewBag.Hoteis = hoteis;
                return View(quarto);
            }
        }

        // GET: Quarto/Delete/5
        [ActionName("Deletar")]
        public ActionResult Delete(int id)
        {
            var model = quartoService.ObterQuarto(id);
            return View(model);
        }

        // POST: Quarto/Delete/5
        [HttpPost]
        [ActionName("Deletar")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var quarto = quartoService.ObterQuarto(id);
            try
            {
                // TODO: Add delete logic here
                quartoService.DeletarQuarto(quarto);
                TempData["resultado"] = "Deletado";
                TempData["nome"] = quarto.Titulo;
                TempData["controller"] = "Quarto";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["resultado"] = "Erro";
                return RedirectToAction("Index");
            }
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
            var quartos = quartoService.ListarQuartos();

            // Cria o arquivo xml
            var workbook = new XLWorkbook();
            // Cria a planilha
            var worksheet = workbook.Worksheets.Add(nome);
            int x = 3;
            // Adicionando os nomes das colunas
            worksheet.Cell("A1").Value = nome + " - Gerado em " + String.Format("{0:dd/MM/yyyy} as {0:HH:mm:ss}", DateTime.Now);
            worksheet.Cell("A2").Value = "#";
            worksheet.Cell("B2").Value = "Titulo";
            worksheet.Cell("C2").Value = "Quantidade";
            worksheet.Cell("D2").Value = "Disponiveis";
            worksheet.Cell("E2").Value = "Max. Ocupantes";
            worksheet.Cell("F2").Value = "Diaria";
            worksheet.Cell("G2").Value = "Diaria Criança";
            worksheet.Cell("H2").Value = "Descrição";
            worksheet.Cell("I2").Value = "Hotel";
            // Adicionando os valores as celulas vindos da lista de vendas
            foreach (Quarto item in quartos)
            {
                worksheet.Cell("A" + x).Value = item.QuartoId;
                worksheet.Cell("B" + x).Value = item.Titulo;
                worksheet.Cell("C" + x).Value = item.Quantidade;
                worksheet.Cell("D" + x).Value = item.Disponiveis;
                worksheet.Cell("E" + x).Value = item.MaximoOcupantes;
                worksheet.Cell("F" + x).Value = item.ValorDiaria;
                worksheet.Cell("G" + x).Value = item.ValorDiariaCrianca;
                worksheet.Cell("H" + x).Value = item.Descricao;
                worksheet.Cell("I" + x).Value = item.Hotel.Nome;
                x++;
            }

            // Definindo a "range" da tabela, celula inicial e final
            var rngTable = worksheet.Range("A1:I" + (x));
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
            var rngHeaders = rngTable.Range("A2:I2");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;
            // Defininto e formatando footer da tabela
            var rngFooter = rngTable.Range(string.Format("A{0}:I{0}", x));
            rngFooter.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            rngFooter.Style.Font.Bold = true;
            rngFooter.Style.Fill.BackgroundColor = XLColor.LightGray;
            // Ajusta o tamanho das colunas de acordo com os valores
            worksheet.Columns(1, 9).AdjustToContents();

            return workbook;
        }
    }
}
