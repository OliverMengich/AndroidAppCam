using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace CBCamera
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(!(CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported))
            {
                await DisplayAlert("No Camera", "No Camera Available", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory="Sample",
                Name="Test.jpg"
            });

            if (file == null)
                return;
            await DisplayAlert("File Location",file.Path,"OK");

            image.Source = ImageSource.FromStream(() =>
              {
                  var stream = file.GetStream();
                  file.Dispose();
                  return stream;
              });
        }
        
    }
}
