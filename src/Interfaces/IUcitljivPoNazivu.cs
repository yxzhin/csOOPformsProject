namespace csOOPformsProject.Interfaces
{
    public interface IUcitljivPoNazivu<T>
        where T : class, IEntitet, INazvan
    {
        T UcitajPoNazivu(string naziv);
    }
}
