using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
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
using SocketIOClient;

namespace FileUpload
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            
            var socket = new Client("http://localhost:80");
            socket.On("txt", (data) =>
            {
                //MessageBox.Show(data.RawMessage);
                String msg = data.Json.Args[0].ToString();
                Console.Write(msg);
                //MessageBox.Show(msg, "Received Data");
            });
            socket.Connect();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();

            if (openfileDialog.ShowDialog() == true)
            {
                if (File.Exists(openfileDialog.FileName))
                {
                    try
                    {
                        if (moduleName.Text == "")
                        {
                            MessageBox.Show("Please type a module name!");
                            return;
                        }
                        WebClient myWebClient = new WebClient();

                        byte[] responseArray = myWebClient.UploadFile("http://185.232.166.115:6430/api/file/upload/" + moduleName.Text, openfileDialog.FileName);

                        MessageBox.Show(Encoding.ASCII.GetString(responseArray));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

    }

    public class ChatMessage
    {
        public string Username { get; set; }
        public string Message { get; set; }
        public long NumUsers { get; set; }
    }


}
