namespace ApiNoticias.Models;

public class Noticia
{
    #region Propriedades |

    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Texto { get; set; }
    public DateTime Data { get; set; }
    public Autor Autor { get; set; }

    #endregion

    #region | Construtor |

    public Noticia(int id, string titulo, string texto, DateTime data, Autor autor)
    {
        Id = id;
        Titulo = titulo;
        Texto = texto;
        Data = data;
        Autor = autor;
    }

    #endregion
}

