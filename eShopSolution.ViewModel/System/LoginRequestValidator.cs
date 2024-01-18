using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModel.System
{
    public class LoginRequestValidator: AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is not requied");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Pass thì nhập").MinimumLength(6).WithMessage("Min leng =6");
        }
    }
}
