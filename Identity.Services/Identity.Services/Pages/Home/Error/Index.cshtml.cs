using System.Threading.Tasks;
using Duende.IdentityServer.Services;
using IdentityServerHost.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace IdentityServerHost.Pages.Error
{
    [AllowAnonymous]
    [SecurityHeaders]
    public class Index : PageModel
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IWebHostEnvironment _environment;

        public IdentityServerHost.Pages.Error.ViewModel View { get; set; }

        public Index(IIdentityServerInteractionService interaction, IWebHostEnvironment environment)
        {
            _interaction = interaction;
            _environment = environment;
        }

        public async Task OnGet(string errorId)
        {
            View = new IdentityServerHost.Pages.Error.ViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                View.Error = message;

                if (!_environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }
        }
    }
}
