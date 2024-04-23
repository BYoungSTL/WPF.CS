using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Windows.Input;
using WPF.CS.Commands;
using WPF.CS.DTOs;
using System.IO;

namespace WPF.CS.Models
{
    public class ImageModel : INotifyPropertyChanged
    {
        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private byte[] _data;
        public byte[] Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged(nameof(Data));
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (_submitCommand == null)
                {
                    _submitCommand = new RelayCommand(
                        param => Submit(),
                        param => true // Always allow submit
                    );
                }
                return _submitCommand;
            }
        }

        private async void Submit()
        {
            if (this != null)
            {
                var image = new ImageDTO()
                {
                    Data = File.ReadAllBytes(ImagePath),
                    FileName = Path.GetFileName(ImagePath),
                    Text = Comment
                };

                using var client = new HttpClient();

                var content = JsonConvert.SerializeObject(image);
                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await client.PostAsync("https://localhost:7202/SaveImage", byteContent);
            }
        }
    }
}
