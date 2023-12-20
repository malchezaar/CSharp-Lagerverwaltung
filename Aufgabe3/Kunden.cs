using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{
    public class Kunden : IReadOnlyList<Kunde>
    {
        private List<Kunde> _kunden;

        public Kunden ()
        {
            _kunden = new List<Kunde>();
        }

        public Kunde this[int index] => _kunden[index];

        public int Count => _kunden.Count;

        public Kunde Add(int kundenNummer, string bezeichnung)
        {
            Kunde k = new Kunde(kundenNummer, bezeichnung);
            _kunden.Add(k);
            return k;
        }

        public IEnumerator<Kunde> GetEnumerator()
        {
            return _kunden.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
