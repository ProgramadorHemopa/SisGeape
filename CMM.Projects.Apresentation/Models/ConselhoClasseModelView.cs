using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMM.Projects.Apresentation.Models
{
    public class ConselhoClasseModelView : IValidatableObject
    {
        [Key]
        public int CONCLA_ID { get; set; }

        [Display(Name = "VÍNCULO")]
        [Required(ErrorMessage = "Informe o VÍNCULO")]
        public Nullable<int> VNC_ID { get; set; }

        [Display(Name = "CONSELHO")]
        [Required(ErrorMessage = "Informe o CONSELHO")]
        public Nullable<int> CON_ID { get; set; }

        [Display(Name = "DATA QUITAÇÃO")]
        [Required(ErrorMessage = "Informe a DATA QUITAÇÃO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public string CONCLA_DATAQUITACAO { get; set; }

        [Display(Name = "DATA RECEBIMENTO")]
        [Required(ErrorMessage = "Informe a DATA RECEBIMENTO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public string CONCLA_DATARECEBIMENTO { get; set; }


        [Display(Name = "REF ANO")]
        public string CONCLA_REFANO { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<int> CONCLA_REGUSER { get; set; }


        [ScaffoldColumn(false)]
        public string CONSELHO { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTime.Now.AddYears(-50).Year.CompareTo(Convert.ToInt32(CONCLA_REFANO)) > 0 && DateTime.Now.Year.CompareTo(Convert.ToInt32(CONCLA_REFANO)) < 0)
            {
                yield return new ValidationResult("A Ref. Ano deve ser num intervalo de" + DateTime.Now.AddYears(-50).Year + "até" + DateTime.Now.Year, new[] { "CONCLA_REFANO" });
            }
        }


    }

    public class ConselhoClasseVinculoModelView
    {
        public byte[] FUN_FOTO { get; set; }

        public int FUN_ID { get; set; }
        public int CRG_ID { get; set; }

        public int VNC_ID { get; set; }

        [Display(Name = "MATRICULA")]
        public string MATRICULA { get; set; }

        [Display(Name = "NOME")]
        public string FUN_NOME { get; set; }

        [Display(Name = "NOME")]
        public string NOME { get; set; }

        [Display(Name = "CARGO")]
        public string CARGO { get; set; }

        [Display(Name = "UNIDADE")]
        public string UNIDADE { get; set; }

        [Display(Name = "TIPO DE VÍNCULO")]
        public string TIPO { get; set; }

        [Display(Name = "SITUAÇÃO")]
        public string SITUACAO { get; set; }

        public virtual ICollection<ConselhoClasseModelView> ConselhoClasse { get; set; }
    }


    public class PesquisaConselhoClasseModelView
    {

        [Key]
        [Display(Name = "MATRICULA")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Somente números")]
        public string MATRICULA { get; set; }
        [Display(Name = "NOME")]
        [StringLength(300)]
        public string NOME { get; set; }

        [Display(Name = "CARGO")]
        public int? IDCARGO { get; set; }

        [Display(Name = "UNIDADE")]
        public int? IDUNIDADE { get; set; }

        [ScaffoldColumn(false)]
        public string UNIDADE { get; set; }

        [ScaffoldColumn(false)]
        public string CARGO { get; set; }

        [Display(Name = "SITUAÇÃO CONSELHO")]
        public int? SITUACAO { get; set; }


        [Display(Name = "ANO")]
        public string ANO { get; set; }


    }
    public class PesquisaConselhoClasseListModelView
    {

        [Key]
        [Display(Name = "MATRICULA")]
        public string MATRICULA { get; set; }


        [Display(Name = "NOME")]
        [StringLength(300)]
        public string NOME { get; set; }

        [Display(Name = "CARGO")]
        public string CARGO { get; set; }

        [Display(Name = "UNIDADE")]
        public string UNIDADE { get; set; }

        [Display(Name = "VÍNCULO")]
        public int? VNC_ID { get; set; }

        [Display(Name = "SITUAÇÃO")]
        public int? SITUACAO { get; set; }


        public string GetSituacao
        {
            get
            {
                if (SITUACAO == 1)
                {
                    return "QUITE";
                }
                else if (SITUACAO == 0)
                {
                    return "PENDENTE";
                }
                else
                {
                    return "";
                }

            }
        }
    }
}