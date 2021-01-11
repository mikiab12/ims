using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ims.domain.Admin
{

    public class WSISResponseModel
    {
        public string SessionID { get; set; }
        public int UserID { get; set; }
    }


    public enum UserActionType
    {
        Login = 1,
        Logout = 2,
        Register = 3,
        UpdateUser = 4,
        ActivateUser = 5,
        DeactivateUser = 6,
        ChangePassword = 7,
        ResetPassword = 8,

        AddWorkflow = 9,
        UpdateWorkflow = 10,
        AddWorkItem = 11,

        AddDocument = 12,
        UpdateDocument = 13,
        DeleteDocument = 14,

        RevokeRole = 15,

    }

    public enum UserRoles
    {
        Admin = 1,
        FactoryOfficer = 2,
        StoreOfficer = 3,
        Purchaseofficer = 4,
        ShopOfficer = 5,
        Finance = 6

    }

    public class UserViewModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int Status { get; set; }
        public long[] Roles { get; set; }
        public long Id { get; set; }
        public string PhoneNo { get; set; }
        public string Position { get; set; }
    }

    public class UserDetialViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int? EmployeeID { get; set; }
        public int Status { get; set; }
        public string[] Roles { get; set; }
        public string RegOn { get; set; }
        public string LastSeen { get; set; }
        public string PhoneNo { get; set; }
        public string Position { get; set; }
    }

    public class UserActionViewModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Action { get; set; }
        public DateTime ActionTime { get; set; }
    }

    public class LoginViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public long Role { get; set; }
    }

    public class RegisterViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Phone")]
        public string PhoneNo { get; set; }
        [Required]
        public string Position { get; set; }
        public int[] Roles { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        public string UserName { get; set; }
        public string NewPassword { get; set; }
    }

    public class UserRoleViewModel
    {
        public string UserName { get; set; }
        public int[] Roles { get; set; }

    }
}
