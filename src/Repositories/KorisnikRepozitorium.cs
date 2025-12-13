using csOOPformsProject.Core;
using csOOPformsProject.Models;
using System.Linq;

namespace csOOPformsProject.Repositories
{
    public sealed class KorisnikRepozitorium
        : NalogRepozitorium<Korisnik>
    {
        public KorisnikRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }

        public override short Promeni(Korisnik entitet,
            int? noviId = null)
        {
            if (noviId == null
                && _entiteti.FirstOrDefault
                (x => x.PunoIme == entitet.PunoIme
                && x.Id != entitet.Id) != null)
            {
                Greska.Show(-2, "puno ime");
                return -3;
            }

            short result = base.Promeni(entitet, noviId);
            switch (result)
            {
                case -1:
                    Greska.Show(-4, "korisnik nije pronadjen!!");
                    break;
                case -2:
                    Greska.Show(-2);
                    break;
            }
            return result;
        }
    }
}
