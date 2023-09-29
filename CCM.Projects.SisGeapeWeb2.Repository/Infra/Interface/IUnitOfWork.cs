using System;
using System.Data.Entity;

namespace CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Return the database reference for this UOW
        /// </summary>
        DbContext Db { get; }
    }
}
