using System;

namespace CCM.Projects.SisGeape2.Domain
{
    public class FeriasDomainModel
    {
        public int FRS_ID { get; set; }
        public int FUN_ID { get; set; }
        public int VNC_ID { get; set; }
        public string VNC_NOME { get; set; }
        public string FUN_NOME { get; set; }
        public string MATRICULA { get; set; }

        public string UNIDADE { get; set; }
        public string FRS_PERIODOAQUISITIVO { get; set; }
        public DateTime? FRS_DATA_INICIOAQUISITIVO { get; set; }
        public DateTime? FRS_DATA_FIMAQUISITIVO { get; set; }
        public DateTime? FRS_DATA_INICIOGOZO { get; set; }
        public DateTime? FRS_DATA_FIMGOZO { get; set; }
        public DateTime? FRS_DATA_RETORNO { get; set; }
        public DateTime? FRS_DATAPORTARIA { get; set; }
        public string FRS_NUMPORTARIA { get; set; }
        public string FRS_OBSERVACAO { get; set; }
        public int? FRS_REGUSER { get; set; }


    }

    public class ComunicadoFeriasDomainModel
    {
        public string FRS_ID { get; set; }
        public string FUN_NOME { get; set; }
        public string FUN_MATRICULA { get; set; }
        public string CRG_NOME { get; set; }
        public string UND_NOME { get; set; }
        public string UNIDADE_RESPONSAVEL { get; set; }
        public string FRS_PERIODOAQUISITIVO { get; set; }
        public string FRS_DATA_INICIOGOZO { get; set; }
        public string FRS_DATA_FIMGOZO { get; set; }

        public string FRS_DATA_RETORNO { get; set; }
        public string FRS_DATAPORTARIA { get; set; }
        public string FRS_NUMPORTARIA { get; set; }



    }
}
