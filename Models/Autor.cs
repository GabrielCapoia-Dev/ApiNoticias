namespace ApiNoticias.Models;

public class Autor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public List<Noticia> Noticias { get; set; }

    public Autor()
    {
        Noticias = new List<Noticia>();
    }

    public Autor(int id, string nome, string email)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Noticias = new List<Noticia>();
    }

    public Autor(int id, string nome, string email, List<Noticia> noticias)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Noticias = noticias ?? new List<Noticia>();
    }
}
