using BCrypt.Net;
using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Initializing
{
    public class DbInitializer : IDbInitializer
    {
        private readonly LibraryDbContext _context;

        public DbInitializer(LibraryDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.Migrate(); 

            if (_context.Authors.Any() || _context.Users.Any())
            {
                Console.WriteLine("RETURN DBIN");
                return; 
            }

            var users = new UserEntity[]
            {
            new UserEntity { Email = "user@example.com", HashPassword = BCrypt.Net.BCrypt.HashPassword("User123!"), DisplayName = "User", Role = "User" },
            new UserEntity { Email = "admin@example.com", HashPassword = BCrypt.Net.BCrypt.HashPassword("Admin123!"), DisplayName = "Admin", Role = "Admin" }
            };

            try
            {
                _context.Users.AddRange(users);
                _context.SaveChanges();
                Console.WriteLine("Users DONE");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving users: {ex.Message}");
            }

            var authors = new AuthorEntity[]
            {
            new AuthorEntity { FirstName = "Лев", Surname = "Толстой", BirthDate = new DateTime(1828, 9, 9), Country = "Россия" },
            new AuthorEntity { FirstName = "Федор", Surname = "Достоевский", BirthDate = new DateTime(1821, 11, 11), Country = "Россия" },
            new AuthorEntity { FirstName = "Антон", Surname = "Чехов", BirthDate = new DateTime(1860, 1, 29), Country = "Россия" },
            new AuthorEntity { FirstName = "Михаил", Surname = "Булгаков", BirthDate = new DateTime(1891, 5, 15), Country = "Россия" },
            new AuthorEntity { FirstName = "Александр", Surname = "Пушкин", BirthDate = new DateTime(1799, 6, 6), Country = "Россия" },
            new AuthorEntity { FirstName = "Габриэль", Surname = "Гарсия Маркес", BirthDate = new DateTime(1927, 3, 6), Country = "Колумбия" },
            new AuthorEntity { FirstName = "Эрнест", Surname = "Хемингуэй", BirthDate = new DateTime(1899, 7, 21), Country = "США" },
            new AuthorEntity { FirstName = "Джейн", Surname = "Остин", BirthDate = new DateTime(1775, 12, 16), Country = "Великобритания" },
            new AuthorEntity { FirstName = "Маргарет", Surname = "Атвуд", BirthDate = new DateTime(1939, 11, 18), Country = "Канада" },
            new AuthorEntity { FirstName = "Габриэль", Surname = "Гарсиа", BirthDate = new DateTime(1927, 3, 6), Country = "Колумбия" }
            };

            
            try
            {
                _context.Authors.AddRange(authors);
                _context.SaveChanges();
                Console.WriteLine("Authors DONE");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving users: {ex.Message}");
            }

            var books = new BookEntity[]
            {
            new BookEntity { ISBN = "16-148410-0", Title = "Война и мир", Genre = "Роман", Description = "Эпопея о жизни русского общества.", AuthorID = authors[0].Id, ImgPath = "covers/vim.jpg" },
            new BookEntity { ISBN = "16-148410-1", Title = "Преступление и наказание", Genre = "Роман", Description = "Роман о моральных дилеммах.", AuthorID = authors[1].Id , ImgPath = "covers/pin.jpg"},
            new BookEntity { ISBN = "16-148410-2", Title = "Чайка", Genre = "Пьеса", Description = "Пьеса о жизни и любви.", AuthorID = authors[2].Id , ImgPath = "covers/chka.jpg"},
            new BookEntity { ISBN = "16-148410-3", Title = "Мастер и Маргарита", Genre = "Роман", Description = "Роман о любви и дьявольских интригах.", AuthorID = authors[3].Id , ImgPath = "covers/mim.jpg"},
            new BookEntity { ISBN = "16-148410-4", Title = "Евгений Онегин", Genre = "Роман", Description = "Литературный роман в стихах.", AuthorID = authors[4].Id , ImgPath = "covers/evg_on.jpg"},
            new BookEntity { ISBN = "16-148410-5", Title = "Сто лет одиночества", Genre = "Роман", Description = "Сага о семье Буэндиа.", AuthorID = authors[5].Id, ImgPath = "covers/100let_od.jpg" },
            new BookEntity { ISBN = "16-148410-6", Title = "Старик и море", Genre = "Роман", Description = "История о борьбе старика с природой.", AuthorID = authors[6].Id , ImgPath = "covers/star_more.jpg"},
            new BookEntity { ISBN = "16-148410-7", Title = "Гордость и предубеждение", Genre = "Роман", Description = "История о любви и социальных различиях.", AuthorID = authors[7].Id, ImgPath = "covers/gord_pred.jpg" },
            new BookEntity { ISBN = "16-148410-8", Title = "Ослепленный", Genre = "Роман", Description = "Роман о слепоте и человечности.", AuthorID = authors[8].Id, ImgPath = "covers/slep.jpg" },
            new BookEntity { ISBN = "16-148410-9", Title = "Книга Природы", Genre = "Научная литература", Description = "Изучение природы и ее законов.", AuthorID = authors[9].Id, ImgPath = "covers/prir.jpg" }
            };

            try
            {
                _context.Books.AddRange(books);
                _context.SaveChanges();
                Console.WriteLine("Books DONE");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving books: {ex.Message}");
            }


            
        }
    }
}
