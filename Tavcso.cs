using System;
using System.Collections.Generic;
using System.Text;

namespace ObszervatoriumApp
{
    internal class Tavcso
    {
        private static int osszesTavcso;

        public string Azonosito { get; set; }
        public int AtmeroCm { get; set; }
        public int Uzemora { get; set; }

        public static int OsszesTavcso
        {
            get { return osszesTavcso; }
        }

        public Tavcso(string azonosito, int atmeroCm, int uzemora)
        {
            Azonosito = azonosito;
            AtmeroCm = atmeroCm;
            Uzemora = uzemora;
            osszesTavcso++;
        }

        public virtual int FenntartasiKolts()
        {
            return AtmeroCm * 100 + Uzemora * 15;
        }

        public override string ToString()
        {
            return $"Távcső [{Azonosito}] | Átmérő: {AtmeroCm} cm | " +
                   $"Üzemóra: {Uzemora} óra | Költség: {FenntartasiKolts()} EUR";
        }
    }
}
