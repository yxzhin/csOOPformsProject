using csOOPformsProject.Models;

namespace csOOPformsProject.Repositories
{
    public class KorisnikRepozitorium : JsonRepozitorium<Korisnik>
    {
        public KorisnikRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }
    }
}
