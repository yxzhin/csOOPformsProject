using System.Collections.Generic;

namespace csOOPformsProject.Interfaces
{
    public interface IRepozitorium<T> where T : class, IEntitet
    {
        List<T> UcitajSve();
        T UcitajPoId(int id);
        void Dodaj(T entitet);
        short Promeni(T entitet, int? noviId = null);
        bool Obrisi(int id);
        bool ObrisiSve();
        void Sacuvaj(); // sacuvaj u json
        int PoslednjiId();
        int PrviId();
    }
}
