using Microsoft.Extensions.Configuration;
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
        public Usuario Create(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public long Delete(long id)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> Get()
        {
            const string sql = @" SELECT U.ID, " +
                                "U.TIPO_USUARIOS_ID, " +
                                "U.NOME, U.EMAIL, " +
                                "U.SENHA, " +
                                "TU.DESCRICAO " +
                                "FROM USUARIOS U " +
                                "INNER JOIN TIPO_USUARIOS TU " +
                                "ON U.TIPO_USUARIOS_ID = TU.ID";
            var table = _context.GetTable(sql);
            var usuarioList = (from DataRow row in table.Rows select Map(row)).ToList();
            return usuarioList;
        }

        public Usuario GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Usuario Login(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Usuario Update(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        internal Usuario Map(DataRow row)
        {
            var tipoUsuario = new TipoUsuario();
            tipoUsuario.Id = MapperDataRowObjectUtil.CreateItemFromRow<long>(row, "TIPO_USUARIOS_ID");
            tipoUsuario.Descricao = MapperDataRowObjectUtil.CreateItemFromRow<string>(row, "DESCRICAO");
            var usuario = new Usuario
            {
                Id = MapperDataRowObjectUtil.CreateItemFromRow<long>(row, "ID"),
                TipoUsuarioId = MapperDataRowObjectUtil.CreateItemFromRow<long>(row, "TIPO_USUARIOS_ID"),
                Nome = MapperDataRowObjectUtil.CreateItemFromRow<string>(row, "NOME"),
                Email = MapperDataRowObjectUtil.CreateItemFromRow<string>(row, "EMAIL"),
                Senha = MapperDataRowObjectUtil.CreateItemFromRow<string>(row, "SENHA"),
                TipoUsuario = tipoUsuario,
            };
            return usuario;
        }
    }
}
