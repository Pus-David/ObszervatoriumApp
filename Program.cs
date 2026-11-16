
using ObszervatoriumApp;

List<Tavcso> tavcsovek = new List<Tavcso>();
int hibasSorokSzama = 0;
string[] adatok;

foreach (var sor in File.ReadAllLines("tavcsovek.txt").Skip(1))
{
    adatok = sor.Split("|");

    if (adatok.Length != 5)
    {
        hibasSorokSzama++;
        continue;
    }

    if (adatok[0] != "O" && adatok[0] != "R")
    {
        hibasSorokSzama++;
        continue;
    }

    if (!int.TryParse(adatok[2], out int atmero) ||
        !int.TryParse(adatok[3], out int uzemora) ||
        !int.TryParse(adatok[4], out int extra))
    {
        hibasSorokSzama++;
        continue;
    }

    if (adatok[0] == "O")
    {
        tavcsovek.Add(new Tavcso(adatok[1], atmero, uzemora));
    }
    else
    {
        tavcsovek.Add(new Radiotavcso(adatok[1], atmero, uzemora, extra));
    }
}

Console.WriteLine("Sikeresen beolvasott távcsövek: " + tavcsovek.Count);
Console.WriteLine("Hibás sorok száma: " + hibasSorokSzama);

Console.WriteLine("=== Beolvasott távcsövek ===");

foreach (var e in tavcsovek)
{
    Console.WriteLine(e);
}

Console.WriteLine("\nÖsszes éves fenntartási költség: " + tavcsovek.Sum(e => e.FenntartasiKolts()) + " EUR");

Console.WriteLine("Átlagos üzemóra: " + tavcsovek.Average(e => e.Uzemora) + " óra");

var legnagyobb = tavcsovek.MaxBy(e => e.AtmeroCm);
var legdragabb = tavcsovek.MaxBy(e => e.FenntartasiKolts());

Console.WriteLine("Legnagyobb átmérőjű távcső: " + legnagyobb.Azonosito + ", " + legnagyobb.AtmeroCm + " cm");

Console.WriteLine("Legnagyobb fenntartási költségű távcső: " + legdragabb.Azonosito + ", " + legdragabb.FenntartasiKolts() + " EUR");

Console.WriteLine("Legalább 1000 üzemórás távcsövek száma: " +
    tavcsovek.Count(e => e.Uzemora >= 1000));

if (tavcsovek.Any(e => e.FenntartasiKolts() > 40000))
{
    Console.WriteLine("Van 40 000 EUR-nál drágább fenntartású távcső.");
}
else
{
    Console.WriteLine("Nincs 40 000 EUR-nál drágább fenntartású távcső.");
}

Console.WriteLine("\nLegalább 300 cm átmérőjű távcsövek:");

foreach (var e in tavcsovek.Where(e => e.AtmeroCm >= 300).OrderByDescending(e => e.AtmeroCm))
{
    Console.WriteLine(e);
}

var radiok = tavcsovek.OfType<Radiotavcso>();

Console.WriteLine("Rádiótávcsövek átlagos frekvenciája: " + radiok.Average(e => e.FrekvenciaMHz) + " MHz");

var aktiv = tavcsovek.Where(e => e.Uzemora > 500).OrderBy(e => e.Uzemora).Select(e => e.Azonosito + ";" + e.AtmeroCm + ";" + e.Uzemora + ";" + e.FenntartasiKolts());

File.WriteAllLines("aktiv_tavcsovek.txt", aktiv);

Console.WriteLine("\nAz aktív távcsövek adatai elmentve az aktiv_tavcsovek.txt fájlba.");