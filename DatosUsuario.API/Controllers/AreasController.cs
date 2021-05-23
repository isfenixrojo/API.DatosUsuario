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
    public class AreasController : ControllerBase
    {
        private readonly IAreasRepository _repository;

        public AreasController(IAreasRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAreas()
        {
            var getArea = await _repository.GetAreas();
            /*if (usuarios.Codigo == 500)
            {
                Log.Error(usuarios.Respuesta);
            }
            Log.Information(usuarios.Respuesta);*/
            return Ok(getArea);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAreaID(int Id)
        {
            var getArea = await _repository.GetAreaID(Id);
            /*if (usuario.Codigo == 500)
            {
                Log.Error(usuario.Respuesta);
            }
            Log.Information(usuario.Respuesta); */

            return Ok(getArea);
        }

        [HttpPost]
        public async Task<IActionResult> PostArea([FromBody] Areas area)
        {
            var postArea = await _repository.PostArea(area);
            /*if (usuario.Codigo == 500)
            {
                Log.Error(usuario.Respuesta);
            }
            Log.Information(usuario.Respuesta);*/
            return Ok(postArea);
        }


        [HttpPut]
        public async Task<IActionResult> PutArea(Areas area)
        {
            var putArea = await _repository.PutArea(area);
            /*if (usuario.Codigo == 500)
            {
                Log.Error(usuario.Respuesta);
            }
            Log.Information(usuario.Respuesta);*/
            return Ok(putArea);
        }
    }
}