using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Models
{
    public class UserModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Role {  get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string DisplayName { get; set; }

        public List<BookModel> Books { get; set; } = new List<BookModel>();
    }
}
