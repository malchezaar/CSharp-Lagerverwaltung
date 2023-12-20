using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

namespace Lagerverwaltung
{

    public class Management
    {
        Produkte _produkte;
        Lieferanten _lieferanten;
        Kunden _kunden;
        Bestellungen _bestellungen;
        Dictionary<int, Lieferung> _lieferungen;

        public Kunden Kunden
        {
            get => _kunden;
        }

        public Produkte Produkte
        {
            get => _produkte;
        }

        public Bestellungen Bestellungen
        {
            get => _bestellungen;
        }

        public Lieferanten Lieferanten
        {
            get => _lieferanten;
        }

        public Management()
        {
            _bestellungen = new Bestellungen();
            _kunden = new Kunden();
            _produkte = new Produkte();
            _lieferungen = new Dictionary<int, Lieferung>();
            _lieferanten = new Lieferanten();

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
                int amount = p.CheckExpirationDates();
                if (amount != 0)
                    list.Add(p.ArtikelNummer, amount);

            }
            return list;
        }

        public void ProcessTodaysOrders()
        {
            
            foreach (var p in _bestellungen)
            {
                if (p.Lieferdatum == DateTime.Today)
                {
                    p.Produkt.RemoveFromStorage(p.Menge);
                }

            }
        }

        public void ProcessTodaysDeliveries()
        {

            foreach (var p in _lieferungen)
            {
                if (p.Value.Lieferdatum == DateTime.Today)
                {
                    p.Value.Produkt.AddToStorage(p.Value);
                }

            }
        }


        public List<Bestellung> GetOrdersByDay(DateTime day)
        {
            var list = new List<Bestellung>();
            foreach (var p in _bestellungen)
            {
                if (p.Lieferdatum == day)
                {
                    list.Add(p);
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
                if (p.Kunde.KundenNummer == kundenNummer)
                {
                    list.Add(p);
                }
            }
            return list;
        }

    }
}

