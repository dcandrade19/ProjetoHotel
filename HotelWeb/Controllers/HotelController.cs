using BLL.Reservas.BLL;
using ClosedXML.Excel;
using Data.Reservas.Model;
using PagedList;
using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HotelWeb.Controllers
{
    public class HotelController : Controller
    {
        static HotelService hotelService = new HotelService();
        // GET: Hotel
        public ActionResult Index(int? pagina)
        {
            var model = hotelService.ListarHoteis();
            int paginaTamanho = 6;
            int paginaNumero = (pagina ?? 1);

            return View(model.ToPagedList(paginaNumero, paginaTamanho));
        }

        // GET: Hotel/Details/5
        [ActionName("Detalhes")]
        public ActionResult Details(int id)
        {
            var model = hotelService.ObterHotel(id);
            ViewBag.Quartos = hotelService.ObterQuartos(id);
            return View(model);
        }

        // GET: Hotel/Novo
        [ActionName("CriarNovo")]
        public ActionResult Create()
        {
            var model = new Hotel();
            return View(model);
        }

        // POST: Hotel/Novo
        [HttpPost]
        [ActionName("CriarNovo")]
        public ActionResult Create(Hotel hotel)
        {
            try
            {
                // TODO: Add insert logic here
                if(ModelState.IsValid)
                {
                    var id = hotelService.IncluirHotel(hotel);
                    TempData["resultado"] = "Salvo";
                    TempData["id"] = id;
                    TempData["nome"] = hotel.Nome;
                    TempData["controller"] = "Hotel";
                    return RedirectToAction("Index");
                } else
                {
                    return View(hotel);
                }
            }
            catch
            {
                TempData["resultado"] = "Erro";
                return View(hotel);
            }
        }

        // GET: Hotel/Edit/5
        [ActionName("Editar")]
        public ActionResult Edit(int id)
        {
            var model = hotelService.ObterHotel(id);
            return View(model);
        }

        // POST: Hotel/Edit/5
        [HttpPost]
        [ActionName("Editar")]
        public ActionResult Edit(int id, Hotel hotel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    hotelService.AlterarHotel(hotel);
                    TempData["resultado"] = "Editado";
                    TempData["id"] = hotel.HotelId;
                    TempData["nome"] = hotel.Nome;
                    TempData["controller"] = "Hotel";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(hotel);
                }
            }
            catch
            {
                TempData["resultado"] = "Erro";
                return View(hotel);
            }
        }

        // GET: Hotel/Delete/5
        [ActionName("Deletar")]
        public ActionResult Delete(int id)
        {
            var model = hotelService.ObterHotel(id);
            return View(model);
        }

        // POST: Hotel/Delete/5
        [HttpPost]
        [ActionName("Deletar")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var hotel = hotelService.ObterHotel(id);
            try
            {
                // TODO: Add delete logic here
                hotelService.DeletarHotel(hotel);
                TempData["resultado"] = "Deletado";
                TempData["nome"] = hotel.Nome;
                TempData["controller"] = "Hotel";
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
            var hoteis = hotelService.ListarHoteis();

            // Cria o arquivo xml
            var workbook = new XLWorkbook();
            // Cria a planilha
            var worksheet = workbook.Worksheets.Add(nome);
            int x = 3;
            // Adicionando os nomes das colunas
            worksheet.Cell("A1").Value = nome + " - Gerado em " + String.Format("{0:dd/MM/yyyy} as {0:HH:mm:ss}", DateTime.Now);
            worksheet.Cell("A2").Value = "#";
            worksheet.Cell("B2").Value = "Nome";
            worksheet.Cell("C2").Value = "DDD";
            worksheet.Cell("D2").Value = "Telefone";
            worksheet.Cell("E2").Value = "CEP";
            worksheet.Cell("F2").Value = "Endereço";
            worksheet.Cell("G2").Value = "Numero";
            worksheet.Cell("H2").Value = "Complemento";
            worksheet.Cell("I2").Value = "UF";
            worksheet.Cell("J2").Value = "Cidade";
            worksheet.Cell("K2").Value = "Bairro";
            worksheet.Cell("L2").Value = "Descrição";
            // Adicionando os valores as celulas vindos da lista de vendas
            foreach (Hotel item in hoteis)
            {
                worksheet.Cell("A" + x).Value = item.HotelId;
                worksheet.Cell("B" + x).Value = item.Nome;
                worksheet.Cell("C" + x).Value = item.Ddd;
                worksheet.Cell("D" + x).Value = item.Telefone;
                worksheet.Cell("E" + x).Value = item.Cep;
                worksheet.Cell("F" + x).Value = item.Endereco;
                worksheet.Cell("G" + x).Value = item.Numero;
                worksheet.Cell("H" + x).Value = item.Complemento;
                worksheet.Cell("I" + x).Value = item.Uf;
                worksheet.Cell("J" + x).Value = item.Cidade;
                worksheet.Cell("K" + x).Value = item.Bairro;
                worksheet.Cell("L" + x).Value = item.Descricao;
                x++;
            }

            // Definindo a "range" da tabela, celula inicial e final
            var rngTable = worksheet.Range("A1:L" + (x));
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
            var rngHeaders = rngTable.Range("A2:L2");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;
            // Defininto e formatando footer da tabela
            var rngFooter = rngTable.Range(string.Format("A{0}:L{0}", x));
            rngFooter.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            rngFooter.Style.Font.Bold = true;
            rngFooter.Style.Fill.BackgroundColor = XLColor.LightGray;
            // Ajusta o tamanho das colunas de acordo com os valores
            worksheet.Columns(1, 12).AdjustToContents();

            return workbook;
        }
    }
}
