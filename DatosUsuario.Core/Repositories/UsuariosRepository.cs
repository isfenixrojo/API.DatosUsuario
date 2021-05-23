using DatosUsuario.Models.DTO;
using DatosUsuario.Models.Interfaces;
using DatosUsuario.Models.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DatosUsuario.Core.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly string _connection;
        public UsuariosRepository(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("DBUsuarios");
        }
        public async Task<Response> GetUsuarios()
        {
            var response = new Response();
            var datosUsuarios = new List<Usuarios>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connection);
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("Usp_SelectUsuarios", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                using SqlDataReader dr = await cmd.ExecuteReaderAsync();
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        var usuario = new Usuarios
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            NombreUsuario = dr["NombreUsuario"].ToString(),
                            IdAreas = Convert.ToInt32(dr["IdAreas"]),
                            NombreArea = dr["NombreArea"].ToString(),
                            Activo = Convert.ToInt32(dr["Activo"]),
                        };

                        datosUsuarios.Add(usuario);

                        response.Codigo = 200;
                        response.Respuesta = "Exito";
                        response.Datos = datosUsuarios;
                    }
                }
                else
                {
                    response.Codigo = 204;
                    response.Respuesta = "Sin datos";
                }
            }
            catch (SqlException e)
            {
                response.Codigo = 500;
                response.Respuesta = e.Message.ToString();
            }
            return response;
        }

        public async Task<Response> GetUsuariosId(int Id)
        {
            var response = new Response();
            try
            {
                using SqlConnection conn = new SqlConnection(_connection);
                using SqlCommand cmd = new SqlCommand("Usp_SelectUsuarioId", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdUsuario", Id));

                await conn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    string respuesta = dr["Response"].ToString();

                    if (respuesta != "Exito")
                    {
                        response.Codigo = 204;
                        response.Respuesta = respuesta;
                    }
                    else
                    {
                        var usuario = new Usuarios
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            NombreUsuario = dr["NombreUsuario"].ToString(),
                            IdAreas = Convert.ToInt32(dr["IdAreas"]),
                            NombreArea = dr["NombreArea"].ToString(),
                            Activo = Convert.ToInt32(dr["Activo"]),
                        };
                        response.Codigo = 200;
                        response.Respuesta = "Exito";
                        response.Datos = usuario;
                    }
                }
            }
            catch (SqlException e)
            {
                response.Codigo = 500;
                response.Respuesta = e.Message.ToString();
            }
            return response;
        }

        public async Task<Response> PostUsuario(Usuarios usuario)
        {
            var response = new Response();
            try
            {
                using SqlConnection conn = new SqlConnection(_connection);
                using SqlCommand cmd = new SqlCommand("Usp_InsertUsuario", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@NombreUsuario", usuario.NombreUsuario));
                cmd.Parameters.Add(new SqlParameter("@IdAreas", usuario.IdAreas));

                await conn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    string respuesta = dr["Response"].ToString();
                    response.Codigo = 200;
                    response.Respuesta = respuesta;
                }
            }
            catch (Exception e)
            {
                response.Codigo = 500;
                response.Respuesta = e.Message.ToString();
            }
            return response;
        }

        public async Task<Response> PutUsuario(Usuarios usuario)
        {
            var response = new Response();
            try
            {
                using SqlConnection conn = new SqlConnection(_connection);
                using SqlCommand cmd = new SqlCommand("Usp_UpdateUsuario", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdUsuario", usuario.IdUsuario));
                cmd.Parameters.Add(new SqlParameter("@NombreUsuario", usuario.NombreUsuario));
                cmd.Parameters.Add(new SqlParameter("@IdAreas", usuario.IdAreas));
                cmd.Parameters.Add(new SqlParameter("@Activo", usuario.Activo));

                await conn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    string respuesta = dr["Response"].ToString();
                    response.Codigo = 200;
                    response.Respuesta = respuesta;
                }
            }
            catch (Exception e)
            {
                response.Codigo = 500;
                response.Respuesta = e.Message.ToString();
            }
            return response;
        }
    }
}
