using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Reservas.Model
{
    [Table("Reservas")]
    public partial class Reserva
    {
        public int ReservaId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name ="Data da Reserva")]
        public DateTime DataReserva { get; set; }

        [ForeignKey("Turista")]
        public int TuristaId { get; set; }

        public virtual Turista Turista { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(TypeName = "date")]
        public DateTime Chegada { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(TypeName = "date")]
        public DateTime Partida { get; set; }

        [Display(Name = "Diaria")]
        [Column(TypeName = "numeric")]
        public decimal? ValorDiaria { get; set; }

        [ForeignKey("Quarto")]
        public int QuartoId { get; set; }

        public virtual Quarto Quarto { get; set; }
    }
}
