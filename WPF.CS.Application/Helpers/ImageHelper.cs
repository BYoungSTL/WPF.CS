using System.Drawing;
using System.Drawing.Imaging;
using WPF.CS.Application.ViewModels;

namespace WPF.CS.Application.Helpers
{
    //This code will work only on Windows 7 and higher
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
    public static class ImageHelper
    {
        public static bool SaveImage(ImageViewModel viewModel)
        {
            using var stream = new MemoryStream(viewModel.Data);

            var filePath = Path.Combine(Environment.CurrentDirectory, @"Data\", $"{viewModel.FileName}");
            var image = Image.FromStream(stream);
            var imageCodecInfo = GetEncoderInfo("image/jpeg");

            if (imageCodecInfo == null)
                return false;

            var encoder = Encoder.Quality;
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(encoder, 50L);

            image.Save(filePath, imageCodecInfo, encoderParameters);

            var exifFile = ExifLibrary.ImageFile.FromFile(filePath);

            exifFile.Properties.Set(ExifLibrary.ExifTag.ImageDescription, viewModel.Text);
            exifFile.Save(filePath);

            return true;
        }

        private static ImageCodecInfo? GetEncoderInfo(string mimeType)
        {
            var encoders = ImageCodecInfo.GetImageEncoders();

            foreach (var encoder in encoders)
            {
                if (encoder.MimeType == mimeType)
                    return encoder;
            }
            return null;
        }
    }
}
