using Lagerverwaltung;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{
    public struct Bestand
    {
        public int Menge;
        public DateTime Verfallsdatum;
    }
    public class Produkt
    {
        private int _artikelNummer, _verfallsZeit, _maximalMenge;
        string _bezeichnung;
        List<Bestand> _bestand;

        public int VerfallsZeit
        {
            get => _verfallsZeit;
        }

        public int MaximalMenge
        {
            get => _maximalMenge;
        }

        public int ArtikelNummer
        {
            get => _artikelNummer;
        }
        internal Produkt(int artikelNummer, int verfallsZeit, string bezeichnung, int maximalMenge)
        {
            _artikelNummer = artikelNummer;
            _verfallsZeit = verfallsZeit;
            _bezeichnung = bezeichnung;
            _maximalMenge = maximalMenge;
            _bestand = new List<Bestand>();
        }

        public void RemoveFromStorage(int menge)
        {
            for (int i = 0; i < _bestand.Count; i++)
            {
                Bestand b = _bestand[0];
                if (b.Menge - menge > 0)
                {
                    b.Menge -= menge;
                    break;
                }
                else
                {
                    menge -= b.Menge;
                    _bestand.RemoveAt(i);
                    i--;
                }
            }
        }

        public int GetProductsInStorage()
        {
            int amount = 0;
            foreach (Bestand b in _bestand)
            {
                amount += b.Menge;
            }
            return amount;
        }
        public void AddToStorage(Lieferung l)
        {
            if (l.Produkt == this)
            {
                Bestand b = new Bestand();
                b.Menge = l.Menge;
                b.Verfallsdatum = l.Lieferdatum;
                b.Verfallsdatum.AddDays(_verfallsZeit);

                _bestand.Add(b);
            }
        }

        public int CheckExpirationDates()
        {
            int amount = 0;
            foreach (Bestand b in _bestand)
            {
                if (b.Verfallsdatum < DateTime.Today)
                {
                    amount += b.Menge;
                    _bestand.Remove(b);
                }
            }
            return amount;
        }
    }
}
