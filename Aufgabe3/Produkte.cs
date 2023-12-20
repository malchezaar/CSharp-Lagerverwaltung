using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{
    public class Produkte : IReadOnlyList<Produkt>
    {
        private List<Produkt> _produkte;

        public Produkte()
        {
            _produkte = new List<Produkt>();
        }

        public Produkt this[int index] => _produkte[index];

        public int Count => _produkte.Count;

        public Produkt Add(int verfallsZeit, string bezeichnung, int maximalMenge)
        {
            Produkt l = new Produkt(_produkte.Count, verfallsZeit, bezeichnung, maximalMenge);
            _produkte.Add(l);
            return l;
        }

        public IEnumerator<Produkt> GetEnumerator()
        {
            return _produkte.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
