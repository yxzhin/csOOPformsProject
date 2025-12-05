using csOOPformsProject.Models;

namespace csOOPformsProject.Repositories
{
    public class ZaduzivanjeRepozitorium : JsonRepozitorium<Zaduzivanje>
    {
        public ZaduzivanjeRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }
    }
}
