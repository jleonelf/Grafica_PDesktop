
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using System;
using System.Collections.Generic;


namespace Grafica_PDesktop
{
    class Cara
    {
        //Punto == Vertice
        float rotarEjeY;

        public List<Vertice> Vertices;
        public Color4 color;
        public Vertice centro;

        public Vector3 rot = Vector3.Zero;
        public Vector3 scale = Vector3.One;

        public Cara(Color4 color)
        {
            this.Vertices = new List<Vertice>();
            this.color = color;
            this.centro = new Vertice(0, 0, 0);
            
        }

        public void addVertice(float x, float y, float z)
        {
            Vertices.Add(new Vertice(x, y, z));
        }

        public void setColor(Color4 color)
        {
            this.color = color;
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

        public void Dibujar(Vector3 centro)
        {  
            GL.PushMatrix();
            GL.Translate(new Vector3(centro.X, centro.Y, centro.Z));

            GL.Begin(PrimitiveType.Polygon);
            GL.Color4(color);

            
            foreach (var vertice in Vertices)
            {
                GL.Vertex3(new Vector3(vertice.x, vertice.y, vertice.z));
            }

            GL.End();
            GL.PopMatrix();
        }

        public void Dibujar(Vector3 tPadre, Vector3 rPadre, Vector3 sPadre)
        {
            // Acumulación jerárquica
            var t = tPadre + new Vector3(centro.x, centro.y, centro.z);
            var r = rPadre + rot;                                   // Euler en grados
            var s = new Vector3(sPadre.X * scale.X, sPadre.Y * scale.Y, sPadre.Z * scale.Z);

            GL.PushMatrix();
            GL.Translate(t);
            GL.Rotate(r.X, 1, 0, 0);
            GL.Rotate(r.Y, 0, 1, 0);
            GL.Rotate(r.Z, 0, 0, 1);
            GL.Scale(s.X, s.Y, s.Z);

            GL.Begin(PrimitiveType.Polygon);
            GL.Color4(color);
            foreach (var v in Vertices) GL.Vertex3(v.x, v.y, v.z);
            GL.End();

            GL.PopMatrix();
        }


    }
}
