using FribergCarRental.Models.ViewModel;
using FribergCarRental.Services;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRental.ViewComponents
{
    public class UserFormViewComponent : ViewComponent
    {
        private readonly IAuthService _authService;

        public UserFormViewComponent(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _authService.GetCurrentUser();
            if (user == null)
            {
                ViewBag.Warning = "Vi kunde inte ladda dina uppgifter.";
                return View(new UserViewModel());
            }

            var model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username
            };

            return View(model);
        }
    }
}
