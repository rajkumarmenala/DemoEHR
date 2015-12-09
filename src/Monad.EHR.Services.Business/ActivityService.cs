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
        public ActivityService(IActivityRepository repository)
        {
            _repository = repository;
        }

        public void AddActivity(Activity activity)
        {
            activity.LastModifiedDateUtc = DateTime.UtcNow;
            activity.CreatedDateUtc = DateTime.UtcNow;
            activity.LastModifiedBy = 1;
            _repository.Create(activity);
        }

        public bool AddActivity(string activityId, int roleId, string description, string value, string createdBy)
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

        public IList<Activity> GetActivitiesByRoleId(int roleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Activity> GetActivitiesByUserId(int userId)
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
