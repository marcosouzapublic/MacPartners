using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MacPartners.Domain.Models;
using MacPartners.Infra.Context;
using MacPartners.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using MacPartners.Domain.Queries;
using Microsoft.AspNetCore.Authorization;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Models.Enums;
using MacPartners.Domain.Commands;

namespace MacPartners.Controllers
{
    [Authorize(Roles = "Partner")]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly Partner _partner;
        private readonly CustomerQueries _customerQueries;
        private readonly CreateCustomerCommand _createCommand;

        public CustomersController(IHttpContextAccessor httpContextAccessor, ICustomerRepository customerRepository, IPartnerRepository partnerRepository)
        {
            _customerRepository = customerRepository;
            _customerQueries = new CustomerQueries(_customerRepository);
            _createCommand = new CreateCustomerCommand(_customerRepository, _customerQueries);

            var partnerQueries = new PartnersQueries(partnerRepository);
            var userName = httpContextAccessor.HttpContext.User.Identities.First().Name;
            _partner = partnerQueries.PartnerByUserEmail(userName);
        }

        // GET: Customers
        public IActionResult Index()
        {
            return View(_customerQueries.PartnerCustomers(_partner));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            string personName, string personLastName, string personCpf, string personEmail, string personPhone,
            string companyCnpj, string companyName, string companyTradeName, string companyPhone,
            string zipCode, string street, string number, string district, string complement, string city, EState state
            )
        {

            ViewBag.PersonName = personName;
            ViewBag.PersonLastName = personLastName;
            ViewBag.PersonCpf = personCpf;
            ViewBag.PersonEmail = personEmail;
            ViewBag.PersonPhone = personPhone;

            ViewBag.CompanyCnpj = companyCnpj;
            ViewBag.CompanyName = companyName;
            ViewBag.CompanyTradeName = companyTradeName;
            ViewBag.CompanyPhone = companyPhone;

            ViewBag.ZipCode = zipCode;
            ViewBag.Street = street;
            ViewBag.Number = number;
            ViewBag.District = district;
            ViewBag.Complement = complement;
            ViewBag.City = city;
            ViewBag.State = state;

            var person = new Person(personName, personLastName, new Cpf(personCpf), null, new Email(personEmail), new Phone(personPhone));
            var company = new Company(new Cnpj(companyCnpj), companyName, companyTradeName, new Phone(companyPhone));
            var address = new Address(new ZipCode(zipCode), street, number, district, complement, city, state);

            var transactionResult = _createCommand.CreateCustomer(person, company, address, _partner);

            if (transactionResult.Status == TransactionStatus.Success)
                return RedirectToAction("Index");
            else
            {
                ViewBag.ErrorMessage = transactionResult.Message;
                ViewBag.Status = transactionResult.Status;

                return View();
            }
        }

        //    // GET: Customers/Edit/5
        //    public async Task<IActionResult> Edit(Guid? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var customer = await _context.Customers.FindAsync(id);
        //        if (customer == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(customer);
        //    }

        //    // POST: Customers/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(Guid id, [Bind("Id,CreatedAt,UpdatedAt")] Customer customer)
        //    {
        //        if (id != customer.Id)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(customer);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!CustomerExists(customer.Id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(customer);
        //    }

        //    // GET: Customers/Delete/5
        //    public async Task<IActionResult> Delete(Guid? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var customer = await _context.Customers
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (customer == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(customer);
        //    }

        //    // POST: Customers/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(Guid id)
        //    {
        //        var customer = await _context.Customers.FindAsync(id);
        //        _context.Customers.Remove(customer);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //}
    }
}
