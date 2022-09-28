using System;
using Nest.Models;
using NuGet.Protocol.Plugins;

namespace Nest.Utilities
{
    public static class FileValidator
    {
        public static async Task<string> FileCreate(this IFormFile file,string root, string folder)
        {
            string fileName = String.Concat(Guid.NewGuid(), file.FileName);
            string path = Path.Combine(root, folder);
            string filePath = Path.Combine(path, fileName);
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception)
            {
                throw new FileLoadException();
            }

            return fileName;
        }


        public static bool ImageIsOkay(this IFormFile file, int mb)
        {
            return file.Length / 1024 / 1024 < mb && file.ContentType.Contains("image/");
            
        }



    }
}

