using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace Grafica_PDesktop
{
    public class Game : GameWindow
    {
        private Escenario escenario;

        private Objeto monitor;
        private Objeto teclado;
        private Objeto cpu;

        private Vector3 escenaOffset = Vector3.Zero;     // traslación global
        private Vector3 escenaRot = Vector3.Zero;     // rotación global (grados)
        private Vector3 escenaScale = Vector3.One;

        private float angMonitor = 0f;   // grados
        private float angBase = 0f;  // grados
        


        public Game(int width, int height)
            : base(width, height, GraphicsMode.Default, "Pc")
        {
           escenario = InicializarPc();
           //const string archivo = "escenario.json";

          //  Serializador.SerializarObjeto(escenario, archivo);

           // escenario = Serializador.DeserializarObjeto<Escenario>(archivo);

           // File.Delete(archivo);


        }
        ///*
        private Escenario InicializarPc()
        {
            // Colores
            Color4 cFrente = new Color4(0.82f, 0.82f, 0.82f, 1f);
            Color4 cPanel = new Color4(0.40f, 0.40f, 0.40f, 1f);
            Color4 cLado = new Color4(0.68f, 0.68f, 0.68f, 1f);
            Color4 cTopClaro = new Color4(0.78f, 0.78f, 0.78f, 1f);
            Color4 cLateralO = new Color4(0.60f, 0.60f, 0.60f, 1f);

            this.escenario = new Escenario(new Vector3(0, 0, 0));


            // ==========================================================
            // MONITOR  (vértices locales; se posiciona con monitor.setCentro)
            this.monitor = new Objeto();
            {
                // ---------- Panel ----------
                float px = 2.9f, py = 1.7f, pz = 0.08f; // semiancho, semialto, semiprofundidad
                Partes panel = new Partes();

                // Frente (z+)
                {
                    Cara m_pf = new Cara(cFrente);
                    m_pf.addVertice(-px, -py, +pz);
                    m_pf.addVertice(+px, -py, +pz);
                    m_pf.addVertice(+px, +py, +pz);
                    m_pf.addVertice(-px, +py, +pz);
                    m_pf.setCentro(new Vertice(0, 0, 0));
                    panel.add("mon_panel_frente", m_pf);
                }
                // Atrás (z-)
                {
                    Cara m_pa = new Cara(cFrente);
                    m_pa.addVertice(-px, -py, -pz);
                    m_pa.addVertice(-px, +py, -pz);
                    m_pa.addVertice(+px, +py, -pz);
                    m_pa.addVertice(+px, -py, -pz);
                    m_pa.setCentro(new Vertice(0, 0, 0));
                    panel.add("mon_panel_atras", m_pa);
                }
                // Izquierda (x-)
                {
                    Cara m_pi = new Cara(cLado);
                    m_pi.addVertice(-px, -py, -pz);
                    m_pi.addVertice(-px, -py, +pz);
                    m_pi.addVertice(-px, +py, +pz);
                    m_pi.addVertice(-px, +py, -pz);
                    m_pi.setCentro(new Vertice(0, 0, 0));
                    panel.add("mon_panel_izq", m_pi);
                }
                // Derecha (x+)
                {
                    Cara m_pd = new Cara(cLado);
                    m_pd.addVertice(+px, -py, -pz);
                    m_pd.addVertice(+px, +py, -pz);
                    m_pd.addVertice(+px, +py, +pz);
                    m_pd.addVertice(+px, -py, +pz);
                    m_pd.setCentro(new Vertice(0, 0, 0));
                    panel.add("mon_panel_der", m_pd);
                }
                // Arriba (y+)
                {
                    Cara m_pt = new Cara(cTopClaro);
                    m_pt.addVertice(-px, +py, -pz);
                    m_pt.addVertice(-px, +py, +pz);
                    m_pt.addVertice(+px, +py, +pz);
                    m_pt.addVertice(+px, +py, -pz);
                    m_pt.setCentro(new Vertice(0, 0, 0));
                    panel.add("mon_panel_arriba", m_pt);
                }
                // Abajo (y-)
                {
                    Cara m_pb = new Cara(cLado);
                    m_pb.addVertice(-px, -py, -pz);
                    m_pb.addVertice(+px, -py, -pz);
                    m_pb.addVertice(+px, -py, +pz);
                    m_pb.addVertice(-px, -py, +pz);
                    m_pb.setCentro(new Vertice(0, 0, 0));
                    panel.add("mon_panel_abajo", m_pb);
                }
                // Pantalla (ligeramente por encima del frente para evitar z-fighting)
                {
                    float margin = 0.35f;
                    float eps = 0.0003f;
                    Cara m_screen = new Cara(cPanel);
                    m_screen.addVertice(-(px - margin), -(py - margin), +pz + eps);
                    m_screen.addVertice(+(px - margin), -(py - margin), +pz + eps);
                    m_screen.addVertice(+(px - margin), +(py - margin), +pz + eps);
                    m_screen.addVertice(-(px - margin), +(py - margin), +pz + eps);
                    m_screen.setCentro(new Vertice(0, 0, 0));
                    panel.add("mon_pantalla", m_screen);
                }
                panel.setCentro(new Vertice(0, 0, 0));
                monitor.addParte("Monitor_Panel", panel);

                // ---------- Cuello ----------
                float nx = 0.18f, ny = 0.22f, nz = 0.10f;
                Partes cuello = new Partes();
                {
                    Cara n_f = new Cara(cFrente);
                    n_f.addVertice(-nx, -ny, +nz);
                    n_f.addVertice(+nx, -ny, +nz);
                    n_f.addVertice(+nx, +ny, +nz);
                    n_f.addVertice(-nx, +ny, +nz);
                    n_f.setCentro(new Vertice(0, 0, 0));
                    cuello.add("mon_cuello_frente", n_f);

                    Cara n_b = new Cara(cFrente);
                    n_b.addVertice(-nx, -ny, -nz);
                    n_b.addVertice(-nx, +ny, -nz);
                    n_b.addVertice(+nx, +ny, -nz);
                    n_b.addVertice(+nx, -ny, -nz);
                    n_b.setCentro(new Vertice(0, 0, 0));
                    cuello.add("mon_cuello_atras", n_b);

                    Cara n_l = new Cara(cLado);
                    n_l.addVertice(-nx, -ny, -nz);
                    n_l.addVertice(-nx, -ny, +nz);
                    n_l.addVertice(-nx, +ny, +nz);
                    n_l.addVertice(-nx, +ny, -nz);
                    n_l.setCentro(new Vertice(0, 0, 0));
                    cuello.add("mon_cuello_izq", n_l);

                    Cara n_r = new Cara(cLado);
                    n_r.addVertice(+nx, -ny, -nz);
                    n_r.addVertice(+nx, +ny, -nz);
                    n_r.addVertice(+nx, +ny, +nz);
                    n_r.addVertice(+nx, -ny, +nz);
                    n_r.setCentro(new Vertice(0, 0, 0));
                    cuello.add("mon_cuello_der", n_r);

                    Cara n_t = new Cara(cTopClaro);
                    n_t.addVertice(-nx, +ny, -nz);
                    n_t.addVertice(-nx, +ny, +nz);
                    n_t.addVertice(+nx, +ny, +nz);
                    n_t.addVertice(+nx, +ny, -nz);
                    n_t.setCentro(new Vertice(0, 0, 0));
                    cuello.add("mon_cuello_arriba", n_t);

                    Cara n_d = new Cara(cLado);
                    n_d.addVertice(-nx, -ny, -nz);
                    n_d.addVertice(+nx, -ny, -nz);
                    n_d.addVertice(+nx, -ny, +nz);
                    n_d.addVertice(-nx, -ny, +nz);
                    n_d.setCentro(new Vertice(0, 0, 0));
                    cuello.add("mon_cuello_abajo", n_d);
                }
                // Offset del cuello respecto al centro del MONITOR:
                // antes: monitor en (-1.2, 1.05, -6.0) y cuello en (-1.2, -0.25, -6.0) => Δ = (0, -1.30, 0)
                cuello.setCentro(new Vertice(0f, -1.70f, 0f));
                monitor.addParte("Monitor_Cuello", cuello);

                // ---------- Base ----------
                float bx = 1.20f, by = 0.10f, bz = 0.55f;
                Partes baseM = new Partes();
                {
                    Cara b_f = new Cara(cFrente);
                    b_f.addVertice(-bx, -by, +bz);
                    b_f.addVertice(+bx, -by, +bz);
                    b_f.addVertice(+bx, +by, +bz);
                    b_f.addVertice(-bx, +by, +bz);
                    b_f.setCentro(new Vertice(0, 0, 0));
                    baseM.add("mon_base_frente", b_f);

                    Cara b_b = new Cara(cFrente);
                    b_b.addVertice(-bx, -by, -bz);
                    b_b.addVertice(-bx, +by, -bz);
                    b_b.addVertice(+bx, +by, -bz);
                    b_b.addVertice(+bx, -by, -bz);
                    b_b.setCentro(new Vertice(0, 0, 0));
                    baseM.add("mon_base_atras", b_b);

                    Cara b_l = new Cara(cLado);
                    b_l.addVertice(-bx, -by, -bz);
                    b_l.addVertice(-bx, -by, +bz);
                    b_l.addVertice(-bx, +by, +bz);
                    b_l.addVertice(-bx, +by, -bz);
                    b_l.setCentro(new Vertice(0, 0, 0));
                    baseM.add("mon_base_izq", b_l);

                    Cara b_r = new Cara(cLado);
                    b_r.addVertice(+bx, -by, -bz);
                    b_r.addVertice(+bx, +by, -bz);
                    b_r.addVertice(+bx, +by, +bz);
                    b_r.addVertice(+bx, -by, +bz);
                    b_r.setCentro(new Vertice(0, 0, 0));
                    baseM.add("mon_base_der", b_r);

                    Cara b_t = new Cara(cTopClaro);
                    b_t.addVertice(-bx, +by, -bz);
                    b_t.addVertice(-bx, +by, +bz);
                    b_t.addVertice(+bx, +by, +bz);
                    b_t.addVertice(+bx, +by, -bz);
                    b_t.setCentro(new Vertice(0, 0, 0));
                    baseM.add("mon_base_arriba", b_t);

                    Cara b_d = new Cara(cLado);
                    b_d.addVertice(-bx, -by, -bz);
                    b_d.addVertice(+bx, -by, -bz);
                    b_d.addVertice(+bx, -by, +bz);
                    b_d.addVertice(-bx, -by, +bz);
                    b_d.setCentro(new Vertice(0, 0, 0));
                    baseM.add("mon_base_abajo", b_d);
                }
                // Offset de la base respecto al centro del MONITOR:
                // antes: base en (-1.2, -0.60, -5.9) => Δ = (0, -1.65, +0.10)
                baseM.setCentro(new Vertice(0f, -2.0f, +0.10f));
                monitor.addParte("Monitor_Base", baseM);
            }

            
            monitor.setCentro(new Vertice(-1.2f, 1.05f, -6.0f));
            escenario.addObjeto("Monitor", monitor);

            // ==========================================================
            // TECLADO  
            this.teclado = new Objeto();
            {
                Partes pt = new Partes();

                float anchoBottom = 2.45f;
                float anchoTop = 2.10f;
                float alto = 0.27f; // semialto de la tapa
                float espesor = 0.10f; // canto frontal
                float skewX = 0.18f; // desplaza la arista superior hacia -X (izq)

                // Tapa (trapecio superior, z = 0)
                {
                    Cara top = new Cara(cTopClaro);
                    top.addVertice(-anchoBottom, -alto, 0f);                 // abajo izq
                    top.addVertice(+anchoBottom, -alto, 0f);                 // abajo der
                    top.addVertice(+anchoTop - skewX, +alto, 0f);            // arriba der
                    top.addVertice(-anchoTop - skewX, +alto, 0f);            // arriba izq
                    top.setCentro(new Vertice(0, 0, 0));
                    pt.add("tk_tapa", top);
                }
                // Frente (canto, un poquito hacia la cámara para evitar z-fighting)
                {
                    Cara frente = new Cara(cLado);
                    frente.addVertice(-anchoBottom, -alto - espesor, 0.0003f);
                    frente.addVertice(+anchoBottom, -alto - espesor, 0.0003f);
                    frente.addVertice(+anchoBottom, -alto, 0.0003f);
                    frente.addVertice(-anchoBottom, -alto, 0.0003f);
                    frente.setCentro(new Vertice(0, 0, 0));
                    pt.add("tk_frente", frente);
                }
                // Lateral IZQUIERDO
                {
                    Cara latL = new Cara(cLado);
                    latL.addVertice(-anchoBottom, -alto - espesor, 0.0002f);
                    latL.addVertice(-anchoBottom, -alto, 0.0002f);
                    latL.addVertice(-anchoTop - skewX, +alto, 0.0002f);
                    latL.addVertice(-anchoTop - skewX, +alto - espesor, 0.0002f);
                    latL.setCentro(new Vertice(0, 0, 0));
                    pt.add("tk_lat_izq", latL);
                }
                // Lateral DERECHO
                {
                    Cara latR = new Cara(cLado);
                    latR.addVertice(+anchoBottom, -alto - espesor, 0.0002f);
                    latR.addVertice(+anchoBottom, -alto, 0.0002f);
                    latR.addVertice(+anchoTop - skewX, +alto, 0.0002f);
                    latR.addVertice(+anchoTop - skewX, +alto - espesor, 0.0002f);
                    latR.setCentro(new Vertice(0, 0, 0));
                    pt.add("tk_lat_der", latR);
                }

                pt.setCentro(new Vertice(0, 0, 0));
                teclado.addParte("Teclado", pt);
            }
            
            teclado.setCentro(new Vertice(-1.2f, -1.55f, -5.7f));
            escenario.addObjeto("Teclado", teclado);

            // ==========================================================
            // CASE (CPU)  (vértices locales; centro global editable)
            this.cpu = new Objeto();
            {
                Partes pc = new Partes();

                float x = 0.9f, y = 1.95f, z = 1.05f; // semidimensiones
                float sx = 1.30f;                     // sesgo X para la cara trasera
                float sy = 0.28f;                     // sesgo Y para la cara trasera

                // Frente (z+)
                {
                    Cara f = new Cara(cFrente);
                    f.addVertice(-x, -y, +z);
                    f.addVertice(+x, -y, +z);
                    f.addVertice(+x, +y, +z);
                    f.addVertice(-x, +y, +z);
                    f.setCentro(new Vertice(0, 0, 0));
                    pc.add("case_frente", f);
                }

                
                // Atrás (z-) con sesgo (-sx, +sy)
                {
                    Cara b = new Cara(cFrente);
                    b.addVertice(-x - sx, -y + sy, -z);
                    b.addVertice(-x - sx, +y + sy, -z);
                    b.addVertice(+x - sx, +y + sy, -z);
                    b.addVertice(+x - sx, -y + sy, -z);
                    b.setCentro(new Vertice(0, 0, 0));
                    pc.add("case_atras", b);
                }
                // Izquierda (x-)
                {
                    Cara l = new Cara(cLado);
                    l.addVertice(-x, -y, +z);
                    l.addVertice(-x, +y, +z);
                    l.addVertice(-x - sx, +y + sy, -z);
                    l.addVertice(-x - sx, -y + sy, -z);
                    l.setCentro(new Vertice(0, 0, 0));
                    pc.add("case_izq", l);
                }
                // Derecha (x+)
                {
                    Cara r = new Cara(cLateralO);
                    r.addVertice(+x, -y, +z);
                    r.addVertice(+x - sx, -y + sy, -z);
                    r.addVertice(+x - sx, +y + sy, -z);
                    r.addVertice(+x, +y, +z);
                    r.setCentro(new Vertice(0, 0, 0));
                    pc.add("case_der", r);
                }
                // Arriba (y+)
                {
                    Cara t = new Cara(cTopClaro);
                    t.addVertice(-x, +y, +z);
                    t.addVertice(+x, +y, +z);
                    t.addVertice(+x - sx, +y + sy, -z);
                    t.addVertice(-x - sx, +y + sy, -z);
                    t.setCentro(new Vertice(0, 0, 0));
                    pc.add("case_arriba", t);
                }
                // Abajo (y-)
                {
                    Cara d = new Cara(cLado);
                    d.addVertice(-x, -y, +z);
                    d.addVertice(-x - sx, -y + sy, -z);
                    d.addVertice(+x - sx, -y + sy, -z);
                    d.addVertice(+x, -y, +z);
                    d.setCentro(new Vertice(0, 0, 0));
                    pc.add("case_abajo", d);
                }



                pc.setCentro(new Vertice(0, 0, 0));
                this.cpu.addParte("CPU", pc);
            }
            // Coloca la CPU en el mundo (antes: 4.1, 1.05, -6.0)
            this.cpu.setCentro(new Vertice(4.1f, 1.05f, -6.0f));
            this.escenario.addObjeto("CPU", this.cpu);

            return escenario;

        }
         //*/

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(1f, 1f, 1f, 1f);
            GL.Enable(EnableCap.DepthTest);

            //mover
            // escenario.getObjeto("CPU").setCentro(new Vertice(1.0f,1.05f,-6.0f));
            // escenario.getObjeto("Monitor").getParte("Monitor_Base").setCentro(new Vertice(0f,-2.2f,+0.50f));

            //escalar
            // escenaScale = new Vector3(0.9f,0.9f,0.9f);
            // escenario.getObjeto("Monitor").setScale(0.7f, 0.7f, 0.7f);

            //reflexion
            //escenaScale = new Vector3(1f,-1f,1f);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);

            float alto = 9.5f;
            float ancho = alto * (Width / (float)Height);
            Matrix4 proj = Matrix4.CreateOrthographic(ancho, alto, 0.1f, 100f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref proj);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 view = Matrix4.LookAt(
                new Vector3(0.0f, 1.2f, 20.0f),   // cámara
                new Vector3(0.0f, 1.0f, -6.0f),  // mira hacia el conjunto
                Vector3.UnitY
            );
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref view);

            
            // escenario.dibujar(new Vector3(0, 0, 0));
            escenario.dibujar(escenaOffset, escenaRot, escenaScale);
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            
            angMonitor = (angMonitor - 90f * (float)e.Time) % 360f;
            angBase = (angBase + 30f * (float)e.Time) % 360f;


            // escenario.getObjeto("Monitor").setRot(0f, angMonitor, 0f);

            // rota alrededor del eje Y de la base
            // escenario.getObjeto("Monitor").getParte("Monitor_Base").setRot(angMonitor, angBase, 0f  );


              foreach (var obj in escenario.listaDeObjetos.Values)
                 obj.setRot(0f, angBase, 0f);

            // escenario.getObjeto("CPU").setRot( 0f,0f,angBase);
           
           // float dt = (float)e.Time;
           // escenaRot.Y = (escenaRot.Y + 15f * dt) % 360f;
        }


    }


}
