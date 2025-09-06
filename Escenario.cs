using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Grafica_PDesktop
{
    class Escenario
    {
        public Dictionary<String, Objeto> listaDeObjetos;
        protected Vector3 centro;

        public Escenario(Vector3 centro)
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

        public void dibujar(Vector3 centro)
        {
             
            foreach (Objeto objetoActual in this.listaDeObjetos.Values)
            {
                objetoActual.dibujarParte(centro);
            }
        }

        public void dibujar(Vector3 offset, Vector3 rot, Vector3 scale) 
        {
            foreach (var objetoActual in this.listaDeObjetos.Values)
                objetoActual.dibujarParte(offset,rot,scale);
        
        }



    }
}
