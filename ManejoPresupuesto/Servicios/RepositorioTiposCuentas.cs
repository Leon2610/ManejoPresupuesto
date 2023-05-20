using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Crear(TipoCuenta tipoCuenta);
    }

    public class RepositorioTiposCuentas : IRepositorioTiposCuentas
    {
        private readonly string connetionString;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connetionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connetionString);
            var id = await connection.QuerySingleAsync<int>
                                                            ($@"INSERT INTO TiposCuentas (Nombre, UsuarioId, Orden)
                                                            VALUES (@Nombre, @UsuarioId, 0);
                                                            SELECT SCOPE_IDENTITY();
                                                            ", tipoCuenta);
            tipoCuenta.Id = id;
        }
    }
}
