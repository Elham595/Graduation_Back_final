using FluentValidation;
using FluentValidation.Results;
using graduation_project.Data;
using graduation_project.Models;
using Newtonsoft.Json.Linq;

namespace graduation_project.Validators
{
    public class CreateAccountValidator : AbstractValidator<JObject>
    {
        private readonly FashionDesignContext _fashionDbContext;

        public CreateAccountValidator(FashionDesignContext fashionDbContext)
        {
            _fashionDbContext = fashionDbContext;
        }

        public override ValidationResult Validate(ValidationContext<JObject> context)
        {
            var instance = context.InstanceToValidate;
            var account = instance["account"].ToObject<Account>();
            var user = instance["user"].ToObject<User>();
            if (account is null)
                context.AddFailure("account", "Invalid Account Data");
            if (user is null)
                context.AddFailure("user", "Invalid User Data");

            var username = _fashionDbContext.Users.Where(a => a.UserName == user.UserName).FirstOrDefault();
            var email = _fashionDbContext.Accounts.Where(a => a.Email == account.Email).FirstOrDefault();

            if (username is not null)
                context.AddFailure("username", "User Already Exists");
            if (email is not null)
                context.AddFailure("email", "Email Already Exits");

            return base.Validate(context);
        }
    }
}
