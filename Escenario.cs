using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Grafica_PDesktop
{   [Serializable]
    class Escenario
    {
        public Dictionary<String, Objeto> listaDeObjetos;
        public Vertice centro;


        public Escenario(Vertice centro)
        {
            this.listaDeObjetos = new Dictionary<String, Objeto>();
            this.centro = centro;
        }

        public void addObjeto(String nombre, Objeto nuevoObjeto)
        {
            this.listaDeObjetos.Add(nombre, nuevoObjeto);
        }

        public void deleteObjeto(String objetoAEliminar)
        {
            this.listaDeObjetos.Remove(objetoAEliminar);
        }

        public Objeto getObjeto(String nombre)
        {
            return this.listaDeObjetos[nombre];
        }

        public void setObjeto(String nombre, Objeto objeto)
        {
            this.listaDeObjetos[nombre] = objeto;
        }
        public void setCentro(float x, float y, float z) 
        {
            centro = new Vertice(x,y,z);   
        }
        public Vector3 CalcularCentroideObjetos()
        {
            if (listaDeObjetos.Count == 0) return Vector3.Zero;
            float sx = 0, sy = 0, sz = 0;
            foreach (var obj in listaDeObjetos.Values)
            {
                sx += obj.centro.x; sy += obj.centro.y; sz += obj.centro.z;
            }
            float n = listaDeObjetos.Count;
            return new Vector3(sx / n, sy / n, sz / n);
        }
        public void setCentro(Vector3 p) => centro = new Vertice(p.X, p.Y, p.Z);
        public Vertice getCentro() => centro;

        public void dibujar(Vector3 centro)
        {
             
            foreach (Objeto objetoActual in this.listaDeObjetos.Values)
            {
                objetoActual.dibujarParte(centro);
            }
        }

        public void dibujar(Vector3 offset, Vector3 rot, Vector3 scale) 
        {
            
            var P = new Vector3(centro.x, centro.y, centro.z);
            var rotSP = Cara.TransformOffset(P, rot, scale);
            var tBase = offset + P - rotSP;

            foreach (var obj in listaDeObjetos.Values)
                obj.dibujarParte(tBase, rot, scale);

        }



    }
}
