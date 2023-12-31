//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApplication.Models
{
    public partial class Employee_tbl
    {
        
        public int EId { get; set; }
        [Required(ErrorMessage ="Enter The FirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter The LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter The Age")]
        public Nullable<int> Age { get; set; }
        [Required(ErrorMessage = "Enter The MobileNumber")]
        public Nullable<long> MobileNumber { get; set; }
        [Required(ErrorMessage = "Enter The Email")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Email must follow this parttern 'eample@gmail.com'")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter The Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter The DepartmentId")]
        public Nullable<int> DepartmentId { get; set; }
        [Required(ErrorMessage = "Enter The Salary")]
        public Nullable<decimal> Salary { get; set; }
        public Nullable<bool> IsFlag { get; set; }
    
        public virtual Department_tbl Department_tbl { get; set; }
    }
}
