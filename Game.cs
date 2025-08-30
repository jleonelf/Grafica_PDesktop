using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Grafica_PDesktop
{
    public class Game : GameWindow
    {
        
        private Escenario escenario;

        
        private Objeto monitor;
        private Objeto teclado;
        private Objeto cpu;

        public Game(int width, int height)
            : base(width, height, GraphicsMode.Default, "Pc")
        {
            
            Color4 cFrente = new Color4(0.82f, 0.82f, 0.82f, 1f);
            Color4 cPanel = new Color4(0.40f, 0.40f, 0.40f, 1f);
            Color4 cLado = new Color4(0.68f, 0.68f, 0.68f, 1f);
            Color4 cTopClaro = new Color4(0.78f, 0.78f, 0.78f, 1f);
            Color4 cLateralO = new Color4(0.60f, 0.60f, 0.60f, 1f);
           // Color4 cDetalle = new Color4(0.55f, 0.55f, 0.55f, 1f);

            
            this.escenario = new Escenario(new Vector3(0, 0, 0));

            // ==========================================================
            // MONITOR 
      
            this.monitor = new Objeto();
            {
                
                // Centro del panel (pm*) y “semidimensiones” (px, py, pz)
                // Nota: “semi” = la mitad del tamaño real.
                float pmx = -1.2f, pmy = 1.05f, pmz = -6.0f; // centro del panel
                float px = 2.9f, py = 1.7f, pz = 0.08f; // semiancho, semialto, semiprofundidad (delgadez)

                Partes panel = new Partes();

                // Frente (z+): rectángulo con Z=pmz + pz
                Cara m_pf = new Cara(cFrente);
                m_pf.addVertice(pmx - px, pmy - py, pmz + pz);
                m_pf.addVertice(pmx + px, pmy - py, pmz + pz);
                m_pf.addVertice(pmx + px, pmy + py, pmz + pz);
                m_pf.addVertice(pmx - px, pmy + py, pmz + pz);
                m_pf.setCentro(new Vertice(0, 0, 0));
                panel.add("mon_panel_frente", m_pf);

                // Atrás (z-): mismo rectángulo, pero Z=pmz - pz
                Cara m_pa = new Cara(cFrente);
                m_pa.addVertice(pmx - px, pmy - py, pmz - pz);
                m_pa.addVertice(pmx - px, pmy + py, pmz - pz);
                m_pa.addVertice(pmx + px, pmy + py, pmz - pz);
                m_pa.addVertice(pmx + px, pmy - py, pmz - pz);
                m_pa.setCentro(new Vertice(0, 0, 0));
                panel.add("mon_panel_atras", m_pa);

                // Izquierda (x-): une borde izquierdo del frente con el del fondo
                Cara m_pi = new Cara(cLado);
                m_pi.addVertice(pmx - px, pmy - py, pmz - pz);
                m_pi.addVertice(pmx - px, pmy - py, pmz + pz);
                m_pi.addVertice(pmx - px, pmy + py, pmz + pz);
                m_pi.addVertice(pmx - px, pmy + py, pmz - pz);
                m_pi.setCentro(new Vertice(0, 0, 0));
                panel.add("mon_panel_izq", m_pi);

                // Derecha (x+)
                Cara m_pd = new Cara(cLado);
                m_pd.addVertice(pmx + px, pmy - py, pmz - pz);
                m_pd.addVertice(pmx + px, pmy + py, pmz - pz);
                m_pd.addVertice(pmx + px, pmy + py, pmz + pz);
                m_pd.addVertice(pmx + px, pmy - py, pmz + pz);
                m_pd.setCentro(new Vertice(0, 0, 0));
                panel.add("mon_panel_der", m_pd);

                // Arriba (y+)
                Cara m_pt = new Cara(cTopClaro);
                m_pt.addVertice(pmx - px, pmy + py, pmz - pz);
                m_pt.addVertice(pmx - px, pmy + py, pmz + pz);
                m_pt.addVertice(pmx + px, pmy + py, pmz + pz);
                m_pt.addVertice(pmx + px, pmy + py, pmz - pz);
                m_pt.setCentro(new Vertice(0, 0, 0));
                panel.add("mon_panel_arriba", m_pt);

                // Abajo (y-)
                Cara m_pb = new Cara(cLado);
                m_pb.addVertice(pmx - px, pmy - py, pmz - pz);
                m_pb.addVertice(pmx + px, pmy - py, pmz - pz);
                m_pb.addVertice(pmx + px, pmy - py, pmz + pz);
                m_pb.addVertice(pmx - px, pmy - py, pmz + pz);
                m_pb.setCentro(new Vertice(0, 0, 0));
                panel.add("mon_panel_abajo", m_pb);

                // “Pantalla”: un rectángulo ligeramente por encima del frente (eps evita z-fighting)
                float margin = 0.35f;      // deja borde negro alrededor
                float eps = 0.0003f;    // empuja un poquito hacia la cámara
                Cara m_screen = new Cara(cPanel);
                m_screen.addVertice(pmx - (px - margin), pmy - (py - margin), pmz + pz + eps);
                m_screen.addVertice(pmx + (px - margin), pmy - (py - margin), pmz + pz + eps);
                m_screen.addVertice(pmx + (px - margin), pmy + (py - margin), pmz + pz + eps);
                m_screen.addVertice(pmx - (px - margin), pmy + (py - margin), pmz + pz + eps);
                m_screen.setCentro(new Vertice(0, 0, 0));
                panel.add("mon_pantalla", m_screen);

                
                panel.setCentro(new Vertice(0, 0, 0));
                monitor.addParte("Monitor_Panel", panel);

                
                // Centro del cuello (nc*) y semidimensiones (nx,ny,nz)
                float ncx = -1.2f, ncy = -0.25f, ncz = -6.0f;
                float nx = 0.18f, ny = 0.22f, nz = 0.10f;

                Partes cuello = new Partes();

                
                Cara n_f = new Cara(cFrente);
                n_f.addVertice(ncx - nx, ncy - ny, ncz + nz);
                n_f.addVertice(ncx + nx, ncy - ny, ncz + nz);
                n_f.addVertice(ncx + nx, ncy + ny, ncz + nz);
                n_f.addVertice(ncx - nx, ncy + ny, ncz + nz);
                n_f.setCentro(new Vertice(0, 0, 0));
                cuello.add("mon_cuello_frente", n_f);

                Cara n_b = new Cara(cFrente);
                n_b.addVertice(ncx - nx, ncy - ny, ncz - nz);
                n_b.addVertice(ncx - nx, ncy + ny, ncz - nz);
                n_b.addVertice(ncx + nx, ncy + ny, ncz - nz);
                n_b.addVertice(ncx + nx, ncy - ny, ncz - nz);
                n_b.setCentro(new Vertice(0, 0, 0));
                cuello.add("mon_cuello_atras", n_b);

                Cara n_l = new Cara(cLado);
                n_l.addVertice(ncx - nx, ncy - ny, ncz - nz);
                n_l.addVertice(ncx - nx, ncy - ny, ncz + nz);
                n_l.addVertice(ncx - nx, ncy + ny, ncz + nz);
                n_l.addVertice(ncx - nx, ncy + ny, ncz - nz);
                n_l.setCentro(new Vertice(0, 0, 0));
                cuello.add("mon_cuello_izq", n_l);

                Cara n_r = new Cara(cLado);
                n_r.addVertice(ncx + nx, ncy - ny, ncz - nz);
                n_r.addVertice(ncx + nx, ncy + ny, ncz - nz);
                n_r.addVertice(ncx + nx, ncy + ny, ncz + nz);
                n_r.addVertice(ncx + nx, ncy - ny, ncz + nz);
                n_r.setCentro(new Vertice(0, 0, 0));
                cuello.add("mon_cuello_der", n_r);

                Cara n_t = new Cara(cTopClaro);
                n_t.addVertice(ncx - nx, ncy + ny, ncz - nz);
                n_t.addVertice(ncx - nx, ncy + ny, ncz + nz);
                n_t.addVertice(ncx + nx, ncy + ny, ncz + nz);
                n_t.addVertice(ncx + nx, ncy + ny, ncz - nz);
                n_t.setCentro(new Vertice(0, 0, 0));
                cuello.add("mon_cuello_arriba", n_t);

                Cara n_d = new Cara(cLado);
                n_d.addVertice(ncx - nx, ncy - ny, ncz - nz);
                n_d.addVertice(ncx + nx, ncy - ny, ncz - nz);
                n_d.addVertice(ncx + nx, ncy - ny, ncz + nz);
                n_d.addVertice(ncx - nx, ncy - ny, ncz + nz);
                n_d.setCentro(new Vertice(0, 0, 0));
                cuello.add("mon_cuello_abajo", n_d);

                cuello.setCentro(new Vertice(0, 0, 0));
                monitor.addParte("Monitor_Cuello", cuello);

                // ---------- Base (caja) ----------
                // Centro de la base (bc*) y semidimensiones (bx,by,bz)
                float bcx = -1.2f, bcy = -0.60f, bcz = -5.9f;
                float bx = 1.20f, by = 0.10f, bz = 0.55f;

                Partes baseM = new Partes();

                
                Cara b_f = new Cara(cFrente);
                b_f.addVertice(bcx - bx, bcy - by, bcz + bz);
                b_f.addVertice(bcx + bx, bcy - by, bcz + bz);
                b_f.addVertice(bcx + bx, bcy + by, bcz + bz);
                b_f.addVertice(bcx - bx, bcy + by, bcz + bz);
                b_f.setCentro(new Vertice(0, 0, 0));
                baseM.add("mon_base_frente", b_f);

                Cara b_b = new Cara(cFrente);
                b_b.addVertice(bcx - bx, bcy - by, bcz - bz);
                b_b.addVertice(bcx - bx, bcy + by, bcz - bz);
                b_b.addVertice(bcx + bx, bcy + by, bcz - bz);
                b_b.addVertice(bcx + bx, bcy - by, bcz - bz);
                b_b.setCentro(new Vertice(0, 0, 0));
                baseM.add("mon_base_atras", b_b);

                Cara b_l = new Cara(cLado);
                b_l.addVertice(bcx - bx, bcy - by, bcz - bz);
                b_l.addVertice(bcx - bx, bcy - by, bcz + bz);
                b_l.addVertice(bcx - bx, bcy + by, bcz + bz);
                b_l.addVertice(bcx - bx, bcy + by, bcz - bz);
                b_l.setCentro(new Vertice(0, 0, 0));
                baseM.add("mon_base_izq", b_l);

                Cara b_r = new Cara(cLado);
                b_r.addVertice(bcx + bx, bcy - by, bcz - bz);
                b_r.addVertice(bcx + bx, bcy + by, bcz - bz);
                b_r.addVertice(bcx + bx, bcy + by, bcz + bz);
                b_r.addVertice(bcx + bx, bcy - by, bcz + bz);
                b_r.setCentro(new Vertice(0, 0, 0));
                baseM.add("mon_base_der", b_r);

                Cara b_t = new Cara(cTopClaro);
                b_t.addVertice(bcx - bx, bcy + by, bcz - bz);
                b_t.addVertice(bcx - bx, bcy + by, bcz + bz);
                b_t.addVertice(bcx + bx, bcy + by, bcz + bz);
                b_t.addVertice(bcx + bx, bcy + by, bcz - bz);
                b_t.setCentro(new Vertice(0, 0, 0));
                baseM.add("mon_base_arriba", b_t);

                Cara b_d = new Cara(cLado);
                b_d.addVertice(bcx - bx, bcy - by, bcz - bz);
                b_d.addVertice(bcx + bx, bcy - by, bcz - bz);
                b_d.addVertice(bcx + bx, bcy - by, bcz + bz);
                b_d.addVertice(bcx - bx, bcy - by, bcz + bz);
                b_d.setCentro(new Vertice(0, 0, 0));
                baseM.add("mon_base_abajo", b_d);

                baseM.setCentro(new Vertice(0, 0, 0));
                monitor.addParte("Monitor_Base", baseM);
            }
            
            monitor.setCentro(new Vertice(0, 0, 0));
            escenario.addObjeto("Monitor", monitor);

            // ==========================================================
            // TECLADO 
            
            this.teclado = new Objeto();
            {
                Partes pt = new Partes();

                
                float cx = -1.2f, cy = -1.55f, zf = -5.7f;

                // Medidas base:
                // anchoBottom vs anchoTop → crean un trapecio (sensación de inclinación)
                // alto → semialto de la tapa
                // espesor → “canto” frontal que simula grosor
                // skewX → desplaza la arista superior hacia la izquierda (más lateral visible)
                float anchoBottom = 2.45f;
                float anchoTop = 2.10f;
                float alto = 0.27f;
                float espesor = 0.10f;
                float skewX = 0.18f;

                //superior
                {
                    Cara top = new Cara(cTopClaro);
                    top.addVertice(cx - anchoBottom, cy - alto, zf); // abajo izq
                    top.addVertice(cx + anchoBottom, cy - alto, zf); // abajo der
                    top.addVertice(cx + anchoTop - skewX, cy + alto, zf); // arriba der ( -X)
                    top.addVertice(cx - anchoTop - skewX, cy + alto, zf); // arriba izq ( -X)
                    top.setCentro(new Vertice(0, 0, 0));
                    pt.add("tk_tapa", top);
                }

                // Frente (canto) – tira frontal que sugiere grosor
                {
                    Cara frente = new Cara(cLado);
                    frente.addVertice(cx - anchoBottom, cy - alto - espesor, zf + 0.0003f);
                    frente.addVertice(cx + anchoBottom, cy - alto - espesor, zf + 0.0003f);
                    frente.addVertice(cx + anchoBottom, cy - alto, zf + 0.0003f);
                    frente.addVertice(cx - anchoBottom, cy - alto, zf + 0.0003f);
                    frente.setCentro(new Vertice(0, 0, 0));
                    pt.add("tk_frente", frente);
                }

                // Lateral IZQUIERDO (cara estrecha que mejora la lectura del volumen)
                {
                    Cara latL = new Cara(cLado);
                    latL.addVertice(cx - anchoBottom, cy - alto - espesor, zf + 0.0002f);
                    latL.addVertice(cx - anchoBottom, cy - alto, zf + 0.0002f);
                    latL.addVertice(cx - anchoTop - skewX, cy + alto, zf + 0.0002f);
                    latL.addVertice(cx - anchoTop - skewX, cy + alto - espesor, zf + 0.0002f);
                    latL.setCentro(new Vertice(0, 0, 0));
                    pt.add("tk_lat_izq", latL);
                }

                // Lateral DERECHO (simétrico al izquierdo; puedes comentarlo si no lo quieres)
                {
                    Cara latR = new Cara(cLado);
                    latR.addVertice(cx + anchoBottom, cy - alto - espesor, zf + 0.0002f);
                    latR.addVertice(cx + anchoBottom, cy - alto, zf + 0.0002f);
                    latR.addVertice(cx + anchoTop - skewX, cy + alto, zf + 0.0002f);
                    latR.addVertice(cx + anchoTop - skewX, cy + alto - espesor, zf + 0.0002f);
                    latR.setCentro(new Vertice(0, 0, 0));
                    pt.add("tk_lat_der", latR);
                }

                pt.setCentro(new Vertice(0, 0, 0));
                this.teclado.addParte("Teclado", pt);
            }
            
            this.teclado.setCentro(new Vertice(0, 0, 0));
            this.escenario.addObjeto("Teclado", this.teclado);

            // ==========================================================
            // CASE 
            
            this.cpu = new Objeto();
            {
                Partes pc = new Partes();

                // Centro del case (cx,cy,cz) y semidimensiones (x,y,z)
                float cx = 4.1f, cy = 1.05f, cz = -6.0f;
                float x = 0.9f, y = 1.95f, z = 1.05f;

                
                float sx = 0.65f;
                float sy = 0.18f;

                // Frente (z+)
                {
                    Cara f = new Cara(cFrente);
                    f.addVertice(cx - x, cy - y, cz + z);
                    f.addVertice(cx + x, cy - y, cz + z);
                    f.addVertice(cx + x, cy + y, cz + z);
                    f.addVertice(cx - x, cy + y, cz + z);
                    f.setCentro(new Vertice(0, 0, 0));
                    pc.add("case_frente", f);
                }

                // Atrás (z-) — mismo tamaño que el frente, pero desplazado por (sx, sy)
                {
                    Cara b = new Cara(cFrente);
                    b.addVertice(cx - x - sx, cy - y + sy, cz - z);
                    b.addVertice(cx - x - sx, cy + y + sy, cz - z);
                    b.addVertice(cx + x - sx, cy + y + sy, cz - z);
                    b.addVertice(cx + x - sx, cy - y + sy, cz - z);
                    b.setCentro(new Vertice(0, 0, 0));
                    pc.add("case_atras", b);
                }

                // Izquierda (x-)
                {
                    Cara l = new Cara(cLado);
                    l.addVertice(cx - x, cy - y, cz + z);
                    l.addVertice(cx - x, cy + y, cz + z);
                    l.addVertice(cx - x - sx, cy + y + sy, cz - z);
                    l.addVertice(cx - x - sx, cy - y + sy, cz - z);
                    l.setCentro(new Vertice(0, 0, 0));
                    pc.add("case_izq", l);
                }

                // Derecha (x+)
                {
                    Cara r = new Cara(cLateralO);
                    r.addVertice(cx + x, cy - y, cz + z);
                    r.addVertice(cx + x - sx, cy - y + sy, cz - z);
                    r.addVertice(cx + x - sx, cy + y + sy, cz - z);
                    r.addVertice(cx + x, cy + y, cz + z);
                    r.setCentro(new Vertice(0, 0, 0));
                    pc.add("case_der", r);
                }

                // Arriba (y+)
                {
                    Cara t = new Cara(cTopClaro);
                    t.addVertice(cx - x, cy + y, cz + z);
                    t.addVertice(cx + x, cy + y, cz + z);
                    t.addVertice(cx + x - sx, cy + y + sy, cz - z);
                    t.addVertice(cx - x - sx, cy + y + sy, cz - z);
                    t.setCentro(new Vertice(0, 0, 0));
                    pc.add("case_arriba", t);
                }

                // Abajo (y-)
                {
                    Cara d = new Cara(cLado);
                    d.addVertice(cx - x, cy - y, cz + z);
                    d.addVertice(cx - x - sx, cy - y + sy, cz - z);
                    d.addVertice(cx + x - sx, cy - y + sy, cz - z);
                    d.addVertice(cx + x, cy - y, cz + z);
                    d.setCentro(new Vertice(0, 0, 0));
                    pc.add("case_abajo", d);
                }

                pc.setCentro(new Vertice(0, 0, 0));
                this.cpu.addParte("CPU", pc);
            }
            
            this.cpu.setCentro(new Vertice(0, 0, 0));
            this.escenario.addObjeto("CPU", this.cpu);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Color del “fondo” de la ventana
            GL.ClearColor(1f, 1f, 1f, 1f);
            // Activa el z-buffer para que las caras se oculten correctamente
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Ajusta el viewport al tamaño actual de la ventana
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
                new Vector3(0.0f, 1.2f, 20.0f),  
                new Vector3(0.0f, 1.0f, -6.0f), 
                Vector3.UnitY                   
            );
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref view);

            // Dibuja todos los objetos del escenario
            escenario.dibujar(new Vector3(0, 0, 0));
            SwapBuffers();
        }
    }
}
