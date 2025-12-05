using csOOPformsProject.Interfaces;
namespace csOOPformsProject.Models
{
    public sealed class Knjiga : IEntitet
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public Autor Autor { get; set; }
        public Kategorija Kategorija { get; set; }
        public bool NaStanju { get; set; } = true;
        public Knjiga(int id, string naziv, Autor autor, Kategorija kategorija,
            bool naStanju = true)
        {
            Id = id;
            Naziv = naziv;
            Autor = autor;
            Kategorija = kategorija;
            NaStanju = naStanju;
        }
        public override string ToString()
        {
            return $"Knjiga.Id: {Id}; Knjiga.Naziv: {Naziv}";
        }
    }
}
