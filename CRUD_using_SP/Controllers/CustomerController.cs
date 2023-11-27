using CRUD_using_SP.Models;
using CRUD_using_SP.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_using_SP.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MainDbContext _context;
        public CustomerController(MainDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Customer> customers = this._context.SearchCustomer("").ToList();
            return View(customers);
        }

        [HttpPost]
        public IActionResult Index(string CustomerName)
        {
            List<Customer> customers = this._context.SearchCustomer(CustomerName).ToList();
            return View(customers);
        }
        public IActionResult InsertCus()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertCus(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var result = await _context.InsertCstomerintoTable( customer.CustName,customer.City,customer.Country,customer.DOB);

                if (result.StartsWith("Error"))
                {
                    ModelState.AddModelError(string.Empty, result);
                    return View(customer);
                }

                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Customers.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("CustomerID,CustName,City,Country,DOB")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _context.UpdateCstomerfromTable(customer.CustomerID, customer.CustName, customer.City, customer.Country, customer.DOB);

                if (result.StartsWith("Error"))
                {
                    ModelState.AddModelError(string.Empty, result);
                    return View(customer);
                }

                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Customers.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _context.DeleteCustomerFromTable(id);

            if (result.StartsWith("Error"))
            {
                ModelState.AddModelError(string.Empty, result);
                return View();
            }

            return RedirectToAction("Index");
        }


    }
}
