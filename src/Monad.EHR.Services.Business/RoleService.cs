using Monad.EHR.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;


namespace Monad.EHR.Services.Business
{
    public class RoleService : IRoleService
    {
        private IRoleRepository _repository;
        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public void AddRole(Role role)
        {
            role.LastModifiedDateUtc = DateTime.UtcNow;
            role.CreatedDateUtc = DateTime.UtcNow;
            role.LastModifiedBy = 1;
            _repository.Create(role);
        }

        public void DeleteRole(Role role)
        {
            _repository.Delete(role);
        }

        public void EditRole(Role role)
        {
            role.LastModifiedDateUtc = DateTime.UtcNow;
            role.CreatedDateUtc = DateTime.UtcNow;
            role.LastModifiedBy = 1;
            _repository.Update(role);
        }

        public IList<Role> GetAllRole()
        {
            return _repository.GetAll().ToList();
        }

        public Role GetRoleById(int id)
        {
            return _repository.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }

        public IList<Role> GetRolesByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
