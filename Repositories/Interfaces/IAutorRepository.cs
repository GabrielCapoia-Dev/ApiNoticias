using ApiNoticias.Models;

namespace ApiNoticias.Repositories.Interfaces;

public interface IAutorRepository
{
    IEnumerable<Autor> Get();
    Autor? GetById(int id);

    void Adicionar(Autor autor);
    void Alterar(Autor autor);
    void Excluir(int id);
}

