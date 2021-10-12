using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace problem3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RandomVideo video;
        
        public MainWindow()
        {
            InitializeComponent();

            btnPlayPause.IsEnabled = false;
            btnNext.IsEnabled = false;
            btnStop.IsEnabled = false;
            videoframe.LoadedBehavior = MediaState.Manual;

        }

        private void btnPlayPause_Click(object sender, RoutedEventArgs e)
        {
            string content = btnPlayPause.Content.ToString().ToLower();

            switch (content)
            {
                case "play":
                    videoframe.Play();
                    btnPlayPause.Content = "Pause";
                    break;
                case "pause":
                    videoframe.Pause();
                    btnPlayPause.Content = "Play";
                    break;
            }

        }

        private void Get_Random_Video_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {

                var api = client.GetAsync("http://pcbstuou.w27.wh-2.com/webservices/3033/api/random/video").Result;

                if (api.IsSuccessStatusCode)
                {

                    video = JsonConvert.DeserializeObject<RandomVideo>(api.Content.ReadAsStringAsync().Result);
                }
            }

            Uri url = new Uri(video.url.ToString());
            videoframe.Source = url;
            btnPlayPause.IsEnabled = true;
            btnNext.IsEnabled = true;
            btnStop.IsEnabled = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            videoframe.Stop();
            btnPlayPause.Content = "Play";
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {

                var api = client.GetAsync("http://pcbstuou.w27.wh-2.com/webservices/3033/api/random/video").Result;

                if (api.IsSuccessStatusCode)
                {

                    video = JsonConvert.DeserializeObject<RandomVideo>(api.Content.ReadAsStringAsync().Result);
                }
            }

            Uri url = new Uri(video.url.ToString());
            videoframe.Source = url;
            videoframe.Pause();
            btnPlayPause.Content = "Play";

        }
    }
}
