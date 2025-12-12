namespace csOOPformsProject.Interfaces
{
    public interface IUcitljivPoPunomImenu<T>
        where T : class, IEntitet, IImenovan
    {
        T UcitajPoPunomImenu(string punoIme);
    }
}
