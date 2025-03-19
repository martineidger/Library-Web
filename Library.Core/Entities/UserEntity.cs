using Library.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Entities
{
    public class UserEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Role {  get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string DisplayName { get; set; }

        public List<BookEntity> Books { get; set; } = new List<BookEntity>();
    }
}
