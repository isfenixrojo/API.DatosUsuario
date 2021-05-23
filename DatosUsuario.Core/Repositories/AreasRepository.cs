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
    public class AreasRepository : IAreasRepository
    {
        private readonly String _connection;

        public AreasRepository(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("DBUsuarios");
        }
        public async Task<Response> GetAreas()
        {
            var response = new Response();
            var datosAreas = new List<Areas>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connection);
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("Usp_SelectAreas", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                using SqlDataReader dr = await cmd.ExecuteReaderAsync();
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        var area = new Areas
                        {
                            IdAreas = Convert.ToInt32(dr["IdAreas"]),
                            NombreArea = dr["NombreArea"].ToString(),
                        };

                        datosAreas.Add(area);

                        response.Codigo = 200;
                        response.Respuesta = "Exito";
                        response.Datos = datosAreas;
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
        public async Task<Response> GetAreaID(int Id)
        {
            var response = new Response();
            try
            {
                using SqlConnection conn = new SqlConnection(_connection);
                using SqlCommand cmd = new SqlCommand("Usp_SelectAreaId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdAreas", Id));

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
                        var area = new Areas
                        {
                            IdAreas = Convert.ToInt32(dr["IdAreas"]),
                            NombreArea = dr["NombreArea"].ToString()
                        };
                        response.Codigo = 200;
                        response.Respuesta = "Exito";
                        response.Datos = area;
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

        public async Task<Response> PostArea(Areas area)
        {
            var response = new Response();
            try
            {
                using SqlConnection conn = new SqlConnection(_connection);
                using SqlCommand cmd = new SqlCommand("Usp_InsertArea", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                               
                cmd.Parameters.Add(new SqlParameter("@NombreArea", area.NombreArea));

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

        public async Task<Response> PutArea(Areas area)
        {
            var response = new Response();
            try
            {
                using SqlConnection conn = new SqlConnection(_connection);
                using SqlCommand cmd = new SqlCommand("Usp_UpdateArea", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdAreas", area.IdAreas));
                cmd.Parameters.Add(new SqlParameter("@NombreArea", area.NombreArea));
                cmd.Parameters.Add(new SqlParameter("@Activo", area.Activo));

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
