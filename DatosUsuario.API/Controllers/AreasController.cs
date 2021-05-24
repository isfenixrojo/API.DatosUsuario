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
    public class AreasController : ControllerBase
    {
        private readonly IAreasRepository _repository;
        private readonly ILogger<AreasController> _logger;

        public AreasController(IAreasRepository repository, ILogger<AreasController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAreas()
        {
            var areas = await _repository.GetAreas();
            if (areas.Codigo != 200)
            {
                _logger.LogError($"Codigo: {areas.Codigo}, " + $"Mensaje: Problemas en {nameof(GetAreas)}, {areas.Respuesta}");
            }
            return Ok(areas);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAreaID(int Id)
        {
            var area = await _repository.GetAreaID(Id);
            if (area.Codigo != 200)
            {
                _logger.LogError($"Codigo: {area.Codigo}, " + $"Mensaje:Problemas en {nameof(GetAreaID)}, {area.Respuesta}");
            }
            return Ok(area);
        }

        [HttpPost]
        public async Task<IActionResult> PostArea([FromBody] Areas area)
        {
            var areas = await _repository.PostArea(area);
            if (areas.Codigo != 200)
            {
                _logger.LogError($"Codigo: {areas.Codigo}, " + $"Mensaje: Problemas en {nameof(PostArea)}, {areas.Respuesta}");
            }
            return Ok(areas);
        }


        [HttpPut]
        public async Task<IActionResult> PutArea(Areas area)
        {
            var areas = await _repository.PutArea(area);
            if (areas.Codigo != 200)
            {
                _logger.LogError($"Codigo: {areas.Codigo}, " + $"Mensaje: Problemas en {nameof(PutArea)}, {areas.Respuesta}");
            }
            return Ok(areas);
        }
    }
}