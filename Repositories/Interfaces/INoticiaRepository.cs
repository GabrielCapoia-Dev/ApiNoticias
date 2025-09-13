using ApiNoticias.Models;

namespace ApiNoticias.Repositories.Interfaces;

public interface INoticiaRepository
{
    IEnumerable<Noticia> Get();
    Noticia? GetById(int id);

    void Adicionar(Noticia noticia);
    void Alterar(Noticia noticia);
    void Excluir(int id);
}

