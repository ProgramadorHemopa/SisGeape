
namespace CMM.Projects.Apresentation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class VinculoModelView : IValidatableObject
    {
        [Key]
        [Display(Name = "VÍNCULO")]
        public int VNC_ID { get; set; }

        [Display(Name = "FUNCIONÁRIO")]
        [Required(ErrorMessage = "Informe FUNCIONÁRIO")]
        public int FUN_ID { get; set; }

        [Display(Name = "TIPO")]
        [Required(ErrorMessage = "Informe TIPO")]
        public int VNCTP_ID { get; set; }

        [Display(Name = "SITUACAO")]
        [Required(ErrorMessage = "Informe SITUACAO")]
        public int VNCST_ID { get; set; }

        [Display(Name = "Nº VINCULO")]
        [Range(0, 999, ErrorMessage = "O valor deverá ser positivo.")]
        [Remote("ValidarNumVinculo", "Vinculo", ErrorMessage = "Servidor já possui {0} cadastrado.", AdditionalFields = "FUN_ID,VNC_ID")]
        public int? VNC_QTD { get; set; }



        [Display(Name = "CARGO")]
        [Required(ErrorMessage = "Informe CARGO")]
        public int CRG_ID { get; set; }

        [Display(Name = "ADMISSAO")]
        [Required(ErrorMessage = "Informe ADMISSAO")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? VNC_ADMISSAO { get; set; }

        [Display(Name = "DEMISSAO")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? VNC_DEMISSAO { get; set; }

        [Display(Name = "CESSÃO")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? VNC_DATACESSAO { get; set; }


        [Display(Name = "ENTRADA APOSENTADORIA")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? VNC_ENTRADA_APOSENT { get; set; }


        [Display(Name = "TIPO DE APOSENTADORIA")]
        public int? VNC_TIPO_APOSENT { get; set; }


        [Display(Name = "CARGA HORÁRIA")]
        [Required(ErrorMessage = "Informe CARGA HORÁRIA")]
        [Range(0, int.MaxValue, ErrorMessage = "Insira um Número válido.")]
        public int? VNC_CARGAHORARIA { get; set; }

        [Display(Name = "ORGÃO DE ORIGEM")]
        [StringLength(100)]
        public string VNC_ORGAO_ORIGEM { get; set; }

        [Display(Name = "ORGÃO DE DESTINO")]
        [StringLength(100)]
        public string VNC_ORGAO_DESTINO { get; set; }

        [Display(Name = "ÔNUS")]
        public string VNC_ONUS { get; set; }

        [Display(Name = "AREA")]
        [Required(ErrorMessage = "Informe AREA")]
        public int VNC_AREA { get; set; }

        [Display(Name = "CONCURSO/PSS")]
        [StringLength(300)]
        public string VNC_CONCURSO { get; set; }

        [Display(Name = "HISTORICO")]
        [StringLength(300)]
        public string VNC_HISTORICO { get; set; }

        [ScaffoldColumn(false)]
        public int? VNC_REGUSER { get; set; }

        [ScaffoldColumn(false)]
        public string VNC_TIPO { get; set; }

        [ScaffoldColumn(false)]
        public string VNC_SITUACAO { get; set; }
        [ScaffoldColumn(false)]
        public string FUN_NOME { get; set; }
        [ScaffoldColumn(false)]
        public string VNC_NOME { get; set; }
        [ScaffoldColumn(false)]
        public string CRG_NOME { get; set; }

        public virtual FuncionarioModelView Funcionario { get; set; }
        public virtual ICollection<VinculoUnidadeModelView> Lotacao { get; set; }

        public enum Situacao
        {
            AguardandoAposentadoria = 1,
            AguardandoExercicio = 2,
            Aposentado = 3,
            Ativo = 4,
            AtivoCedido = 5,
            Cedido = 6,
            Desligado = 7,
            Devolvido = 8,
            Falecimento = 9,
            Licenca = 10,
            NaoAprovado = 11

        }

        public enum Tipo
        {
            Cedido = 1,
            Comissionado = 2,
            ComissionadoCedido = 3,
            Efetivo = 4,
            EfetivoCedido = 5,
            EfetivoComissionado = 6,
            EfetivoComissionadoCedido = 7,
            EstagiarioCurricular = 8,
            EstatutarioEstavel = 9,
            EstatutarioEstavelComissionado = 10,
            EstatutarioNaoEstavel = 11,
            EstatutarioEstavelCedido = 12,
            Temporario = 13,
            TemporarioComissionado = 14,
            Residente = 15

        }

        public string GetVinculoArea
        {
            get
            {
                if (VNC_AREA == 1)
                {
                    return "MEIO";
                }
                else if (VNC_AREA == 2)
                {
                    return "FIM";
                }
                else
                {
                    return "";
                }

            }
        }
        public string ONUS
        {
            get
            {
                if (VNC_ONUS == "D")
                {
                    return "DESTINO";
                }
                else if (VNC_ONUS == "O")
                {
                    return "ORIGEM";
                }
                else
                {
                    return "";
                }

            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (VNC_DEMISSAO < VNC_ADMISSAO)
            {
                yield return new ValidationResult("Data Demissão não pode ser menor que Data de Admissão", new[] { "VNC_DEMISSAO" });
            }

            if (Convert.ToInt32(VNCST_ID) == (int)Situacao.Desligado && VNC_DEMISSAO == null)
            {
                yield return new ValidationResult("Para informar o Desligamento do Servidor, informe a Data de Demissão", new[] { "VNC_DEMISSAO" });

            }
            else if (Convert.ToInt32(VNCST_ID) == (int)Situacao.Ativo && VNC_DEMISSAO != null || Convert.ToInt32(VNCST_ID) == (int)Situacao.AtivoCedido && VNC_DEMISSAO != null)
            {
                yield return new ValidationResult("Para reativar este vínculo, você deverá retirar a DATA DE DEMISSÃO", new[] { "VNC_DEMISSAO" });

            }
            else if (Convert.ToInt32(VNCST_ID) == (int)Situacao.AguardandoAposentadoria && VNC_ENTRADA_APOSENT == null || Convert.ToInt32(VNCST_ID) == (int)Situacao.AguardandoExercicio && VNC_ENTRADA_APOSENT == null || Convert.ToInt32(VNCST_ID) == (int)Situacao.Aposentado && VNC_ENTRADA_APOSENT == null)
            {
                yield return new ValidationResult("Informe a Data de Entrada na Aposentadoria", new[] { "VNC_ENTRADA_APOSENT" });
            }
            else if (Convert.ToInt32(VNCST_ID) == (int)Situacao.AguardandoAposentadoria && VNC_ENTRADA_APOSENT != null && Convert.ToInt32(VNC_TIPO_APOSENT) == 0 || Convert.ToInt32(VNCST_ID) == (int)Situacao.AguardandoExercicio && VNC_ENTRADA_APOSENT != null && Convert.ToInt32(VNC_TIPO_APOSENT) == 0 || Convert.ToInt32(VNCST_ID) == (int)Situacao.Aposentado && VNC_ENTRADA_APOSENT != null && Convert.ToInt32(VNC_TIPO_APOSENT) == 0)
            {
                yield return new ValidationResult("Informe o Tipo de Aposentadoria", new[] { "VNC_TIPO_APOSENT" });

            }
            else if (Convert.ToInt32(VNCST_ID) == (int)Situacao.Cedido && VNC_ORGAO_DESTINO == null)
            {
                yield return new ValidationResult("Informe o ORGÃO DE DESTINO do servidor", new[] { "VNC_ORGAO_DESTINO" });
            }




        }
    }



}