using MotosBackEnd.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotosBackEnd.Datos.Interfaz
{
    internal interface IMotosDAO //Preparado los metodos
    {
        List<Moto> ConsultarDB(); // GET - Todas las Motos
    
        bool insertMoto2(Moto moto); // POST - Una Moto

        bool updateMoto(Moto moto); // PUT - Una Moto

        bool deleteMoto(int id); // DELETE - Una Moto

        List<Moto> ConsultarDBconParam(string Modelo); // GET - Con parametros
    }
}
