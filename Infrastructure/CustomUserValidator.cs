using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Models;

namespace TravelAgency.Infrastructure
{
    public class CustomUserValidator : UserValidator<AppUser>
    {
        public CustomUserValidator(AppUserManager manager)
           : base(manager)
        { }

        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);

            if (!user.Email.ToLower().EndsWith("@mail.com"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Любой email-адрес, отличный от mail.com запрещен");
                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}