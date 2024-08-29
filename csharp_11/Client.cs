using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Policy;
using System.Windows.Documents;

namespace csharp_11
{
    class Client
    {
        //Параметры клиента
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Fam { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Otch { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Серия, номер паспорта
        /// </summary>
        public string Pass { get; set; }

        /// <summary>
        /// Счета
        /// </summary>
        public ObservableCollection<Account> accounts { get; set; }
       

        /// <summary>
        /// Создание клиента
        /// </summary>
        /// <param name="Fam"></param>
        /// <param name="Name"></param>
        /// <param name="Otch"></param>
        /// <param name="Number"></param>
        /// <param name="Pass"></param>
        /// <param name="accs"></param>
        public Client(string Fam, string Name, string Otch, string Number, string Pass)
        {
            this.Fam = Fam;
            this.Name = Name;
            this.Otch = Otch;
            this.Number = Number;
            this.Pass = Pass;
            accounts = new ObservableCollection<Account>();
        }
    }

    //csharp_12

    class Account
    {
        public int Index { get; set; }

        public double Money { get; set; }


        public Account(int Index, double Money)
        {
            this.Index = Index;
            this.Money = Money;
        }
    }
}
