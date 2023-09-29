using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using System.Data.Entity;

namespace CCM.Projects.SisGeapeWeb2.Repository.Infra
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly geapedbContextEntities _dbContext;

        public UnitOfWork()
        {
            _dbContext = new geapedbContextEntities();
        }

        public DbContext Db
        {
            get { return _dbContext; }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
