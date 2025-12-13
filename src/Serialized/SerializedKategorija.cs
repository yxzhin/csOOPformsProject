namespace csOOPformsProject.Serialized
{
    public class SerializedKategorija
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public SerializedKategorija(int id, string naziv)
        {
            Id = id;
            Naziv = naziv;
        }
    }
}
