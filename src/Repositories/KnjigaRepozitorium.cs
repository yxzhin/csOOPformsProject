using csOOPformsProject.Core;
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

        public override short Promeni(Knjiga entitet,
            int? noviId = null)
        {
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
