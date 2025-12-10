using csOOPformsProject.Models;

namespace csOOPformsProject.Repositories
{
    public sealed class KorisnikRepozitorium : NalogRepozitorium<Korisnik>
    {
        public KorisnikRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }
    }
}
