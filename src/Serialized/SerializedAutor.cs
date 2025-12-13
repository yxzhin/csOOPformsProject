namespace csOOPformsProject.Serialized
{
    public class SerializedAutor
    {
        public int Id { get; set; }
        public string PunoIme { get; set; }
        public SerializedAutor(int id, string punoIme)
        {
            Id = id;
            PunoIme = punoIme;
        }
    }
}
