using Lagerverwaltung;

Management m = new Management();

Produkt produkt = m.CreateProdukt(1, 30, "Bananensaft", 100);
Lieferant lieferant = m.CreateLieferant(1, "Bananensaft GmbH");
lieferant.AddProduct(produkt.ArtikelNummer, 12);
Lieferung? lieferung = m.CreateLieferung(1, produkt.ArtikelNummer, lieferant, 15);
lieferung = m.CreateLieferung(2, produkt.ArtikelNummer, lieferant, 85);
if (lieferung != null)
    lieferung.Stornieren();

List<Lieferung> l = m.GetExpectedDeliveries();

Kunde k = m.CreateKunde(1, "Klaus");

Bestellung b = new Bestellung(1, k, produkt, 25, DateTime.Today.AddDays(10));



