using MotosBackEnd.Datos;
using MotosBackEnd.Datos.Implement;
using MotosBackEnd.Datos.Interfaz;
using MotosBackEnd.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotosBackEnd.Negocio
{
    public class DataApi : iDataApi // Este despues se va a llamar servicio y vamos a hacer una FACTORY
    {
        private IMotosDAO DAO;

        public DataApi()
        {
            DAO = new MotosDAO();
        }
        public List<Moto> ConsultarDB()
        {
            return DAO.ConsultarDB();
            
        }

        public List<Moto> ConsultarDBconParam(string Modelo)
        {
            return DAO.ConsultarDBconParam(Modelo);
        }

        public bool DeleteMotos(int codigo)
        {
            return DAO.deleteMoto(codigo);
        }

        public bool InsertarDB(Moto moto)
        {
            return DAO.insertMoto2(moto);
        }

        public bool ModificarDB(Moto moto)
        {
          return   DAO.updateMoto(moto);    
        }

        
    }
}
