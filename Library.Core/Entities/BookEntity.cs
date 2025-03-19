using Library.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Entities
{
    public class BookEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public AuthorEntity Author { get; set; }
        public Guid AuthorID { get; set; }
        public string? ImgPath {  get; set; }

        public DateTime? PickDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}
