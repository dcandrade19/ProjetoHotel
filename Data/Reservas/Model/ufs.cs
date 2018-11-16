using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Data.Reservas.Model
{
    public partial class Ufs
    {
        public Ufs()
        {
            Cidades = new HashSet<Cidades>();
        }

        [Key]
        [StringLength(2)]
        public string UfId { get; set; }

        [Required]
        [StringLength(60)]
        public string DescUf { get; set; }

        public virtual ICollection<Cidades> Cidades { get; set; }
    }
}
