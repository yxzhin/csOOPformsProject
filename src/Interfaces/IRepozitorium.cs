using System.Collections.Generic;

namespace csOOPformsProject.Interfaces
{
    public interface IRepozitorium<T> where T : IEntitet
    {
        List<T> UcitajSve();
        T UcitajPoId(int id);
        void Dodaj(T entitet);
        bool Promeni(T entitet, int? noviId = null);
        bool Obrisi(int id);
        void Sacuvaj(); // sacuvaj u json
        int PoslednjiId();
    }
}
