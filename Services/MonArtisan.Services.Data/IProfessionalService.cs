namespace MonArtisan.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IProfessionalService
    {
        Task<bool> UploadDocument(IFormFile file);
    }
}
