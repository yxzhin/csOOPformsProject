using csOOPformsProject.Models;
using System.Collections.Generic;
using System.IO;

namespace csOOPformsProject.Core
{
    public static class Helpers
    {
        public static string DataFolder =>
            GetSolutionFolderPathFromProject() + "\\src\\Data";
        public static string GetSolutionFolderPathFromProject()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string solutionPath =
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(currentDirectory)));
            return solutionPath;
        }

        public static class Serialize
        {
            public static List<SerializedKnjiga> Knjige(List<Knjiga> knjige)
            {
                List<SerializedKnjiga> serializedKnjige = new List<SerializedKnjiga>();
                foreach (Knjiga knjiga in knjige)
                {
                    SerializedKnjiga serializedKnjiga = new SerializedKnjiga(
                        knjiga.Id,
                        knjiga.Naziv,
                        knjiga.Autor.PunoIme,
                        knjiga.Kategorija.Naziv,
                        knjiga.NaStanju
                    );
                    serializedKnjige.Add(serializedKnjiga);
                }
                return serializedKnjige;
            }
            public static List<SerializedKorisnik> Korisnici(List<Korisnik> korisnici)
            {
                List<SerializedKorisnik> serializedKorisnici = new List<SerializedKorisnik>();
                foreach (Korisnik korisnik in korisnici)
                {
                    SerializedKorisnik serializedKorisnik = new SerializedKorisnik(
                        korisnik.Id,
                        korisnik.Ime,
                        korisnik.Prezime,
                        korisnik.DatumRodjenja,
                        korisnik.DatumClanarine,
                        korisnik.Zaduzivanja
                    );
                    serializedKorisnici.Add(serializedKorisnik);
                }
                return serializedKorisnici;
            }
        }
    }
}
