# Opis projektu
Prosta aplikacja do zarządzania pracownikami szpitala i ich dyżurami, napisana jako projekt na zaliczenie zajęć z c#. Musiała spełniać określone wymagania, opisana w [celu projektu](#cel-projektu)

# Technologa
Aplikacja została napisana w C# obiektowo i wykorzystuje bibliotekę klas .dll oraz dziedziczenie. Dodatkowo następuje serializacja i deserializacja pliku dane.dat, który zawiera wszystkie dane. Jeśli go nie ma, tworzony jest nowy plik i pojawia się prośba o stworzenie konta administratora do systemu.

# Instrukcja uruchomienie
Wystarczy uruchomić skrót "Uruchom_Projek" i projekt powinien się uruchomić korzystając z przykładowego pliku dane.dat w ścieżce \Projekt_Szpital_Jakub_Gogolewski\bin\Debug\net6.0-windows\.<br /><br />**Przykładowy login i hasło dla administratora z pliku to admin admin.** 

# Screeny z aplikacji
Ekran logowania:
![Ekran logowania](https://github.com/jakub-gogolewski/Zarzadzanie-szpitalem/assets/68034177/6324515d-ca28-40fe-a001-be9175914b2d)
<br /><br />
Widok po zalogowaniu administratora:
![widok](https://github.com/jakub-gogolewski/Zarzadzanie-szpitalem/assets/68034177/18fd9c19-be35-4660-a844-fe2b5ed8eb6f)
<br /><br />
Kilka przykładowych dyżurów dla lekarza:
![dyzury](https://github.com/jakub-gogolewski/Zarzadzanie-szpitalem/assets/68034177/dd94a499-b06a-4c24-96f2-827a17185dcb)
<br /><br />
Nie można dodać dyżurów zgodnie z narzuconymi ograniczeniami z [celu projektu](#cel-projektu):
![ograniczenia](https://github.com/jakub-gogolewski/Zarzadzanie-szpitalem/assets/68034177/0e486c4e-737f-4a5b-8d4e-e90abb13b40f)
<br />

# Cel projektu
Celem projektu jest stworzenie systemu administracyjnego dla szpitala. System ma umożliwiać 
użytkownikom prostą ewidencję pracowników szpitala. Każdy pracownik posiada imię, nazwisko i pesel 
oraz nazwę użytkownika i hasło. W systemie wyróżniamy następujące typy użytkowników: lekarz, 
pielęgniarka, administrator.

Lekarz, poza standardowymi danymi każdego użytkownika, ma dodatkowo specjalność (kardiolog, 
urolog, neurolog lub laryngolog) oraz numer PWZ. Lekarze i pielęgniarki mają również listę swoich 
całodobowych dyżurów, przy założeniu, że jedna osoba może mieć maksymalnie 10 dyżurów w miesiącu 
oraz jej dyżury nie mogą występować dzień po dniu. Ponadto, danego dnia dyżur może mieć tylko jeden 
lekarz na daną specjalizację (np. danego dnia dyżur może mieć kardiolog, urolog i laryngolog, ale nie 
dwóch kardiologów).

System po uruchomieniu prosi o podanie nazwy użytkownika i hasła. Po zalogowaniu, w przypadku 
lekarzy i pielęgniarek, możliwe jest jedynie wyświetlenie listy wszystkich lekarzy i pielęgniarek (Imię, 
Nazwisko, posada + ewentualna specjalizacja) oraz planu dyżurów wskazanej osoby w danym miesiącu.
Administrator po zalogowaniu widzi wszystkich użytkowników na liście. Może ponadto edytować dane 
każdego z nich (razem z planem dyżurów) oraz dodawać nowych użytkowników (w tym administratorów) 
do systemu.

Na zakończenie działania programu cała lista pracowników jest serializowana i zapisywana do pliku, 
a podczas uruchamiania – odczytywana i deserializowana.

Cały świat pracowników ma być zamodelowany w osobnym projekcie (biblioteka klas), a panel 
sterowania – jako osobna aplikacja (Console, Win Forms lub WPF).

Projekt musi być zrealizowany zgodnie z paradygmatem programowania obiektowego i musi 
wykorzystywać dziedziczenie, hermetyzację, abstrakcję oraz polimorfizm. Ponadto, musi być odporny na 
błędy (zarówno użytkownika jak i systemowe, np. brak pliku).
