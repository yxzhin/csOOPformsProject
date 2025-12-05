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
        private readonly List<T> _entiteti;

        private List<T> Ucitaj()
        {
            if (!File.Exists(_putanjaFajla))
            {
                return null;
            }

            string json = File.ReadAllText(_putanjaFajla);
            try
            {
                return JsonSerializer.Deserialize<List<T>>(json, _options);
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
            entitet.Id = _entiteti.Count > 0 ? _entiteti.Max(x => x.Id) + 1 : 1;
            _entiteti.Add(entitet);
            Sacuvaj();
        }

        public void Promeni(T entitet)
        {
            int index = _entiteti.FindIndex(x => x.Id == entitet.Id);
            if (index == -1)
            {
                return;
            }

            _entiteti[index] = entitet;
            Sacuvaj();
        }

        public void Obrisi(int id)
        {
            _ = _entiteti.RemoveAll(x => x.Id == id);
            Sacuvaj();
        }

        public void Sacuvaj()
        {
            string json = JsonSerializer.Serialize(_entiteti, _options);
            File.WriteAllText(_putanjaFajla, json);
        }
    }

}
