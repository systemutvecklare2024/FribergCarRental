using FribergCarRental.Models.ViewModel;
using FribergCarRental.Services;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRental.ViewComponents
{
    public class ContactFormViewComponent : ViewComponent
    {
        private readonly IAuthService _authService;

        public ContactFormViewComponent(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _authService.GetAccount();
            if (user == null)
            {
                ViewBag.Warning = "Vi kunde inte ladda dina uppgifter.";
                return View(new UserViewModel());
            }

            var contact = user.Contact;
            if (contact == null)
            {
                ViewBag.Warning = "Vi kunde inte ladda dina uppgifter.";
                return View(new UserViewModel());
            }

            var contactViewModel = new ContactViewModel
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Address = contact.Address,
                City = contact.City,
                PostalCode = contact.PostalCode,
                Phone = contact.Phone
            };

            return View(contactViewModel);
        }
    }
}
