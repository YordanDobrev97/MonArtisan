namespace MonArtisan.Services.Data
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class ProfessionalService : IProfessionalService
    {
        private readonly Cloudinary cloudinary;

        public ProfessionalService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<bool> UploadDocument(IFormFile file)
        {
            byte[] fileBytes;

            using var stream = new MemoryStream();
            file.CopyTo(stream);
            fileBytes = stream.ToArray();

            var destination = new MemoryStream(fileBytes);
            var fileName = $"{Guid.NewGuid().ToString()}";
            var uploadParameters = new ImageUploadParams()
            {
                File = new FileDescription(fileName, destination),
                PublicId = file.FileName,
                Folder = "proDocuments/pdf",
            };

            var result = await this.cloudinary.UploadAsync(uploadParameters);
            return true;
        }
    }
}
