using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagerverwaltung
{

    public class Kunde
    {
        string _bezeichnung;
        int _kundenNummer;
        public int KundenNummer
        {
            get => _kundenNummer;
        }
        internal Kunde(int kundenNummer, string bezeichnung)
        {
            _bezeichnung = bezeichnung;
            _kundenNummer = kundenNummer;
        }
    }
}
