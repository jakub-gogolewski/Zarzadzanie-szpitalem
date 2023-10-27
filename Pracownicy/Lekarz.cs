using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pracownicy
{
    [Serializable]
    public class Lekarz : Pielegniarka
    {
        private string specjalizacja, pwz;


        public string Specjalizacja
        {
            get { return specjalizacja; }
            set { specjalizacja = value; }
        }
        public string Pwz
        {
            get { return pwz; }
            set { pwz = value; }
        }

        public Lekarz(string imie, string nazwisko, string pesel, string login, string haslo, string specjalizacja, string pwz) : base(imie, nazwisko, pesel, login, haslo)
        {
            this.Specjalizacja = specjalizacja;
            this.Pwz = pwz;
        }

        public override void Wyswietl()
        {
            base.Wyswietl();
            if (whoami == "Admin")
                Console.Write($"{specjalizacja} {pwz} {GetType().Name.ToLower()}");
            else 
                Console.Write($"{GetType().Name.ToLower()} {specjalizacja}");

        }

        public override void DodajDyzury(DateTime data)
        {
            base.DodajDyzury(data);
        }

        public override void WyswietlDyzury()
        {
            base.WyswietlDyzury();
        }

        public override bool max10(DateTime data)
        {

            return base.max10(data);

            
        }

        public override bool break24(DateTime data)
        {
            return base.break24(data);
        }

        public bool powtorka_specjalizacji(List<Osoba> ListaOsob, Osoba displayed, DateTime data)
        {
            foreach(var i in ListaOsob)
            {
                if(i.GetType().Name == "Lekarz" ) { 
                    bool sprawdzenie = ((Lekarz)i).Specjalizacja == ((Lekarz)displayed).Specjalizacja && ((Lekarz)i).ListaDyzurow.Contains(data);
                    if (sprawdzenie) return true;

                }
            }

            return false;
        }

    }
}
