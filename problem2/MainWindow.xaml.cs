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

namespace problem2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            char[] splittingChar = {'-',' ','/',':','.' };
            var breed = txtBreed.Text;
            String apiStringFirst="";
            String apiStringSecond="";
            if (String.IsNullOrWhiteSpace(breed)==true)
            {
                MessageBox.Show("Breed name can not be empty");
                txtBreed.Clear();
            }
            else
            {
                string[] breedName=breed.Split(splittingChar);
                int breedNameCount = breedName.Count();
                if (breedNameCount==1)
                {
                    try
                    {
                        using (var client = new HttpClient())
                        {
                            var api = client.GetStringAsync(@"https://dog.ceo/api/breed/" + breedName[0] + @"/images/random").Result;

                            RandomDog randomDog = JsonConvert.DeserializeObject<RandomDog>(api);

                            if (randomDog.status.ToString() != "success")
                            {
                                MessageBox.Show("Breed name is invalid");
                            }
                            else
                            {
                                imgBreed.Source = new BitmapImage(new Uri(randomDog.message.ToString()));
                            }
                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Breed name is Invalid. try another name");
                        txtBreed.Clear();
                    }

                }

                else if (breedNameCount>2)
                {
                    MessageBox.Show("It is an Invalid Name. Please Retype.");
                    txtBreed.Clear();
                }
                else
                {
                    
                            for (int i = 0; i < breedName.Length; i++)
                            {
                                if (breedName[i].ToLower().Contains("dog"))
                                {
                                    apiStringFirst = breedName[i].ToLower();
                                }
                                else
                                {
                                    apiStringSecond = breedName[i].ToLower();
                                }
                            }

                    try
                    {
                        using (var client = new HttpClient())
                        {

                            var api = client.GetStringAsync("https://dog.ceo/api/breed/" + apiStringFirst + "/" + apiStringSecond + "/images/random").Result;

                            RandomDog randomDog = JsonConvert.DeserializeObject<RandomDog>(api);

                            if (randomDog.status.ToString() != "success")
                            {
                                MessageBox.Show("Breed name is invalid");
                            }
                            else
                            {

                                imgBreed.Source = new BitmapImage(new Uri(randomDog.message.ToString()));
                            }

                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Breed name is Invalid. try another name");
                        txtBreed.Clear();
                    }
                           
                }

            }


        }
    }
}
