using Microsoft.AspNetCore.Mvc;
using EntityLayer;
using Microsoft.AspNetCore.Authorization;
using ServiceLayer;

namespace ETicaretKurumsalSite.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "UserPolicy")]
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: Admin/Contacts
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetContactsAsync();
            return View(contacts);
        }

        // GET: Admin/Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactService.GetContactAsync(id.Value);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Admin/Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _contactService.AddContactAsync(contact);
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Admin/Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactService.GetContactAsync(id.Value);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _contactService.UpdateContactAsync(contact);
                }
                catch
                {
                    if (!await _contactService.ContactExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Admin/Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactService.GetContactAsync(id.Value);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Admin/Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _contactService.DeleteContactAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ContactExistsAsync(int id)
        {
            return await _contactService.ContactExists(id);
        }

    }
}
