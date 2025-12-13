using csOOPformsProject.Core;
using csOOPformsProject.Interfaces;
using csOOPformsProject.Models;
using System.Linq;

namespace csOOPformsProject.Repositories
{
    public class KnjigaRepozitorium
        : JsonRepozitorium<Knjiga>, IUcitljivPoNazivu<Knjiga>
    {
        public KnjigaRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }

        public Knjiga UcitajPoNazivu(string naziv)
        {
            return _entiteti.FirstOrDefault(x => x.Naziv == naziv);
        }

        public override short Promeni(Knjiga entitet,
            int? noviId = null)
        {
            if (noviId == null
                && _entiteti.FirstOrDefault
                (x => x.Naziv == entitet.Naziv
                && x.Id != entitet.Id) != null)
            {
                Greska.Show(-2, "naziv");
                return -3;
            }

            short result = base.Promeni(entitet, noviId);
            switch (result)
            {
                case -1:
                    Greska.Show(-4, "knjiga nije pronadjena!!");
                    break;
                case -2:
                    Greska.Show(-2);
                    break;
            }
            return result;
        }
    }
}
