using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CMM.Projects.Apresentation.Models
{
    public class LicencaModelView
    {
        [Key]
        public int LIC_ID { get; set; }

        [Display(Name = "TIPO")]
        public int LICTP_ID { get; set; }


        [Display(Name = "DATA RECEBIMENTO")]
        [Required(ErrorMessage = "Informe DATA RECEBIMENTO")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LIC_DATAENTRADA { get; set; }

        [Display(Name = "DATA INICIO")]
        [Required(ErrorMessage = "Informe DATA INICIO")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LIC_DATAINICIO { get; set; }

        [Display(Name = "DATA RETORNO")]
        [Required(ErrorMessage = "Informe DATA RETORNO")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LIC_DATARETORNO { get; set; }

        [Display(Name = "DATA PORTARIA")]
        [Required(ErrorMessage = "Informe DATA PORTARIA")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LIC_DATAPORTARIA { get; set; }

        [Display(Name = "Nº PORTARIA")]
        [Required(ErrorMessage = "Informe Nº PORTARIA")]
        public DateTime? LIC_NUMPORTARIA { get; set; }

        [Display(Name = "VÍNCULO")]
        public int VNC_ID { get; set; }

        [ScaffoldColumn(false)]
        public string VNC_NOME { get; set; }

        [ScaffoldColumn(false)]
        public string LICTP_NOME { get; set; }

        [ScaffoldColumn(false)]
        public int FUN_ID { get; set; }

        [Display(Name = "Anexo")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ANEXO { get; set; }

        [ScaffoldColumn(false)]
        public int? LIC_REGUSER { get; set; }

    }

    public class LicencaTipoModelView
    {
        [Key]
        public int LICTP_ID { get; set; }

        [Display(Name = "NOME")]
        [StringLength(200)]
        [Required(ErrorMessage = "Informe o NOME", AllowEmptyStrings = false)]
        public string LICTP_NOME { get; set; }


        [Display(Name = "DIAS DE AFASTAMENTO")]
        [Required(ErrorMessage = "Informe o AFASTAMENTO")]
        [Range(0, Int32.MaxValue, ErrorMessage = "O valor deve ser positivo")]
        public int LICTP_AFASTAMENTO { get; set; }

        [ScaffoldColumn(false)]
        public int? LICTP_REGUSER { get; set; }


    }
    public class LicencaPericiaModelView
    {

        [Key]
        public int LICPRC_ID { get; set; }

        [Display(Name = "LICENÇA")]
        public int LIC_ID { get; set; }


        [Display(Name = "SITUAÇÃO PERÍCIA")]
        public int STPRC_ID { get; set; }

        [Display(Name = "SENHA")]
        [StringLength(200)]
        [Required(ErrorMessage = "Informe o SENHA", AllowEmptyStrings = false)]
        public string LICPRC_SENHA { get; set; }

        [Display(Name = "DATA PERÍCIA")]
        [Required(ErrorMessage = "Informe DATA PERÍCIA")]
        [DataType(DataType.DateTime), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? LICPRC_DATAHORA { get; set; }

        [Display(Name = "DATA AGENDAMENTO")]
        [Required(ErrorMessage = "Informe DATA AGENDAMENTO")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LICPRC_DATAAGENDAMENTO { get; set; }

        [Display(Name = "DATA RETORNO")]
        [Required(ErrorMessage = "Informe DATA RETORNO")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LICPRC_RETORNO { get; set; }


        [StringLength(200)]
        public string LICPRC_NOMEARQUIVO { get; set; }

        public byte[] LICPRC_ARQUIVO { get; set; }

        [Display(Name = "Anexo")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ANEXOUPLOAD { get; set; }

        [ScaffoldColumn(false)]
        public int? LICPRC_REGUSER { get; set; }



    }
}