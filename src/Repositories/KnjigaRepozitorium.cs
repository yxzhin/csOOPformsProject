using csOOPformsProject.Models;

namespace csOOPformsProject.Repositories
{
    public class KnjigaRepozitorium : JsonRepozitorium<Knjiga>
    {
        public KnjigaRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }
    }
}
