using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using Reski.Application.DTO.Request;
using Reski.Application.DTO.Response;
using Reski.Infrastructure.Context;

namespace Reski.Controller
{
    [ApiController]
    [Route("api/v{version:apiVersion}/oracle")]
    [ApiVersion("1.0")]
    public class OracleIntegrationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public OracleIntegrationController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        private OracleConnection CreateConnection()
        {
            var connString = _config.GetConnectionString("Oracle");

            if (string.IsNullOrWhiteSpace(connString))
                throw new InvalidOperationException(
                    "ConnectionStrings:Oracle não está configurada. Verifique appsettings.json.");

            return new OracleConnection(connString);
        }
        
        [HttpPost("usuarios/from-procedure")]
        public async Task<IActionResult> InserirUsuarioViaProcedure(
            [FromBody] UsuarioProcedureRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hasher = new PasswordHasher<object?>();
            var senhaHash = hasher.HashPassword(null, req.Senha);

            using var connection = CreateConnection();
            await connection.OpenAsync();

            using var cmd = new OracleCommand("PKG_RESKI_CADASTRO.PRC_INSERIR_USUARIO", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("P_NOME", OracleDbType.Varchar2).Value = req.Nome;
            cmd.Parameters.Add("P_EMAIL", OracleDbType.Varchar2).Value = req.Email;
            cmd.Parameters.Add("P_SENHAHASH", OracleDbType.Varchar2).Value = senhaHash;
            cmd.Parameters.Add("P_CPF", OracleDbType.Varchar2).Value = req.Cpf;

            var output = new OracleParameter("P_ID_OUT", OracleDbType.Int32)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(output);

            try
            {
                await cmd.ExecuteNonQueryAsync();

                int novoId = 0;

                if (output.Value is OracleDecimal oraDec)
                {
                    novoId = oraDec.ToInt32();
                }
                else if (output.Value != null)
                {
                    novoId = Convert.ToInt32(output.Value.ToString());
                }

                return Ok(new
                {
                    Mensagem = "Usuário inserido com sucesso via procedure Oracle.",
                    NovoId = novoId
                });
            }
            catch (OracleException ex)
            {
                return BadRequest(new
                {
                    Erro = "Falha ao inserir usuário via procedure.",
                    Detalhe = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Erro = "Erro inesperado ao inserir usuário.",
                    Detalhe = ex.Message
                });
            }
        }
        
        [HttpGet("usuarios/{id:int}/perfil-json")]
        public async Task<IActionResult> ObterPerfilJson(int id)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();

            using var cmd = new OracleCommand("PKG_RESKI_ANALISE.FN_GERAR_PERFIL_JSON", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            var returnParam = new OracleParameter("RETURN_VALUE", OracleDbType.Clob)
            {
                Direction = ParameterDirection.ReturnValue
            };
            cmd.Parameters.Add(returnParam);

            cmd.Parameters.Add("P_USUARIO_ID", OracleDbType.Int32).Value = id;

            await cmd.ExecuteNonQueryAsync();

            string json = "{}";

            if (returnParam.Value is OracleClob clob && !clob.IsNull)
            {
                json = clob.Value; 
            }

            return Content(json, "application/json");
        }
        
        [HttpPost("compatibilidade")]
        public async Task<IActionResult> CalcularCompatibilidade(
            [FromBody] CompatibilidadeRequest req)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();

            using var cmd = new OracleCommand("PKG_RESKI_ANALISE.FN_VALIDAR_E_CALCULAR_COMPAT", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            var returnParam = new OracleParameter("RETURN_VALUE", OracleDbType.Varchar2, 4000)
            {
                Direction = ParameterDirection.ReturnValue
            };
            cmd.Parameters.Add(returnParam);

            cmd.Parameters.Add("P_EMAIL", OracleDbType.Varchar2).Value = req.Email;
            cmd.Parameters.Add("P_CPF", OracleDbType.Varchar2).Value = req.Cpf;
            cmd.Parameters.Add("P_PONTUACAO_USUARIO", OracleDbType.Int32).Value = req.PontuacaoUsuario;
            cmd.Parameters.Add("P_PONTUACAO_VAGA", OracleDbType.Int32).Value = req.PontuacaoVaga;

            await cmd.ExecuteNonQueryAsync();

            var json = returnParam.Value?.ToString() ?? "{}";

            return Content(json, "application/json");
        }
        
        [HttpGet("dataset-json")]
        public async Task<IActionResult> ExportarDatasetJson()
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();

            using var cmd = new OracleCommand("PKG_RESKI_ANALISE.PRC_EXPORTAR_DATASET_JSON", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            var jsonOut = new OracleParameter("P_JSON", OracleDbType.Clob)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(jsonOut);

            await cmd.ExecuteNonQueryAsync();

            string json = "[]";

            if (jsonOut.Value is OracleClob clob && !clob.IsNull)
            {
                json = clob.Value;
            }

            return Content(json, "application/json");
        }

    }
}
