using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Reservas.Model
{
    [Table("Turistas")]
    public partial class Turista
    {
        [Key]
        public int TuristaId { get; set; }

        [Required(ErrorMessage = "Informe o nome do turista", AllowEmptyStrings = false)]
        [StringLength(150)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o email do turista", AllowEmptyStrings = false)]
        [StringLength(150)]
        public string Email { get; set; }

        public bool Sexo { get; set; }

        [Display(Name ="Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(TypeName = "date")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [StringLength(14)]
        public string Cpf { get; set; }

        [StringLength(50)]
        public string Senha { get; set; }
    }
}
