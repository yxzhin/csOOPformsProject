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
    }
}
