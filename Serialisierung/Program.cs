using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Serialisierung
{
    internal class Program
    {
        List<Person> PersonenListe { get; set; }

        Program()
        {
            PersonenListe = new List<Person>();

        }
        Program(List<Person> personenListe)
        {
            PersonenListe.AddRange(personenListe);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Willkommen im Personal Verwaltungsprogramm!");
            Console.ReadKey();
            Console.Clear();
            while (true)
            {
                PrintMenu();
                ChoiceMenu();
            }

        }

        static string? GetUserInputStr()
        {
            return Console.ReadLine();
        }

        static char GetUserInputChar()
        {
            while (true)
                try
                {
                    string? input = Console.ReadLine().ToLower();
                    if (!string.IsNullOrEmpty(input) && input.Length == 1)
                    {
                        char checkChar = input[0];
                        if (char.IsLetter(checkChar))
                        {
                            return checkChar;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
        }

        static int GetUserInputInt()
        {
            while (true)
                try
                {
                    string? input = Console.ReadLine();
                    if (int.TryParse(input, out int result))
                    {
                        return result;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
        }

        static float GetUserInputFloat()
        {
            while (true)
                try
                {
                    string? input = Console.ReadLine();
                    if (float.TryParse(input, out float result))
                    {
                        return result;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
        }

        static void PrintMenu()
        {
            Console.WriteLine("Bitte wählen Sie eine der folgenden Optionen:");
            Console.WriteLine("1. Neue Person anlegen");
            Console.WriteLine("2. Personendaten anzeigen/ändern");
            Console.WriteLine("3. Personendaten speichern");
            Console.WriteLine("4. Personendaten löschen");
            Console.WriteLine("5. Programm beenden");
        }

        static void ChoiceMenu()
        {
            PrintMenu();
            int? choice = GetUserInputInt();
            switch (choice)
            {
                case 1:
                    CreatePerson();
                    break;
                case 2:
                    // Personendaten anzeigen
                    break;
                case 3:
                    // Personendaten speichern
                    break;
                case 4:
                    // Personendaten löschen
                    break;
                case 5:
                    Console.WriteLine("Programm wird beendet. Bis zum nächsten mal!");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe. Bitte versuchen Sie es erneut.");
                    break;
            }
        }

        static void CreatePerson()
        {
            List<Person> personenListe = new List<Person>();
            Console.WriteLine("Geben Sie den Vornamen der Person ein:");
            string? name = GetUserInputStr();
            Console.WriteLine("Geben Sie den Nachnamen der Person ein:");
            string? surname = GetUserInputStr();
            Console.WriteLine("Geben Sie das Alter der Person ein:");
            int age = GetUserInputInt();
            Console.WriteLine("Geben Sie die Anzahl der Urlaubstage der Person ein:");
            float vacationDays = GetUserInputFloat();
            Console.WriteLine("Gibt es bereits Krankheitstage dieser Person? Y/N");

            Person person;
            if (GetUserInputChar() == 'y')
            {
                List<DateOnly> krankheitstage = new List<DateOnly>();
                Console.WriteLine("Wieviel Krankheitstage gab es insgesamt?");
                int krankheitstageAnzahl = GetUserInputInt();
                for (int i = 0; i < krankheitstageAnzahl; i++)
                {
                    Console.WriteLine($"Geben Sie das Datum des {i + 1}. Krankheitstages im Format JJJJ-MM-TT ein:");
                    string? dateInput = GetUserInputStr();
                    if (DateOnly.TryParse(dateInput, out DateOnly date))
                    {
                        krankheitstage.Add(date);
                    }
                    else
                    {
                        Console.WriteLine("Ungültiges Datum. Bitte versuchen Sie es erneut.");
                        i--;
                    }
                }
                person = new Person(name, surname, age, vacationDays, krankheitstage);

            }
            else
            {
                Console.WriteLine("Es wurden keine Krankheitstage hinzugefügt.");
                List<DateOnly> krankheitstage = new List<DateOnly>();
                person = new Person(name, surname, age, vacationDays, krankheitstage);
            }
            personenListe.Add(person);
            Console.WriteLine("Soll eine Weitere Person hinzugefügt werden? Y/N");
            if (GetUserInputChar() == 'y')
            {
                CreatePerson();
            }
            else
            {
                Program program = new Program(personenListe);
                Console.WriteLine("Personen wurden erfolgreich hinzugefügt. Drücken Sie eine Taste um zum Menü zurückzukehren.");
                Console.ReadKey();
                Console.Clear();
            }
        }

    
        
    }
}
