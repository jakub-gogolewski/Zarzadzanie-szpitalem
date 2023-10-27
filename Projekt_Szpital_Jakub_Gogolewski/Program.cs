using Pracownicy;
using System;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows.Forms;


bool kontrola;

int wybor=6;
int who;
int ileadminow = 0;

char odpowiedz = 't';

string podawanylogin, podawanehaslo;
string imie, nazwisko, pesel, login, haslo, specjalizacja, pwz;
string n_imie, n_nazwisko, n_pesel, n_login, n_haslo, n_specjalizacja, n_pwz;
string typ;
string rola;

Regex lettersonly = new Regex(@"^[\p{L}]+$"); // regex obsługuje polskie znaki
Regex numsonly11 = new Regex(@"^[0-9]{11}$"); // regex pesel
Regex numsonly7 = new Regex(@"^[0-9]{7}$"); // regex pwz

var listaspecjalizacji = new string[] { "kardiolog", "urolog", "neurolog", "laryngolog" };
var listatypow = new string[] { "admin", "lekarz", "pielegniarka" };

List<Osoba> ListaOsob = new List<Osoba>();

BinaryFormatter binFormatter = new BinaryFormatter();

string format = "d/M/yyyy";
CultureInfo culture = new CultureInfo("pl-PL");
DateTime data;

//FUNKCJE
void clear()
{
    Console.WriteLine("\nWciśnij cokolwiek aby kontynuować\n");
    Console.ReadKey();
    Console.Clear();
}
string upperonstart(string txt)
{
    return char.ToUpper(txt[0]) + txt.Substring(1).ToLower();
}

void allworkers()
{
    ileadminow = 0;
    ListaOsob.Sort((x, y) => string.Compare(x.GetType().Name, y.GetType().Name));
    int counter = 1;
    if (Osoba.whoami == "Admin") Console.WriteLine("\nIMIE / NAZWISKO / PESEL / LOGIN / HASŁO / SPECJALIZACJA / PWZ / ROLA");
    else Console.WriteLine("\nIMIE / NAZWISKO / ROLA / SPECJALIZACJA");
    foreach (var i in ListaOsob)
    {
        if(Osoba.whoami == "Admin") {

        Console.Write($"{counter}. ");
        i.Wyswietl();
        Console.WriteLine();
        counter++;
        }
        else
        {

            if (i.GetType().Name == "Administrator")
            {
                ileadminow++;
            }
            var typ = i.GetType().Name.ToLower();
            if (typ=="pielegniarka" || typ == "lekarz") {
            Console.Write($"{counter}. ");
            i.Wyswietl();
            Console.WriteLine();
            counter++;
            }
        }
    }
}

bool PowtorkaLoginu(string login)
{
    foreach (var i in ListaOsob)
    {
        if (i.Login == login)
        {
            return true;
        }
    }
    return false;
}
void DodajNowego(string typ) // dodawanie nowego pracownika przez admina
{

    if (kontrola = String.IsNullOrWhiteSpace(typ))
    {
        Console.Write("\nTyp nie może być pusty! Podaj poprawny typ dodawanego pracownika (admin/lekarz/pielęgniarka): ");
        typ = Console.ReadLine();
        DodajNowego(typ);
    }

    typ = typ.ToLower();
    if (listatypow.Contains(typ))
    {

        if (typ == "admin")
            Console.WriteLine("Wprowadź dane nowego administratora:\n");

        if (typ == "pielegniarka")
            Console.WriteLine("Wprowadź dane nowej pielęgniarki:\n");

        if (typ == "lekarz")
            Console.WriteLine("Wprowadź dane nowego lekarza:\n");

        do
        {
            Console.Write("Podaj imie: ");
            imie = Console.ReadLine();

            if (kontrola = String.IsNullOrWhiteSpace(imie))
            {
                Console.WriteLine("Imie nie może być puste!");
            }
            else if (!lettersonly.IsMatch(imie))
            {
                kontrola = true;
                Console.WriteLine("Imie zawiera niedozwolone znaki - użyj tylko liter!");
            }
        } while (kontrola == true);

        do
        {
            Console.Write("\nPodaj nazwisko: ");
            nazwisko = Console.ReadLine();

            if (kontrola = String.IsNullOrWhiteSpace(nazwisko))
            {
                Console.WriteLine("Nazwisko nie może być puste!");
            }
            else if (!lettersonly.IsMatch(imie))
            {
                kontrola = true;
                Console.WriteLine("Imie zawiera niedozwolone znaki - użyj tylko liter!");
            }
        } while (kontrola == true);

        do
        {
            Console.Write("\nPodaj pesel: ");
            pesel = Console.ReadLine();
            if (numsonly11.IsMatch(pesel))
            {
                kontrola = false;
            }
            else
            {
                Console.WriteLine("Pesel musi mieć 11 cyfr!");
                kontrola = true;
            }
        } while (kontrola == true);

        do
        {
            Console.Write("\nPodaj login: ");
            login = Console.ReadLine();

            if (kontrola = String.IsNullOrWhiteSpace(login))
            {
                Console.WriteLine("Login nie może być pusty!");
            }
            if (PowtorkaLoginu(login))
            {
                kontrola = true;
                Console.WriteLine("Podany login istnieje już w systemie - wprowadź inny!");
            }
        } while (kontrola == true);

        do
        {
            Console.Write("\nPodaj hasło: ");
            haslo = Console.ReadLine();

            if (kontrola = String.IsNullOrWhiteSpace(haslo))
            {
                Console.WriteLine("Hasło nie może być puste!");
            }
        } while (kontrola == true);

        if (typ == "lekarz")
        {
            do
            {
                Console.Write("\nPodaj specjalizacje (kardiolog/urolog/neurolog/laryngolog): ");
                specjalizacja = Console.ReadLine();

                if (kontrola = String.IsNullOrWhiteSpace(specjalizacja))
                {
                    Console.WriteLine("Specjalziacja nie może być pusta!");
                }
                else if (!listaspecjalizacji.Contains(specjalizacja.ToLower()))
                {
                    kontrola = true;
                    Console.WriteLine("Podaj poprawną specjalizację! (kardiolog/urolog/neurolog/laryngolog)");
                }
            } while (kontrola == true);

            do
            {
                Console.Write("\nPodaj 7 cyfrowy numer PWZ: ");
                pwz = Console.ReadLine();
                if (numsonly7.IsMatch(pwz))
                {
                    kontrola = false;
                }
                else
                {
                    Console.WriteLine("Numer PWZ musi mieć 7 cyfr!");
                    kontrola = true;
                }
            } while (kontrola == true);

            ListaOsob.Add(new Lekarz(upperonstart(imie), upperonstart(nazwisko), pesel, login, haslo, specjalizacja.ToLower(), pwz));
            Console.WriteLine("Pomyślnie dodano nowego lekarza!");

        }

        if (typ == "admin")
        {
            ListaOsob.Add(new Administrator(upperonstart(imie), upperonstart(nazwisko), pesel, login, haslo));
            Console.WriteLine("Pomyślnie dodano nowego admina!");
        }

        if (typ == "pielegniarka")
        {
            ListaOsob.Add(new Pielegniarka(upperonstart(imie), upperonstart(nazwisko), pesel, login, haslo));
            Console.WriteLine("Pomyślnie dodano nową pielęgniarkę!");
        }

    }
    else if (!listatypow.Contains(typ))
    {
        Console.Write("\nPodaj poprawny typ dodawanego pracownika (admin/lekarz/pielęgniarka): ");
        typ = Console.ReadLine();
        DodajNowego(typ);
    }

}

void EdytujPracownika(int who)
{

    Console.Write("\nEdytowany pracownik to: ");
    var edited = ListaOsob[who - 1];
    edited.Wyswietl();

    Console.WriteLine();

    do
    {

        Console.Write("Wpisz nowe imie lub pozostaw stare: ");
        System.Windows.Forms.SendKeys.SendWait(edited.Imie);

        n_imie = Console.ReadLine();
        if (kontrola = String.IsNullOrEmpty(n_imie))
        {
            Console.WriteLine("Nie można zmienić na puste imie!");
        }
        else if (!lettersonly.IsMatch(n_imie))
        {
            kontrola = true;
            Console.WriteLine("Nowe imie zawiera niedozwolone znaki - użyj tylko liter!");
        }
    } while (kontrola == true);
    edited.Imie = upperonstart(n_imie);

    do
    {

        Console.Write("Wpisz nowe nazwisko lub pozostaw stare: ");
        System.Windows.Forms.SendKeys.SendWait(edited.Nazwisko);

        n_nazwisko = Console.ReadLine();
        if (kontrola = String.IsNullOrEmpty(n_nazwisko))
        {
            Console.WriteLine("Nie można zmienić na puste nazwisko!");
        }
        else if (!lettersonly.IsMatch(n_nazwisko))
        {
            kontrola = true;
            Console.WriteLine("Nowe nazwisko zawiera niedozwolone znaki - użyj tylko liter!");
        }
    } while (kontrola == true);
    edited.Nazwisko = upperonstart(n_nazwisko);

    do
    {

        Console.Write("Wpisz nowy pesel lub pozostaw stary: ");
        System.Windows.Forms.SendKeys.SendWait(edited.Pesel);

        n_pesel = Console.ReadLine();
        if (!numsonly11.IsMatch(n_pesel))
        {
            kontrola = true;
            Console.WriteLine("Nowy pesel musi zawierać 11 cyfr!");
        }
        else { kontrola = false; }
    } while (kontrola == true);
    edited.Pesel = n_pesel;

    do
    {

        Console.Write("Wpisz nowy login lub pozostaw stary: ");
        System.Windows.Forms.SendKeys.SendWait(edited.Login);

        n_login = Console.ReadLine();
        if (kontrola = String.IsNullOrWhiteSpace(n_login))
        {
            Console.WriteLine("Nowy login nie może być pusty!");
        }
        if (PowtorkaLoginu(n_login))
        {
            if (n_login != edited.Login)
            {
                kontrola = true;
                Console.WriteLine("Podany nowy login istnieje już w systemie - wprowadź inny!");
            }
        }
    } while (kontrola == true);
    edited.Login = n_login;

    do
    {

        Console.Write("Wpisz nowe hasło lub pozostaw stare: ");
        System.Windows.Forms.SendKeys.SendWait(edited.Haslo);

        n_haslo = Console.ReadLine();
        if (kontrola = String.IsNullOrEmpty(n_haslo))
        {
            Console.WriteLine("Nie można zmienić na puste hasło!");
        }
    } while (kontrola == true);
    edited.Haslo = n_haslo;

    if (edited.GetType().Name.ToLower() == "lekarz")
    {
        
        do
        {
            
            Console.Write("\nPodaj nową specjalizację lub pozostaw starą (kardiolog/urolog/neurolog/laryngolog): ");
            System.Windows.Forms.SendKeys.SendWait(((Lekarz)edited).Specjalizacja);
            n_specjalizacja = Console.ReadLine();

            if (kontrola = String.IsNullOrWhiteSpace(n_specjalizacja))
            {
                Console.WriteLine("Nowa specjalizacja nie może być pusta!");
            }
            else if (!listaspecjalizacji.Contains(n_specjalizacja.ToLower()))
            {
                kontrola = true;
                Console.WriteLine("Podaj poprawną nową specjalizację! (kardiolog/urolog/neurolog/laryngolog)");
            }
        } while (kontrola == true);
        ((Lekarz)edited).Specjalizacja = n_specjalizacja.ToLower();

        do
        {
            Console.Write("\nPodaj nowy 7 cyfrowy numer PWZ lub pozostaw stary: ");
            System.Windows.Forms.SendKeys.SendWait(((Lekarz)edited).Pwz);
            n_pwz = Console.ReadLine();
            if (numsonly7.IsMatch(n_pwz))
            {
                kontrola = false;
            }
            else
            {
                Console.WriteLine("Nowy numer PWZ musi mieć 7 cyfr!");
                kontrola = true;
            }
        } while (kontrola == true);
        ((Lekarz)edited).Pwz = n_pwz;
    }

    Console.WriteLine("\nPracownik po edycji: ");
    edited.Wyswietl();

}

void UsunPracownika(int who)
{
    var removed = ListaOsob[who - 1];
    if (removed.Login == Osoba.loginzalogowanego)
    {
        Console.WriteLine("Nie możesz usunąć obecnie zalogowanego użytkownika!"); 
    }
    Console.Write("\nUsuwany pracownik to: ");
    removed.Wyswietl();

    ListaOsob.Remove(removed);

    Console.WriteLine("\nPomyślnie usunięto pracownika!");
}

void WyswietlDyzur(int who)
{

    Osoba displayed = ListaOsob[who - 1];

    if (Osoba.whoami != "Admin")
    {
        displayed = ListaOsob[who - 1 + ileadminow ];
    }
    


    if (displayed.GetType().Name == "Lekarz")
    {
        Console.Write($"\nDyżury lekarza: ");
        displayed.Wyswietl();
        Console.Write(":\n");
        
        int counter = 0;

        foreach (var i in ((Lekarz)displayed).ListaDyzurow)
        {
            counter++;
            Console.Write($"{counter}. {i.Date.ToShortDateString()}");
            Console.WriteLine();
        }
        if (counter == 0)
        {
            Console.WriteLine("Ten pracownik nie ma dyżurów!");
        }
    }
    else if (displayed.GetType().Name == "Pielegniarka")
    {
        Console.Write($"\nDyżury pielęgniarki: ");
        displayed.Wyswietl();
        Console.Write(":\n");

        int counter = 0;

        foreach (var i in ((Pielegniarka)displayed).ListaDyzurow)
        {
            counter++;
            Console.Write($"{counter}. {i.Date.ToShortDateString()}");
            Console.WriteLine();
        }
        if (counter == 0)
        {
            Console.WriteLine("Ten pracownik nie ma dyżurów!");
        }
    }
    else if (displayed.GetType().Name == "Administrator")
    {
        Console.WriteLine("Administratorzy nie posiadają dyżurów!");

        do
        {
            Console.Write("\nWskaż numer pracownika, dla którego chcesz wyświetlić dyżury: ");
            kontrola = int.TryParse(Console.ReadLine(), out who);

            if (kontrola == false || who < 1 || who > ListaOsob.Count())
            {
                Console.WriteLine("Podaj poprawny numer z listy!");

            }
        } while (kontrola == false || who < 1 || who > ListaOsob.Count());

        WyswietlDyzur(who);

    }

}

void DodajDyzur(int who)
{
    Osoba displayed = ListaOsob[who - 1];


    do{
        char odpowiedz;
        do
        {
        Console.WriteLine("\nPodaj datę dyżuru, który chcesz dodać (Format: DZIEŃ/MIESIĄC/ROK)");
        kontrola = DateTime.TryParseExact(Console.ReadLine(), format, culture, 0, out data);

        if (kontrola == false)
        {
            Console.WriteLine("Podaj poprawną datę!\n");
        }

        } while (kontrola == false);


        if (displayed.GetType().Name == "Lekarz")
        {
        

        if (((Lekarz)displayed).max10(data))
        {
            Console.WriteLine("W tym miesiącu lekarz osiągnęł już maksymalne 10 zaplanowanych dużurów! Zmień datę!");
            DodajDyzur(who);
        }
        else if (((Lekarz)displayed).break24(data))
        {
                DodajDyzur(who);
        }
        else if (((Lekarz)displayed).powtorka_specjalizacji(ListaOsob,displayed,data))
        {
             Console.WriteLine($"W tym dniu ma już dyżur ze specjalizacją tego lekarza! ({((Lekarz)displayed).Specjalizacja})");
             DodajDyzur(who);
        }
            
        else if (!((Lekarz)displayed).max10(data) && !((Lekarz)displayed).break24(data) && !((Lekarz)displayed).powtorka_specjalizacji(ListaOsob, displayed, data)) 
        { 
            ((Lekarz)displayed).DodajDyzury(data);
            Console.Write($"\nPomyślnie dodano dyżur lekarzowi w dniu {data.ToShortDateString()}!");
        }

        }
        else if (displayed.GetType().Name == "Pielegniarka")
        {
        if (((Pielegniarka)displayed).max10(data))
        {
            Console.WriteLine("W tym miesiącu pielęgniarka osiągnęła już maksymalne 10 zaplanowanych dużurów! Zmień datę!");
            DodajDyzur(who);
        }
        else if (((Pielegniarka)displayed).break24(data))
        {
            DodajDyzur(who);
        }
        else if (!((Pielegniarka)displayed).max10(data) && !((Pielegniarka)displayed).break24(data)) 
        {
            ((Pielegniarka)displayed).DodajDyzury(data);
            Console.Write($"\nPomyślnie dodano dyżur pielęgniarce w dniu {data.ToShortDateString()}!");
        }
        }
        else if (displayed.GetType().Name == "Administrator")
        {
        Console.WriteLine("Administratorzy nie posiadają dyżurów!");

        do
        {
            Console.Write("\nWskaż numer pracownika, dla którego chcesz dodawać dyżury: ");
            kontrola = int.TryParse(Console.ReadLine(), out who);

            if (kontrola == false || who < 1 || who > ListaOsob.Count())
            {
                Console.WriteLine("Podaj poprawny numer z listy!");

            }
        } while (kontrola == false || who < 1 || who > ListaOsob.Count());

        DodajDyzur(who);

       }

        do
        {
            Console.Write("\nCzy chcesz dodać kolejny dyżury? (t/n): ");
            kontrola = char.TryParse(Console.ReadLine(), out odpowiedz);
            if (odpowiedz.ToString().ToLower() != "t" && odpowiedz.ToString().ToLower() != "n" || kontrola == false)
            {
                Console.WriteLine("Podaj poprawną odpowiedź!");
            }
        } while (odpowiedz.ToString().ToLower() != "t" && odpowiedz.ToString().ToLower() != "n" || kontrola == false);
    } while (odpowiedz.ToString().ToLower() == "t");
    
}

void EdytujDyzur(int who)
{

    Osoba displayed = ListaOsob[who - 1];
    char odpowiedz = 'n';
    int numer;
    DateTime n_data;

    do { 
    if (displayed.GetType().Name == "Administrator")
    {
        Console.WriteLine("Administratorzy nie posiadają dyżurów!");

        do
        {
            Console.Write("\nWskaż numer pracownika, dla którego chcesz edytować dyżury: ");
            kontrola = int.TryParse(Console.ReadLine(), out who);

            if (kontrola == false || who < 1 || who > ListaOsob.Count())
            {
                Console.WriteLine("Podaj poprawny numer z listy!");

            }
        } while (kontrola == false || who < 1 || who > ListaOsob.Count());

        EdytujDyzur(who);

    }

    Console.Write("\nDyżury pracownika ");
    displayed.Wyswietl();
    Console.Write(": \n");
    int counter = 0;
    


    foreach (var i in ((Pielegniarka)displayed).ListaDyzurow)
    {
            counter++;
            Console.Write($"{counter}. {i.Date.ToShortDateString()}");
            Console.WriteLine();
    }
    if(counter == 0)
    {
        Console.WriteLine("Ten pracownik nie ma dyżurów!");
    }
    else
    {
        do
        {
            Console.Write("\nWskaż numer dyżuru, który chcesz edytować: ");
            kontrola = int.TryParse(Console.ReadLine(), out numer);

            if (kontrola == false || numer < 1 || numer > ((Pielegniarka)displayed).ListaDyzurow.Count())
            {
                Console.WriteLine("Podaj poprawny numer z listy!");
            }

        } while (kontrola == false || numer < 1 || numer > ((Pielegniarka)displayed).ListaDyzurow.Count());

        string tekst = ((Pielegniarka)displayed).ListaDyzurow[numer - 1].Date.ToShortDateString();
        System.Windows.Forms.SendKeys.SendWait(tekst);

        do
        {
            kontrola = DateTime.TryParseExact(Console.ReadLine(), format, culture, 0, out n_data);

            if (kontrola == false)
            {
                Console.WriteLine("Podaj poprawną datę!\n");
            }

        } while (kontrola == false);

            if (((Pielegniarka)displayed).max10(n_data))
            {
                Console.WriteLine("W tym miesiącu pielęgniarka osiągnęła już maksymalne 10 zaplanowanych dużurów! Zmień datę!");
                EdytujDyzur(who);
            }
            else if (((Pielegniarka)displayed).break24(n_data))
            {
                EdytujDyzur(who);
            }
            else if (!((Pielegniarka)displayed).max10(n_data) && !((Pielegniarka)displayed).break24(n_data))
            {
                ((Pielegniarka)displayed).ListaDyzurow[numer - 1] = n_data;
                Console.Write($"\nPomyślnie edytowano dyżur {tekst} na {n_data.ToShortDateString()}!");
            }
        
        do
        {
            Console.Write("\nCzy chcesz edytować kolejny dyżury? (t/n): ");
            kontrola = char.TryParse(Console.ReadLine(), out odpowiedz);
            if (odpowiedz.ToString().ToLower() != "t" && odpowiedz.ToString().ToLower() != "n" || kontrola == false)
            {
                Console.WriteLine("Podaj poprawną odpowiedź!");
            }
        } while (odpowiedz.ToString().ToLower() != "t" && odpowiedz.ToString().ToLower() != "n" || kontrola == false);
    }
    } while (odpowiedz.ToString().ToLower() == "t") ;
}

void UsunDyzur(int who)
{

    Osoba displayed = ListaOsob[who - 1];
    char odpowiedz = 'n';
    int numer;

    do
    {
        if (displayed.GetType().Name == "Administrator")
        {
            Console.WriteLine("Administratorzy nie posiadają dyżurów!");

            do
            {
                Console.Write("\nWskaż numer pracownika, dla którego chcesz usunąć dyżury: ");
                kontrola = int.TryParse(Console.ReadLine(), out who);

                if (kontrola == false || who < 1 || who > ListaOsob.Count())
                {
                    Console.WriteLine("Podaj poprawny numer z listy!");

                }
            } while (kontrola == false || who < 1 || who > ListaOsob.Count());

            UsunDyzur(who);
        }else{ 
        Console.Write("\nDyżury pracownika ");
        displayed.Wyswietl();
        Console.Write(": \n");
        int counter = 0;



        foreach (var i in ((Pielegniarka)displayed).ListaDyzurow)
        {
            counter++;
            Console.Write($"{counter}. {i.Date.ToShortDateString()}");
            Console.WriteLine();
        }
        if (counter == 0)
        {
            Console.WriteLine("Ten pracownik nie ma dyżurów!");
            break;
        }
        else
        {
            do
            {
                Console.Write($"\nWskaż numer dyżuru, który chcesz usunąć (WCIŚNIĘCIE 0 USUNIE WSZYSTKIE DYŻURY): ");
                kontrola = int.TryParse(Console.ReadLine(), out numer);

                if (kontrola == false || numer < 0 || numer > ((Pielegniarka)displayed).ListaDyzurow.Count())
                {
                    Console.WriteLine("Podaj poprawny numer z listy!");
                }

            } while (kontrola == false || numer < 0 || numer > ((Pielegniarka)displayed).ListaDyzurow.Count());

            if(numer == 0)
                {
                    ((Pielegniarka)displayed).ListaDyzurow.Clear();
                    Console.WriteLine("Pomyślnie usunięto wszystkie dyżury!");
                }
                else
                {
                    ((Pielegniarka)displayed).ListaDyzurow.RemoveAt(numer - 1);
                    Console.WriteLine("Pomyślnie usunięto dyżur!");
                }
            

            do
            {
                Console.Write("\nCzy chcesz usunąć kolejny dyżury? (t/n): ");
                kontrola = char.TryParse(Console.ReadLine(), out odpowiedz);
                if (odpowiedz.ToString().ToLower() != "t" && odpowiedz.ToString().ToLower() != "n" || kontrola == false)
                {
                    Console.WriteLine("Podaj poprawną odpowiedź!");
                }
            } while (odpowiedz.ToString().ToLower() != "t" && odpowiedz.ToString().ToLower() != "n" || kontrola == false);
        }
        }
    } while (odpowiedz.ToString().ToLower() == "t");

}

    void zaloguj()
{
    do
    {
        do
        {

            Console.Write("\nPodaj login: ");
            podawanylogin = Console.ReadLine();

            if (kontrola = String.IsNullOrWhiteSpace(podawanylogin))
            {
                Console.WriteLine("Login nie może być pusty!");
            }

        } while (kontrola == true);

        do
        {

            Console.Write("\nPodaj hasło: ");
            podawanehaslo = Console.ReadLine();

            if (kontrola = String.IsNullOrWhiteSpace(podawanehaslo))
            {
                Console.WriteLine("Hasło nie może być puste!");
            }
        } while (kontrola == true);

        if (Osoba.logowanie(podawanylogin, podawanehaslo, ListaOsob))
        {
            odpowiedz = 'n';
        }
        else
        {
            Console.WriteLine("Brak takiego loginu lub hasła w systemie!");
            Console.WriteLine("Chcesz spróbować zalogować się ponownie? t/n");
            odpowiedz = char.Parse(Console.ReadLine());
            

        }
    } while (char.ToLower(odpowiedz) == 't');
}
//DESERIALIZACJA
try
{
    using (Stream fStream = new FileStream("dane.dat", FileMode.Open, FileAccess.Read))
    {
        ListaOsob = (List<Osoba>)binFormatter.Deserialize(fStream);
    }
}
catch (Exception)
{
    Console.WriteLine("Nie znaleziono żadnego pliku z danymi! Dodaj konto administratora!\n");
    DodajNowego("admin");
}

Console.Clear();
Console.WriteLine("---------------------- HOSPITAL OS 2.0 -----------------------\n");
Console.WriteLine("                         ,(&@%*.                            \r\n                    &@@@@@@@@@@@@@@@,                       \r\n                .@@@@@@@@@@@@@@@@@@@@@@@@%                  \r\n              #@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                \r\n              .@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@.              \r\n                &@@@@@@@@@@@@@@@%*    ,@@@@@@%              \r\n                /@@@@@@%.               &@@@@%              \r\n                *@@@@,                  *@@@@,              \r\n                ,@@@(                   /@@&                \r\n                @#.%%                   %@*#@               \r\n                @(                         %&               \r\n                .@@&.                   @&@&                \r\n                   &#                  .@*                  \r\n                    @@                /@(                   \r\n                      %@#           @@*                     \r\n                       &,%@@@@@@@@@/ (                      \r\n                     #@&.           ,@@(                    \r\n       .*(&@@@@@@@( (@@@@@@#     ,@@@@@@, /@@@@@&%/*.       \r\n   (@@@@@@@@@@@@&,  .&@@@@@@@@@@@@@@@@@@% ,@@@@@@@@@@@@@&   \r\n  @@@@@@@@@@@@%  #@@%  %@@@@@@@@@@@@@@@@% ,@@@@@@@@@@@@@@@/ \r\n #@@@@@@@@@@@% /@@@@@@( &@@@@@@@@@@@@@       %@@@@@@@@@@@@@ \r\n @@@@@@@@@@@% #@@@@@@@@% &@@@@@@@@@@@. .@@@%  @@@@@@@@@@@@@*\r\n @@@@@@@@@@@,,@@/&@@&/&@,.@@@@@@@@@@@@.      @@@@@@@@@@@@@@@\r\n/@@@@@@@@@@@@@&&/%@@&/&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
Console.WriteLine("\n!!! Wymagane logowanie !!!");


zaloguj();
rola = Osoba.whoami;


//MENU//

switch (rola)
{
    case "Admin":
        {
            do
            {
                Console.WriteLine("Lista wszystkich pracowników naszego szpitala:");
                allworkers();

                Console.WriteLine("\n-------------------- OPERACJE NA PRACOWNIKACH --------------------------");
                Console.WriteLine("(1) Dodaj pracownika");
                Console.WriteLine("(2) Edytuj pracownika");
                Console.WriteLine("(3) Usuń pracownika");
                Console.WriteLine("\n-------------------- OPERACJE NA DYŻURACH ------------------------------");
                Console.WriteLine("(4) Wyświetl dyżury pracownika");
                Console.WriteLine("(5) Dodaj dyżury pracownika");
                Console.WriteLine("(6) Edytuj dyżury pracownika");
                Console.WriteLine("(7) Usuń dyżury pracownika");
                Console.WriteLine("\n(8) Wyjście\n");

                do
                {
                    Console.Write("Twój wybór: ");
                    kontrola = int.TryParse(Console.ReadLine(), out wybor);
                    if (kontrola == false | wybor < 1 | wybor > 8)
                    {
                        Console.WriteLine("Podaj poprawną wartość (od 1 do 8)!");
                    }
                } while (kontrola == false | wybor < 1 | wybor > 8);
                Console.Clear();

                switch (wybor)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("\nPodaj typ nowego pracownika (admin/lekarz/pielęgniarka): ");
                        typ = Console.ReadLine();
                        DodajNowego(typ);
                        clear();
                        break;

                    case 2:
                        allworkers();
                        do
                        {
                            Console.Write("\nWskaż numer pracownika z listy, którego chcesz edytować: ");
                            kontrola = int.TryParse(Console.ReadLine(), out who);

                            if (kontrola == false || who < 1 || who > ListaOsob.Count())
                            {
                                Console.WriteLine("Podaj poprawny numer z listy!");
                            }


                        } while (kontrola == false || who < 1 || who > ListaOsob.Count());
                        EdytujPracownika(who);
                        clear();

                        break;

                    case 3:
                        allworkers();
                        do
                        {
                            Console.Write("\nWskaż numer pracownika z listy, którego chcesz usunąć: ");
                            kontrola = int.TryParse(Console.ReadLine(), out who);

                            if (kontrola == false || who < 1 || who > ListaOsob.Count())
                            {
                                Console.WriteLine("Podaj poprawny numer z listy!");
                            }
                            else if (ListaOsob[who - 1].Login == Osoba.loginzalogowanego)
                            {
                                kontrola = false;
                                Console.WriteLine("Nie możesz usunąć obecnie zalogowanego użytkownika!");
                            }

                        } while (kontrola == false || who < 1 || who > ListaOsob.Count());
                        UsunPracownika(who);
                        clear();
                        break;

                    case 4:

                        allworkers();
                        do
                        {
                            Console.Write("\nWskaż numer pracownika, dla którego chcesz wyświetlić dyżury: ");
                            kontrola = int.TryParse(Console.ReadLine(), out who);

                            if (kontrola == false || who < 1 || who > ListaOsob.Count())
                            {
                                Console.WriteLine("Podaj poprawny numer z listy!");
                            }


                        } while (kontrola == false || who < 1 || who > ListaOsob.Count());
                        WyswietlDyzur(who);
                        clear();
                        break;

                    case 5:
                        allworkers();
                        do
                        {
                            Console.Write("\nWskaż numer pracownika, dla którego chcesz dodawać dyżury: ");
                            kontrola = int.TryParse(Console.ReadLine(), out who);

                            if (kontrola == false || who < 1 || who > ListaOsob.Count())
                            {
                                Console.WriteLine("Podaj poprawny numer z listy!");
                            }


                        } while (kontrola == false || who < 1 || who > ListaOsob.Count());
                        DodajDyzur(who);
                        clear();

                        break;

                    case 6:
                        allworkers();
                        do
                        {
                            Console.Write("\nWskaż numer pracownika, dla którego chcesz edytować dyżury: ");
                            kontrola = int.TryParse(Console.ReadLine(), out who);

                            if (kontrola == false || who < 1 || who > ListaOsob.Count())
                            {
                                Console.WriteLine("Podaj poprawny numer z listy!");
                            }


                        } while (kontrola == false || who < 1 || who > ListaOsob.Count());
                        EdytujDyzur(who);
                        clear();

                        break;

                    case 7:

                        allworkers();
                        do
                        {
                            Console.Write("\nWskaż numer pracownika, dla którego chcesz usuwać dyżury: ");
                            kontrola = int.TryParse(Console.ReadLine(), out who);

                            if (kontrola == false || who < 1 || who > ListaOsob.Count())
                            {
                                Console.WriteLine("Podaj poprawny numer z listy!");
                            }


                        } while (kontrola == false || who < 1 || who > ListaOsob.Count());
                        UsunDyzur(who);
                        clear();

                        break;

                    case 8:
                        try
                        {
                            using (Stream fStream = new FileStream("dane.dat", FileMode.Create, FileAccess.Write))
                            {
                                binFormatter.Serialize(fStream, ListaOsob);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Wystąpił problem z plikiem!");
                        }

                        break;
                }
            } while (wybor != 8);

            break;
        }
    case "Lekarz" or "Pielegniarka":

        do
        {
            Console.WriteLine("\n(1) Wyświetl listę lekarzy i pielęgniarek");
            Console.WriteLine("(2) Wyświetl dyżur pracownika");
            Console.WriteLine("(3) Wyjście\n");

            do
            {
                Console.Write("Twój wybór: ");
                kontrola = int.TryParse(Console.ReadLine(), out wybor);
                if (kontrola == false | wybor < 1 | wybor > 3)
                {
                    Console.WriteLine("Podaj poprawną wartość (od 1 do 3)!");
                }
            } while (kontrola == false | wybor < 1 | wybor > 3);
            Console.Clear();

            switch (wybor)
            {
                case 1:
                    Console.WriteLine("Lista lekarzy i pielęgniarek naszego szpitala:");
                    allworkers();
                    clear();
                    break;
                case 2:
                    allworkers();
                    do
                    {
                        Console.Write("\nWskaż numer pracownika, dla którego chcesz wyświetlić dyżury: ");
                        kontrola = int.TryParse(Console.ReadLine(), out who);

                        if (kontrola == false || who < 1 || who > ListaOsob.Count()-ileadminow)
                        {
                            Console.WriteLine("Podaj poprawny numer z listy!");
                        }


                    } while (kontrola == false || who < 1 || who > ListaOsob.Count()-ileadminow);
                    WyswietlDyzur(who);
                    clear();
                    break;
                case 3:
                    try
                    {
                        using (Stream fStream = new FileStream("dane.dat", FileMode.Create, FileAccess.Write))
                        {
                            binFormatter.Serialize(fStream, ListaOsob);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Wystąpił problem z plikiem!");
                    }

                    break;
            }
           
        } while (wybor != 3);
        break;
}



