using BLL.Reservas.BLL;
using Data.Reservas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelWeb.Controllers
{
    public class HotelApiController : ApiController
    {
        HotelService hotelService = new HotelService();
        TuristaService turistaService = new TuristaService();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        public IEnumerable<object> ObterQuartos(int hotelId)
        {
            var quartos = hotelService.ObterQuartosDisponiveis(hotelId).Select(q => new { q.QuartoId, q.Titulo });

            return quartos;
        }

        [HttpGet]
        public Turista BuscarTurista(string turistaCpf)
        {
            var turista = turistaService.ObterTuristaPorCpf(turistaCpf);

            return turista;
        }

        [HttpGet]
        public Endereco ObterEndereco(string cep)
        {
            int c = int.Parse(cep.Replace("-", ""));
            Logradouros dados = hotelService.ObterEndereco(c);
            if(dados != null)
            {
                Endereco end = new Endereco
                {
                    Logradouro = dados.DescLogradouro,
                    Cidade = dados.Bairros.Cidades.DescCidade,
                    Uf = dados.Bairros.Cidades.FlgEstado,
                    Bairro = dados.Bairros.DescBairro
                };
                return end;
            } else
            {
                return null;
            }  
        }
    }
    public class Endereco
    {
        public string Logradouro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Bairro { get; set; }
    }
}