using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{
    public class Bestellung
    {
        Kunde _kunde;
        Produkt _produkt;
        int _menge;
        int _bestellNummer;
        DateTime _lieferdatum;

        public bool Stornieren()
        {
            if (_lieferdatum.AddDays(-3) > DateTime.Today)
                return true;
            return false;
        }

        internal Kunde Kunde
        {
            get => _kunde;
        }

        public DateTime Lieferdatum
        {
            get => _lieferdatum;
        }
        public int Menge
        {
            get => _menge;
        }

        public Produkt Produkt
        {
            get => _produkt;
        }

        public Bestellung(int bestellNummer, Kunde kunde, Produkt produkt, int menge, DateTime lieferdatum)
        {
            _kunde = kunde;
            _produkt = produkt;
            _menge = menge;
            _lieferdatum = lieferdatum;
            _bestellNummer = bestellNummer;
        }
    }
}
