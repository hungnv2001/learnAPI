using eShopSolution.ViewModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Aplication.System.Users
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequest loginRequest);
        Task<bool> Resister(RegisterRequest registerRequest);
    }
}
