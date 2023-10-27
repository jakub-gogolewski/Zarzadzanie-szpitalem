using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pracownicy
{
    [Serializable]
    public class Administrator : Osoba
    {
        public Administrator(string Imie, string Nazwisko, string Pesel, string Login, string Haslo) : base(Imie, Nazwisko, Pesel, Login, Haslo)
        {

        }

        public override void Wyswietl()
        {
            base.Wyswietl();
            Console.Write($"{GetType().Name.ToLower()}");
        }

    }
}
