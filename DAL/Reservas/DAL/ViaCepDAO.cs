using Data.Reservas.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Reservas.Util;

namespace DAL.Reservas.DAL
{
    public class ViaCepDAO
    {
        public Logradouros BuscarEndereco(int cep)
        {
            // http://viacep.com.br/ws/04140000/json
            WebApiClient client = new WebApiClient("http://viacep.com.br");
            string s = String.Format("ws/{0:00000000}/json", cep);
            ViaCep viacep = client.GetJson<ViaCep>(s);
            if (viacep != null && viacep.Localidade != null)
            {
                Cidades cidade = new Cidades
                {
                    DescCidade = RemoveDiacritics(viacep.Localidade).ToUpper(),
                    FlgEstado = viacep.Uf
                };
                Bairros bairro = new Bairros
                {
                    DescBairro = RemoveDiacritics(viacep.Bairro).ToUpper(),
                    Cidades = cidade
                };
                int primeiroEspaco = viacep.Logradouro.IndexOf(' ');
                Logradouros logradouro = new Logradouros
                {
                    DescLogradouro = RemoveDiacritics(viacep.Logradouro.Substring(primeiroEspaco + 1)).ToUpper(),
                    Bairros = bairro,
                    NumCep = cep,
                    DescTipo = RemoveDiacritics(viacep.Logradouro.Substring(0, primeiroEspaco)).ToUpper()
                };
                return logradouro;
            }
            return null;
        }
        /// <summary>
        /// Rotina que remove acentos do texto
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
    public class ViaCep
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Unidade { get; set; }
        public string Ibge { get; set; }
        public string Gia { get; set; }
    }
}
