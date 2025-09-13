using ApiNoticias.Models;
using ApiNoticias.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiNoticias.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly IAutorRepository _repository;

    public AutorController(IAutorRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _repository.Get();

        if (result.Count() > 0)
        {
            return Ok(result);
        }

        return NoContent();
    }

    [HttpGet("porId/{id}")]
    public IActionResult GetById(int id)
    {
        var result = _repository.GetById(id);
        if (result != null)
        {
            return Ok(result);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Adicionar(Autor autor)
    {
        try
        {
            if (string.IsNullOrEmpty(autor.Nome) || string.IsNullOrEmpty(autor.Email))
            {
                return BadRequest("Nome e Email são obrigatórios.");
            }

            _repository.Adicionar(autor);
            return Ok(autor);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Alterar(int id, Autor autor)
    {
        try
        {
            autor.Id = id;
            var autorExistente = _repository.GetById(id);
            if (autorExistente == null)
            {
                return NotFound();
            }

            _repository.Alterar(autor);
            return Ok(autor);
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
            var autor = _repository.GetById(id);
            if (autor == null)
            {
                return NotFound();
            }

            _repository.Excluir(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

