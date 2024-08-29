using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;

namespace csharp_11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Vars
        static ObservableCollection<Client> clients = new ObservableCollection<Client>();
        static string path = "emp.csv";
        bool managerBool = false;
        bool consultantBool = false;
        bool listBoxClick = false;
        #endregion

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
            if (managerBool)
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
            if (FamNew.Text != String.Empty 
                && NameNew.Text != String.Empty 
                && OtchNew.Text != String.Empty 
                && NumberNew.Text != String.Empty 
                && PassNew.Text != String.Empty)
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
            AccountsListBox.ItemsSource = client.accounts;
            TransferFromComboBox.ItemsSource = client.accounts;
            TransferToComboBox.ItemsSource = client.accounts;
            ButtonTB.IsEnabled = true;
            listBoxClick = true;
        }

        /// <summary>
        /// Чтение всего файла с рабочими и возврат массива данных
        /// </summary>
        /// <returns></returns>
        static ObservableCollection<Client> GetAllClients()
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
            if (managerBool)
            {
                FamTB.Text = client.Fam;
                NameTB.Text = client.Name;
                OtchTB.Text = client.Otch;
                NumberTB.Text = client.Number;
                PassTB.Text = client.Pass;
            }
            else if (consultantBool)
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
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. " +
                        $"Имя: {client.Name}, " +
                        $"Отчество: {client.Otch}, " +
                        $"Номер: {client.Number}, " +
                        $"Паспорт: {client.Pass}";
                    OptionsChange(client);
                }
                else if (client.Number == String.Empty && client.Pass != String.Empty)
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. " +
                        $"Имя: {client.Name}, " +
                        $"Отчество: {client.Otch}, " +
                        $"Номер: {"отсутствует"}, " +
                        $"Паспорт: {client.Pass}";
                    OptionsChange(client);
                }
                else if (client.Number != String.Empty && client.Pass == String.Empty)
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. " +
                        $"Имя: {client.Name}, " +
                        $"Отчество: {client.Otch}, " +
                        $"Номер: {client.Number}, " +
                        $"Паспорт: {"отсутствует"}";
                    OptionsChange(client);
                }
                else
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. " +
                        $"Имя: {client.Name}, " +
                        $"Отчество: {client.Otch}, " +
                        $"Номер: {"отсутствует"}, " +
                        $"Паспорт: {"отсутствует"}";
                    OptionsChange(client);
                }
            }
            //Вывод информации о клиента для консультанта
            else if (consultantBool)
            {
                if (client.Number != String.Empty && client.Pass != String.Empty)
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. " +
                        $"Имя: {client.Name}, " +
                        $"Отчество: {client.Otch}, " +
                        $"Номер: {client.Number}, " +
                        $"Паспорт: {"***"}";
                    OptionsChange(client);
                }
                else if (client.Number == String.Empty && client.Pass != String.Empty)
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. " +
                        $"Имя: {client.Name}, " +
                        $"Отчество: {client.Otch}, " +
                        $"Номер: {"отсутствует"}, " +
                        $"Паспорт: {"***"}";
                    OptionsChange(client);
                }
                else if (client.Number != String.Empty && client.Pass == String.Empty)
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. " +
                        $"Имя: {client.Name}, " +
                        $"Отчество: {client.Otch}, " +
                        $"Номер: {client.Number}, " +
                        $"Паспорт: {"отсутствует"}";
                    OptionsChange(client);
                }
                else
                {
                    ClientInformationtextBlock.Text = $"Фамилия: {client.Fam}. " +
                        $"Имя: {client.Name}, " +
                        $"Отчество: {client.Otch}, " +
                        $"Номер: {"отсутствует"}, " +
                        $"Паспорт: {"отсутствует"}";
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
            ClientsListBox.IsEnabled = true;  //Включение листбокса с клиентами
            ManagerButton.IsEnabled = false;  //Отключение активности нажатой кнопки менеджера
            ConsultantButton.IsEnabled = true;  //Включение активности кнопки консультанта

            FamTB.IsEnabled = true;  //Включение окошка изменения фамилии
            NameTB.IsEnabled = true;  //Включение окошка изменения имени
            OtchTB.IsEnabled = true;  //Включение окошка изменения отчества
            NumberTB.IsEnabled = true;  //Включение окошка изменения номера
            PassTB.IsEnabled = true;  //Включение окошка изменения паспорта

            ButtonNew.IsEnabled = true;  //Включение кнопки добавления нового клиента для менеджера
            FamNew.IsEnabled = true;  //Включение окошка добавления фамилии
            NameNew.IsEnabled = true;  //Включение окошка добавления имени
            OtchNew.IsEnabled = true;  //Включение окошка добавления отчества
            NumberNew.IsEnabled = true;  //Включение окошка добавления номера
            PassNew.IsEnabled = true;  //Включение окошка добавления паспорта

            consultantBool = false;
            managerBool = true;

            if (listBoxClick)  //Проверка нажатия на окошко с клиентами
            {
                Client client = ClientsListBox.SelectedItem as Client;
                ClientInformation(client);
            }
        }

        /// <summary>
        /// Логика нажатия на кнопку консультанта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsultantButton_Click(object sender, RoutedEventArgs e)
        {
            ClientsListBox.IsEnabled = true;  //Включение листбокса с клиентами
            ConsultantButton.IsEnabled = false;  //Отключение активности нажатой кнопки консультанта
            ManagerButton.IsEnabled = true;  //Включение активности кнопки менеджера

            FamTB.IsEnabled = false;  //Отключение окошка изменения фамилии
            NameTB.IsEnabled = false;  //Отключение окошка изменения имени
            OtchTB.IsEnabled = false;  //Отключение окошка изменения отчества
            NumberTB.IsEnabled = true;  //Отключение окошка изменения номера
            PassTB.IsEnabled = false;  //Отключение окошка изменения паспорта

            ButtonNew.IsEnabled = false;  //Отключение кнопки добавления нового клиента для консультанта
            FamNew.IsEnabled = false;  //Отключение окошка добавления фамилии
            NameNew.IsEnabled = false;  //Отключение окошка добавления имени
            OtchNew.IsEnabled = false;  //Отключение окошка добавления отчества
            NumberNew.IsEnabled = false;  //Отключение окошка добавления номера
            PassNew.IsEnabled = false;  //Отключение окошка добавления паспорта



            managerBool = false;
            consultantBool = true;

            if (listBoxClick)  //Проверка нажатия на окошко с клиентами
            {
                Client client = ClientsListBox.SelectedItem as Client;
                ClientInformation(client);
            }
        }



        //Методы для 1 задания 12 практической



        /// <summary>
        /// Создание нового счета для клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = ClientsListBox.SelectedItem as Client;

            if(double.TryParse(NewMoneyTextBox.Text, out double i) && i >= 0)
            {
                client.accounts.Add(new Account(client.accounts.Count + 1, i));
            }
        }

        /// <summary>
        /// Закрытие счета клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = ClientsListBox.SelectedItem as Client;
            Account account = AccountsListBox.SelectedItem as Account;
            client.accounts.Remove(account);
        }

        /// <summary>
        /// Перевод средств с одного счета клиента, на другой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            Account accountFrom = TransferFromComboBox.SelectedItem as Account;
            Account accountTo = TransferToComboBox.SelectedItem as Account;

            if(accountFrom != null && accountTo != null)
            {
                if (double.TryParse(TransferMoneyTextBox.Text, out double i) && i >= 0 && i <= accountFrom.Money)
                {
                    accountFrom.Money = accountFrom.Money - i;
                    accountTo.Money = accountTo.Money + i;
                }
            }
        }
    }
}
