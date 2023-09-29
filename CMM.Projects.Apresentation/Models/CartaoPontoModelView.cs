using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMM.Projects.Apresentation.Models
{


    public class UnidadeCartaoPontoModelView
    {

        [Key]
        [Display(Name = "UNIDADE")]
        public int UND_ID { get; set; }

        [Display(Name = "NOME")]
        public string UNIDADE { get; set; }

        [ScaffoldColumn(false)]
        public string CaminhoUnidade { get; set; }


        [Display(Name = "PERÍODO")]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy/MM}")]
        public DateTime? PERIODO { get; set; }

        public virtual ICollection<FuncionarioCartaoPontoModelView> Funcionarios { get; set; }
    }

    //Adicionado por Angelo Matos em 27022020
    public class UnidadeCartaoPontoFaltasModelView
    {

        [Key]
        [Display(Name = "UNIDADE")]
        public int UND_ID { get; set; }

        [Display(Name = "NOME")]
        public string UNIDADE { get; set; }

        [ScaffoldColumn(false)]
        public string CaminhoUnidade { get; set; }


        [Display(Name = "PERÍODO INICIO")]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? PERIODO_INICIO { get; set; }
        [Display(Name = "PERÍODO FIM")]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? PERIODO_FIM { get; set; }

        public virtual ICollection<FuncionarioCartaoPontoModelView> Funcionarios { get; set; }
    }


    public class FuncionarioCartaoPontoModelView
    {

        [Key]
        public int FUN_ID { get; set; }

        [Display(Name = "MATRICULA")]
        [StringLength(15)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Somente números")]
        public string MATRICULA { get; set; }

        [Display(Name = "FOTO")]
        public byte[] FUN_FOTO { get; set; }

        [Display(Name = "NOME")]
        [StringLength(300)]
        public string NOME { get; set; }


        [Display(Name = "TIPAGEM")]
        public TipagemSanguinea? TIPAGEM { get; set; }


        public virtual ICollection<CartaoPontoModelView> CartaoPonto { get; set; }

        public enum TipagemSanguinea
        {
            [Display(Name = "Não Informado")]
            NãoInformado = 0,
            [Display(Name = "A-")]
            Anegativo = 1,
            [Display(Name = "A+")]
            Apositivo = 2,
            [Display(Name = "B-")]
            Bnegativo = 3,
            [Display(Name = "B+")]
            Bpositivo = 4,
            [Display(Name = "AB-")]
            ABnegativo = 5,
            [Display(Name = "AB+")]
            ABApositivo = 6,
            [Display(Name = "O-")]
            Onegativo = 7,
            [Display(Name = "O+")]
            Opositivo = 8,

        }
    }

    public class CartaoPontoModelView
    {

        [Key]
        public int VNC_ID { get; set; }

        [ScaffoldColumn(false)]
        public int FUN_ID { get; set; }

        [Display(Name = "DATA")]
        public DateTime DATA { get; set; }


        [Display(Name = "ENTRADA")]
        public TimeSpan? ENTRADA1 { get; set; }

        [Display(Name = "SAÍDA")]
        public TimeSpan? SAIDA1 { get; set; }


        public virtual TimeSpan? Diferenca()
        {
            TimeSpan? dif = new TimeSpan?();
            if (ENTRADA1 != null && SAIDA1 != null)
            {
                dif = SAIDA1.Value.Subtract(ENTRADA1.Value);
            }

            return dif;
        }


        public virtual bool FALTA()
        {
            bool result = false;
            if ((int)DATA.DayOfWeek != 6 && (int)DATA.DayOfWeek != 0)
            {
                if ((ENTRADA1 == null || SAIDA1 == null) && (JUSTIFICATIVA == null && FERIAS == null && FERIADO == null))
                    result = true;
            }

            return result;
        }

        [Display(Name = "JUSTIFICATIVA")]
        public string JUSTIFICATIVA { get; set; }
        public string JUSTIFICATIVAPONTO_OBS { get; set; }
        public int FRS_ID { get; set; }
        public int JUSTP_ID { get; set; }
        public string FERIAS { get; set; }
        public string FERIAS_OBS { get; set; }


        public string FERIADO { get; set; }
        public string FERIADO_OBS { get; set; }

        public virtual string Justificativa()
        {
            string result = "";
            if (JUSTIFICATIVA != null && FERIAS == null && FERIADO == null)
            {
                result = JUSTIFICATIVA;
            }
            else if (JUSTIFICATIVA == null && FERIAS != null && FERIADO == null)
            {
                result = FERIAS;
            }
            else if (JUSTIFICATIVA == null && FERIADO != null && FERIAS == null)
            {
                result = FERIADO;
            }
            else if (JUSTIFICATIVA != null && FERIAS != null && FERIADO != null)
            {
                result = String.Format("{0} - {1} - {2}", FERIAS, FERIADO, JUSTIFICATIVA);
            }
            else
            {
                result = "";
            }

            return result;
        }
    }

    public class PesquisaCartaoPontoModelView
    {

        [Key]
        [Display(Name = "MATRICULA")]
        public string MATRICULA { get; set; }
        [Display(Name = "NOME")]
        public string NOME { get; set; }
        [ScaffoldColumn(false)]
        public int? IDCARGO { get; set; }
        [ScaffoldColumn(false)]
        public int? IDUNIDADE { get; set; }
        [Display(Name = "UNIDADE")]
        public string UNIDADE { get; set; }
        [Display(Name = "CARGO")]
        public string CARGO { get; set; }
        [ScaffoldColumn(false)]
        public int? FUN_ID { get; set; }
        [ScaffoldColumn(false)]
        public byte[] FUN_FOTO { get; set; }

    }

    public class CartaoPontoPlantaoView
    {
        [Display(Name = "NOME")]
        public string NOME_FUNCIONARIO { get; set; }
        [Display(Name = "DATA")]
        public DateTime DATA { get; set; }
        [Display(Name = "ENTRADA")]
        public TimeSpan? ENTRADA { get; set; }
        [Display(Name = "SAÍDA")]
        public TimeSpan? SAIDA { get; set; }
        [Display(Name = "DIFERENÇA")]
        public TimeSpan? DIFERENCA { get; set; }
        [Display(Name = "OBSERVAÇÕES")]
        public string OBSERVACOES { get; set; }
        [Display(Name = "FERIADO")]
        public string FERIADO { get; set; }
    }



}