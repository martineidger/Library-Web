using Library.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Models
{
    public class BookModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public Guid AuthorID { get; set; }
        public IFormFile? ImgFile { get; set; }
        public string? ImgPath {  get; set; }

        public DateTime? PickDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
