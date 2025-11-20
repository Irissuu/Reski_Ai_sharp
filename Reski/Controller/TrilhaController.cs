﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reski.Application.DTO.Hateoas;
using Reski.Application.DTO.Request;
using Reski.Application.DTO.Response;
using Reski.Domain.Entity;
using Reski.Infrastructure.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace Reski.Controller;

/// <summary>Endpoints para gerenciamento de trilhas.</summary>
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/trilhas")]
[Produces("application/json")]
public class TrilhaController : ControllerBase
{
    private readonly AppDbContext _context;

    public TrilhaController(AppDbContext context) => _context = context;

    private static TrilhaResponse ToResponse(Trilha t) => new()
    {
        Id          = t.Id,
        Status      = t.Status,
        Conteudo    = t.Conteudo,
        Competencia = t.Competencia
    };

    private TrilhaResponse WithLinks(TrilhaResponse r)
    {
        var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

        r.Links ??= new List<Link>();
        r.Links.Clear();

        var self   = Url.Link(nameof(GetTrilha),    new { id = r.Id, version });
        var update = Url.Link(nameof(UpdateTrilha), new { id = r.Id, version });
        var delete = Url.Link(nameof(DeleteTrilha), new { id = r.Id, version });

        if (self   is not null) r.Links.Add(new Link("self",   self,   "GET"));
        if (update is not null) r.Links.Add(new Link("update", update, "PUT"));
        if (delete is not null) r.Links.Add(new Link("delete", delete, "DELETE"));

        return r;
    }

    /// <summary>Lista todas as trilhas.</summary>
    [SwaggerOperation(Summary = "Lista todas as trilhas")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TrilhaResponse>), 200)]
    public async Task<ActionResult<IEnumerable<TrilhaResponse>>> GetTrilhas()
    {
        var trilhas = await _context.Trilhas.AsNoTracking().ToListAsync();
        var resp = trilhas.Select(t => WithLinks(ToResponse(t)));
        return Ok(resp);
    }

    /// <summary>Busca uma trilha por ID.</summary>
    [SwaggerOperation(Summary = "Busca uma trilha por ID")]
    [HttpGet("{id:int}", Name = nameof(GetTrilha))]
    [ProducesResponseType(typeof(TrilhaResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<TrilhaResponse>> GetTrilha(int id)
    {
        var trilha = await _context.Trilhas.FindAsync(id);
        if (trilha == null) return NotFound();

        return Ok(WithLinks(ToResponse(trilha)));
    }

    /// <summary>Cadastra uma nova trilha.</summary>
    [SwaggerOperation(Summary = "Cadastra uma nova trilha")]
    [HttpPost]
    [ProducesResponseType(typeof(TrilhaResponse), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<TrilhaResponse>> CreateTrilha([FromBody] TrilhaRequest request)
    {
        try
        {
            var trilha = new Trilha(request.Status, request.Conteudo, request.Competencia);

            _context.Trilhas.Add(trilha);
            await _context.SaveChangesAsync();

            var resp    = WithLinks(ToResponse(trilha));
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

            return CreatedAtAction(nameof(GetTrilha), new { id = trilha.Id, version }, resp);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>Atualiza uma trilha existente.</summary>
    [SwaggerOperation(Summary = "Atualiza uma trilha existente")]
    [HttpPut("{id:int}", Name = nameof(UpdateTrilha))]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateTrilha(int id, [FromBody] TrilhaRequest request)
    {
        var trilha = await _context.Trilhas.FindAsync(id);
        if (trilha == null) return NotFound();

        try
        {
            trilha.AtualizarDados(request.Status, request.Conteudo, request.Competencia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>Remove uma trilha.</summary>
    [SwaggerOperation(Summary = "Remove uma trilha")]
    [HttpDelete("{id:int}", Name = nameof(DeleteTrilha))]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteTrilha(int id)
    {
        var trilha = await _context.Trilhas.FindAsync(id);
        if (trilha == null) return NotFound();

        _context.Trilhas.Remove(trilha);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}