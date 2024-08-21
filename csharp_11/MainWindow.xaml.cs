using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace csharp_11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<Client> clients = new List<Client>();
        static string path = "emp.csv";
        bool managerBool = false;
        bool consultantBool = false;

        
        public MainWindow()
        {
            GetAllClients();
            InitializeComponent();
            ClientsListBox.ItemsSource = clients.OrderBy(client => client.Fam);
        }
       
        /// <summary>
        /// Кнопка изменения параметров клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTB_Click(object sender, RoutedEventArgs e)
        {
            Client client1 = ClientsListBox.SelectedItem as Client;
            if(managerBool)
            {
                client1.Fam = FamTB.Text;
                client1.Name = NameTB.Text;
                client1.Otch = OtchTB.Text;
                client1.Number = NumberTB.Text;
                client1.Pass = PassTB.Text;
            }
            else if (consultantBool)
            {
                client1.Number = NumberTB.Text;
            }

            ClientsListBox.ItemsSource = clients.OrderBy(client => client.Fam);
        }

        /// <summary>
        /// Кнопка добавления нового клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            if(FamNew.Text != String.Empty && NameNew.Text != String.Empty && OtchNew.Text != String.Empty && NumberNew.Text != String.Empty && PassNew.Text != String.Empty)
            {
                clients.Add(new Client(
                               FamNew.Text,                       //Фамилия
                               NameNew.Text,                      //Имя
                               OtchNew.Text,                      //Отчество
                               NumberNew.Text,                    //Номер телефона
                               PassNew.Text));                    //Паспорт
            }

            ClientsListBox.ItemsSource = clients.OrderBy(client => client.Fam);
        }

        /// <summary>
        /// ListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client client = ClientsListBox.SelectedItem as Client;
            ClientInformation(client);
            ManagerButton.IsEnabled = true;
            ConsultantButton.IsEnabled = true;
            ButtonTB.IsEnabled = true;
        }

        /// <summary>
        /// Чтение всего файла с рабочими и возврат массива данных
        /// </summary>
        /// <returns></returns>
        static List<Client> GetAllClients()
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string[] lines = sr.ReadToEnd().Split('\n');    //Массив строк

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i] != string.Empty)
                        {
                            string[] words = lines[i].Split('#');       //Массив слов

                            clients.Add(new Client(
                                words[0],                      //Фамилия
                                words[1],                      //Имя
                                words[2],                      //Отчество
                                words[3],                      //Номер телефона
                                words[4]));                    //Паспорт
                        }
                    }
                }
            }
            return clients;
        }

        /// <summary>
        /// Включение кнопок в соответствии с должностью
        /// </summary>
        /// <param name="client"></param>
        private void OptionsChange(Client client)
        {
            if(managerBool)
            {
                FamTB.Text = client.Fam;
                NameTB.Text = client.Name;
                OtchTB.Text = client.Otch;
                NumberTB.Text = client.Number;
                PassTB.Text = client.Pass;
            }
            else if(consultantBool)
            {
                FamTB.Text = client.Fam;
                NameTB.Text = client.Name;
                OtchTB.Text = client.Otch;
                NumberTB.Text = client.Number;
                PassTB.Text = "***";
            }
        }

        /// <summary>
        /// Вывод информации о клиенте
        /// </summary>
        /// <param name="client"></param>
        private void ClientInformation(Client client)
        {
            //Вывод информации о клиента для менеджера
            if (managerBool)
            {
                if (client.Number != String.Empty && client.Pass != String.Empty)
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. Имя: {client.Name}, Отчество: {client.Otch}, Номер: {client.Number}, Паспорт: {client.Pass}";
                    OptionsChange(client);
                }
                else if (client.Number == String.Empty && client.Pass != String.Empty)
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. Имя: {client.Name}, Отчество: {client.Otch}, Номер: {"отсутствует"}, Паспорт: {client.Pass}";
                    OptionsChange(client);
                }
                else if (client.Number != String.Empty && client.Pass == String.Empty)
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. Имя: {client.Name}, Отчество: {client.Otch}, Номер: {client.Number}, Паспорт: {"отсутствует"}";
                    OptionsChange(client);
                }
                else
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. Имя: {client.Name}, Отчество: {client.Otch}, Номер: {"отсутствует"}, Паспорт: {"отсутствует"}";
                    OptionsChange(client);
                }
            }
            //Вывод информации о клиента для консультанта
            else if (consultantBool)
            {
                if (client.Number != String.Empty && client.Pass != String.Empty)
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. Имя: {client.Name}, Отчество: {client.Otch}, Номер: {client.Number}, Паспорт: {"***"}";
                    OptionsChange(client);
                }
                else if (client.Number == String.Empty && client.Pass != String.Empty)
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. Имя: {client.Name}, Отчество: {client.Otch}, Номер: {"отсутствует"}, Паспорт: {"***"}";
                    OptionsChange(client);
                }
                else if (client.Number != String.Empty && client.Pass == String.Empty)
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. Имя: {client.Name}, Отчество: {client.Otch}, Номер: {client.Number}, Паспорт: {"отсутствует"}";
                    OptionsChange(client);
                }
                else
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. Имя: {client.Name}, Отчество: {client.Otch}, Номер: {"отсутствует"}, Паспорт: {"отсутствует"}";
                    OptionsChange(client);
                }
            }
        }

        /// <summary>
        /// Логика нажатия на кнопку менеджера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManagerButton_Click(object sender, RoutedEventArgs e)
        {
            FamTB.IsEnabled = true;
            NameTB.IsEnabled = true;
            OtchTB.IsEnabled = true;
            NumberTB.IsEnabled = true;
            PassTB.IsEnabled = true;

            ButtonNew.IsEnabled = true;
            FamNew.IsEnabled = true;
            NameNew.IsEnabled = true;
            OtchNew.IsEnabled = true;
            NumberNew.IsEnabled = true;
            PassNew.IsEnabled = true;

            consultantBool = false;
            managerBool = true;

            Client client = ClientsListBox.SelectedItem as Client;
            ClientInformation(client);
        }

        /// <summary>
        /// Логика нажатия на кнопку консультанта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsultantButton_Click(object sender, RoutedEventArgs e)
        {
            FamTB.IsEnabled = false;
            NameTB.IsEnabled = false;
            OtchTB.IsEnabled = false;
            NumberTB.IsEnabled = true;
            PassTB.IsEnabled = false;

            ButtonNew.IsEnabled = false;
            FamNew.IsEnabled = false;
            NameNew.IsEnabled = false;
            OtchNew.IsEnabled = false;
            NumberNew.IsEnabled = false;
            PassNew.IsEnabled = false;

            managerBool = false;
            consultantBool = true;

            Client client = ClientsListBox.SelectedItem as Client;
            ClientInformation(client);
        }
    }
}
