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
        private IRoleRightRepository _repository;
        private IActivityRepository _activityRepository;
        private IResourceRepository _resourceRepository;

        public RoleService(IRoleRightRepository repository, IActivityRepository activityRepository, IResourceRepository resourceRepository)
        {
            _repository = repository;
            _activityRepository = activityRepository;
            _resourceRepository = resourceRepository;
        }


        public void AddRoleRight(RoleRight roleRight)
        {
            roleRight.LastModifiedDateUtc = DateTime.UtcNow;
            roleRight.CreatedDateUtc = DateTime.UtcNow;
            roleRight.LastModifiedBy = 1;
            _repository.Create(roleRight);
        }

        public void DeleteRoleRight(RoleRight roleRight)
        {
            _repository.Delete(roleRight);
        }

        public void EditRoleRight(RoleRight roleRight)
        {
            roleRight.LastModifiedDateUtc = DateTime.UtcNow;
            roleRight.LastModifiedBy = 1;
            _repository.Update(roleRight);
        }

        public RoleRight GetActivityById(int id)
        {
            return _repository.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }
       

        public IList<RoleRight> GetAllRightByRoleId(string roleId)
        {
            return _repository.GetAll().Where(x => x.RoleId == roleId).ToList();
        }

    }
}
