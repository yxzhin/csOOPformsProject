using csOOPformsProject.Models;

namespace csOOPformsProject.Serialized
{
    public sealed class SerializedKnjiga
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Autor { get; set; }
        public string Kategorija { get; set; }
        public bool NaStanju { get; set; }
        public SerializedKnjiga(int id, string naziv, Autor autor,
            Kategorija kategorija, bool naStanju)
        {
            Id = id;
            Naziv = naziv;
            Autor = autor.ToString();
            Kategorija = kategorija.ToString();
            NaStanju = naStanju;
        }
    }
}
