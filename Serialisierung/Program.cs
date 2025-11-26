using System;
using System.Text.Json;
using System.Collections.Generic;


namespace Serialisierung
{
    internal class Program
    {
        private static List<Person> PersonenListe { get; } = new List<Person>();
        private const string DataFile = "personen.json";

        static void Main(string[] args)
        {
            LoadPersons();
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
            {
                try
                {
                    string? input = Console.ReadLine();
                    input = input?.ToLower();
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
        }

        static int GetUserInputInt()
        {
            while (true)
            {
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
        }

        static float GetUserInputFloat()
        {
            while (true)
            {
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
        }

        static void PrintMenu()
        {
            Console.WriteLine("Bitte wählen Sie eine der folgenden Optionen:");
            Console.WriteLine("1. Neue Person anlegen");
            Console.WriteLine("2. Krankheitstage ergänzen");
            Console.WriteLine("3. Personendaten anzeigen/ändern");
            Console.WriteLine("4. Personendaten speichern");
            Console.WriteLine("5. Personendaten löschen");
            Console.WriteLine("6. Programm beenden");
        }

        static void ChoiceMenu()
        {
            int choice = GetUserInputInt();
            switch (choice)
            {
                case 1:
                    CreatePerson();
                    break;
                case 2:
                        PrintAllPersons();
                    Console.WriteLine("Von welcher Person möchten Sie Krankheitstage ergänzen?");
                    int personIndex = GetUserInputInt() - 1;
                    var selectedPerson = PersonenListe[personIndex];
                    Console.WriteLine("Wie viele Krankheitstage möchten Sie hinzufügen?");
                    int krankheitstageAnzahl = GetUserInputInt();
                    for (int i = 0; i < krankheitstageAnzahl; i++)
                    {
                        Console.WriteLine($"Geben Sie das Datum des {i + 1}. Krankheitstages im Format JJJJ-MM-TT ein:");
                        string? dateInput = GetUserInputStr();
                        if (DateOnly.TryParse(dateInput, out DateOnly date))
                        {
                            selectedPerson.Krankheitstage.Add(date);
                            Console.WriteLine($"Krankheitstag {date} zu Person {selectedPerson.Vorname} {selectedPerson.Nachname} hinzugefügt.");
                        }
                        else
                        {
                            Console.WriteLine("Ungültiges Datum. Bitte versuchen Sie es erneut.");
                            i--;
                        }
                    }
                    break;
                case 3:
                    PrintAllPersons();
                    Console.WriteLine("Möchten Sie den vorhandenen Datensatz bearbeiten?");
                    char editChoice = GetUserInputChar();
                    if (editChoice == 'y')
                    {
                        ChangePersonData();
                    }
                    else
                    {
                        Console.Clear();
                    }
                    break;
                case 4:
                    SavePersons();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 5:
                    DeletePerson();
                    break;
                case 6:
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

            PersonenListe.Add(person);
            Console.WriteLine("Person wurde erfolgreich hinzugefügt. Drücken Sie eine Taste um zum Menü zurückzukehren.");
            Console.ReadKey();
            Console.Clear();
        }

        static void PrintAllPersons()
        {
            Console.Clear();
            if (PersonenListe.Count == 0)
            {
                Console.WriteLine("Keine Personen vorhanden.");
            }
            else
            {
                for (int i = 0; i < PersonenListe.Count; i++)
                {
                    var p = PersonenListe[i];
                    Console.WriteLine($"{i + 1}: {p.Vorname} {p.Nachname} | Alter: {p.Alter} | Urlaubstage: {p.Urlaubstage} | Krankheitstage: {p.Krankheitstage.Count}");
                    int j = 0;
                    foreach (var date in p.Krankheitstage)
                    {
                        j++;
                        Console.WriteLine($" Krankheit Tag {j} - {date}");
                    }
                }
                Console.WriteLine();
            }
        }

        static void DeletePerson()
        {
            Console.Clear();
            if (PersonenListe.Count == 0)
            {
                Console.WriteLine("Keine Personen vorhanden, die gelöscht werden können.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.WriteLine("Welche Person möchten Sie löschen? Bitte Nummer eingeben:");
            for (int i = 0; i < PersonenListe.Count; i++)
            {
                var p = PersonenListe[i];
                Console.WriteLine($"{i + 1}: {p.Vorname} {p.Nachname}");
            }

            int index = GetUserInputInt() - 1;
            if (index >= 0 && index < PersonenListe.Count)
            {
                var removed = PersonenListe[index];
                PersonenListe.RemoveAt(index);
                Console.WriteLine($"Person {removed.Vorname} {removed.Nachname} wurde entfernt.");
            }
            else
            {
                Console.WriteLine("Ungültige Auswahl.");
            }

            Console.ReadKey();
            Console.Clear();
        }

        static void ChangePersonData()
        {
            Console.WriteLine("Von welcher Person sollen Daten verändert werden?");
            foreach (var person in PersonenListe)
            {
                Console.WriteLine($"{PersonenListe.IndexOf(person) + 1}: {person.Vorname} {person.Nachname}");
            }
            int index = GetUserInputInt() - 1;
            if (index >= 0 && index < PersonenListe.Count)
            {
                var person = PersonenListe[index];
                Console.WriteLine($"Aktuelle Daten von {person.Vorname} {person.Nachname}:");
                Console.WriteLine($"1. Vorname: {person.Vorname}");
                Console.WriteLine($"2. Nachname: {person.Nachname}");
                Console.WriteLine($"3. Alter: {person.Alter}");
                Console.WriteLine($"4. Urlaubstage: {person.Urlaubstage}");
                Console.WriteLine("Welche Daten möchten Sie ändern? (Geben Sie die Nummer ein oder 0 zum Abbrechen)");
                int choice = GetUserInputInt();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Neuer Vorname:");
                        person.Vorname = GetUserInputStr() ?? person.Vorname;
                        break;
                    case 2:
                        Console.WriteLine("Neuer Nachname:");
                        person.Nachname = GetUserInputStr() ?? person.Nachname;
                        break;
                    case 3:
                        Console.WriteLine("Neues Alter:");
                        person.Alter = GetUserInputInt();
                        break;
                    case 4:
                        Console.WriteLine("Neue Anzahl der Urlaubstage:");
                        person.Urlaubstage = GetUserInputFloat();
                        break;
                    case 0:
                        Console.WriteLine("Änderung abgebrochen.");
                        break;
                    default:
                        Console.WriteLine("Ungültige Auswahl.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ungültige Auswahl.");
            }
        }




        // KI Unterstütze Bereiche





        //KI erzeugte aber selbst bereinigte Methode zum Speichern der Personendaten in einer JSON-Datei - ehrlich gesagt nicht wirklich verstanden - Datei wird erstellt im ausführungspfad des Programms
        static void SavePersons()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                options.Converters.Add(new DateOnlyJsonConverter());
                string json = JsonSerializer.Serialize(PersonenListe, options);
                File.WriteAllText(DataFile, json);
                Console.WriteLine($"Daten in '{DataFile}' gespeichert.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern: {ex.Message}");
            }
        }

        // KI erzeugt aber selbst bereinigte Methode zum Laden der Personendaten aus einer JSON-Datei - ehrlich gesagt nicht wirklich verstanden - Datei wird aus dem ausführungspfad des Programms geladen
        static void LoadPersons()
        {
            try
            {
                if (!File.Exists(DataFile))
                    return;

                var options = new JsonSerializerOptions();
                options.Converters.Add(new DateOnlyJsonConverter());
                string json = File.ReadAllText(DataFile);
                var loaded = JsonSerializer.Deserialize<List<Person>>(json, options);
                if (loaded != null)
                {
                    PersonenListe.Clear();
                    PersonenListe.AddRange(loaded);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Daten: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
            }
        }

        // KI erzeugte JsonConverter-Klasse für DateOnly - wird benötigt, da DateOnly standardmäßig nicht serialisierbar ist
        private class DateOnlyJsonConverter : System.Text.Json.Serialization.JsonConverter<DateOnly>
        {
            private const string Format = "yyyy-MM-dd";
            public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var s = reader.GetString();
                if (DateOnly.TryParse(s, out var d))
                    return d;
                return DateOnly.FromDateTime(DateTime.MinValue);
            }

            public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(Format));
            }
        }
    }
}




// Abgabe Aufgabe - Known Issues: Datums Formate könnten angepasst werden je nach Region - ansonsten funktioniert das Programm wie gewünscht
// Persönliche Änderungswünsche: Passwordschutz für ändern von Personendaten, bessere Menüführung, Auswahl der Person über Name zusätzlich zur Nummer, schönere Ausgabeformatierung