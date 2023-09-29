using System;

namespace CCM.Projects.SisGeape2.Domain
{
    public class BancoDomainModel
    {
        public int BNC_ID { get; set; }
        public string BNC_DESCRICAO { get; set; }
        public DateTime? BNC_REGDATE { get; set; }
        public int BNC_REGUSER { get; set; }
    }
}
