using System;

namespace CCM.Projects.SisGeape2.Domain
{
    public class FuncionarioDependenteDomainModel
    {


        public int FUNDEP_ID { get; set; }


        public int FUN_ID { get; set; }
        public string FUNDEP_NOME { get; set; }
        public int FUNDEP_TIPO { get; set; }
        public string FUNDEP_TIPO_NOME { get; set; }

        public string FUNDEP_CPF { get; set; }
        public DateTime? FUNDEP_DATANASCIMENTO { get; set; }
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
