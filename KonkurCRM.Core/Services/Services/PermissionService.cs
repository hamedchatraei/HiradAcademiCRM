using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.Core.DTOs.User;
using KonkurCRM.Core.Services.Interfaces;
using KonkurCRM.DataLayer.Context;
using KonkurCRM.DataLayer.Entities.Permission;
using KonkurCRM.DataLayer.Entities.User;

namespace KonkurCRM.Core.Services.Services
{
    public class PermissionService : IPermissionService
    {
        private KonkurCRMContext _context;

        public PermissionService(KonkurCRMContext context)
        {
            _context = context;
        }

        public void AddPermissionsToRole(int roleId, List<int> permission)
        {
            foreach (var p in permission)
            {
                _context.RolePermissions.Add(new RolePermission()
                {
                    PermissionId = p,
                    RoleId = roleId
                });
            }

            _context.SaveChanges();
        }

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (int roleId in roleIds)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });
            }

            _context.SaveChanges();
        }

        public bool CheckPermission(int permissionId, string userName)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserId;

            List<int> UserRoles = _context.UserRoles
                .Where(r => r.UserId == userId).Select(r => r.RoleId).ToList();

            if (!UserRoles.Any())
                return false;

            List<int> RolesPermission = _context.RolePermissions
                .Where(p => p.PermissionId == permissionId)
                .Select(p => p.RoleId).ToList();

            return RolesPermission.Any(p => UserRoles.Contains(p));
        }

        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            UpdateRole(role);
        }

        public void EditRole(EditRoleViewModel editRole)
        {
            Role role = GetRoleById(editRole.RoleId);

            role.RoleId = editRole.RoleId;
            role.RoleTitle = editRole.RoleTitle;

            UpdateRole(role);
        }

        public EditRoleViewModel GetDataForEditRole(int roleId)
        {
            return _context.Roles.Where(r => r.RoleId == roleId).Select(r => new EditRoleViewModel()
            {
                RoleId = roleId,
                RoleTitle = r.RoleTitle,
                RolePermissions = r.RolePermissions.Select(p=>p.PermissionId).ToList()
            }).Single();
        }

        public void EditRolesUser(int userId, List<int> rolesId)
        {
            //Delete All Roles User
            _context.UserRoles.Where(r => r.UserId == userId).ToList().ForEach(r => _context.UserRoles.Remove(r));

            //Add New Roles
            AddRolesToUser(rolesId, userId);
        }

        public List<Permission> GetAllPermission()
        {
            return _context.Permissions.ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public List<int> PermissionsRole(int roleId)
        {
            return _context.RolePermissions
                .Where(r => r.RoleId == roleId)
                .Select(r => r.PermissionId).ToList();
        }

        public void UpdatePermissionsRole(int roleId, List<int> permissions)
        {
            _context.RolePermissions.Where(p => p.RoleId == roleId)
                .ToList().ForEach(p => _context.RolePermissions.Remove(p));

            AddPermissionsToRole(roleId, permissions);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }
    }
}
