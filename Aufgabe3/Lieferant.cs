using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{
    public class Lieferant
    {
        string _bezeichnung;
        int _lieferantenNummer;
        Dictionary<int, int> _deliverableProducts;
        public int LieferantenNummer
        {
            get => _lieferantenNummer;
        }
        internal Lieferant(int lieferantenNummer, string bezeichnung)
        {
            _bezeichnung = bezeichnung;
            _lieferantenNummer = lieferantenNummer;
            _deliverableProducts = new Dictionary<int, int>();

        }

        public int CanDeliver(int artikelNummer)
        {
            if (_deliverableProducts.ContainsKey(artikelNummer))
                return _deliverableProducts[artikelNummer];
            return -1;
        }
        public void AddProduct(int artikelNummer, int lieferZeit)
        {
            if (!_deliverableProducts.ContainsKey(artikelNummer))
                _deliverableProducts.Add(artikelNummer, lieferZeit);
        }

        public void RemoveProduct(int artikelNummer)
        {
            if (!_deliverableProducts.ContainsKey(artikelNummer))
                _deliverableProducts.Remove(artikelNummer);
        }


    }
}
