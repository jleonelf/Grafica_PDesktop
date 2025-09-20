using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using System;
using System.Collections.Generic;

namespace Grafica_PDesktop
{
    [Serializable]
    class Cara
    {
        






        public List<Vertice> Vertices;
        public Color4 color;
        public Vertice centro;

        public Vector3 rot = Vector3.Zero;
        public Vector3 scale = Vector3.One;

        
        public bool reflectX = false;
        public bool reflectY = false;
        public bool reflectZ = false;

       
        public Cara(Color4 color)
        {
            this.Vertices = new List<Vertice>();
            this.color = color;
            this.centro = new Vertice(0, 0, 0);
        }

        
        public void addVertice(float x, float y, float z) => Vertices.Add(new Vertice(x, y, z));
        public void setColor(Color4 color) => this.color = color;
        public void setCentro(Vertice centro) => this.centro = centro;
        public void setRot(float rx, float ry, float rz) => rot = new Vector3(rx, ry, rz);
        public void setScale(float sx, float sy, float sz) => scale = new Vector3(sx, sy, sz);

        
        public struct Pose
        {
            public Vector3 t, r, s;
            public Pose(Vector3 t, Vector3 r, Vector3 s)
            { this.t = t; this.r = r; this.s = s; }
        }

        
        public Pose ComposePose(Vector3 tPadre, Vector3 rPadre, Vector3 sPadre)
        {
            var t = tPadre + TransformOffset(new Vector3(centro.x, centro.y, centro.z), rPadre, sPadre);
            var r = rPadre + rot;
            var s = new Vector3(sPadre.X * scale.X, sPadre.Y * scale.Y, sPadre.Z * scale.Z);
            return new Pose(t, r, s);
        }

       
        public void DrawWithPose(in Pose p)
        {
            GL.PushMatrix();

            ApplyPoseGL(p.t, p.r);

            // Reflexión aplicada a la escala compuesta (p.s) y control de winding
            var (sFinal, flipped) = ComputeReflectedScale(p.s);
            if (flipped) GL.FrontFace(FrontFaceDirection.Cw);
            GL.Scale(sFinal.X, sFinal.Y, sFinal.Z);

            DrawVertices();

            if (flipped) GL.FrontFace(FrontFaceDirection.Ccw);
            GL.PopMatrix();
        }

        
        private static void ApplyPoseGL(in Vector3 t, in Vector3 r)
        {
            GL.Translate(t);
            GL.Rotate(r.X, 1, 0, 0);
            GL.Rotate(r.Y, 0, 1, 0);
            GL.Rotate(r.Z, 0, 0, 1);
        }

        
        private (Vector3 sFinal, bool flipped) ComputeReflectedScale(Vector3 sCompuesta)
        {
            var sFinal = new Vector3(
                reflectX ? -sCompuesta.X : sCompuesta.X,
                reflectY ? -sCompuesta.Y : sCompuesta.Y,
                reflectZ ? -sCompuesta.Z : sCompuesta.Z
            );
            bool flipped = (sFinal.X * sFinal.Y * sFinal.Z) < 0f;
            return (sFinal, flipped);
        }

        
        private void DrawVertices()
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Color4(color);
            foreach (var v in Vertices)
                GL.Vertex3(v.x, v.y, v.z);
            GL.End();
        }

        

        public static Vector3 TransformOffset(Vector3 localCenter, Vector3 parentR, Vector3 parentS)
        {
            var cEsc = new Vector3(localCenter.X * parentS.X,
                                   localCenter.Y * parentS.Y,
                                   localCenter.Z * parentS.Z);
            return RotarXYZ(cEsc, parentR);
        }

        private static Vector3 RotarXYZ(Vector3 v, Vector3 deg)
        {
            float rx = MathHelper.DegreesToRadians(deg.X);
            float ry = MathHelper.DegreesToRadians(deg.Y);
            float rz = MathHelper.DegreesToRadians(deg.Z);

            float cx = (float)Math.Cos(rx), sx = (float)Math.Sin(rx);
            var vx = new Vector3(v.X, v.Y * cx - v.Z * sx, v.Y * sx + v.Z * cx);

            float cy = (float)Math.Cos(ry), sy = (float)Math.Sin(ry);
            var vy = new Vector3(vx.X * cy + vx.Z * sy, vx.Y, -vx.X * sy + vx.Z * cy);

            float cz = (float)Math.Cos(rz), sz = (float)Math.Sin(rz);
            return new Vector3(vy.X * cz - vy.Y * sz, vy.X * sz + vy.Y * cz, vy.Z);
        }

        public void Dibujar(Vector3 centro)
        {
            GL.PushMatrix();
            GL.Translate(new Vector3(centro.X, centro.Y, centro.Z));

            GL.Begin(PrimitiveType.Polygon);
            GL.Color4(color);
            foreach (var vertice in Vertices)
                GL.Vertex3(new Vector3(vertice.x, vertice.y, vertice.z));
            GL.End();

            GL.PopMatrix();
        }


        public void Dibujar(Vector3 tPadre, Vector3 rPadre, Vector3 sPadre)
        {
            var pose = ComposePose(tPadre, rPadre, sPadre);
            DrawWithPose(pose);
        }

    }
}
