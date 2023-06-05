using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Client.WPF.Models;

namespace TaskManager.Client.WPF.Services
{
    internal class UsersRequestService
    {
        public AuthToken GetToken(string userName, string password)
        {
            var api = RestService.For<>
        }
    }
}
