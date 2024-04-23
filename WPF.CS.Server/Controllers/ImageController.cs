using Microsoft.AspNetCore.Mvc;
using WPF.CS.Application.Interfaces;
using WPF.CS.Application.ViewModels;

namespace WPF.CS.Server.Controllers
{
    [ApiController]
    public class ImageController(IImageAppService imageAppService) : ControllerBase
    {
        [HttpGet("allImages")]
        public IActionResult GetImages()
        {
            return Ok(imageAppService.GetAll());
        }

        [HttpPost("SaveImage")]
        public async Task<IActionResult> SaveFile(ImageViewModel viewModel)
        {
            await imageAppService.SaveImageAsync(viewModel);
            return Ok();
        }
    }
}
