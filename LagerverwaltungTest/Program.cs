using Lagerverwaltung;

Management m = new Management();

Produkt produkt = m.Produkte.Add(30, "Bananensaft", 100);
Lieferant lieferant = m.Lieferanten.Add(1, "Bananensaft GmbH");
lieferant.AddProduct(produkt.ArtikelNummer, 12);
Lieferung? lieferung = m.CreateLieferung(1, produkt.ArtikelNummer, lieferant, 15);
if (lieferung != null)
    lieferung.Stornieren();

List<Lieferung> l = m.GetExpectedDeliveries();
List<Lieferung> l2 = m.GetExpectedDeliveriesFromSupplier(0);


Kunde k = m.Kunden.Add(1, "Klaus");

Bestellung b = m.Bestellungen.Add(1, k, produkt, 25, DateTime.Today.AddDays(10));

m.ProcessTodaysDeliveries();
m.ProcessTodaysOrders();

