using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Data.Reservas.Model
{
    public partial class Cidades
    {
        
        public Cidades()
        {
            Bairros = new HashSet<Bairros>();
        }

        [Key]
        public int CidadeId { get; set; }

        [Required]
        [StringLength(60)]
        public string DescCidade { get; set; }

        [Required]
        [StringLength(2)]
        public string FlgEstado { get; set; }

        public virtual ICollection<Bairros> Bairros { get; set; }

        public virtual Ufs Ufs { get; set; }
    }
}
