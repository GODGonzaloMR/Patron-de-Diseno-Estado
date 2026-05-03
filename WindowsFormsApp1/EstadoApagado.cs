using System;
using System.Drawing;

namespace WindowsFormsApp1
{
    
    // ESTADO CONCRETO — Apagado
    
    // Desde apagado se puede activar CUALQUIER modo.
    // No puede volver a apagarse (ya está apagado).
    
    public class EstadoApagado : EstadoAC
    {
        public override string Nombre      => "Apagado";
        public override string Potencia    => "0 W";
        public override string Eficiencia  => "—";
        public override Color  ColorEstado => Color.FromArgb(136, 135, 128);

        public override void Enfriar (AireAcondicionado ac) { ac.SetEstado(new EstadoEnfriar());  }
        public override void Calentar(AireAcondicionado ac) { ac.SetEstado(new EstadoCalentar()); }
        public override void Ventilar(AireAcondicionado ac) { ac.SetEstado(new EstadoVentilar()); }
        public override void Eco     (AireAcondicionado ac) { ac.SetEstado(new EstadoEco());      }
        public override void Apagar  (AireAcondicionado ac) { /* ya está apagado */              }
    }
}
