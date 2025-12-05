using csOOPformsProject.Models;

namespace csOOPformsProject.Repositories
{
    public class BibliotekarRepozitorium : JsonRepozitorium<Bibliotekar>
    {
        public BibliotekarRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }
    }
}
