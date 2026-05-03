using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    
    // PARTE 4: FORMULARIO PRINCIPAL (UI Controller)
    
    // Sin if/else ni switch en ninguna parte.
    // El botón activo se resuelve con un Dictionary<string, Button>
    // que mapea nombre de estado → botón correspondiente.
    
    public partial class Form1 : Form
    {
        private readonly AireAcondicionado _ac;
        private readonly Timer _timer;

        // Dictionary que reemplaza el switch de botones
        private Dictionary<string, Button> _modoBoton;

        private Label  lblBrand, lblStatusBadge;
        private Label  lblTempAmbVal, lblTempObjVal;
        private Label  lblTargetDisplay;
        private Label  lblInfoModo, lblInfoPotencia, lblInfoEficiencia;
        private Button btnMinus, btnPlus;
        private Button btnApagar, btnEnfriar, btnCalentar, btnVentilar, btnEco;
        private Panel  pnlIndicator;

        public Form1()
        {
            InitializeComponent();
            InicializarUI();

            // Crear el contexto (núcleo del patrón Estado)
            _ac = new AireAcondicionado();

            // Suscribirse al evento — cuando el AC cambie, actualizamos la UI
            _ac.EstadoCambiado += (s, e) => ActualizarUI();

            // Timer de simulación: tick cada 1500ms
            _timer = new Timer();
            _timer.Interval = 1500;
            _timer.Tick += (s, e) => _ac.Tick();
            _timer.Start();

            ActualizarUI();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        // CONSTRUCCIÓN DE LA INTERFAZ
        
        private void InicializarUI()
        {
            this.Text            = "Gonzalo Cortez Huerta · Inverter — Patrón Estado";
            this.Size            = new Size(600, 430);
            this.MinimumSize     = new Size(600, 430);
            this.MaximumSize     = new Size(600, 430);
            this.BackColor       = Color.FromArgb(250, 250, 249);
            this.Font            = new Font("Segoe UI", 9f);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Franja de color superior
            pnlIndicator = new Panel
            {
                Location  = new Point(0, 0),
                Size      = new Size(600, 5),
                BackColor = Color.FromArgb(200, 200, 200)
            };

            // Header 
            var pnlHeader = new Panel
            {
                Location  = new Point(0, 5),
                Size      = new Size(600, 52),
                BackColor = Color.White
            };
            pnlHeader.Paint += (s, e) =>
                e.Graphics.DrawLine(new Pen(Color.FromArgb(230, 228, 220), 1), 0, 51, 600, 51);

            lblBrand = new Label
            {
                Text      = "Gonzalo Cortez Huerta 22210761  ·  INVERTER 4",
                Location  = new Point(20, 16),
                AutoSize  = true,
                Font      = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(100, 98, 94)
            };

            lblStatusBadge = new Label
            {
                Text        = "Apagado",
                Location    = new Point(450, 13),
                Size        = new Size(130, 26),
                TextAlign   = ContentAlignment.MiddleCenter,
                Font        = new Font("Segoe UI", 8.5f),
                ForeColor   = Color.FromArgb(95, 94, 90),
                BackColor   = Color.FromArgb(241, 239, 232),
                BorderStyle = BorderStyle.FixedSingle
            };

            pnlHeader.Controls.Add(lblBrand);
            pnlHeader.Controls.Add(lblStatusBadge);

            // Temperaturas 
            var pnlTemps = new Panel
            {
                Location  = new Point(0, 57),
                Size      = new Size(600, 110),
                BackColor = Color.White
            };
            pnlTemps.Paint += (s, e) =>
            {
                var pen = new Pen(Color.FromArgb(230, 228, 220), 1);
                e.Graphics.DrawLine(pen, 300, 10, 300, 100);
                e.Graphics.DrawLine(pen, 0, 109, 600, 109);
            };

            CrearBloqueTemp(pnlTemps, 0,   "Temperatura ambiente", out lblTempAmbVal);
            CrearBloqueTemp(pnlTemps, 300, "Temperatura objetivo",  out lblTempObjVal);

            // Control +/-
            var pnlControl = new Panel
            {
                Location  = new Point(0, 167),
                Size      = new Size(600, 90),
                BackColor = Color.White
            };
            pnlControl.Paint += (s, e) =>
                e.Graphics.DrawLine(new Pen(Color.FromArgb(230, 228, 220), 1), 0, 89, 600, 89);

            var lblControlLbl = new Label
            {
                Text      = "Ajustar temperatura objetivo",
                Location  = new Point(0, 10),
                Size      = new Size(600, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(100, 98, 94),
                Font      = new Font("Segoe UI", 8.5f)
            };

            btnMinus = CrearBtnAdj("−", new Point(150, 35));
            btnMinus.Click += (s, e) =>
            {
                _ac.TempObjetivo = Math.Max(16, _ac.TempObjetivo - 1);
                ActualizarUI();
            };

            lblTargetDisplay = new Label
            {
                Text      = "21°C",
                Location  = new Point(220, 35),
                Size      = new Size(160, 45),
                TextAlign = ContentAlignment.MiddleCenter,
                Font      = new Font("Segoe UI", 26f),
                ForeColor = Color.FromArgb(55, 65, 81)
            };

            btnPlus = CrearBtnAdj("+", new Point(400, 35));
            btnPlus.Click += (s, e) =>
            {
                _ac.TempObjetivo = Math.Min(30, _ac.TempObjetivo + 1);
                ActualizarUI();
            };

            pnlControl.Controls.Add(lblControlLbl);
            pnlControl.Controls.Add(btnMinus);
            pnlControl.Controls.Add(lblTargetDisplay);
            pnlControl.Controls.Add(btnPlus);

            // Botones de modo 
            var pnlModos = new Panel
            {
                Location  = new Point(0, 257),
                Size      = new Size(600, 80),
                BackColor = Color.White
            };
            pnlModos.Paint += (s, e) =>
                e.Graphics.DrawLine(new Pen(Color.FromArgb(230, 228, 220), 1), 0, 79, 600, 79);

            btnApagar   = CrearBtnModo("Apagar",   new Point(0,   0), pnlModos);
            btnEnfriar  = CrearBtnModo("Enfriar",  new Point(120, 0), pnlModos);
            btnCalentar = CrearBtnModo("Calentar", new Point(240, 0), pnlModos);
            btnVentilar = CrearBtnModo("Ventilar", new Point(360, 0), pnlModos);
            btnEco      = CrearBtnModo("Eco",      new Point(480, 0), pnlModos);

            btnApagar.Click   += (s, e) => _ac.Apagar();
            btnEnfriar.Click  += (s, e) => _ac.Enfriar();
            btnCalentar.Click += (s, e) => _ac.Calentar();
            btnVentilar.Click += (s, e) => _ac.Ventilar();
            btnEco.Click      += (s, e) => _ac.Eco();

            
            // Reemplaza completamente el switch
            _modoBoton = new Dictionary<string, Button>
            {
                { "Apagado",  btnApagar   },
                { "Enfriar",  btnEnfriar  },
                { "Calentar", btnCalentar },
                { "Ventilar", btnVentilar },
                { "Eco",      btnEco      }
            };

            // Barra de info 
            var pnlInfo = new Panel
            {
                Location  = new Point(0, 337),
                Size      = new Size(600, 58),
                BackColor = Color.FromArgb(248, 247, 244)
            };

            lblInfoModo       = CrearLabelInfo(pnlInfo,  10, "Modo: —");
            lblInfoPotencia   = CrearLabelInfo(pnlInfo, 210, "Potencia: —");
            lblInfoEficiencia = CrearLabelInfo(pnlInfo, 420, "Eficiencia: —");

            // Agregar todo al Form 
            this.Controls.Add(pnlIndicator);
            this.Controls.Add(pnlHeader);
            this.Controls.Add(pnlTemps);
            this.Controls.Add(pnlControl);
            this.Controls.Add(pnlModos);
            this.Controls.Add(pnlInfo);
        }

        
        // PARTE 5: ACTUALIZAR UI — Sin if/else ni switch
       
        
        private void ActualizarUI()
        {
            if (InvokeRequired) { Invoke(new Action(ActualizarUI)); return; }

            var estado = _ac.GetEstado();
            var color  = estado.ColorEstado;

            // Indicador y badge
            pnlIndicator.BackColor   = color;
            lblStatusBadge.Text      = estado.Nombre;
            lblStatusBadge.ForeColor = DarkenColor(color, 0.5f);
            lblStatusBadge.BackColor = LightenColor(color, 0.85f);

            // Temperaturas
            lblTempAmbVal.Text         = _ac.TempAmbiente.ToString("F1") + "°C";
            lblTempAmbVal.ForeColor    = color;
            lblTempObjVal.Text         = _ac.TempObjetivo.ToString("F0") + "°C";
            lblTempObjVal.ForeColor    = color;
            lblTargetDisplay.Text      = _ac.TempObjetivo.ToString("F0") + "°C";
            lblTargetDisplay.ForeColor = color;

            // Info
            lblInfoModo.Text       = "Modo: "       + estado.Nombre;
            lblInfoPotencia.Text   = "Potencia: "   + estado.Potencia;
            lblInfoEficiencia.Text = "Eficiencia: " + estado.Eficiencia;

            // Resaltar botón activo 
            ResetearBotones();
            Button btnActivo;
            _modoBoton.TryGetValue(estado.Nombre, out btnActivo);
            btnActivo.BackColor = LightenColor(color, 0.80f);
            btnActivo.ForeColor = DarkenColor(color, 0.4f);
            btnActivo.FlatAppearance.BorderColor = color;
        }

        
        // HELPERS
        
        private void CrearBloqueTemp(Panel parent, int x, string titulo, out Label lblValor)
        {
            var lblTitulo = new Label
            {
                Text      = titulo,
                Location  = new Point(x + 10, 18),
                Size      = new Size(280, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(100, 98, 94),
                Font      = new Font("Segoe UI", 8.5f)
            };
            lblValor = new Label
            {
                Text      = "—°C",
                Location  = new Point(x + 10, 42),
                Size      = new Size(280, 52),
                TextAlign = ContentAlignment.MiddleCenter,
                Font      = new Font("Segoe UI", 28f),
                ForeColor = Color.FromArgb(55, 65, 81)
            };
            parent.Controls.Add(lblTitulo);
            parent.Controls.Add(lblValor);
        }

        private Button CrearBtnAdj(string texto, Point loc)
        {
            var btn = new Button
            {
                Text      = texto,
                Location  = loc,
                Size      = new Size(38, 38),
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 16f),
                ForeColor = Color.FromArgb(55, 65, 81),
                BackColor = Color.White,
                Cursor    = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(200, 198, 192);
            btn.FlatAppearance.BorderSize  = 1;
            return btn;
        }

        private Button CrearBtnModo(string texto, Point loc, Panel parent)
        {
            var btn = new Button
            {
                Text      = texto,
                Location  = loc,
                Size      = new Size(120, 80),
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(80, 78, 74),
                BackColor = Color.White,
                Cursor    = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(230, 228, 220);
            btn.FlatAppearance.BorderSize  = 1;
            parent.Controls.Add(btn);
            return btn;
        }

        private Label CrearLabelInfo(Panel parent, int x, string texto)
        {
            var lbl = new Label
            {
                Text      = texto,
                Location  = new Point(x, 0),
                Size      = new Size(200, 58),
                TextAlign = ContentAlignment.MiddleLeft,
                Font      = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(80, 78, 74)
            };
            parent.Controls.Add(lbl);
            return lbl;
        }

        private void ResetearBotones()
        {
            foreach (var btn in _modoBoton.Values)
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(80, 78, 74);
                btn.FlatAppearance.BorderColor = Color.FromArgb(230, 228, 220);
            }
        }

        private Color LightenColor(Color c, float factor)
        {
            return Color.FromArgb(
                (int)(c.R + (255 - c.R) * factor),
                (int)(c.G + (255 - c.G) * factor),
                (int)(c.B + (255 - c.B) * factor));
        }

        private Color DarkenColor(Color c, float factor)
        {
            return Color.FromArgb(
                (int)(c.R * (1 - factor)),
                (int)(c.G * (1 - factor)),
                (int)(c.B * (1 - factor)));
        }
    }
}
