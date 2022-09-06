using EmployeePayrollMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using static EmployeePayrollMVC.Helper;

namespace EmployeePayrollMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext employeeContext;
        public EmployeeController(EmployeeContext employeeContext)
        {
            this.employeeContext = employeeContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.employeeContext.EmployeePayrollTable.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> AddEmployee(int Id = 0)
        {
            if (Id == 0)
            {
                return View(new EmployeeModel());
            }
            else
            {
                var emp = await employeeContext.EmployeePayrollTable.FindAsync(Id);
                if (emp == null)
                {
                    return NotFound();
                }

                return View(emp);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(int Id, [Bind("EmployeeID,EmployeeName,ProfileImage,Gender,Department,Salary,StartDate,Notes")] EmployeeModel emps)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (Id == 0)
                {
                    employeeContext.Add(emps);
                    await employeeContext.SaveChangesAsync();
                }

                //// Update
                //else
                //{
                //    try
                //    {
                //        employeePayrollDbContext.Update(emps);
                //        await employeePayrollDbContext.SaveChangesAsync();
                //    }
                //    catch (DbUpdateConcurrencyException)
                //    {
                //        if (!EmployeeModelExists(emps.Emp_Id))
                //        {
                //            return NotFound();
                //        }
                //        else
                //        {
                //            throw;
                //        }
                //    }
                //}

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", employeeContext.EmployeePayrollTable.ToList()) });
            }

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEmployee", emps) });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(int? Id)
        {
            if (Id == 0)
            {
                return View(new EmployeeModel());
            }
            else
            {
                var emp = await employeeContext.EmployeePayrollTable.FindAsync(Id);
                if (emp == null)
                {
                    return NotFound();
                }

                return View(emp);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployee(int Id, [Bind("EmployeeID,EmployeeName,ProfileImage,Gender,Department,Salary,StartDate,Notes")] EmployeeModel emps)
        {
            if (ModelState.IsValid)
            {
                // Update
                try
                {
                    employeeContext.Update(emps);
                    await employeeContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeModelExists(emps.EmployeeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", employeeContext.EmployeePayrollTable.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEmployee", emps) });
        }

        // GET: Employee/DeleteEmployee
        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empl = await employeeContext.EmployeePayrollTable.FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (empl == null)
            {
                return NotFound();
            }

            return View(empl);
        }

        // POST: Employee/DeleteEmployee/5
        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empl = await employeeContext.EmployeePayrollTable.FindAsync(id);
            employeeContext.EmployeePayrollTable.Remove(empl);
            await employeeContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", employeeContext.EmployeePayrollTable.ToList()) });
        }

        private bool EmployeeModelExists(int id)
        {
            return employeeContext.EmployeePayrollTable.Any(e => e.EmployeeID == id);
        }
    }
}