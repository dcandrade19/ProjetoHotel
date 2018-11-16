using Data.Reservas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reservas.DAL
{
    public class CepDAO
    {
        public Logradouros ObterEndereco(int cep)
        {
            using (var contexto = new ReservasModelDb())
            {
                return contexto.Logradouros
                    .Include(l => l.Bairros.Cidades.Ufs)
                    .Where(l => l.NumCep == cep).FirstOrDefault();
            }
        }
        /// <summary>
        /// logradouro deve conter referência para bairro
        /// bairro deve ter referência para cidade
        /// cidade deverá ter a flag_estado
        /// </summary>
        /// <param name="logradouro"></param>
        public void IncluirLogradouroComDependencias(Logradouros logradouro)
        {
            using (var contexto = new ReservasModelDb())
            {
                Bairros bairro = logradouro.Bairros;
                Cidades cidade = bairro.Cidades;
                var uf = cidade.FlgEstado;

                Ufs u = contexto.Ufs.Where(x => x.UfId == uf).FirstOrDefault();
                if (u == null)
                {
                    var newUf = new Ufs
                    {
                        UfId = uf,
                        DescUf = "Estado"
                    };
                    contexto.Ufs.Add(newUf);
                    contexto.SaveChanges();
                }
                // verifica se a c
                Cidades c = contexto.Cidades.Where(cid => cid.DescCidade == cidade.DescCidade).FirstOrDefault();
                if (c == null)
                {
                    // se não encontrou a cidade na base de dados, então insere
                    contexto.Cidades.Add(cidade);
                    contexto.SaveChanges();
                    bairro.BairroId = 0; // insere também o bairro
                }
                else
                {
                    cidade = c;
                    // se achou a cidade, vamos procurar o bairro
                    Bairros b = contexto.Bairros.Where(bai => bai.DescBairro == bairro.DescBairro).FirstOrDefault();
                    if (b != null)
                    {
                        bairro = b;
                    }
                    else
                    {
                        // se não encontrou vamos inserir
                        bairro.BairroId = 0;
                    }
                }
                if (bairro.BairroId == 0)
                {
                    bairro.Cidades = null;
                    bairro.Logradouros = null;
                    bairro.CidadeId = cidade.CidadeId;
                    contexto.Bairros.Add(bairro);
                    contexto.SaveChanges();
                }
                logradouro.Bairros = null;
                logradouro.BairroId = bairro.BairroId;
                contexto.Logradouros.Add(logradouro);
                contexto.SaveChanges();
            }
        }
    }
}
