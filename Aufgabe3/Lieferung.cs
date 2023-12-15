using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{
    public class Lieferung
    {
        //Lieferant _lieferant;
        Produkt _produkt;
        DateTime _lieferdatum;
        Lieferant _lieferant;
        int _menge, _lieferNummer;

        internal Produkt Produkt
        {
            get => _produkt;
        }

        public Lieferant Lieferant
        {
            get => _lieferant;
        }
        public DateTime Lieferdatum
        {
            get => _lieferdatum;
        }
        public int Menge
        {
            get => _menge;
        }

        public bool Stornieren()
        {
            if (_lieferdatum.AddDays(-_lieferant.CanDeliver(_produkt.ArtikelNummer)) > DateTime.Today)
                return true;
            return false;
        }
        public Lieferung(int lieferNummer, Produkt produkt, int menge, Lieferant lieferant, DateTime lieferdatum)
        {
            _lieferdatum = lieferdatum;
            _lieferNummer = lieferNummer;
            _lieferant = lieferant;
            _produkt = produkt;
            _menge = menge;
        }
    }
}
