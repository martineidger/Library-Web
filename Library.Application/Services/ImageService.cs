﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Core.Abstractions.ServicesAbstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Library.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly string _storagePath;
        private readonly IWebHostEnvironment _env;

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
            //_storagePath =  "/Covers";
            _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }


        public async Task<string> SaveAsync(IFormFile file)
        {
            //var foldName = "Covers";
            var foldName = _storagePath;

            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is not provided.");
            }

            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp" };
            if (!allowedTypes.Contains(file.ContentType))
            {
                throw new ArgumentException("File is not a valid image type.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("File does not have a valid image extension.");
            }

            var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(_storagePath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/{foldName}/{uniqueFileName}";
        }

        public async Task<bool> DeleteAsync(string fileName)
        {
            var filePath = Path.Combine(_storagePath, fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", fileName);
            }

            File.Delete(filePath);
            return await Task.FromResult(true); 
        }
    }
}
