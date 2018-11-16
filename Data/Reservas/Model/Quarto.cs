using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Reservas.Model
{
    [Table("Quartos")]
    public partial class Quarto
    {
        public Quarto()
        {
            Reservas = new HashSet<Reserva>();
        }

        [Key]
        public int QuartoId { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }

        [StringLength(250)]
        public string Descricao { get; set; }

        public int Quantidade { get; set; }

        public int Disponiveis { get; set; }

        [Display(Name = "Maximo de Ocupantes")]
        public int MaximoOcupantes { get; set; }

        [Required]
        [Display(Name = "Diaria")]
        [Column(TypeName = "numeric")]
        public decimal? ValorDiaria { get; set; }

        [Required]
        [Display(Name = "Diaria Criança")]
        [Column(TypeName = "numeric")]
        public decimal? ValorDiariaCrianca { get; set; }

        [Required]
        [Display(Name = "Diaria por Ocupante")]
        public bool? DiariaPorOcupante { get; set; }

        public virtual Hotel Hotel { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
