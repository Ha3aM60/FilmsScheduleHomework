using ClassLibrary2;
using System.Text.Json;
using System.Web.Mvc;

namespace ClassLibrary1
{
    [Serializable]
    public class FilmRepository : IFilmIRepository
    {
        
      public  List<Film> Films { get; set; }
        public FilmRepository()
        {
            Films = new List<Film>
        {
            new Film {Id=1, Name = "Spider Man", Author = "Author name", Style = "Style film 111" , Description ="qqqwqwwqwqwqwqwqw"},
            new Film {Id=2, Name = "Film name tatata", Author = "Author name qqwqwq", Style = "Style 2wqesdasd", Description ="Something about" },
            new Film {Id=3, Name = "Name qqqq", Author = "Tratatatata", Style = "Style pipipi", Description ="Qwertyuiopaqsdsasdasdas" }
        };
        }
        public void AddFilm(Film f)
        {
            f.Id = Films.Max(x => x.Id) + 1;
            Films.Add(f);
            using (FileStream fs = new FileStream("films.json", FileMode.Create))
            {
                JsonSerializer.SerializeAsync<List<Film>>(fs, Films);
            }
        }

        public void DeleteFilm(int id)
        {
            var f = Films.FirstOrDefault(x => x.Id == id);
            if (f != null)
                Films.Remove(f);
            using (FileStream fs = new FileStream("films.json", FileMode.Create))
            {
                JsonSerializer.SerializeAsync<List<Film>>(fs, Films);
            }
        }

        public List<Film> GetAllFilm()
        {
            using (FileStream fs = new FileStream("films.json", FileMode.Open))
            { 
                try
                {
                Films = JsonSerializer.Deserialize<List<Film>>(fs);
                }
                catch
                {
                    Films = new List<Film>
        {
            new Film {Id=1, Name = "Spider Man", Author = "Author name", Style = "Style film 111" , Description ="qqqwqwwqwqwqwqwqw"},
            new Film {Id=2, Name = "Film name tatata", Author = "Author name qqwqwq", Style = "Style 2wqesdasd", Description ="Something about" },
            new Film {Id=3, Name = "Name qqqq", Author = "Tratatatata", Style = "Style pipipi", Description ="Qwertyuiopaqsdsasdasdas" }
        };
                    JsonSerializer.Serialize<List<Film>>(fs, Films);
                }

            }
            return Films;
        }

        public Film GetFilmById(int id)
        {
            return Films.FirstOrDefault(x => x.Id == id);
        }
        [HttpPost]
        public void UpdateFilm(Film f)
        {
            var existingFilm = Films.FirstOrDefault(x => x.Id == f.Id);
            Films.Remove(existingFilm);
            Films.Add(f);
            using (FileStream fs = new FileStream("films.json", FileMode.Create))
            {
                JsonSerializer.SerializeAsync<List<Film>>(fs, Films);
            }
        }
    }
}
