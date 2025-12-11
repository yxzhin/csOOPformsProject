using csOOPformsProject.Models;

namespace csOOPformsProject.Repositories
{
    public class AutorRepozitorium : JsonRepozitorium<Autor>
    {
        public AutorRepozitorium(string putanjaFajla)
            : base(putanjaFajla)
        {
            return;
        }
    }
}
