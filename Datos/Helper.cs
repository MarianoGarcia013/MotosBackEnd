using MotosBackEnd.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MotosBackEnd.Datos
{
    public class Helper // Todos lo necesario para comunicarse con la DB
    {
        SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-SFDA7AL\MSSQLSERVER2;Initial Catalog=Concesionaria;Integrated Security=True");
        SqlCommand comando = new SqlCommand(); //Se lo puede crear un solo comando o un cmd dentro de cada metodo cuando sea necesario
        private static Helper instancia;

        public static Helper ObtenerInstancia() // La salida para usarla en el DAO
        {
            if(instancia == null)
                instancia= new Helper();
            return instancia;
        }
        

        private void Conectar()
        {
            conexion.Open();
            comando.CommandType = CommandType.Text;
            comando.Connection = conexion;
        }

        public DataTable ConsultarDB(string consultaSQL) // Consulta a la DB simple con un SELECT * FROM
        {
            DataTable dt = new DataTable();
            Conectar();
            comando.CommandText = consultaSQL;
            dt.Load(comando.ExecuteReader());
            conexion.Close();
            return dt;
        }

        public DataTable querySQL(string SQL, List<Parametros> value)
        {
            DataTable dt = new DataTable();
            conexion.Open();
            SqlCommand cmd = new SqlCommand(SQL, conexion);
            cmd.CommandType = CommandType.Text;
            if (value != null)
            {
                foreach(Parametros param in value) 
                {
                    cmd.Parameters.AddWithValue(param.key, param.value);
                }
            }
                
            dt.Load(cmd.ExecuteReader());
                
            conexion.Close();
                
            return dt;
        }

        public void BorrarModelo(int posicion)
        {
            conexion.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection= conexion;
            cmd.CommandText = "DELETE Motos WHERE ";
            cmd.CommandType = CommandType.Text;            
        }

        public bool EjecutarSQLParam(string strSql, List<SqlParameter> values) // Actualiza los datos pasandolo por los parametros
        {
            bool ok = true;
            SqlTransaction t = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                conexion.Open();
                t = conexion.BeginTransaction();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = strSql;
                cmd.Transaction = t;

                if (values != null)
                {
                    foreach (SqlParameter param in values)
                    {
                        cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                    }
                }

                cmd.ExecuteNonQuery();
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                    ok = false;
                }
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();

            }

            return ok;
        }

        public bool ActualizarDB2(Moto moto)
        {
            conexion.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandText = "sp_InsertarMotos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codigo", moto.codigo);
            cmd.Parameters.AddWithValue("@modelo", moto.modelo);
            cmd.Parameters.AddWithValue("@id_marca", moto.marca);
            cmd.Parameters.AddWithValue("@precio", moto.precio);
            cmd.Parameters.AddWithValue("@fecha", moto.fecha);

            int filas = cmd.ExecuteNonQuery();
            conexion.Close();
            if (filas > 0)
                return true;
            else
                return false;
        }


        public bool ActualizarDB(string consultaSQL, List<Parametros> lst) // No se puede usar en WebApi
        {
            Conectar();
            comando.CommandText = consultaSQL;
            foreach (Parametros p in lst)
            {
                comando.Parameters.AddWithValue(p.key, p.value);
            }
            int filas = comando.ExecuteNonQuery();
            conexion.Close();
            if (filas > 0)
                return true;
            else
                return false;
        }

    }
}
