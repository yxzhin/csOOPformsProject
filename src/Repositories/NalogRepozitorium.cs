using csOOPformsProject.Interfaces;
using csOOPformsProject.Models;
using System.Linq;

namespace csOOPformsProject.Repositories
{
    public class NalogRepozitorium<T>
        : JsonRepozitorium<T>, IUcitljivPoPunomImenu<T>
        where T : Osoba
    {
        public NalogRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }

        public T UcitajPoPunomImenu(string punoIme)
        {
            return _entiteti.FirstOrDefault(x => x.PunoIme == punoIme);
        }

        public T UcitajPoPodacima(string ime, string prezime, string sifra)
        {
            return _entiteti.FirstOrDefault(x =>
            x.Ime == ime && x.Prezime == prezime && x.Sifra == sifra);
        }
    }
}
