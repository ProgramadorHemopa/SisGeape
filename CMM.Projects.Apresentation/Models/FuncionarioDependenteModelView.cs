
namespace CMM.Projects.Apresentation.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FuncionarioDependenteModelView
    {
        [Key]
        public int FUNDEP_ID { get; set; }


        public int FUN_ID { get; set; }

        [Display(Name = "NOME")]
        [StringLength(300)]
        [Required(ErrorMessage = "Informe o NOME", AllowEmptyStrings = false)]
        public string FUNDEP_NOME { get; set; }

        [Display(Name = "TIPO")]
        [Required(ErrorMessage = "Informe o TIPO de Dependente")]
        public int FUNDEP_TIPO { get; set; }

        [Display(Name = "CPF")]
        [StringLength(45)]
        [Required(ErrorMessage = " Informe a CPF")]
        public string FUNDEP_CPF { get; set; }


        [Display(Name = "DATA NASCIMENTO")]
        [Required(ErrorMessage = "Informe a DATA NASCIMENTO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? FUNDEP_DATANASCIMENTO { get; set; }


        [ScaffoldColumn(false)]
        public int? FUNDEP_REGUSER { get; set; }

        public string _TIPODEPENDENTE
        {
            get
            {
                switch (FUNDEP_TIPO)
                {
                    case 1: return "Pai";
                    case 2: return "Mãe";
                    case 3: return "Filho(a)";
                    case 4: return "Avô(ó)";
                    case 5: return "Enteado(a)";
                    default:
                        return "";
                }

            }
        }

    }
}