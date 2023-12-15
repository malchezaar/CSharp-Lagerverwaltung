using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

namespace Lagerverwaltung
{

    public class Management
    {
        Dictionary<int, Produkt> _produkte;
        Dictionary<int, Lieferant> _lieferanten;
        Dictionary<int, Kunde> _kunden;
        Dictionary<int, Bestellung> _bestellungen;
        Dictionary<int, Lieferung> _lieferungen;

        public Management()
        {
            _bestellungen = new Dictionary<int, Bestellung>();
            _kunden = new Dictionary<int, Kunde>();
            _produkte = new Dictionary<int, Produkt>();
            _lieferungen = new Dictionary<int, Lieferung>();
            _lieferanten = new Dictionary<int, Lieferant>();

        }

        public Produkt CreateProdukt(int artikelNummer, int verfallsZeit, string bezeichnung, int maximalMenge)
        {

            Produkt p = new Produkt(artikelNummer, verfallsZeit, bezeichnung, maximalMenge);
            _produkte.Add(artikelNummer, p);
            return p;
        }

        public Lieferant CreateLieferant(int lieferantenNummer, string bezeichnung)
        {

            Lieferant l = new Lieferant(lieferantenNummer, bezeichnung);
            _lieferanten.Add(lieferantenNummer, l);
            return l;
        }

        public Kunde CreateKunde(int kundenNummer, string bezeichnung)
        {

            Kunde k = new Kunde(kundenNummer, bezeichnung);
            _kunden.Add(kundenNummer, k);
            return k;
        }

        public Bestellung CreateBestellung(int bestellNummer, Kunde kunde, Produkt produkt, int menge, DateTime lieferdatum)
        {
            Bestellung b = new Bestellung(bestellNummer, kunde, produkt, menge, lieferdatum);
            _bestellungen.Add(bestellNummer, b);
            return b;
        }

        public Lieferung? CreateLieferung(int lieferNummer, int artikelNummer, Lieferant lieferant, int menge)
        {
            int lieferzeit = lieferant.CanDeliver(artikelNummer);
            if (lieferzeit != -1)
            {
                bool lagerVoll = false;
                int bestandsMenge = _produkte[artikelNummer].GetProductsInStorage();
                DateTime lieferDatum = DateTime.Today.AddDays(lieferzeit);
                List<Bestellung> bestellungen = GetOrdersByDay(lieferDatum);
                int mengeGleicherTag = 0;
                foreach (var p in bestellungen)
                {
                    mengeGleicherTag += p.Menge;
                }
                if (bestandsMenge + menge - mengeGleicherTag > _produkte[artikelNummer].MaximalMenge)
                    lagerVoll = true;
                if (!lagerVoll && lieferzeit != 0)
                {
                    Lieferung l = new Lieferung(lieferNummer, _produkte[artikelNummer], menge, lieferant, lieferDatum);
                    _lieferungen.Add(lieferNummer, l);
                    return l;
                }
            }
            return null;
        }

        public Dictionary<int, int> CheckExpirationDates()
        {
            var list = new Dictionary<int, int>();
            foreach (var p in _produkte)
            {
                int amount = p.Value.CheckExpirationDates();
                if (amount != 0)
                    list.Add(p.Value.ArtikelNummer, amount);

            }
            return list;
        }

        public void ProcessTodaysOrders()
        {
            
            foreach (var p in _bestellungen)
            {
                if (p.Value.Lieferdatum == DateTime.Today)
                {
                    p.Value.Produkt.RemoveFromStorage(p.Value.Menge);
                }

            }
        }

        public List<Bestellung> GetOrdersByDay(DateTime day)
        {
            var list = new List<Bestellung>();
            foreach (var p in _bestellungen)
            {
                if (p.Value.Lieferdatum == day)
                {
                    list.Add(p.Value);
                }
            }
            return list;
        }
        public List<Lieferung> GetExpectedDeliveries()
        {
            var list = new List<Lieferung>();
            foreach (var p in _lieferungen)
            {
                if (p.Value.Lieferdatum > DateTime.Today)
                {
                    list.Add(p.Value);
                }
            }
            return list;
        }

        public List<Lieferung> GetExpectedDeliveriesFromSupplier(int lieferantenNummer)
        {
            var list = new List<Lieferung>();
            foreach (var p in _lieferungen)
            {
                if (p.Value.Lieferdatum > DateTime.Today && p.Value.Lieferant.LieferantenNummer == lieferantenNummer)
                {
                    list.Add(p.Value);
                }
            }
            return list;
        }

        public List<Bestellung> GetOrdersFromCustomer(int kundenNummer)
        {
            var list = new List<Bestellung>();
            foreach (var p in _bestellungen)
            {
                if (p.Value.Kunde.KundenNummer == kundenNummer)
                {
                    list.Add(p.Value);
                }
            }
            return list;
        }

    }
}

