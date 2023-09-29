using System;
using System.Collections.Generic;

namespace CCM.Projects.SisGeape2.Domain.SASS
{
    public class CartaoSaudeDomainView
    {

    }

    public class FuncionarioCartaoSaudeDomainModel
    {
        public int FUN_ID { get; set; }
        public string FUN_MATRICULA { get; set; }
        public string FUN_NOME { get; set; }
        public string FUN_NOMECRACHA { get; set; }
        public string FUN_SEXO { get; set; }
        public string FUN_ENDERECO { get; set; }
        public int? FUN_TIPOSANGUINEO { get; set; }
        public string FUN_NUMEROEND { get; set; }
        public string FUN_BAIRRO { get; set; }
        public string FUN_COMPLEMENTO { get; set; }
        public byte[] FUN_FOTO { get; set; }
        public Nullable<System.DateTime> FUN_DATANASCIMENTO { get; set; }
        public virtual List<ConsultaDomainModel> Consulta { get; set; }

    }

    //public class ProntuarioDomainModel
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

    //    public virtual FuncionarioCartaoSaudeDomainModel Funcionario { get; set; }
    //    public virtual ICollection<ConsultaDomainModel> Consulta { get; set; }
    //}

    public class ConsultaDomainModel
    {
        public int CON_ID { get; set; }
        public string FUN_NOME { get; set; }

        public Nullable<int> FUN_ID { get; set; }
        public Nullable<System.DateTime> CON_DATA { get; set; }
        public Nullable<int> CON_FUMA { get; set; }
        public Nullable<int> CON_ETILISMO { get; set; }
        public Nullable<int> CON_SEDENTARISMO { get; set; }
        public Nullable<int> CON_HIPERTENSO { get; set; }
        public Nullable<int> CON_DIABETICO { get; set; }
        public string CON_OBSERVACAO { get; set; }
        public Nullable<double> CON_ANTIHBS { get; set; }

        public Nullable<double> CON_PESO { get; set; }
        public Nullable<double> CON_ALTURA { get; set; }
        public Nullable<double> CON_IMC { get; set; }
        public Nullable<double> CON_PRESSAOARTERIALMAX { get; set; }
        public Nullable<double> CON_PRESSAOARTERIALMIN { get; set; }
        public Nullable<double> CON_GLICEMIA { get; set; }
        public Nullable<double> CON_COLESTEROLTOTAL { get; set; }
        public Nullable<double> CON_TRIGLICERIDEOS { get; set; }
        public Nullable<double> CON_HDL { get; set; }
        public Nullable<double> CON_LDL { get; set; }
        public Nullable<double> CON_ACIDOURICO { get; set; }
        public Nullable<int> CON_CIRCUNFERENCIAABDOMINAL { get; set; }
        public Nullable<int> CON_TIPO { get; set; }
        public Nullable<int> CON_RESPATENDIMENTO { get; set; }
        public string NOME_RESPATENDIMENTO { get; set; }

        public Nullable<System.DateTime> CON_REGDATE { get; set; }
        public Nullable<int> CON_REGUSER { get; set; }
        public string CON_STATUS { get; set; }


        public void Inativo()
        {
            this.CON_REGDATE = DateTime.Now;
            this.CON_STATUS = "I";
        }

        public virtual FuncionarioCartaoSaudeDomainModel Funcionario { get; set; }

    }
}
