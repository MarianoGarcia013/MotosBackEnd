using MotosBackEnd.Datos.Interfaz;
using MotosBackEnd.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotosBackEnd.Datos.Implement
{
    internal class MotosDAO : IMotosDAO // Implementar a los metodos
    {
        public List<Moto> ConsultarDB() // SELECT LISTO
        {
            List<Moto> lst = new List<Moto>();
            DataTable t = Helper.ObtenerInstancia().ConsultarDB("SELECT * FROM Motos");

            foreach (DataRow dr in t.Rows) // Por cada fila (DataRow) en el conjunto de filas (t.Rows) realizar una accion.
            {
                Moto moto = new Moto();
                moto.codigo = Convert.ToInt32(dr[0].ToString());
                moto.modelo = dr[1].ToString();
                moto.marca = Convert.ToInt32((dr[2]).ToString());
                moto.precio = Convert.ToDouble(dr[3].ToString());
                moto.fecha = Convert.ToDateTime(dr[4].ToString());

                lst.Add(moto);
            }
            return lst;
        }
     

        public bool insertMoto2(Moto moto) //Tiene que ser como objeto para que se pueda mandar por API         INSERT LISTO
        {
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@codigo", moto.codigo));
            list.Add(new SqlParameter("@modelo", moto.modelo));
            list.Add(new SqlParameter("@id_marca", moto.marca));
            list.Add(new SqlParameter("@precio", moto.precio));
            list.Add(new SqlParameter("@fecha", moto.fecha));

            return Helper.ObtenerInstancia().EjecutarSQLParam("sp_InsertarMotos", list);
        }

        public bool updateMoto(Moto moto) // UPDATE LISTO
        {
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@codigo", moto.codigo));
            list.Add(new SqlParameter("@modelo", moto.modelo));
            list.Add(new SqlParameter("@marca", moto.marca));
            list.Add(new SqlParameter("@precio", moto.precio));

            return Helper.ObtenerInstancia().EjecutarSQLParam("SP_UPDATEMOTOS", list);  
        }

        public bool deleteMoto(int codigo ) // DELETE LISTO
        {
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@codigo", codigo));
            return Helper.ObtenerInstancia().EjecutarSQLParam("SP_DELETEMOTOS", list);
        }

        public List<Moto> ConsultarDBconParam(string Modelo) // Todavia no sale bien
        {
            DataTable dt = Helper.ObtenerInstancia().querySQL($"select * from Motos where Modelo like '%{Modelo}%' ", null);
            List<Moto> result = new List<Moto>();

            foreach(DataRow fila in dt.Rows)
            {
                Moto moto = new Moto();
                moto.codigo = (int)fila[0];
                moto.modelo = fila[1].ToString();
                moto.marca = (int)fila[2];
                moto.precio = (double)fila[3];
                moto.fecha = Convert.ToDateTime(fila[4]);
                result.Add(moto);
            }

            return result;              

        }
    }
        
}

