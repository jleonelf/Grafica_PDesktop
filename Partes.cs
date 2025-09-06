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

        public Vector3 rot = Vector3.Zero;
        public Vector3 scale = Vector3.One;


        public Partes()
        {
            listaDePoligonos = new Dictionary<string, Cara>();
            this.color = new Color4(0, 0, 0, 0);
            centro = new Vertice(0f,0f,0f);
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

        
        public void setCentro(Vertice centro)
        {
            this.centro = centro;
          
        }

        public void setRot(float rx, float ry, float rz) 
        {
            rot = new Vector3(rx, ry, rz);
        }

        public void setScale(float sx, float sy, float sz)
        {
            scale = new Vector3(sx, sy, sz);
        }     
            
        public void dibujarPoligono(Vector3 centroObj)
        {
            var centroParte = centroObj + new Vector3(centro.x,centro.y,centro.z);
              foreach (Cara poligono in listaDePoligonos.Values)
              {
                  poligono.Dibujar(centroParte);
              }
        }

        public void dibujarPoligono(Vector3 t, Vector3 r, Vector3 s)
        {
            var tParte = t + new Vector3(centro.x, centro.y, centro.z);
            var rParte = r + rot;
            var sParte = new Vector3(s.X * scale.X, s.Y * scale.Y, s.Z * scale.Z);

            foreach (var cara in listaDePoligonos.Values)
                cara.Dibujar(tParte, rParte, sParte);
        }

    }
}
