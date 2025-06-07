using AutoMapper;
using Codepulse.API.Application.DTOs.ImageUpload;
using Codepulse.API.Domain.Entities;
using Codepulse.API.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SwaggerOperationAttribute = Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute;

namespace Codepulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        //private readonly IImageRepository _imageRepository;
        //private readonly IMapper _mapper;

        //public ImageController(IImageRepository imageRepository, IMapper mapper)
        //{
        //    _imageRepository = imageRepository;
        //    _mapper = mapper;
        //}

        ////[HttpPost]
        ////[Consumes("multipart/form-data")]
        ////[SwaggerOperation(Summary = "Uploads an image", Description = "Allows users to upload an image file")]
        //public async Task<IActionResult> UploadImage(
        //    [FromForm, SwaggerParameter("The image file to upload")] IFormFile file,
        //    [FromForm, SwaggerParameter("The file name")] string fileName,
        //    [FromForm, SwaggerParameter("The image title")] string title)
        //{
        //    ValidateFileUpload(file);
        //    if (ModelState.IsValid)
        //    {
        //        var blogImage = new BlogImage
        //        {
        //            FileExtension = Path.GetExtension(file.FileName).ToLower(),
        //            FileName = fileName,
        //            Title = title,
        //            DateCreated = DateTime.Now,
        //        };

        //        await _imageRepository.Upload(file, blogImage);

        //        BlogImageToDisplay blogImageToDisplay = _mapper.Map<BlogImageToDisplay>(blogImage);

        //        return Ok(blogImageToDisplay);
        //    }
        //    return BadRequest(ModelState);
        //}

        //private void ValidateFileUpload(IFormFile file)
        //{
        //    var allowedExensions = new string[] { ".jpg", ".jpeg", ".png" };

        //    if (!allowedExensions.Contains(Path.GetExtension(file.FileName).ToLower()))
        //    {
        //        ModelState.AddModelError("FILE", "Unsupported file format");
        //    }
        //    if (file.Length > 1045760)
        //    {
        //        ModelState.AddModelError("FILE", "File size cannot be greater than 10mb");
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetImagesAsnyc()
        //{
        //    var images = await _imageRepository.GetAll();
        //    var imageToDisplay = _mapper.Map<List<BlogImageToDisplay>>(images);
        //    return Ok(imageToDisplay);
        //}
    }
}
