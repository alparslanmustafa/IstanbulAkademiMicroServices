using AkademiECommerce.Shared.ControllerBases;
using AkademiECommerce.Shared.Dtos;
using ECommerce.Services.PhotoStock.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile != null & formFile.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", formFile.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await formFile.CopyToAsync(stream, cancellationToken);
                var returnPath = formFile.FileName;
                PhotoDto photoDto = new()
                {
                    URL = returnPath
                };
                return CreateActionResultInstance(ResponseDTO<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResultInstance(ResponseDTO<PhotoDto>.Fail("Bir hata oluştu.",400));
        }
        [HttpDelete]
        public IActionResult PhotoDelete(string photoURL)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoURL);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(ResponseDTO<NoContent>.Fail("Fotoğraf bulunamadı", 404));
            }
            System.IO.File.Delete(path);
            return CreateActionResultInstance(ResponseDTO<NoContent>.Success(204));
        }
    }
}
