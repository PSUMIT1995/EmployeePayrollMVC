using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace EmployeePayrollMVC.Models
{
    public class EmployeeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        public string EmployeeName { get; set; }

       // [Required(ErrorMessage = "This Field is required.")]
        public string ProfileImage { get; set; }


        [Required(ErrorMessage = "This Field is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        public string Department { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        public double Salary { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        public string Notes { get; set; }
    }
}
