using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pracownicy
{
    [Serializable]
    public class Pielegniarka : Osoba
    {

        private List<DateTime> _ListaDyzurow;

        public List<DateTime> ListaDyzurow
        {
            get { return _ListaDyzurow; }
            set { _ListaDyzurow = value; }

        }

        public Pielegniarka(string Imie, string Nazwisko, string Pesel, string Login, string Haslo) : base(Imie, Nazwisko, Pesel, Login, Haslo)
        {
            this.ListaDyzurow = new List<DateTime>();
        }

        public override void Wyswietl()
        {
            base.Wyswietl();

            if (GetType().Name.ToLower() == "pielegniarka") Console.Write($"{GetType().Name.ToLower()}");

        }


        public virtual void DodajDyzury(DateTime data)
        {
            this.ListaDyzurow.Add(data);
        }

        public virtual void WyswietlDyzury()
        {
            ListaDyzurow.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            foreach (var data in ListaDyzurow)
            {
                Console.WriteLine(data.ToShortDateString());
            }
        }
        public virtual bool max10(DateTime data)
        {

                if (ListaDyzurow.Count(d => d.Month == data.Month && d.Year == data.Year) >= 10)
                {
                    return true;
                }

            return false;
        }

        public virtual bool break24(DateTime data)
        {

            foreach (var i in ListaDyzurow)
            {
                if (data.Subtract(i).Days == 1 || data.Subtract(i).Days == -1) {
                    Console.WriteLine("Należy zachować 24h przerwy między dyżurami!");
                    return true;
                }
                if (data.Subtract(i).Days == 0)
                {
                    Console.WriteLine("W tym dniu istnieje już dyżur!");
                    return true;
                }
            }
            return false;
        }



    }
}