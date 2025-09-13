using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;

namespace Grafica_PDesktop
{
    [Serializable]
    class Objeto
    {

        public Dictionary<String, Partes> listaDePartes;
        public Color4 color;
        public Vertice centro;

        public Vector3 rot = Vector3.Zero;   // grados (x,y,z)
        public Vector3 scale = Vector3.One;  // escala local (x,y,z)

        
        public Objeto()
        {
            this.listaDePartes = new Dictionary<String, Partes>();
            this.color = new Color4(0, 0, 0, 0);
            centro = new Vertice(0f,0f,0f);
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
        public void setColor(String parte, String poligono, Color4 color)
        {
            this.color = color;
            listaDePartes[parte].setColor(poligono, this.color);
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

        public void dibujarParte(Vector3 centroEsc)
        {
            var centroObjeto = centroEsc + new Vector3(centro.x, centro.y, centro.z);
            foreach (Partes partes in listaDePartes.Values)
            {
                partes.dibujarPoligono(centroObjeto);
            }
        }

        public void dibujarParte(Vector3 t, Vector3 r, Vector3 s)
        {

            var tObj = t + Cara.TransformOffset(new Vector3(centro.x, centro.y, centro.z), r, s);
            var rObj = r + rot;
            var sObj = new Vector3(s.X * scale.X, s.Y * scale.Y, s.Z * scale.Z);

            foreach (var parte in listaDePartes.Values)
                parte.dibujarPoligono(tObj, rObj, sObj);
        }

    }
}
