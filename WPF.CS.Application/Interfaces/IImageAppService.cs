using WPF.CS.Application.ViewModels;

namespace WPF.CS.Application.Interfaces
{
    public interface IImageAppService
    {
        Task SaveImageAsync(ImageViewModel viewModel);

        List<ImageViewModel> GetAll();
    }
}
