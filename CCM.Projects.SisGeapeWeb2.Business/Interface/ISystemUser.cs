using CCM.Projects.SisGeape2.Domain;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface ISystemUser
    {
        int TotalRegistrosUser();
        int TotalRegistrosRoles();

        List<SystemUserDomainModel> GetAllUserPermission();
        List<SystemUserDomainModel> GetAllUser();

        List<SystemUserDomainModel> GetUserRecords(int pageStart, int pageSize);
        List<SystemUserDomainModel> GetUserByName(string name);
        SystemUserDomainModel GetLoginUser(string login, string password);
        SystemUserRolesDomainView GetUserbyId(int Id);
        bool AddUser(SystemUserRolesDomainView _domainModel);
        bool EditUser(SystemUserRolesDomainView _domainModel);
        bool DeleteUser(SystemUserRolesDomainView _domainModel);
        bool VerificaSenha(SystemUserEditDomainModel _domainModel);
        bool Salvar();

        bool DeleteRole(RoleDomainModel _domainModel);
        void AddUpdateRoles(RoleDomainModel _domainModel);
        List<RoleDomainModel> GetRoleByUser(int User);
        List<RoleDomainModel> GetRoleByUserName(string Username);

        List<RoleDomainModel> GetAllRoles();
        List<RoleDomainModel> GetRolesbyName(string name);

        List<RoleDomainModel> ListRoleDropList();
    }
}
