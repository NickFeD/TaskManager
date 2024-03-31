using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Command.Models
{
    public class RefreshTokenRequest
    {
        public required string ExpiredToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
