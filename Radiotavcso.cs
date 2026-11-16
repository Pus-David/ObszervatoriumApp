using System;
using System.Collections.Generic;
using System.Text;

namespace ObszervatoriumApp
{
    internal class Radiotavcso : Tavcso
    {
        public int FrekvenciaMHz { get; set; }

        public Radiotavcso(string azonosito, int atmeroCm, int uzemora, int frekvenciaMHz) : base(azonosito, atmeroCm, uzemora)
        {
            FrekvenciaMHz = frekvenciaMHz;
        }

        public override int FenntartasiKolts()
        {
            return base.FenntartasiKolts() + FrekvenciaMHz * 2;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Frekvencia: {FrekvenciaMHz} MHz";
        }
    }
}
