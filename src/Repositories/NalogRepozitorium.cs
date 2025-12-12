using csOOPformsProject.Models;
using System.Linq;

namespace csOOPformsProject.Repositories
{
    public class NalogRepozitorium<T>
        : JsonRepozitorium<T> where T : Osoba
    {
        public NalogRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }

        public T UcitajPoPodacima(string ime, string prezime, string sifra)
        {
            return _entiteti.FirstOrDefault(x =>
            x.Ime == ime && x.Prezime == prezime && x.Sifra == sifra);
        }
    }
}
