using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotosBackEnd.Dominio
{
    public class Moto
    {
        public int codigo { get; set; }
        public string modelo { get; set; }
        public int marca { get; set; }
        public double precio { get; set; }
        public DateTime fecha { get; set; }

        public Moto(int codigo, string modelo, int marca, double precio, DateTime fecha)
        {
            this.codigo = codigo;
            this.modelo = modelo;
            this.marca = marca;
            this.precio = precio;
            this.fecha = fecha;
        }

        public Moto()
        {
        }

        public override string ToString()
        {
            return codigo + ", " + modelo;
        }
    }
}
