using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{
    public class Lieferanten : IReadOnlyList<Lieferant>
    {
        private List<Lieferant> _lieferanten;

        public Lieferanten()
        {
            _lieferanten = new List<Lieferant>();
        }

        public Lieferant this[int index] => _lieferanten[index];

        public int Count => _lieferanten.Count;

        public Lieferant Add(int lieferantenNummer, string bezeichnung)
        {
            Lieferant l = new Lieferant(lieferantenNummer, bezeichnung);
            _lieferanten.Add(l);
            return l;
        }

        public IEnumerator<Lieferant> GetEnumerator()
        {
            return _lieferanten.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
