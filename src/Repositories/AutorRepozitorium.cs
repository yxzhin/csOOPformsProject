using csOOPformsProject.Core;
using csOOPformsProject.Models;
using System.Linq;

namespace csOOPformsProject.Repositories
{
    public class AutorRepozitorium : JsonRepozitorium<Autor>
    {
        public AutorRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }

        public Autor UcitajPoPunomImenu(string punoIme)
        {
            return _entiteti.FirstOrDefault(x => x.PunoIme == punoIme);
        }

        public override short Promeni(Autor entitet,
            int? noviId = null)
        {
            if (_entiteti.FirstOrDefault
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
                    Greska.Show(-4, "autor nije pronadjen!!");
                    break;
                case -2:
                    Greska.Show(-2);
                    break;
            }
            return result;
        }
    }
}
