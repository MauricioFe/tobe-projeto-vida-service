using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProjetoVidaTOBE_Server.Data;
using ProjetoVidaTOBE_Server.Model;
using ProjetoVidaTOBE_Server.Repositories.Contracts;
using ProjetoVidaTOBE_Server.Utils.Constantes;
using ProjetoVidaTOBE_Server.Utils.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjetoVidaTOBE_Server.Repositories.Implemantation
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataAccess _context;
        public UsuarioRepository(IConfiguration config)
        {
            _context = new DataAccess(config.GetConnectionString(Constantes.connectionStringName));

        }
        public long Create(Usuario usuario)
        {
            try
            {
                const string sql = @"INSERT INTO Usuarios (nome, email, senha, tipo_usuarios_id) 
                                    VALUES (@nome, @email, @senha, @tipoUsuario)";
                var parameters = new[]
                {
                    new MySqlParameter("@nome", usuario.Nome),
                    new MySqlParameter("@email", usuario.Email),
                    new MySqlParameter("@senha", usuario.Senha),
                    new MySqlParameter("@tipoUsuario", usuario.TipoUsuarioId)
                };
                return _context.ExecuteCommandInsert(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public long Delete(long id)
        {
            try
            {
                const string sql = @"Delete From Usuarios where id = @id";
                var parameters = new[]
                {
                    new MySqlParameter("@id", id)
                };
                return _context.ExecuteCommand(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public List<Usuario> Get()
        {
            var usuarioList = new List<Usuario>();
            try
            {
                const string sql = @" SELECT U.ID, " +
                             "U.TIPO_USUARIOS_ID, " +
                             "U.NOME, U.EMAIL, " +
                             "TU.DESCRICAO " +
                             "FROM USUARIOS U " +
                             "INNER JOIN TIPO_USUARIOS TU " +
                             "ON U.TIPO_USUARIOS_ID = TU.ID";
                var reader = _context.GetReader(sql);
                while (reader.Read())
                {
                    usuarioList.Add(Map(reader));
                }
            }
            catch (Exception ex)
            {
                usuarioList = null;
                Console.WriteLine(ex.Message);
            }
            return usuarioList;
        }

        public Usuario Find(long id)
        {
            try
            {
                const string sql = @" SELECT U.ID, " +
                              "U.TIPO_USUARIOS_ID, " +
                              "U.NOME, U.EMAIL, " +
                              "TU.DESCRICAO " +
                              "FROM USUARIOS U " +
                              "INNER JOIN TIPO_USUARIOS TU " +
                              "ON U.TIPO_USUARIOS_ID = TU.ID where U.ID = @id";
                var parameters = new[]
                {
                new MySqlParameter("@id", id)
                };
                var row = _context.GetRow(sql, parameters);
                return Map(row);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Usuario Login(Usuario usuario)
        {
            try
            {
                const string sql = @" SELECT U.ID, " +
                                "U.TIPO_USUARIOS_ID, " +
                                "U.NOME, U.EMAIL, " +
                                "TU.DESCRICAO " +
                                "FROM USUARIOS U " +
                                "INNER JOIN TIPO_USUARIOS TU " +
                                "ON U.TIPO_USUARIOS_ID = TU.ID where U.EMAIL = @email and U.SENHA = @senha";
                var parameters = new[]
                {
                    new MySqlParameter("@email", usuario.Email),
                    new MySqlParameter("@senha", usuario.Senha)
                };
                var row = _context.GetRow(sql, parameters);
                return Map(row);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public long Update(Usuario usuario, long id)
        {
            try
            {
                const string sql = @"Update Usuarios set nome = @nome, email = @email, tipo_usuarios_id = @tipoUsuario where id = @id";
                var parameters = new[]
                {
                    new MySqlParameter("@nome", usuario.Nome),
                    new MySqlParameter("@email", usuario.Email),
                    new MySqlParameter("@tipoUsuario", usuario.TipoUsuarioId),
                    new MySqlParameter("@id", id)
                };
                return _context.ExecuteCommand(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        internal Usuario Map(DataRow row)
        {
            var tipoUsuario = new TipoUsuario();
            tipoUsuario.Id = MapperObjectUtil.CreateItemFromRow<long>(row, "TIPO_USUARIOS_ID");
            tipoUsuario.Descricao = MapperObjectUtil.CreateItemFromRow<string>(row, "DESCRICAO");
            var usuario = new Usuario
            {
                Id = MapperObjectUtil.CreateItemFromRow<long>(row, "ID"),
                TipoUsuarioId = MapperObjectUtil.CreateItemFromRow<long>(row, "TIPO_USUARIOS_ID"),
                Nome = MapperObjectUtil.CreateItemFromRow<string>(row, "NOME"),
                Email = MapperObjectUtil.CreateItemFromRow<string>(row, "EMAIL"),
                TipoUsuario = tipoUsuario,
            };
            return usuario;
        }
        internal Usuario Map(IDataReader reader)
        {
            var tipoUsuario = new TipoUsuario();
            tipoUsuario.Id = MapperObjectUtil.FromDataReader<long>(reader, "TIPO_USUARIOS_ID");
            tipoUsuario.Descricao = MapperObjectUtil.FromDataReader<string>(reader, "DESCRICAO");
            var usuario = new Usuario
            {
                Id = MapperObjectUtil.FromDataReader<long>(reader, "ID"),
                TipoUsuarioId = MapperObjectUtil.FromDataReader<long>(reader, "TIPO_USUARIOS_ID"),
                Nome = MapperObjectUtil.FromDataReader<string>(reader, "NOME"),
                Email = MapperObjectUtil.FromDataReader<string>(reader, "EMAIL"),
                TipoUsuario = tipoUsuario,
            };
            return usuario;
        }
    }
}
