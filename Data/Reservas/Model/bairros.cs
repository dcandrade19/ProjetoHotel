using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Data.Reservas.Model
{
   
    public partial class Bairros
    {   
        public Bairros()
        {
            Logradouros = new HashSet<Logradouros>();
        }

        [Key]
        public int BairroId { get; set; }

        [ForeignKey("Cidades")]
        public int CidadeId { get; set; }

        [Required]
        [StringLength(45)]
        public string DescBairro { get; set; }

        public virtual Cidades Cidades { get; set; }

        public virtual ICollection<Logradouros> Logradouros { get; set; }
    }
}
