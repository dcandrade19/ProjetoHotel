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
    public class TuristaController : Controller
    {
        static TuristaService turistaService = new TuristaService();
        // GET: Turista
        public ActionResult Index(int? pagina)
        {
            var model = turistaService.ListarTuristas();
            int paginaTamanho = 6;
            int paginaNumero = (pagina ?? 1);

            return View(model.ToPagedList(paginaNumero, paginaTamanho));
        }

        // GET: Turista/Details/5
        [ActionName("Detalhes")]
        public ActionResult Details(int id)
        {
            var model = turistaService.ObterTurista(id);
            ViewBag.Reservas = turistaService.ObterReservas(id);
            return View(model);
        }

        // GET: Turista/Novo
        [ActionName("CriarNovo")]
        public ActionResult Create()
        {
            var model = new Turista();
            return View(model);
        }

        // POST: Turista/Novo
        [HttpPost]
        [ActionName("CriarNovo")]
        public ActionResult Create(Turista turista)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var id = turistaService.IncluirTurista(turista);
                    TempData["resultado"] = "Salvo";
                    TempData["id"] = id;
                    TempData["nome"] = turista.Nome;
                    TempData["controller"] = "Turista";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(turista);
                }
            }
            catch
            {
                TempData["resultado"] = "Erro";
                return View(turista);
            }
        }

        // GET: Turista/Edit/5
        [ActionName("Editar")]
        public ActionResult Edit(int id)
        {
            var model = turistaService.ObterTurista(id);
            return View(model);
        }

        // POST: Turista/Edit/5
        [HttpPost]
        [ActionName("Editar")]
        public ActionResult Edit(int id, Turista turista)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    turistaService.AlterarTurista(turista);
                    TempData["resultado"] = "Editado";
                    TempData["id"] = turista.TuristaId;
                    TempData["nome"] = turista.Nome;
                    TempData["controller"] = "Turista";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(turista);
                }
            }
            catch
            {
                TempData["resultado"] = "Erro";
                return View(turista);
            }
        }

        // GET: Turista/Delete/5
        [ActionName("Deletar")]
        public ActionResult Delete(int id)
        {

            var model = turistaService.ObterTurista(id);
            return View(model);
        }

        // POST: Turista/Delete/5
        [HttpPost]
        [ActionName("Deletar")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var turista = turistaService.ObterTurista(id);
            try
            {
                // TODO: Add delete logic here
                turistaService.DeletarTurista(turista);
                TempData["resultado"] = "Deletado";
                TempData["nome"] = turista.Nome;
                TempData["controller"] = "Turista";
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
            Response.AddHeader("content-disposition", "attachment;filename=" + nome +".xlsx");

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
            var turistas = turistaService.ListarTuristas();
            
            // Cria o arquivo xml
            var workbook = new XLWorkbook();
            // Cria a planilha
            var worksheet = workbook.Worksheets.Add(nome);
            int x = 3;
            // Adicionando os nomes das colunas
            worksheet.Cell("A1").Value = nome + " - Gerado em " + String.Format("{0:dd/MM/yyyy} as {0:HH:mm:ss}", DateTime.Now);
            worksheet.Cell("A2").Value = "#";
            worksheet.Cell("B2").Value = "Nome";
            worksheet.Cell("C2").Value = "Email";
            worksheet.Cell("D2").Value = "Sexo";
            worksheet.Cell("E2").Value = "Data de Nascimento";
            worksheet.Cell("F2").Value = "CPF";
            // Adicionando os valores as celulas vindos da lista de vendas
            foreach (Turista item in turistas)
            {
                worksheet.Cell("A" + x).Value = item.TuristaId;
                worksheet.Cell("B" + x).Value = item.Nome;
                worksheet.Cell("C" + x).Value = item.Email;
                worksheet.Cell("D" + x).Value = item.Sexo == true ? "Masculino" : "Feminino";
                worksheet.Cell("E" + x).Value = item.DataNascimento;
                worksheet.Cell("F" + x).Value = item.Cpf;
                x++;
            }

            // Definindo a "range" da tabela, celula inicial e final
            var rngTable = worksheet.Range("A1:F" + (x));
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
            var rngHeaders = rngTable.Range("A2:F2");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;
            // Defininto e formatando footer da tabela
            var rngFooter = rngTable.Range(string.Format("A{0}:F{0}", x));
            rngFooter.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            rngFooter.Style.Font.Bold = true;
            rngFooter.Style.Fill.BackgroundColor = XLColor.LightGray;
            // Ajusta o tamanho das colunas de acordo com os valores
            worksheet.Columns(1, 6).AdjustToContents();

            return workbook;
        }
    }
}
