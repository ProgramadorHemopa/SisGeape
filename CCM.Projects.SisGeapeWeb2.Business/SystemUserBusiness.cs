using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCM.Projects.SisGeapeWeb2.Business
{
    public class SystemUserBusiness : ISystemUser
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SystemUserRepository _userRepository;
        private readonly SystemUserRolesRepository _userxrolesRepository;
        private readonly RolesRepository _rolesRepository;


        public SystemUserBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = new SystemUserRepository(_unitOfWork);
            _userxrolesRepository = new SystemUserRolesRepository(_unitOfWork);
            _rolesRepository = new RolesRepository(_unitOfWork);

        }

        public void AddUpdateRoles(RoleDomainModel _domainModel)
        {
            if (_domainModel.ROL_ID == 0)
            {
                roles _role = new roles();
                _role.ROL_NOME = _domainModel.ROL_NOME.ToUpper();
                _role.ROL_REGUSER = _domainModel.ROL_REGUSER;
                _role.ROL_STATUS = "A";
                _role.ROL_REGDATE = DateTime.Now;

                _rolesRepository.Insert(_role);
            }
            else
            {
                roles _role = _rolesRepository.SingleOrDefault(x => x.ROL_STATUS == "A" && x.ROL_ID == _domainModel.ROL_ID);

                if (_role != null)
                {
                    _role.ROL_NOME = _domainModel.ROL_NOME.ToUpper();
                    _role.ROL_REGDATE = DateTime.Now;
                    _role.ROL_REGUSER = _domainModel.ROL_REGUSER;
                    _rolesRepository.Update(_role);
                }
            }
        }

        public bool AddUser(SystemUserRolesDomainView _domainModel)
        {
            //if (_domainModel.SUSR_ID == 0)
            //{
            //    systemuser _user = new systemuser();
            //    _user.SUSR_NAME = _domainModel.SUSR_NAME;
            //    _user.SUSR_PASSWORD = APB.Framework.Encryption.Crypto.Encode(_domainModel.SUSR_PASSWORD.ToUpper()); // GetPasswordHash(_domainModel.SUSR_PASSWORD.ToUpper());
            //    _user.SUSR_LOGIN = GetLoginName(_domainModel.SUSR_NAME);
            //    _user.SUSR_REGUSER = _domainModel.SUSR_REGUSER;
            //    _user.SUSR_STATUS = "A";
            //    _user.SUSR_REGDATE = DateTime.Now;
            //    systemuserxroles _userxroles = new systemuserxroles();
            //    _userxroles.ROL_ID = _domainModel.SUSR_PERFIL;
            //    _userxroles.SIS_ID = _domainModel.SIS_ID;
            //    _userxroles.SUROL_REGUSER = _domainModel.SUSR_REGUSER;
            //    _userxroles.SUROL_REGDATE = DateTime.Now;
            //    _userxroles.SUROL_STATUS = "A";
            //    _user.systemuserxroles.Add(_userxroles);
            //    _userRepository.Insert(_user);

            //}

            systemuser _user = _userRepository.SingleOrDefault(x => x.SUSR_ID == _domainModel.SUSR_ID);
            if (_user != null)
            {
                systemuserxroles _userxroles = new systemuserxroles();
                _userxroles.ROL_ID = _domainModel.SUSR_PERFIL;
                _userxroles.SIS_ID = _domainModel.SIS_ID;
                _userxroles.SUROL_REGUSER = _domainModel.SUROL_REGUSER;
                _userxroles.SUROL_REGDATE = DateTime.Now;
                _userxroles.SUROL_STATUS = "A";
                _user.systemuserxroles.Add(_userxroles);
                _userRepository.Update(_user);
                return true;
            }
            return false;
        }

        public bool DeleteRole(RoleDomainModel _domainModel)
        {
            bool result = false;
            roles _role = _rolesRepository.SingleOrDefault(x => x.ROL_STATUS == "A" && x.ROL_ID == _domainModel.ROL_ID);

            if (_role != null)
            {
                _role.ROL_REGUSER = _domainModel.ROL_REGUSER;
                _role.ROL_REGDATE = DateTime.Now;
                _role.ROL_STATUS = "I";
                _rolesRepository.Update(_role);

                result = true;
            }

            return result;
        }

        public bool DeleteUser(SystemUserRolesDomainView _domainModel)
        {
            //bool result = false;
            //systemuser _user = _userRepository.SingleOrDefault(x => x.SUSR_STATUS == "A" && x.SUSR_ID == _domainModel.SUSR_ID);

            //if (_user != null)
            //{
            //    _user.SUSR_REGUSER = _domainModel.SUROL_REGUSER;
            //    _user.SUSR_REGDATE = DateTime.Now;
            //    _user.SUSR_STATUS = "I";

            //    foreach (var role in _user.systemuserxroles.ToArray())
            //    {
            //        role.SUROL_REGDATE = DateTime.Now;
            //        role.SUROL_REGUSER = _domainModel.SUROL_REGUSER;
            //        role.SUROL_STATUS = "I";
            //    }
            //    _userRepository.Update(_user);

            //    result = true;
            //}

            //return result;
            systemuserxroles _userRole = _userxrolesRepository.SingleOrDefault(x => x.SUROL_ID == _domainModel.SUROL_ID);
            if (_userRole != null)
            {

                _userRole.SUROL_REGUSER = _domainModel.SUROL_REGUSER;
                _userRole.SUROL_REGDATE = DateTime.Now;
                _userRole.SUROL_STATUS = "I";
                _userxrolesRepository.Update(_userRole);
                return true;
            }
            return false;
        }

        public bool EditUser(SystemUserRolesDomainView _domainModel)
        {
            systemuserxroles _userRole = _userxrolesRepository.SingleOrDefault(x => x.SUROL_ID == _domainModel.SUROL_ID);
            if (_userRole != null)
            {
                _userRole.ROL_ID = _domainModel.SUSR_PERFIL;
                _userRole.SIS_ID = _domainModel.SIS_ID;
                _userRole.SUROL_REGUSER = _domainModel.SUROL_REGUSER;
                _userRole.SUROL_REGDATE = DateTime.Now;
                _userRole.SUROL_STATUS = "A";
                _userxrolesRepository.Update(_userRole);
                return true;
            }
            return false;

            //systemuser _user = _userRepository.SingleOrDefault(x => x.SUSR_STATUS == "A" && x.SUSR_ID == _domainModel.SUSR_ID);

            //if (_user != null)
            //{
            //    //_user.SUSR_PASSWORD = APB.Framework.Encryption.Crypto.Encode(_domainModel.SUSR_PASSWORD.ToUpper());
            //    //_user.SUSR_REGUSER = _domainModel.SUSR_REGUSER;
            //    //_user.SUSR_REGDATE = DateTime.Now;
            //    /*
            //    _user.systemuserxroles.FirstOrDefault().ROL_ID = _domainModel.SUSR_PERFIL;
            //    _user.systemuserxroles.FirstOrDefault().SUROL_REGDATE = DateTime.Now;
            //    _user.systemuserxroles.FirstOrDefault().SUROL_REGUSER = _domainModel.SUSR_REGUSER;

            //     */


            //    _userRepository.Update(_user);
            //    return true;
            //}
            //return false;

        }

        public List<RoleDomainModel> GetAllRoles()
        {
            return _rolesRepository.GetAll(x => x.ROL_STATUS == "A").Select(x => new RoleDomainModel { ROL_ID = x.ROL_ID, ROL_NOME = x.ROL_NOME }).ToList();
        }

        public List<SystemUserDomainModel> GetAllUserPermission()
        {
            return _userRepository.GetAll(x => x.SUSR_STATUS == "A" && x.systemuserxroles.Any(z => z.SUROL_STATUS == "A")).ToList().Select(x => new SystemUserDomainModel
            {
                SUSR_ID = x.SUSR_ID,
                SUSR_LOGIN = x.SUSR_LOGIN,
                SUSR_NAME = x.SUSR_NAME,
                SIS_ID = x.systemuserxroles.FirstOrDefault(z => z.SUROL_STATUS == "A").SIS_ID.Value,
                SUSR_PERFIL = x.systemuserxroles.FirstOrDefault(z => z.SUROL_STATUS == "A").ROL_ID.Value,
            }).ToList();
        }

        public List<RoleDomainModel> GetRoleByUser(int User)
        {
            return _userxrolesRepository.GetAll(x => x.SUROL_STATUS == "A" && x.SUSR_ID == User).Select(x => new RoleDomainModel
            {
                ROL_ID = x.ROL_ID.Value,
                ROL_NOME = x.roles.ROL_NOME,
                ROL_REGUSER = x.roles.ROL_REGUSER,

            }).ToList();
        }

        public List<RoleDomainModel> GetRolesbyName(string name)
        {
            return _rolesRepository.GetAll(x => x.ROL_STATUS == "A" && x.ROL_NOME.Contains(name)).Select(x => new RoleDomainModel
            {
                ROL_ID = x.ROL_ID,
                ROL_NOME = x.ROL_NOME,
                ROL_REGUSER = x.ROL_REGUSER,

            }).ToList();
        }

        public SystemUserRolesDomainView GetUserbyId(int Id)
        {
            systemuser user = _userRepository.SingleOrDefault(x => x.SUSR_STATUS == "A" && x.SUSR_ID == Id);


            SystemUserRolesDomainView _domainModel = new SystemUserRolesDomainView();

            _domainModel.SUSR_ID = user.SUSR_ID;
            _domainModel.SIS_ID = user.systemuserxroles.FirstOrDefault(x => x.SUROL_STATUS == "A").SIS_ID.Value;
            _domainModel.SUROL_ID = user.systemuserxroles.FirstOrDefault(x => x.SUROL_STATUS == "A").SUROL_ID;
            _domainModel.SUSR_PERFIL = user.systemuserxroles.Where(z => z.SUROL_STATUS == "A").FirstOrDefault().ROL_ID.Value;

            return _domainModel;
        }

        public List<SystemUserDomainModel> GetUserByName(string name)
        {
            return _userRepository.GetAll(x => x.SUSR_STATUS == "A" && x.SUSR_LOGIN.ToUpper().Contains(name)).AsQueryable().Select(x => new SystemUserDomainModel
            {
                SUSR_ID = x.SUSR_ID,
                SUSR_LOGIN = x.SUSR_LOGIN,
                SUSR_NAME = x.SUSR_NAME,
                SIS_ID = x.systemuserxroles.FirstOrDefault(z => z.SUROL_STATUS == "A").SIS_ID.Value,
                SUSR_PERFIL = x.systemuserxroles.FirstOrDefault(z => z.SUROL_STATUS == "A").ROL_ID.Value,



            }).ToList();
        }

        public List<SystemUserDomainModel> GetUserRecords(int pageStart, int pageSize)
        {
            return _userRepository.GetPagedRecords(x => x.SUSR_STATUS == "A", x => x.SUSR_LOGIN, pageStart, pageSize).Select(x => new SystemUserDomainModel
            {
                SUSR_ID = x.SUSR_ID,
                SUSR_LOGIN = x.SUSR_LOGIN,
                SUSR_NAME = x.SUSR_NAME,
                SUSR_PERFIL = x.systemuserxroles.Where(z => z.SUROL_STATUS == "A").FirstOrDefault().ROL_ID.Value
            }).ToList();
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _userRepository.Save();
                return result;
            }
            catch (Exception e)
            {
                throw e;
                //return !result;
            }
        }

        public int TotalRegistrosRoles()
        {
            return _rolesRepository.Count(x => x.ROL_STATUS == "A");
        }
        public int TotalRegistrosUser()
        {
            return _userRepository.Count(x => x.SUSR_STATUS == "A");
        }

        //private string GetPasswordHash(string sPASSWORD)
        //{
        //    System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        //    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(sPASSWORD);
        //    byte[] hash = md5.ComputeHash(inputBytes);
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    for (int i = 0; i < hash.Length; i++)
        //    {
        //        sb.Append(hash[i].ToString("X1"));
        //    }

        //    return sb.ToString();
        //}

        private string GetLoginName(string nome)
        {
            string[] lUsuario = nome.Split(' ');

            nome = lUsuario[0] + "." + lUsuario[lUsuario.Length - 1];

            return nome.ToUpper();
        }

        public List<RoleDomainModel> ListRoleDropList()
        {
            return _rolesRepository.GetAll(x => x.ROL_STATUS == "A").Select(x => new RoleDomainModel { ROL_NOME = x.ROL_NOME, ROL_ID = x.ROL_ID }).ToList();
        }

        public bool VerificaSenha(SystemUserEditDomainModel _domainModel)
        {
            bool result = false;
            int compare = 0;
            systemuser _user = _userRepository.SingleOrDefault(x => x.SUSR_STATUS == "A" && x.SUSR_ID == _domainModel.SUSR_ID);

            if (_user != null)
            {
                compare = String.Compare(_user.SUSR_PASSWORD, APB.Framework.Encryption.Crypto.Encode(_domainModel.SUSR_PASSWORD), ignoreCase: true);
            }

            if (compare != 0)
                result = true;


            return result;
        }

        public SystemUserDomainModel GetLoginUser(string login, string password)
        {
            SystemUserDomainModel _domainModel = null;
            password = APB.Framework.Encryption.Crypto.Encode(password);
            systemuser user = _userRepository.SingleOrDefault(x => x.SUSR_STATUS == "A" && x.SUSR_LOGIN.ToUpper() == login && x.SUSR_PASSWORD == password && x.systemuserxroles.Any(z => z.SUROL_STATUS == "A"));

            if (user != null)
            {
                _domainModel = new SystemUserDomainModel()
                {
                    SUSR_ID = user.SUSR_ID,
                    SUSR_LOGIN = user.SUSR_LOGIN,
                    SUSR_NAME = user.SUSR_NAME,
                    SIS_ID = user.systemuserxroles.FirstOrDefault(z => z.SUROL_STATUS == "A").SIS_ID.Value,
                    SUSR_PERFIL = user.systemuserxroles.FirstOrDefault(z => z.SUROL_STATUS == "A").ROL_ID.Value,

                };
            }

            //_domainModel.SUSR_ID = user.SUSR_ID;
            //_domainModel.SUSR_LOGIN = user.SUSR_LOGIN;
            //_domainModel.SUSR_NAME = user.SUSR_NAME;
            // _domainModel.SUSR_PERFIL = user.systemuserxroles.Where(z => z.SUROL_STATUS == "A").FirstOrDefault().ROL_ID.Value;

            return _domainModel;
        }

        public List<RoleDomainModel> GetRoleByUserName(string Username)
        {


            return _userxrolesRepository.GetAll(x => x.SUROL_STATUS == "A" && x.systemuser.SUSR_LOGIN.ToUpper().Contains(Username))
                .Select(x => new RoleDomainModel { ROL_NOME = x.roles.ROL_NOME, ROL_ID = x.ROL_ID.Value }).ToList();

        }



        public List<SystemUserDomainModel> GetAllUser()
        {
            return _userRepository.GetAll(x => x.SUSR_STATUS == "A").ToList().Select(x => new SystemUserDomainModel
            {
                SUSR_ID = x.SUSR_ID,
                SUSR_LOGIN = x.SUSR_LOGIN,
                SUSR_NAME = x.SUSR_NAME,
                SIS_ID = x.systemuserxroles.Any(z => z.SUROL_STATUS == "A") ? x.systemuserxroles.FirstOrDefault().SIS_ID.Value : 0,
                SUSR_PERFIL = x.systemuserxroles.Any(z => z.SUROL_STATUS == "A") ? x.systemuserxroles.FirstOrDefault().ROL_ID.Value : 0,
            }).OrderBy(x => x.SUSR_NAME).ToList();
        }
    }
}
