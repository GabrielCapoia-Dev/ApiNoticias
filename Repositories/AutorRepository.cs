using ApiNoticias.Models;
using ApiNoticias.Repositories.Interfaces;

namespace ApiNoticias.Repositories;

public class AutorRepository : IAutorRepository
{
    private static List<Autor> _autores = new List<Autor>();
    private static int _nextId = 1;

    public AutorRepository()
    {
        // Dados iniciais para teste
        if (_autores.Count == 0)
        {
            _autores.Add(new Autor(1, "João Silva", "joao.silva@email.com"));
            _autores.Add(new Autor(2, "Maria Santos", "maria.santos@email.com"));
            _nextId = 3;
        }
    }

    public IEnumerable<Autor> Get()
    {
        return _autores;
    }

    public Autor? GetById(int id)
    {
        return _autores.FirstOrDefault(a => a.Id == id);
    }

    public void Adicionar(Autor autor)
    {
        autor.Id = _nextId++;
        _autores.Add(autor);
    }

    public void Alterar(Autor autor)
    {
        var autorExistente = GetById(autor.Id);
        if (autorExistente != null)
        {
            autorExistente.Nome = autor.Nome;
            autorExistente.Email = autor.Email;
        }
    }

    public void Excluir(int id)
    {
        var autor = GetById(id);
        if (autor != null)
        {
            _autores.Remove(autor);
        }
    }
}

