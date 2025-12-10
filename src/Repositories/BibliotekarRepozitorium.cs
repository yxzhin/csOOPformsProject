using csOOPformsProject.Models;

namespace csOOPformsProject.Repositories
{
    public sealed class BibliotekarRepozitorium : NalogRepozitorium<Bibliotekar>
    {
        public BibliotekarRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }
    }
}
