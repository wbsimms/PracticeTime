﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PracticeTime.Web.DataAccess;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Account Type")]
        public string SelectedAccountType { get; set; }

        public SelectList AccountTypes
        {
            get
            {
                List<NameValue> nv = new List<NameValue>();
                nv.Add(new NameValue() { Name = PracticeTimeRoles.Student.ToString(), Value = "Student" });
                nv.Add(new NameValue() { Name = PracticeTimeRoles.Instructor.ToString(), Value = "Student" });
                return new SelectList(nv, "Value", "Name");
            }
        }
        [ReadOnly(true)]
        public string StudentToken { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [DisplayName("Public Profile?")]
        public bool StudentPublicProfile { get; set; }

        [Required]
        public string City { get; set; }

        public SelectList StateTypes
        {
            get
            {
                List<NameValue> nv = new List<NameValue>();
                foreach (string stateName in Enum.GetNames(typeof(States)))
                {
                    nv.Add(new NameValue() { Name = stateName, Value = stateName });
                }
                return new SelectList(nv, "Value", "Name");
            }
        }

        [Required]
        public States State { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [ReadOnly(true)]
        public string StudentToken { get; set; }

        [DisplayName("Public Profile?")]
        public bool StudentPublicProfile { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name="Account Type")]
        public string SelectedAccountType { get; set; }

        public SelectList AccountTypes {
            get
            {
                List<NameValue> nv = new List<NameValue>();
                nv.Add(new NameValue() { Name = PracticeTimeRoles.Student.ToString(), Value = PracticeTimeRoles.Student.ToString() });
                nv.Add(new NameValue() { Name = PracticeTimeRoles.Instructor.ToString(), Value = PracticeTimeRoles.Instructor.ToString() });
                return new SelectList(nv,"Value","Name");
            }
        }

        [ReadOnly(true)]
        public string StudentToken { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [DisplayName("Public Profile?")]
        public bool StudentPublicProfile { get; set; }
        public SelectList StateTypes
        {
            get
            {
                List<NameValue> nv = new List<NameValue>();
                foreach (string stateName in Enum.GetNames(typeof(States)))
                {
                    nv.Add(new NameValue() { Name = stateName, Value = stateName });
                }
                return new SelectList(nv, "Value", "Name");
            }
        }

        [Required]
        public States State { get; set; }

        [Required]
        public string City { get; set; }

    }

    public class NameValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
 
