namespace CMM.Projects.Apresentation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    public class UnidadeModelView : IValidatableObject
    {
        [Key]
        [Display(Name = "UNIDADE")]
        public int UND_ID { get; set; }

        [Display(Name = "CÓDIGO")]
        [StringLength(45)]
        // [Required(ErrorMessage = "Informe o CODIGO da Unidade", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Digite somente numeros")]
        public string UND_CODIGO { get; set; }

        [Display(Name = "SIGLA")]
        [StringLength(45)]
        [MinLength(2, ErrorMessage = "A sigla deverá conter o mínimo de 2 caracteres."), MaxLength(45)]
        [Required(ErrorMessage = "Informe o CODIGO da Unidade", AllowEmptyStrings = false)]
        [Remote("ExistUnidade", "Unidade", ErrorMessage = "Já existe uma UNIDADE cadastrada com esta SIGLA.", AdditionalFields = "UND_ID")]
        public string UND_SIGLA { get; set; }


        [Display(Name = "RAMAL")]
        [StringLength(45)]
        public string UND_RAMAL { get; set; }


        [Display(Name = "COMPETÊNCIA")]
        [DataType(DataType.MultilineText)]
        public string UND_COMPETENCIA { get; set; }


        [Display(Name = "DATA INICIO")]
        [Required(ErrorMessage = "Informe a DATA INICIO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? UND_DATAINICIO { get; set; }

        [Display(Name = "DATA FIM")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? UND_DATAFIM { get; set; }


        [Display(Name = "NOME")]
        [StringLength(300)]
        [Required(ErrorMessage = "Informe o NOME da Unidade", AllowEmptyStrings = false)]
        public string UND_NOME { get; set; }

        [Display(Name = "NÍVEL")]
        [Required(ErrorMessage = "Informe o Nível Hierárquico da Unidade")]
        public NivelHierarquico? UND_NIVEL { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        public string UND_NIVEL_NOME { get; set; }


        [Display(Name = "UNIDADE RESP.")]
        [Required(ErrorMessage = "Informe o Responsável da Unidade")]
        public int? UND_PAI { get; set; }

        [Display(Name = "UNIDADE RESP.")]
        public string UND_PAI_NOME { get; set; }

        [Display(Name = "DIRETÓRIO")]
        [StringLength(300)]
        public string UND_DIRETORIO { get; set; }

        [ScaffoldColumn(false)]
        public int UND_REGUSER { get; set; }

        //public string Nivel
        //{

        //    get
        //    {
        //        if (UND_NIVEL.ToString() == "1")
        //            return "Presi";
        //        else if (UND_NIVEL.ToString() == "2")
        //            return "Diretoria";
        //        else if (UND_NIVEL.ToString() == "3")
        //            return "Coordenação";
        //        else if (UND_NIVEL.ToString() == "4")
        //            return "Gerência";
        //        else if (UND_NIVEL.ToString() == "5")
        //            return "Assessoria";
        //        else return "";
        //    }

        //}

        public enum NivelHierarquico
        {
            [Display(Name = "Presidência")]
            Presidencia = 1,
            [Display(Name = "Diretoria")]
            Diretoria = 2,
            [Display(Name = "Coordenação")]
            Coordenacao = 3,
            [Display(Name = "Gerencia")]
            Gerencia = 4,
            [Display(Name = "Assessoria")]
            Assessoria = 5,
            [Display(Name = "Hemonúcleo")]
            Hemonucleo = 6,
            [Display(Name = "Hemocentro")]
            Hemocentro = 7

        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (UND_DATAFIM < UND_DATAINICIO)
            {
                yield return new ValidationResult("Data Fim não pode ser menor que Data Inicio", new[] { "VNCU_DATAINICIO" });

            }

            if (UND_DATAINICIO > DateTime.Today)
            {
                yield return new ValidationResult("Data Inicio não pode ser maior que Data Atual", new[] { "VNCU_DATAINICIO" });

            }
        }
    }
}