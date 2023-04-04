using Microsoft.VisualBasic.FileIO;
using MotosBackEnd.Datos;
using MotosBackEnd.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotosBackEnd.Negocio
{
    public interface iDataApi
    {
        public List<Moto> ConsultarDB();
        public bool InsertarDB(Moto moto);

        public bool ModificarDB(Moto moto);

        public bool DeleteMotos(int codigo);
        List<Moto> ConsultarDBconParam(string Modelo);
    }
}
