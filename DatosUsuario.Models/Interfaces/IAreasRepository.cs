using DatosUsuario.Models.DTO;
using DatosUsuario.Models.Responses;
using System.Threading.Tasks;

namespace DatosUsuario.Models.Interfaces
{
    public interface IAreasRepository
    {
        Task<Response> GetAreas();
        Task<Response> GetAreaID(int Id);
        Task<Response> PutArea(Areas area);
        Task<Response> PostArea(Areas area);
    }
}
