using System;
using System.Drawing;

namespace WindowsFormsApp1
{
    
    // ESTADO CONCRETO — Eco
    // Calcula por separado cuánto enfriar y cuánto calentar.
    // Solo uno de los dos deltas será mayor que 0 a la vez,
    // el otro será 0. Suma y resta sin condicionales.
    
    public class EstadoEco : EstadoAC
    {
        public override string Nombre      => "Eco";
        public override string Potencia    => "600 W";
        public override string Eficiencia  => "A+++";
        public override Color  ColorEstado => Color.FromArgb(99, 153, 34);

        public override void Enfriar (AireAcondicionado ac) { ac.SetEstado(new EstadoEnfriar());  }
        public override void Calentar(AireAcondicionado ac) { ac.SetEstado(new EstadoCalentar()); }
        public override void Ventilar(AireAcondicionado ac) { ac.SetEstado(new EstadoVentilar()); }
        public override void Eco     (AireAcondicionado ac) { /* ya está en este modo */          }
        public override void Apagar  (AireAcondicionado ac) { ac.SetEstado(new EstadoApagado());  }

        // Cuando hace calor: deltaFrio > 0, deltaCalor = 0
        // Cuando hace frío: deltaCalor > 0, deltaFrio = 0
        // En zona confortable: ambos = 0, no cambia nada
        public override void Tick(AireAcondicionado ac)
        {
            double deltaFrio   = Math.Max(0, ac.TempAmbiente - ac.TempObjetivo - 1.0);
            double deltaCalor  = Math.Max(0, ac.TempObjetivo - 1.0 - ac.TempAmbiente);
            ac.TempAmbiente -= Math.Min(deltaFrio,  0.15);
            ac.TempAmbiente += Math.Min(deltaCalor, 0.15);
        }
    }
}
