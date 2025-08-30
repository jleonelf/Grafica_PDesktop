using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;

namespace Grafica_PDesktop
{
    class Objeto
    {

        public Dictionary<String, Partes> listaDePartes;
        public Color4 color;
        public Vertice centro;
        public int timer = 0;
        public Objeto()
        {
            this.listaDePartes = new Dictionary<String, Partes>();
            this.color = new Color4(0, 0, 0, 0);
        }

        public void addParte(String nombre, Partes nuevaParte)
        {
            listaDePartes.Add(nombre, nuevaParte);
        }

        public void deleteParte(String nombre)
        {
            this.listaDePartes.Remove(nombre);
        }

        public Partes getParte(String nombre)
        {
            return this.listaDePartes[nombre];
        }

        public void dibujarParte(Vector3 centro)
        {
            foreach (Partes partes in listaDePartes.Values)
            {
                partes.dibujarPoligono(centro);
            }
        }

        public void setColor(String parte, String poligono, Color4 color)
        {
            this.color = color;
            listaDePartes[parte].setColor(poligono, this.color);
        }

        public void setCentro(Vertice centro)
        {
            this.centro = centro;
            foreach (Partes parteActual in listaDePartes.Values)
            {
                parteActual.setCentro(centro);
            }
        }


    }
}
