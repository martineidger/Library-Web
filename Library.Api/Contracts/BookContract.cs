using Library.Core.Entities;

namespace Library.Api.Contracts
{
    public class BookContract
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public Guid AuthorID { get; set; }
        public IFormFile? ImgFile { get; set; }

        public DateTime? PickDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
