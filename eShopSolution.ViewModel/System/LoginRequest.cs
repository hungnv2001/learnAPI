using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModel.System
{
    public class LoginRequest
    {
        [Required(ErrorMessage ="not requied")]
        public string UserName { get; set; }
        [MaxLength( 1,ErrorMessage ="Min length=5")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
