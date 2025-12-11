using csOOPformsProject.Core;
using csOOPformsProject.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace csOOPformsProject.Repositories
{
    public class JsonRepozitorium<T>
        : IRepozitorium<T> where T : class, IEntitet
    {

        private readonly string _putanjaFajla;
        private readonly JsonSerializerOptions _options =
            new JsonSerializerOptions()
            {
                WriteIndented = true,
                ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };
        protected readonly List<T> _entiteti;

        private List<T> Ucitaj()
        {
            if (!File.Exists(_putanjaFajla))
            {
                return null;
            }

            string json = File.ReadAllText(_putanjaFajla);
            try
            {
                return JsonSerializer.Deserialize<List<T>>
                    (json, _options);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public JsonRepozitorium(string putanjaFajla)
        {
            _putanjaFajla = putanjaFajla;
            _entiteti = Ucitaj() ?? new List<T>();
        }

        public List<T> UcitajSve()
        {
            return _entiteti;
        }

        public T UcitajPoId(int id)
        {
            return _entiteti.FirstOrDefault(x => x.Id == id);
        }

        public void Dodaj(T entitet)
        {
            // auto increment
            entitet.Id += 1;
            _entiteti.Add(entitet);
            Sacuvaj();
        }

        public virtual short Promeni(T entitet, int? noviId = null)
        {
            int index = _entiteti.FindIndex(x => x.Id == entitet.Id);
            // nije pronadjen
            if (index == -1)
            {
                return -1;
            }

            // vec postoji
            if (_entiteti.FindAll(x => x.Id == noviId).Count() > 0)
            {
                return -2;
            }

            if (noviId != null)
            {
                entitet.Id = (int)noviId;
            }

            _entiteti[index] = entitet;
            Sacuvaj();

            return 1;
        }

        public bool Obrisi(int id)
        {
            int obrisani = _entiteti.RemoveAll(x => x.Id == id);
            Sacuvaj();
            return obrisani > 0;
        }

        public bool ObrisiSve()
        {
            int obrisani = _entiteti.RemoveAll(x => true);
            Sacuvaj();
            return _entiteti.Count > 0;
        }

        public void Sacuvaj()
        {
            string json = JsonSerializer.Serialize(_entiteti, _options);
            if (!Directory.Exists(Helpers.DataFolder))
            {
                _ = Directory.CreateDirectory(Helpers.DataFolder);
            }
            File.WriteAllText(_putanjaFajla, json);
        }

        public int PoslednjiId()
        {
            return _entiteti.Count > 0 ? _entiteti.Max(x => x.Id) : 0;
        }

        public int PrviId()
        {
            return _entiteti.Count > 0 ? _entiteti.Min(x => x.Id) : 0;
        }
    }
}
