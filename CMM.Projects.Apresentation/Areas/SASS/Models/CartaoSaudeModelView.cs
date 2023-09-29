using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMM.Projects.Apresentation.Areas.SASS.Models
{

    public class CartaoSaudeModelView
    {

    }

    public class FuncionarioCartaoSaudeModelView
    {
        [Key]
        public int FUN_ID { get; set; }

        [Display(Name = "MATRICULA")]
        [StringLength(15)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Somente números")]
        [Required]
        public string FUN_MATRICULA { get; set; }
        [Display(Name = "DATA ATENDIMENTO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> FUN_DATANASCIMENTO { get; set; }


        [Display(Name = "NOME")]
        [StringLength(300)]
        [Required(ErrorMessage = "Informe o NOME", AllowEmptyStrings = false)]
        public string FUN_NOME { get; set; }

        [Display(Name = "NOME CRACHÁ")]
        [StringLength(300)]
        public string FUN_NOMECRACHA { get; set; }

        [Display(Name = "SEXO")]
        [Required(ErrorMessage = "Informe o SEXO")]
        public string FUN_SEXO { get; set; }


        [Display(Name = "ENDEREÇO")]
        [StringLength(300)]
        //[Required(ErrorMessage = "Informe ENDEREÇO")]
        public string FUN_ENDERECO { get; set; }

        [Display(Name = "TIPAGEM")]
        [Required(ErrorMessage = "Informe a TIPAGEM")]
        public int? FUN_TIPOSANGUINEO { get; set; }


        [Display(Name = "NUMERO")]
        [StringLength(300)]
        public string FUN_NUMEROEND { get; set; }


        [Display(Name = "BAIRRO")]
        [StringLength(300)]
        //[Required(ErrorMessage = "Informe BAIRRO")]
        public string FUN_BAIRRO { get; set; }

        [Display(Name = "COMPLEMENTO")]
        [StringLength(300)]
        public string FUN_COMPLEMENTO { get; set; }



        public virtual List<ConsultaModelView> Consulta { get; set; }

        public string SEXO
        {
            get
            {
                string sexo = "";
                if (FUN_SEXO == "M")
                    sexo = "MASCULINO";
                else if (FUN_SEXO == "F")
                    sexo = "FEMININO";

                return sexo;
            }
        }

        public byte[] FUN_FOTO { get; set; }

        public string TIPAGEM
        {
            get
            {
                int? esc = FUN_TIPOSANGUINEO;
                if (esc == 1)
                    return "A-";
                else if (esc == 2)
                    return "A+";
                else if (esc == 3)
                    return "B-";
                else if (esc == 4)
                    return "B+";
                else if (esc == 5)
                    return "AB-";
                else if (esc == 6)
                    return "AB+";
                else if (esc == 7)
                    return "O-";
                else if (esc == 8)
                    return "O+";
                else
                    return "";
            }
        }


        public int Idade()
        {
            int idade = DateTime.Now.Year - FUN_DATANASCIMENTO.Value.Year;
            if (DateTime.Now.Month < FUN_DATANASCIMENTO.Value.Month || (DateTime.Now.Month == FUN_DATANASCIMENTO.Value.Month && DateTime.Now.Day < FUN_DATANASCIMENTO.Value.Day))
                idade--;

            return idade;
        }



    }


    //public class ProntuarioModelView
    //{
    //    public int PRT_ID { get; set; }
    //    public Nullable<int> VNC_ID { get; set; }
    //    public Nullable<int> PRT_ACIDENTETRAB { get; set; }
    //    public string PRT_ACIDENTETRAB_DESCRICAO { get; set; }
    //    public Nullable<System.DateTime> PRT_DATAINICIO { get; set; }
    //    public Nullable<System.DateTime> PRT_DATAFIM { get; set; }
    //    public string PRT_ESCREVE { get; set; }
    //    public string PRT_CARTEIRATRAB_ATIVIDADES { get; set; }
    //    public Nullable<int> PRT_RUIDO { get; set; }
    //    public string PRT_RUIDO_TEMPO { get; set; }
    //    public string PRT_RUIDO_TIPO { get; set; }
    //    public Nullable<int> PRT_EXAMEAUDIOMETRICO_QTD { get; set; }
    //    public string PRT_EXAMEAUDIOMETRICO_EMPRESA { get; set; }
    //    public string PRT_ANTECEDENTEFAMILIAR { get; set; }
    //    public string PRT_ANTECEDENTEPESSOAL { get; set; }
    //    public Nullable<int> PRT_FILHO { get; set; }
    //    public string PRT_FILHO_TIPO { get; set; }
    //    public Nullable<int> PRT_REPCADASTRO { get; set; }
    //    public Nullable<int> PRT_REGUSER { get; set; }

    //    public virtual FuncionarioCartaoSaudeModelView Funcionario { get; set; }
    //    public virtual ICollection<ConsultaModelView> Consulta { get; set; }
    //}

    public class ConsultaModelView
    {
        [ScaffoldColumn(false)]
        public string FUN_NOME { get; set; }

        [Key]
        [Display(Name = "ATENDIMENTO")]
        public int CON_ID { get; set; }




        [Display(Name = "SERVIDOR")]
        public int FUN_ID { get; set; }
        [Display(Name = "DATA ATENDIMENTO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [Required(ErrorMessage = "Informe a DATA ATENDIMENTO")]
        public Nullable<System.DateTime> CON_DATA { get; set; }
        [Display(Name = "FUMA")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        public SimNao? CON_FUMA { get; set; }
        [Display(Name = "ETILISMO")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        public SimNao? CON_ETILISMO { get; set; }
        [Display(Name = "SEDENTARISMO")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        public SimNao? CON_SEDENTARISMO { get; set; }
        [Display(Name = "HIPERTENSO")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        public SimNao? CON_HIPERTENSO { get; set; }
        [Display(Name = "DIABÉTICO")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        public SimNao? CON_DIABETICO { get; set; }
        [Display(Name = "OBSERVAÇÃO")]
        public string CON_OBSERVACAO { get; set; }
        [Display(Name = "PESO")]
        [DisplayFormat(DataFormatString = "{0:###.##}")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        public Nullable<double> CON_PESO { get; set; }
        [Display(Name = "ALTURA")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        public Nullable<double> CON_ALTURA { get; set; }
        [Display(Name = "IMC")]
        [DisplayFormat(DataFormatString = "{0:##.##}")]
        public Nullable<double> CON_IMC { get; set; }
        [Display(Name = "ANTI-HBS")]
        [RegularExpression(@"^\d+(,\d{0,3})?$", ErrorMessage = "Campo deve possuir 3 decimais, apos a virgula.")]
        [DisplayFormat(DataFormatString = "{0:##0.000#}", ApplyFormatInEditMode = true)]
        public Nullable<double> CON_ANTIHBS { get; set; }
        [Display(Name = "PRESSÃO ARTERIAL MAX.")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        public Nullable<double> CON_PRESSAOARTERIALMAX { get; set; }
        [Display(Name = "PRESSÃO ARTERIAL MIN.")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        public Nullable<double> CON_PRESSAOARTERIALMIN { get; set; }
        [Display(Name = "GLICEMIA")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        [DisplayFormat(DataFormatString = "{0:##.##}")]
        public Nullable<double> CON_GLICEMIA { get; set; }
        [Display(Name = "COLESTEROL TOT")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        public Nullable<double> CON_COLESTEROLTOTAL { get; set; }
        [Display(Name = "TRIGLICERIDEOS")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        [DisplayFormat(DataFormatString = "{0:##.##}")]
        public Nullable<double> CON_TRIGLICERIDEOS { get; set; }
        [Display(Name = "HDL")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        [DisplayFormat(DataFormatString = "{0:##.##}")]
        public Nullable<double> CON_HDL { get; set; }
        [Display(Name = "LDL")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        [DisplayFormat(DataFormatString = "{0:##.##}")]
        public Nullable<double> CON_LDL { get; set; }
        [Display(Name = "ÁCIDO ÚRICO")]
        [DisplayFormat(DataFormatString = "{0:##.##}")]
        public Nullable<double> CON_ACIDOURICO { get; set; }
        [Display(Name = "CIRC. ABDOMINAL")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        public Nullable<int> CON_CIRCUNFERENCIAABDOMINAL { get; set; }
        [Display(Name = "TIPO DE ATENDIMENTO")]
        [Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        public TipoAtend? CON_TIPO { get; set; }
        [Display(Name = "RESP. ATENDIMENTO")]
        //[Required(ErrorMessage = "* campo {0}, é obrigatório.")]
        public Nullable<int> CON_RESPATENDIMENTO { get; set; }
        [ScaffoldColumn(false)]
        public string NOME_RESPATENDIMENTO { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<int> CON_REGUSER { get; set; }

        public virtual FuncionarioCartaoSaudeModelView Funcionario { get; set; }

        public enum TipoAtend
        {
            [Display(Name = "Admissional")]
            Admissional = 1,
            [Display(Name = "Períodico")]
            Períodico = 2,
            [Display(Name = "Retorno ao Trabalho")]
            RetornoAoTrabalho = 3,
            [Display(Name = "Demissional")]
            Demissional = 4,
        }


        public enum SimNao
        {

            [Display(Name = "Sim")]
            Sim = 1,
            [Display(Name = "Não")]
            Não = 0,
            [Display(Name = "Não Informado")]
            NãoInformado = 2
        }

        public string AntiHBS
        {

            get
            {
                int? esc = (int?)CON_ANTIHBS;
                if (esc != null && esc < 10)
                    return "Não Reagente";
                else if (esc >= 10)
                    return "Reagente";
                else
                    return "N/I";
            }
        }


        //public string TipoAtendimento
        //{

        //    get
        //    {
        //        int? esc = (int)CON_TIPO;
        //        if (esc == 1)
        //            return "Admissional";
        //        else if (esc == 2)
        //            return "Períodico";
        //        else if (esc == 3)
        //            return "Retorno ao Trabalho";
        //        else if (esc == 4)
        //            return "Demissional";
        //        else if (esc == 5)
        //            return "Acolhimento";

        //        else
        //            return "";
        //    }
        //}

        //public string SimNaoNI(SimNao value)
        //{
        //    int? esc = (int)value;
        //    if (esc == 0)
        //        return "Não";
        //    else if (esc == 1)
        //        return "Sim";
        //    else if (esc == 2)
        //        return "Não Informado";
        //    else
        //        return "";

        //}

    }
}