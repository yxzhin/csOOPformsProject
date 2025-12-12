using csOOPformsProject.Core;
using csOOPformsProject.Models;
using System.Linq;

namespace csOOPformsProject.Repositories
{
    public class KategorijaRepozitorium : JsonRepozitorium<Kategorija>
    {
        public KategorijaRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }

        public Kategorija UcitajPoNazivu(string naziv)
        {
            return _entiteti.FirstOrDefault(x => x.Naziv == naziv);
        }

        public override short Promeni(Kategorija entitet,
            int? noviId = null)
        {
            if (_entiteti.FirstOrDefault
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
                    Greska.Show(-4, "kategorija nije pronadjena!!");
                    break;
                case -2:
                    Greska.Show(-2);
                    break;
            }
            return result;
        }
    }
}
