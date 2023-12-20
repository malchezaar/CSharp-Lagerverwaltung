using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{
    public class Bestellungen : IReadOnlyList<Bestellung>
    {
        private List<Bestellung> _bestellungen;

        public Bestellungen()
        {
            _bestellungen = new List<Bestellung>();
        }

        public Bestellung this[int index] => _bestellungen[index];

        public int Count => _bestellungen.Count;

        public Bestellung Add(int bestellNummer, Kunde kunde, Produkt produkt, int menge, DateTime lieferdatum)
        {
            Bestellung b = new Bestellung(bestellNummer, kunde, produkt, menge, lieferdatum);
            _bestellungen.Add(b);
            return b;
        }

        public IEnumerator<Bestellung> GetEnumerator()
        {
            return _bestellungen.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
