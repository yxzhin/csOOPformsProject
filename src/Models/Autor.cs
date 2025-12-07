using csOOPformsProject.Interfaces;
namespace csOOPformsProject.Models
{
    public sealed class Autor : IEntitet, IImenovan
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string PunoIme => $"{Ime} {Prezime}";
        public Autor(int id, string ime, string prezime)
        {
            Id = id;
            Ime = ime;
            Prezime = prezime;
        }
        public override string ToString()
        {
            return PunoIme;
        }
    }
}
