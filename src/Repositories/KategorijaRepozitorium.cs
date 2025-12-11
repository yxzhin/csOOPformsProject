using csOOPformsProject.Models;

namespace csOOPformsProject.Repositories
{
    public class KategorijaRepozitorium : JsonRepozitorium<Kategorija>
    {
        public KategorijaRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }
    }
}
