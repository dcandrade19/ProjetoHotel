using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Data.Reservas.Model
{
    public partial class Logradouros
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumCep { get; set; }

        [ForeignKey("Bairros")]
        public int BairroId { get; set; }

        [Required]
        [StringLength(45)]
        public string DescLogradouro { get; set; }

        [Required]
        [StringLength(10)]
        public string DescTipo { get; set; }

        public virtual Bairros Bairros { get; set; }
    }
}
