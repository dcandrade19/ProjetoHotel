using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Reservas.Model
{
    [Table("Hoteis")]
    public partial class Hotel
    {
        public Hotel()
        {
            Quartos = new HashSet<Quarto>();
        }

        [Key]
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Informe o nome do hotel", AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o endereço do hotel", AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Endereco { get; set; }

        public int Numero { get; set; }

        [Required]
        [StringLength(20)]
        public string Complemento { get; set; }


        [Required]
        [StringLength(9)]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Informe a cidade do hotel", AllowEmptyStrings = false)]
        [StringLength(60)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe o bairro do hotel", AllowEmptyStrings = false)]
        [StringLength(60)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(2)]
        public string Uf { get; set; }

        [Required]
        [StringLength(4)]
        public string Ddd { get; set; }

        [Required]
        [StringLength(9)]
        public string Telefone { get; set; }

        [StringLength(250)]
        public string Descricao { get; set; }

        public virtual ICollection<Quarto> Quartos { get; set; }
    }
}
