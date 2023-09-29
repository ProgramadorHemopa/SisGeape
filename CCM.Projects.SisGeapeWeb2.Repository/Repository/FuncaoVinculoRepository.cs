﻿using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class FuncaoVinculoRepository : BaseRepository<ap_funcaoxvinculo>
    {
        public FuncaoVinculoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
