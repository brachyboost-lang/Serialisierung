using System;
using System.Collections.Generic;
using System.Text;

namespace Serialisierung
{
    [Serializable]
    internal class Person
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public int Alter { get; set; }
        public float Urlaubstage { get; set; }
        public List<DateOnly> Krankheitstage { get; set; }

        public Person(string vorname, string nachname, int alter, float urlaubstage, List<DateOnly> krankheitstage) 
        {
            Vorname = vorname;
            Nachname = nachname;
            Alter = alter;
            Urlaubstage = urlaubstage;
            Krankheitstage = krankheitstage;
        }
    }
}
