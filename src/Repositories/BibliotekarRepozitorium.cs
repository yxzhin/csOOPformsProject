using csOOPformsProject.Core;
using csOOPformsProject.Models;
using System.Linq;

namespace csOOPformsProject.Repositories
{
    public sealed class BibliotekarRepozitorium
        : NalogRepozitorium<Bibliotekar>
    {
        public BibliotekarRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }

        public override short Promeni(Bibliotekar entitet,
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
                    Greska.Show(-4, "bibliotekar nije pronadjen!!");
                    break;
                case -2:
                    Greska.Show(-2);
                    break;
            }
            return result;
        }
    }
}
