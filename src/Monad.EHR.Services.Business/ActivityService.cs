using Monad.EHR.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;


namespace Monad.EHR.Services.Business
{
    public class ActivityService : IActivityService
    {

        private IActivityRepository _repository;
        private IActivityRoleRepository _activityRoleRepository;

        public ActivityService(IActivityRepository repository, IActivityRoleRepository activityRoleRepository)
        {
            _repository = repository;
            _activityRoleRepository = activityRoleRepository;
        }

        public void AddActivity(Activity activity)
        {
            activity.LastModifiedDateUtc = DateTime.UtcNow;
            activity.CreatedDateUtc = DateTime.UtcNow;
            activity.LastModifiedBy = 1;
            _repository.Create(activity);
        }

        public bool AddActivity(string activityId, string roleId, string description, string value, string createdBy)
        {
            throw new NotImplementedException();
        }

        public void DeleteActivity(Activity activity)
        {
            _repository.Delete(activity);
        }

        public void EditActivity(Activity activity)
        {
            activity.LastModifiedDateUtc = DateTime.UtcNow;
            activity.CreatedDateUtc = DateTime.UtcNow;
            activity.LastModifiedBy = 1;
            _repository.Update(activity);
        }

        //public IList<Activity> GetActivitiesByRoleName(string roleName)
        //{
        //    var query = from r in _roleRepository.GetAll()
        //                join arr in _activityRoleRepository.GetAll() on r.Id equals arr.RoleID
        //                join a in _repository.GetAll() on arr.ActivityID equals a.Id
        //                where string.Equals(r.Name, roleName)
        //                select a;
        //    return query.ToList();
        //}


        public IList<Activity> GetActivitiesByRoleId(string roleId)
        {
            var query = from arr in _activityRoleRepository.GetAll()
            join a in _repository.GetAll() on arr.ActivityID equals a.Id
            where string.Equals(arr.RoleID, roleId)
            select a;
            return query.ToList();
        }

        public IEnumerable<Activity> GetActivitiesByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Activity GetActivityById(int id)
        {
            return _repository.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }

        public IList<Activity> GetAllActivity()
        {
            return _repository.GetAll().ToList();
        }
    }
}
