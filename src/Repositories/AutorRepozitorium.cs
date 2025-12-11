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
    }
}
