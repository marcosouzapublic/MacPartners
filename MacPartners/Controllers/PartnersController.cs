using System;
using Microsoft.AspNetCore.Mvc;
using MacPartners.Domain.Repositories;
using MacPartners.Domain.Commands;
using MacPartners.Domain.Queries;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Models.Enums;
using MacPartners.Domain.Interfaces;

namespace MacPartners.Controllers
{
    public class PartnersController : Controller
    {
        private readonly CreatePartnerCommand _createCommand;
        private readonly CreateUserCommand _createUserCommand;
        private readonly PartnersQueries _queries;
        private readonly UsersController _usersController;

        public PartnersController(IPartnerRepository repository, IUserRepository _userRepository, ICrypter crypter)
        {
            _createCommand = new CreatePartnerCommand(repository);
            _createUserCommand = new CreateUserCommand(_userRepository, crypter);
            _queries = new PartnersQueries(repository);
            _usersController = new UsersController(_userRepository, crypter);
        }

        // GET: Partners
        public IActionResult Index()
        {
            return View(_queries.UnblockedPartners());
        }

        // GET: Partners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Partners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name, string lastName, string cpf, DateTime? birthday, string email, string phone)
        {
            var person = new Person(name, lastName, new Cpf(cpf), birthday, new Email(email), new Phone(phone));
            var transactionResult = _createCommand.CreatePartner(person, _usersController);

            if (transactionResult.Status == TransactionStatus.Success)
                return RedirectToAction("Index");
            else
            {
                ViewBag.Person = person;
                ViewBag.ErrorMessage = transactionResult.Message;
                ViewBag.Status = transactionResult.Status;
                return View();
            }
        }

        //// GET: Partners/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var partner = await _context.Partners.FindAsync(id);
        //    if (partner == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(partner);
        //}

        //// POST: Partners/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id,CreatedAt,BlockedAt,IsBlocked")] Partner partner)
        //{
        //    if (id != partner.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(partner);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PartnerExists(partner.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(partner);
        //}

        //// GET: Partners/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var partner = await _context.Partners
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (partner == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(partner);
        //}

        //// POST: Partners/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var partner = await _context.Partners.FindAsync(id);
        //    _context.Partners.Remove(partner);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
