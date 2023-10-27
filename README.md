# Opis projektu
Prosta aplikacja do zarządzania pracownikami szpitala i ich dyżurami, napisana jako projekt na zaliczenie zajęć z c#

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
