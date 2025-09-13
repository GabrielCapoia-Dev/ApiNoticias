using ApiNoticias.Models;
using ApiNoticias.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiNoticias.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NoticiaController : ControllerBase
{
    private readonly INoticiaRepository _noticiaRepository;
    private readonly IAutorRepository _autorRepository;

    public NoticiaController(INoticiaRepository noticiaRepository, IAutorRepository autorRepository)
    {
        _noticiaRepository = noticiaRepository;
        _autorRepository = autorRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _noticiaRepository.Get();

        if (result.Count() > 0)
        {
            return Ok(result);
        }

        return NoContent();
    }

    [HttpGet("porId/{id}")]
    public IActionResult GetById(int id)
    {
        var result = _noticiaRepository.GetById(id);
        if (result != null)
        {
            return Ok(result);
        }
        return NotFound();
    }

    [HttpGet("porIdAutor/{autorId}")]
    public IActionResult GetByAutor(int autorId)
    {
        var noticias = _noticiaRepository.Get().Where(n => n.Autor.Id == autorId);
        
        if (noticias.Count() > 0)
        {
            return Ok(noticias);
        }

        return NoContent();
    }

    [HttpPost]
    public IActionResult Adicionar(NoticiaRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Titulo) || string.IsNullOrEmpty(request.Texto))
            {
                return BadRequest("Título e Texto são obrigatórios.");
            }

            var autor = _autorRepository.GetById(request.AutorId);
            if (autor == null)
            {
                return BadRequest("Autor não encontrado.");
            }

            var noticia = new Noticia(0, request.Titulo, request.Texto, DateTime.Now, autor);
            _noticiaRepository.Adicionar(noticia);
            
            return Ok(noticia);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Alterar(int id, NoticiaRequest request)
    {
        try
        {
            var noticiaExistente = _noticiaRepository.GetById(id);
            if (noticiaExistente == null)
            {
                return NotFound();
            }

            var autor = _autorRepository.GetById(request.AutorId);
            if (autor == null)
            {
                return BadRequest("Autor não encontrado.");
            }

            var noticia = new Noticia(id, request.Titulo, request.Texto, noticiaExistente.Data, autor);
            _noticiaRepository.Alterar(noticia);
            
            return Ok(noticia);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Excluir(int id)
    {
        try
        {
            var noticia = _noticiaRepository.GetById(id);
            if (noticia == null)
            {
                return NotFound();
            }

            _noticiaRepository.Excluir(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class NoticiaRequest
{
    public string Titulo { get; set; }
    public string Texto { get; set; }
    public int AutorId { get; set; }
}

