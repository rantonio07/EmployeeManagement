using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeManagement.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly DataContext _context;

        public EmployeeController(DataContext context)
        {
            _context = context;
        }

        // GET: EmployeeController

        public IActionResult Employee()
        {
            return View();
        }

        // GET: EmployeeController/Details/5
        [HttpGet] //Get all employee
        public JsonResult GetAllEmployee()
        {

            var empleados = new List<Employee>
            {
                new Employee { Id = 1, FirstName = "María", LastName = "Gerente", Email= "g@g.com" },
                new Employee { Id = 2, FirstName = "Carlos", LastName = "Analista", Email= "g@g.com"},
                new Employee { Id = 3, FirstName = "Juan", LastName = "Desarrollador", Email= "g@g.com" },
            };

            //var empleados = _context.Empleados.ToList();
            return Json(new { data = empleados });
        }

        [HttpGet("{id}")] //get employee
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Empleados.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }
        // GET: EmployeeController/Create
        [HttpPost] //Create
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Empleados.Add(employee);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Empleado agregado correctamente" });
            }
            return Json(new { success = false, message = "Error al agregar empleado" });
        }

        // GET: /Edit
        public async Task<IActionResult> Edit(Employee employee)
        {
            var ifExistEmployee = _context.Empleados.Find(employee.Id);
            if (ifExistEmployee != null)
            {
                ifExistEmployee.FirstName = employee.FirstName;
                ifExistEmployee.LastName = employee.LastName;
                ifExistEmployee.Email = employee.Email;
                ifExistEmployee.DateOfBirth = employee.DateOfBirth;
                ifExistEmployee.Position = employee.Position;
                _context.SaveChangesAsync();
                return Json(new { success = true, message = "se editaron los datos correctamente" });
            }
            return Json(new { success = true, message = "Falla en los datos de edicion" });
        }


        [HttpPost] //delete employee
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Empleados.FindAsync(id);
            if (employee == null)
            {
                _context.Empleados.Remove(employee);
                _context.SaveChangesAsync();
                return Json(new { success = true, message = "Empleado eliminado correctamente" });
            }

            return Json(new { success = true, message = "Error de eliminación" });
        }
    }
}
