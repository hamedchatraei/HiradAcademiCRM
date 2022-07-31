using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.Core.DTOs.User;
using KonkurCRM.Core.Generator;
using KonkurCRM.Core.Security;
using KonkurCRM.Core.Services.Interfaces;
using KonkurCRM.DataLayer.Context;
using KonkurCRM.DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace KonkurCRM.Core.Services.Services
{
    public class UserService : IUserService
    {
        private KonkurCRMContext _context;

        public UserService(KonkurCRMContext context)
        {
            _context = context;
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public int AddUserFromAdmin(CreateUserViewModel user)
        {
            User addUser = new User();
            addUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            addUser.Mobile = user.Mobile;
            addUser.IsActive = true;
            addUser.RegisterDate = DateTime.Now;
            addUser.UserName = user.UserName;
            addUser.UserAliasName = user.UserAliasName;

            #region Save Avatar

            if (user.UserAvatar != null)
            {
                string imagePath = "";
                addUser.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(user.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", addUser.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(stream);
                }
            }

            #endregion

            return AddUser(addUser);
        }

        public void ChangeUserPassword(string userName, string newPassword)
        {
            var user = GetUserByUserName(userName);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            UpdateUser(user);
        }

        public bool CompareOldPassword(string oldPassword, string userName)
        {
            string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _context.Users.Any(u => u.UserName == userName && u.Password == hashOldPassword);
        }

        public void DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            user.IsDelete = true;
            UpdateUser(user);
        }

        public void EditProfile(string userName, EditProfileViewModel profile)
        {
            User user = GetUserById(profile.UserId);

            if (profile.UserAvatar != null)
            {
                //Delete old Image
                if (profile.AvatarName != "Default.jpg")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }

                //Save New Image
                user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(profile.UserAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    profile.UserAvatar.CopyTo(stream);
                }
            }

            user.UserName = profile.UserName;
            user.UserAliasName = profile.UserAliasName;
            user.Mobile = profile.Mobile;

            UpdateUser(user);
        }

        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
            User user = GetUserById(editUser.UserId);
            user.UserAliasName = editUser.UserAliasName;
            user.Mobile = editUser.Mobile;

            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
            }

            if (editUser.UserAvatar != null)
            {
                //Delete old Image
                if (editUser.AvatarName != "Default.jpg")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editUser.AvatarName);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }

                //Save New Image
                user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(editUser.UserAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editUser.UserAvatar.CopyTo(stream);
                }
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public EditProfileViewModel GetDataForEditProfileUser(string userName)
        {
            User user = GetUserByUserName(userName);

            return _context.Users.Where(u => u.UserId == user.UserId).Select(u => new EditProfileViewModel()
            {
                UserId = user.UserId,
                AvatarName = u.UserAvatar,
                Mobile = u.Mobile,
                UserName = u.UserName,
                UserAliasName = u.UserAliasName
            }).Single();
        }

        public UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterMobile = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users.IgnoreQueryFilters().Where(u => u.IsDelete);

            if (!string.IsNullOrEmpty(filterMobile))
            {
                result = result.Where(u => u.Mobile.Contains(filterMobile));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }

            // Show Item In Page
            int take = 10;
            int skip = (pageId - 1) * take;


            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName).Select(u => new SideBarUserPanelViewModel()
            {
                UserName = u.UserName,
                UserAliasName = u.UserAliasName,
                RegisterDate = u.RegisterDate,
                ImageName = u.UserAvatar
            }).Single();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public User GetUserByUserAliasName(string userAliasName)
        {
            return _context.Users.SingleOrDefault(u => u.UserAliasName == userAliasName);
        }

        public EditUserViewModel GetUserForShowInEditMode(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId)
                .Select(u => new EditUserViewModel()
                {
                    UserId = u.UserId,
                    AvatarName = u.UserAvatar,
                    Mobile = u.Mobile,
                    UserName = u.UserName,
                    UserAliasName = u.UserAliasName,
                    UserRoles = u.UserRoles.Select(r => r.RoleId).ToList()
                }).Single();
        }

        public InformationUserViewModel GetUserInformation(string userName)
        {
            var user = GetUserByUserName(userName);
            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.UserAliasName = user.UserAliasName;
            information.Mobile = user.Mobile;
            information.RegisterDate = user.RegisterDate;

            return information;
        }

        public InformationUserViewModel GetUserInformation(int userId)
        {
            var user = GetUserById(userId);
            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.UserAliasName = user.UserAliasName;
            information.Mobile = user.Mobile;
            information.RegisterDate = user.RegisterDate;

            return information;
        }

        public List<int> GetUserRoles(int userId)
        {
            List<UserRole> query = new List<UserRole>();

            query = _context.UserRoles.ToList();

            List<int> UserRoles = query.Where(u => u.UserId == userId).Select(u => u.RoleId).ToList();

            return UserRoles;
        }

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName).UserId;
        }

        public UserForAdminViewModel GetUsers(int pageId = 1, string filterMobile = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users.Where(u => u.IsDelete == false);

            if (!string.IsNullOrEmpty(filterMobile))
            {
                result = result.Where(u => u.Mobile.Contains(filterMobile));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }

            // Show Item In Page
            int take = 10;
            int skip = (pageId - 1) * take;


            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)result.Count() / take);
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string userName = login.UserName;
            return _context.Users.SingleOrDefault(u => u.UserName == userName && u.Password == hashPassword);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }
    }
}
