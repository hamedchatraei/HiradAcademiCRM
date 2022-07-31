using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.Core.DTOs.User;
using KonkurCRM.DataLayer.Entities.Permission;
using KonkurCRM.DataLayer.Entities.User;

namespace KonkurCRM.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Roles
        List<Role> GetRoles();
        int AddRole(Role role);
        Role GetRoleById(int roleId);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        void EditRole(EditRoleViewModel editRole);
        EditRoleViewModel GetDataForEditRole(int roleId);
        void AddRolesToUser(List<int> roleIds, int userId);
        void EditRolesUser(int userId, List<int> rolesId);


        #endregion

        #region Permission
        List<Permission> GetAllPermission();
        void AddPermissionsToRole(int roleId, List<int> permission);
        List<int> PermissionsRole(int roleId);
        void UpdatePermissionsRole(int roleId, List<int> permissions);

        bool CheckPermission(int permissionId, string userName);

        #endregion
    }
}
