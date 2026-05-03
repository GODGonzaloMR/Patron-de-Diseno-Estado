using System;
using System.Drawing;

namespace WindowsFormsApp1
{
    
    // ESTADO CONCRETO — Calentar
    
    // Tick sin if/else:
    // Calcula cuánto hay que subir con Math.Max y Math.Min.
    // Si la temperatura ya está en el objetivo, delta = 0
    // y no cambia nada. Sin condicionales.
    
    public class EstadoCalentar : EstadoAC
    {
        public override string Nombre      => "Calentar";
        public override string Potencia    => "1500 W";
        public override string Eficiencia  => "A+";
        public override Color  ColorEstado => Color.FromArgb(239, 159, 39);

        public override void Enfriar (AireAcondicionado ac) { ac.SetEstado(new EstadoEnfriar());  }
        public override void Calentar(AireAcondicionado ac) { /* ya está en este modo */          }
        public override void Ventilar(AireAcondicionado ac) { ac.SetEstado(new EstadoVentilar()); }
        public override void Eco     (AireAcondicionado ac) { ac.SetEstado(new EstadoEco());      }
        public override void Apagar  (AireAcondicionado ac) { ac.SetEstado(new EstadoApagado());  }

        // delta es 0 cuando la temp ya alcanzó el objetivo
        // Math.Max(0, ...) asegura que nunca sea negativo
        public override void Tick(AireAcondicionado ac)
        {
            double delta = Math.Max(0, ac.TempObjetivo - 0.5 - ac.TempAmbiente);
            ac.TempAmbiente += Math.Min(delta, 0.4);
        }
    }
}
