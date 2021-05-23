using DatosUsuario.Models.DTO;
using DatosUsuario.Models.Responses;
using System.Threading.Tasks;

namespace DatosUsuario.Models.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<Response> GetUsuarios();
        Task<Response> GetUsuariosId(int Id);
        Task<Response> PutUsuario(Usuarios usuario);
        Task<Response> PostUsuario(Usuarios usuario);
    }
}
