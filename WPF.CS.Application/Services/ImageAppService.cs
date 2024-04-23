using ExifLibrary;
using System.Text.RegularExpressions;
using WPF.CS.Application.Helpers;
using WPF.CS.Application.Interfaces;
using WPF.CS.Application.ViewModels;

namespace WPF.CS.Application.Services
{
    public class ImageAppService : IImageAppService//(ImageService imageService) : IImageAppService
    {
        public List<ImageViewModel> GetAll()
        {
            var directory = Path.Combine(Environment.CurrentDirectory, @"Data\");
            var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
            var images = new List<ImageViewModel>();

            foreach (string file in files)
            {
                if (!Regex.IsMatch(file, @"\.jpg$|\.png$|\.bmp$"))
                    continue;

                var image = new ImageViewModel()
                {
                    FileName = Path.GetFileName(file),
                    Data = File.ReadAllBytes(file)
                };
                
                var exifFile = ImageFile.FromFile(file);
                image.Text = exifFile.Properties.Get(ExifTag.ImageDescription).Value.ToString() ?? string.Empty;

                images.Add(image);
            }

            return images;
        }

        public async Task SaveImageAsync(ImageViewModel viewModel)
        {
            //var image = new Image()
            //{
            //    EntityId = Guid.NewGuid(),
            //    Text = viewModel.Text,
            //    Data = viewModel.Data,
            //    FileName = viewModel.FileName
            //};

            ImageHelper.SaveImage(viewModel);

            //await imageService.AddAsync(image);
        }
    }
}
