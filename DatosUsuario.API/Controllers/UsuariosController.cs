using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatosUsuario.Models.DTO;
using DatosUsuario.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DatosUsuario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _repository;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(IUsuariosRepository repository, ILogger<UsuariosController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _repository.GetUsuarios();
            if (usuarios.Codigo != 200)
            {
                _logger.LogError($"Codigo: {usuarios.Codigo}, " + $"Mensaje: Problemas en {nameof(GetUsuarios)}, {usuarios.Respuesta}");

            }
            return Ok(usuarios);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUsuarioId(int Id)
        {
            var usuario = await _repository.GetUsuariosId(Id);
            if (usuario.Codigo != 200)
            {
                _logger.LogError($"Codigo: {usuario.Codigo}, " + $"Mensaje: Problemas en {nameof(GetUsuarioId)}, {usuario.Respuesta}");
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuarios dato)
        {
            var usuario = await _repository.PostUsuario(dato);
            if (usuario.Codigo != 200)
            {
                _logger.LogError($"Codigo: {usuario.Codigo}, " + $"Mensaje: Problemas en {nameof(PostUsuario)}, {usuario.Respuesta}");
            }
            return Ok(usuario);
        }

        [HttpPut]
        public async Task<IActionResult> PutUsuario(Usuarios dato)
        {
            var usuario = await _repository.PutUsuario(dato);
            if (usuario.Codigo != 200)
            {
                _logger.LogError($"Codigo: {usuario.Codigo}, " + $"Mensaje: Problemas en {nameof(PutUsuario)}, {usuario.Respuesta}");
            }
            return Ok(usuario);
        }
    }
}