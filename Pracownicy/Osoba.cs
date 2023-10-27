namespace Pracownicy
{
    [Serializable]
    public class Osoba
    {
        private static string _whoami, _loginzalogowanego;


        private string imie, nazwisko, pesel, login, haslo;

        public static string whoami
        {
            get { return _whoami; }
            set { _whoami = value; }
        }

        public static string loginzalogowanego
        {
            get { return _loginzalogowanego; }
            set { _loginzalogowanego = value; }
        }
        public string Imie
        {
            get { return imie; }
            set { imie = value; }
        }
        public string Nazwisko
        {
            get { return nazwisko; }
            set { nazwisko = value; }
        }
        public string Pesel
        {
            get { return pesel; }
            set { pesel = value; }
        }
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Haslo
        {
            get { return haslo; }
            set { haslo = value; }
        }
        public Osoba(string imie, string nazwisko, string pesel, string login, string haslo)
        {
            this.Imie = imie;
            this.Nazwisko = nazwisko;
            this.Pesel = pesel;
            this.Login = login;
            this.Haslo = haslo;

        }

        public static bool logowanie(string podanylogin, string podanehaslo, List<Osoba> ListaOsob)
        {
            foreach (var i in ListaOsob)
            {

                if (podanylogin == i.Login && podanehaslo == i.Haslo)
                {
                    if (i.GetType() == typeof(Administrator))
                    {
                        whoami = "Admin";
                    }
                    else if (i.GetType() == typeof(Lekarz))
                    {
                        whoami = "Lekarz";
                    }
                    else if (i.GetType() == typeof(Pielegniarka))
                    {
                        whoami = "Pielegniarka";
                    }

                    loginzalogowanego = i.Login;

                    Console.Clear();
                    Console.WriteLine($"Witaj {i.Imie} {i.Nazwisko}! Twoja rola w szpitalu to: {whoami.ToLower()}\n");
                    return true;

                }

            }
            return false;
        }

        virtual public void Wyswietl()
        {
            if (whoami == "Admin")
            {

                Console.Write($"{imie} {nazwisko} {pesel} {login} {haslo} ");
            }
            else
            {
                Console.Write($"{imie} {nazwisko} ");
            }

        }




        

    }
}