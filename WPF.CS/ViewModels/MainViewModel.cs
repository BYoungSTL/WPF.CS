using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Input;
using WPF.CS.Commands;
using WPF.CS.DTOs;
using WPF.CS.Models;

namespace WPF.CS.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ImageModel> _loadedImages = new ObservableCollection<ImageModel>();
        public ObservableCollection<ImageModel> LoadedImages
        {
            get { return _loadedImages; }
            set
            {
                _loadedImages = value;
                OnPropertyChanged(nameof(LoadedImages));
            }
        }

        private ObservableCollection<ImageModel> _images = new ObservableCollection<ImageModel>();

        public ObservableCollection<ImageModel> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnPropertyChanged(nameof(Images));
            }
        }

        private ImageModel _selectedImage;
        public ImageModel SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }

        private ICommand _addImageCommand;
        public ICommand AddImageCommand
        {
            get
            {
                if (_addImageCommand == null)
                {
                    _addImageCommand = new RelayCommand(
                        param => AddImage(),
                        param => true // Always allow adding image
                    );
                }
                return _addImageCommand;
            }
        }

        private void AddImage()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    var image = new ImageModel() { ImagePath = filePath };
                    Images.Add(image);
                }
            }
        }

        private ICommand _loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new RelayCommand(
                        param => Load(),
                        param => true // Always allow load
                    );
                }
                return _loadCommand;
            }
        }

        private async void Load()
        {
            using var client = new HttpClient();

            var images = await client.GetFromJsonAsync<List<ImageDTO>>("https://localhost:7202/allImages");

            if (images != null)
            {
                LoadedImages = new ObservableCollection<ImageModel>();

                foreach (var image in images)
                {
                    LoadedImages.Add(new ImageModel() { Comment = image.Text, ImagePath = image.FileName, Data = image.Data });
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
