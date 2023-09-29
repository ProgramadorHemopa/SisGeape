namespace CCM.Projects.SisGeape2.Domain
{
    public class VinculoTipoDomainModel
    {
        public int VNCTP_ID { get; set; }
        public string VNCTP_DESCRICAO { get; set; }
        public int? VNCTP_REGUSER { get; set; }



        public enum Tipo
        {
            cedido = 1,
            comissionado = 2,
            comissionadoCedido = 3,
            efetivo = 4,
            efeitoCedido = 5,
            efeitoComissionado = 6,
            efeitoComissionadoCedido = 7,
            estagiario = 8,
            estatutarioEstavel = 9,
            estatutarioComissionado = 10,
            estatutarioNaoEstavel = 11,
            estatutarioEstavelCedido = 12,
            temporario = 13,
            temporarioComissionado = 14,
            residente = 15,
        }
    }
}
