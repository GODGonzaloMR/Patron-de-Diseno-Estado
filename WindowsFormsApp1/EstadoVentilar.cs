using System;
using System.Drawing;

namespace WindowsFormsApp1
{
    
    // ESTADO CONCRETO — Ventilar
    // Solo circula el aire.
    
    public class EstadoVentilar : EstadoAC
    {
        private readonly Random _rnd = new Random();

        public override string Nombre      => "Ventilar";
        public override string Potencia    => "80 W";
        public override string Eficiencia  => "A+++";
        public override Color  ColorEstado => Color.FromArgb(29, 158, 117);

        public override void Enfriar (AireAcondicionado ac) { ac.SetEstado(new EstadoEnfriar());  }
        public override void Calentar(AireAcondicionado ac) { ac.SetEstado(new EstadoCalentar()); }
        public override void Ventilar(AireAcondicionado ac) { /* ya está en este modo */          }
        public override void Eco     (AireAcondicionado ac) { ac.SetEstado(new EstadoEco());      }
        public override void Apagar  (AireAcondicionado ac) { ac.SetEstado(new EstadoApagado());  }

        // Simulación física: variación aleatoria natural del ambiente
        public override void Tick(AireAcondicionado ac)
        {
            double drift = (_rnd.NextDouble() - 0.5) * 0.1;
            ac.TempAmbiente = Math.Round(ac.TempAmbiente + drift, 1);
        }
    }
}
