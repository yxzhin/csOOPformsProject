using csOOPformsProject.Interfaces;

namespace csOOPformsProject.Models
{
    public sealed class Kategorija : IEntitet
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public Kategorija(int id, string naziv)
        {
            Id = id;
            Naziv = naziv;
        }
        public override string ToString()
        {
            return Naziv;
        }
    }
}
