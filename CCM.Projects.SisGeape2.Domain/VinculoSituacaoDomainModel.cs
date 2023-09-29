namespace CCM.Projects.SisGeape2.Domain
{
    public class VinculoSituacaoDomainModel
    {
        public int VNCST_ID { get; set; }
        public string VNCST_DESCRICAO { get; set; }

        public int? VNCST_REGUSER { get; set; }


        public enum Situacao
        {
            aguardandoAposentadoria = 1,
            aguardandoExercicio = 2,
            aposentado = 3,
            ativo = 4,
            ativoCedido = 5,
            cedido = 6,
            desligado = 7,
            devolvido = 8,
            falecimento = 9,
            licencaSemVencimento = 10,
            NaoAprovado = 11

        }
    }
}
