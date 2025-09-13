using ApiNoticias.Models;
using ApiNoticias.Repositories.Interfaces;

namespace ApiNoticias.Repositories;

public class NoticiaRepository : INoticiaRepository
{
    private static List<Noticia> _noticias = new List<Noticia>();
    private static int _nextId = 1;
    private readonly IAutorRepository _autorRepository;

    public NoticiaRepository(IAutorRepository autorRepository)
    {
        _autorRepository = autorRepository;
        
        // Dados iniciais para teste
        if (_noticias.Count == 0)
        {
            var autor1 = _autorRepository.GetById(1);
            var autor2 = _autorRepository.GetById(2);
            
            if (autor1 != null)
            {
                _noticias.Add(new Noticia(1, "Primeira Notícia", "Conteúdo da primeira notícia de teste.", DateTime.Now.AddDays(-2), autor1));
            }
            
            if (autor2 != null)
            {
                _noticias.Add(new Noticia(2, "Segunda Notícia", "Conteúdo da segunda notícia de teste.", DateTime.Now.AddDays(-1), autor2));
            }
            
            _nextId = 3;
        }
    }

    public IEnumerable<Noticia> Get()
    {
        return _noticias;
    }

    public Noticia? GetById(int id)
    {
        return _noticias.FirstOrDefault(n => n.Id == id);
    }

    public void Adicionar(Noticia noticia)
    {
        noticia.Id = _nextId++;
        noticia.Data = DateTime.Now;
        _noticias.Add(noticia);
    }

    public void Alterar(Noticia noticia)
    {
        var noticiaExistente = GetById(noticia.Id);
        if (noticiaExistente != null)
        {
            noticiaExistente.Titulo = noticia.Titulo;
            noticiaExistente.Texto = noticia.Texto;
            noticiaExistente.Autor = noticia.Autor;
        }
    }

    public void Excluir(int id)
    {
        var noticia = GetById(id);
        if (noticia != null)
        {
            _noticias.Remove(noticia);
        }
    }
}

