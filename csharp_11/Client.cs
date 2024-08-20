using System;

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
        /// Создание клиента
        /// </summary>
        /// <param name="Fam"></param>
        /// <param name="Name"></param>
        /// <param name="Otch"></param>
        /// <param name="Number"></param>
        /// <param name="Pass"></param>
        public Client(string Fam, string Name, string Otch, string Number, string Pass)
        {
            this.Fam = Fam;
            this.Name = Name;
            this.Otch = Otch;
            this.Number = Number;
            this.Pass = Pass;
        }
    }
}
