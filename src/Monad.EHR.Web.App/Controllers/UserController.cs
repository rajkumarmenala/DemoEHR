using Microsoft.AspNet.Mvc;
using System;
using System.IO;
using Microsoft.Extensions.OptionsModel;
using Monad.EHR.Common.Utility;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Services.Interface;
using Monad.EHR.Web.App.Models;
using Microsoft.AspNet.Authorization;

namespace Monad.EHR.Web.App.Controllers
{

    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IOptions<AppSettings> _appSettings;
        public UserController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings;
        }

        [HttpGet]
        [Route("GetUserDetails")]
        public ApplicationUserViewModel GetUserDetails(string userName)
        {
            var applicationUser = _userService.GetUserByName(userName);
            if (applicationUser != null && applicationUser.ProfilePicture == null)
            {
                string defaultprofilePicture = "DefaultProfilePicture.png";
                applicationUser.ProfilePicture = defaultprofilePicture;
            }

            return new ApplicationUserViewModel
            {
                Id = applicationUser.Id,
                UserName = applicationUser.UserName,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                EmailAddress = applicationUser.EmailAddress,
                Designation = applicationUser.Designation,
                AddressLine1 = applicationUser.AddressLine1,
                AddressLine2 = applicationUser.AddressLine2,
                Zip = applicationUser.Zip,
                City = applicationUser.City,
                State = applicationUser.State,
                ProfilePicture = applicationUser.ProfilePicture
            };
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("EditUserProfile")]
        public IActionResult EditUserProfile([FromBody]ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Id = model.Id,
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailAddress = model.EmailAddress,
                    Designation = model.Designation,
                    AddressLine1 = model.AddressLine1,
                    AddressLine2 = model.AddressLine2,
                    Zip = model.Zip,
                    City = model.City,
                    State = model.State,
                    ProfilePicture = model.ProfilePicture
                };
                _userService.EditUser(user);
            }
            return new HttpStatusCodeResult(200);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("UploadImage")]
        public void UploadImage([FromBody]UserImageViewModel image)
        {
            if (image.FileName != null)
            {
                if (!System.IO.Directory.Exists(_appSettings.Value.ImagePath))
                {
                    System.IO.Directory.CreateDirectory(_appSettings.Value.ImagePath);
                }
                var applicationPath = Path.Combine(_appSettings.Value.ImagePath, image.FileName);
                if (System.IO.File.Exists(applicationPath))
                {
                    System.IO.File.Delete(applicationPath);
                }
                byte[] bytes = System.Convert.FromBase64String(image.Base64);
                using (System.IO.FileStream fs = new System.IO.FileStream(applicationPath, System.IO.FileMode.CreateNew))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }

        [HttpGet]
        [Route("GetUploadedImage")]
        public System.Byte[] GetUploadedImage(string imageName)
        {
            if (!System.IO.Directory.Exists(_appSettings.Value.ImagePath))
            {
                System.IO.Directory.CreateDirectory(_appSettings.Value.ImagePath);
            }
            string defaultImage = "DefaultProfilePicture.png";
            string applicationPath;
            if (imageName == defaultImage)
                applicationPath = Path.Combine(_appSettings.Value.ImagePath, defaultImage);
            else
                applicationPath = Path.Combine(_appSettings.Value.ImagePath, imageName);
            byte[] bytes = null;
            string filePath = applicationPath;
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                using (System.IO.BinaryReader br = new System.IO.BinaryReader(fs))
                {
                    bytes = br.ReadBytes((Int32)fs.Length);

                }
            }
            catch (Exception e)
            {
            }

            return bytes;
        }
    }
}