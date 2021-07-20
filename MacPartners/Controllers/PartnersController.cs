using System;
using Microsoft.AspNetCore.Mvc;
using MacPartners.Domain.Repositories;
using MacPartners.Domain.Commands;
using MacPartners.Domain.Queries;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Models.Enums;
using MacPartners.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace MacPartners.Controllers
{
    [Authorize(Roles = ("Administrator"))]
    public class PartnersController : Controller
    {
        private readonly CreatePartnerCommand _createCommand;
        private readonly EditPartnerCommand _editCommand;
        private readonly BlockPartnerCommand _blockCommand;
        private readonly PartnersQueries _queries;
        private readonly UsersController _usersController;
        private readonly IPartnerRepository _repository;

        public PartnersController(IPartnerRepository repository, IUserRepository _userRepository, ICrypter crypter)
        {
            _repository = repository;
            _queries = new PartnersQueries(_repository);
            _createCommand = new CreatePartnerCommand(_repository, _queries);
            _editCommand = new EditPartnerCommand(_repository);
            _blockCommand = new BlockPartnerCommand(_repository, _userRepository);
            _usersController = new UsersController(_userRepository, crypter);
        }

        // GET: Partners
        public IActionResult Index(string errorMessage, TransactionStatus status = TransactionStatus.Success)
        {
            ViewBag.ErrorMessage = errorMessage != null? errorMessage : "";

            return View(_queries.UnblockedPartners());
        }

        public PartialViewResult _BlockedPartners()
        {
            ViewBag.BlockedPartners = _queries.BlockedPartners();

            return PartialView();
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
                ViewBag.Name = name;
                ViewBag.LastName = lastName;
                ViewBag.Cpf = cpf;
                ViewBag.Birthday = birthday;
                ViewBag.Email = email;
                ViewBag.Phone = phone;
                ViewBag.ErrorMessage = transactionResult.Message;
                ViewBag.Status = transactionResult.Status;
                return View();
            }
        }

        // GET: Partners/Edit/5
        public IActionResult Edit(Guid id)
        {
            var partner = _repository.Find(id);

            return View(partner);
        }

        // POST: Partners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, string name, string lastName, DateTime? birthday, string phone)
        {
            var partner = _repository.Find(id);
            partner.EditPerson(name, lastName, phone, birthday);

            var transactionResult = _editCommand.EditPartner(partner);

            if (transactionResult.Status == TransactionStatus.Success)
                return RedirectToAction("Index");
            else
            {
                ViewBag.ErrorMessage = transactionResult.Message;
                ViewBag.Status = transactionResult.Status;
                ViewBag.Name = name;
                ViewBag.LastName = lastName;
                ViewBag.Birthday = birthday;
                ViewBag.Phone = phone;
                return View(partner);
            }
        }

        [HttpGet, ActionName("Block")]
        public IActionResult Block(Guid id)
        {
            var partner = _repository.Find(id);
            var transactionResult = _blockCommand.BlockPartner(partner);

            if (transactionResult.Status == TransactionStatus.Success)
                return RedirectToAction("Index");
            else
            {
                return RedirectToAction("Index", new {errorMessage = transactionResult.Message, status = transactionResult.Status});
            }
        }

        [HttpGet, ActionName("Unblock")]
        public IActionResult Unblock(Guid id)
        {
            var partner = _repository.Find(id);
            var transactionResult = _blockCommand.UnblockPartner(partner);

            if (transactionResult.Status == TransactionStatus.Success)
                return RedirectToAction("Index");
            else
            {
                return RedirectToAction("Index", new { errorMessage = transactionResult.Message, status = transactionResult.Status });
            }
        }
    }
}
