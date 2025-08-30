using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;

namespace Grafica_PDesktop
{
    class Partes
    {
        public Dictionary<String, Cara> listaDePoligonos;
        public Color4 color;
        public Vertice centro;

        public Partes()
        {
            listaDePoligonos = new Dictionary<string, Cara>();
            this.color = new Color4(0, 0, 0, 0);
        }

        public void add(String nombre, Cara poligono)
        {
            this.listaDePoligonos.Add(nombre, poligono);
        }

        public void delete(String nombre)
        {
            this.listaDePoligonos.Remove(nombre);
        }

        public Cara getPoligono(String nombre)
        {
            return this.listaDePoligonos[nombre];
        }

        public void setColor(String nombre, Color4 color)
        {
            this.color = color;
            listaDePoligonos[nombre].setColor(this.color);
        }

        //centro de masa
        public void setCentro(Vertice centro)
        {
            this.centro = centro;
            foreach (Cara poligono in listaDePoligonos.Values)
            {
                poligono.setCentro(centro);
            }
        }

        public void dibujarPoligono(Vector3 centro)
        {
            foreach (Cara poligono in listaDePoligonos.Values)
            {
                poligono.Dibujar(centro);
            }
        }



    }
}
