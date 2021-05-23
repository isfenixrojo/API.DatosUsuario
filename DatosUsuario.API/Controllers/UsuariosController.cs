using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatosUsuario.Models.DTO;
using DatosUsuario.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatosUsuario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _repository;

        public UsuariosController(IUsuariosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _repository.GetUsuarios();
            /*if (usuarios.Codigo == 500)
            {
                Log.Error(usuarios.Respuesta);
            }
            Log.Information(usuarios.Respuesta);*/
            return Ok(usuarios);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUsuarioID(int Id)
        {
            var usuario = await _repository.GetUsuariosId(Id);
            /*if (usuario.Codigo == 500)
            {
                Log.Error(usuario.Respuesta);
            }
            Log.Information(usuario.Respuesta); */

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuarios dato)
        {
            var usuario = await _repository.PostUsuario(dato);
            /*if (usuario.Codigo == 500)
            {
                Log.Error(usuario.Respuesta);
            }
            Log.Information(usuario.Respuesta);*/
            return Ok(usuario);
        }


        [HttpPut]
        public async Task<IActionResult> PutUsuario(Usuarios dato)
        {
            var usuario = await _repository.PutUsuario(dato);
            /*if (usuario.Codigo == 500)
            {
                Log.Error(usuario.Respuesta);
            }
            Log.Information(usuario.Respuesta);*/
            return Ok(usuario);
        }
    }
}