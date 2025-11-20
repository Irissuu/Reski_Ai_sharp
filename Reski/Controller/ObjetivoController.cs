﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reski.Application.DTO.Hateoas;
using Reski.Application.DTO.Request;
using Reski.Application.DTO.Response;
using Reski.Domain.Entity;
using Reski.Infrastructure.Context;
using Swashbuckle.AspNetCore.Annotations;
using Reski.Application.ML;

namespace Reski.Controller;

/// <summary>Endpoints para gerenciamento de objetivos.</summary>
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/objetivos")]
[Produces("application/json")]
public class ObjetivoController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IRecomendacaoTrilha _recomendacaoTrilha;

    public ObjetivoController(
        AppDbContext context,
        IRecomendacaoTrilha recomendacaoTrilha)
    {
        _context = context;
        _recomendacaoTrilha = recomendacaoTrilha;
    }

    private static ObjetivoResponse ToResponse(Objetivo o) => new()
    {
        Id        = o.Id,
        Cargo     = o.Cargo,
        Area      = o.Area,
        Descricao = o.Descricao,
        Demanda   = o.Demanda
    };

    private ObjetivoResponse WithLinks(ObjetivoResponse r)
    {
        var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

        r.Links ??= new List<Link>();
        r.Links.Clear();

        var self   = Url.Link(nameof(GetObjetivo),    new { id = r.Id, version });
        var update = Url.Link(nameof(UpdateObjetivo), new { id = r.Id, version });
        var delete = Url.Link(nameof(DeleteObjetivo), new { id = r.Id, version });

        if (self   is not null) r.Links.Add(new Link("self",   self,   "GET"));
        if (update is not null) r.Links.Add(new Link("update", update, "PUT"));
        if (delete is not null) r.Links.Add(new Link("delete", delete, "DELETE"));

        return r;
    }

    /// <summary>Lista todos os objetivos.</summary>
    [SwaggerOperation(Summary = "Lista todos os objetivos")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ObjetivoResponse>), 200)]
    public async Task<ActionResult<IEnumerable<ObjetivoResponse>>> GetObjetivos()
    {
        var objetivos = await _context.Objetivos.AsNoTracking().ToListAsync();
        var resp = objetivos.Select(o => WithLinks(ToResponse(o)));
        return Ok(resp);
    }

    /// <summary>Busca um objetivo por ID.</summary>
    [SwaggerOperation(Summary = "Busca um objetivo por ID")]
    [HttpGet("{id:int}", Name = nameof(GetObjetivo))]
    [ProducesResponseType(typeof(ObjetivoResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ObjetivoResponse>> GetObjetivo(int id)
    {
        var objetivo = await _context.Objetivos.FindAsync(id);
        if (objetivo == null) return NotFound();

        return Ok(WithLinks(ToResponse(objetivo)));
    }

    /// <summary>Cadastra um novo objetivo.</summary>
    [SwaggerOperation(Summary = "Cadastra um novo objetivo")]
    [HttpPost]
    [ProducesResponseType(typeof(ObjetivoResponse), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ObjetivoResponse>> CreateObjetivo([FromBody] ObjetivoRequest request)
    {
        try
        {
            var objetivo = new Objetivo(request.Cargo, request.Area, request.Descricao, request.Demanda);

            _context.Objetivos.Add(objetivo);
            await _context.SaveChangesAsync();

            var resp    = WithLinks(ToResponse(objetivo));
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

            return CreatedAtAction(nameof(GetObjetivo), new { id = objetivo.Id, version }, resp);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>Atualiza um objetivo existente.</summary>
    [SwaggerOperation(Summary = "Atualiza um objetivo existente")]
    [HttpPut("{id:int}", Name = nameof(UpdateObjetivo))]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateObjetivo(int id, [FromBody] ObjetivoRequest request)
    {
        var objetivo = await _context.Objetivos.FindAsync(id);
        if (objetivo == null) return NotFound();

        try
        {
            objetivo.AtualizarDados(request.Cargo, request.Area, request.Descricao, request.Demanda);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>Remove um objetivo.</summary>
    [SwaggerOperation(Summary = "Remove um objetivo")]
    [HttpDelete("{id:int}", Name = nameof(DeleteObjetivo))]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteObjetivo(int id)
    {
        var objetivo = await _context.Objetivos.FindAsync(id);
        if (objetivo == null) return NotFound();

        _context.Objetivos.Remove(objetivo);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Sugere o nível de trilha para um objetivo usando ML.NET.
    /// </summary>
    [SwaggerOperation(Summary = "Sugere nível de trilha para um objetivo (ML.NET)")]
    [HttpPost("sugerir-nivel")]
    [ProducesResponseType(typeof(object), 200)]
    public ActionResult<object> SugerirNivelTrilha([FromBody] ObjetivoRequest request)
    {
        var nivel = _recomendacaoTrilha.RecomendarNivel(
            request.Cargo,
            request.Area,
            request.Demanda
        );

        return Ok(new
        {
            cargo = request.Cargo,
            area = request.Area,
            demanda = request.Demanda,
            nivelRecomendado = nivel
        });
    }
}
