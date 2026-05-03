using System;
using System.Drawing;

namespace WindowsFormsApp1
{
    
    // INTERFAZ ESTADO (Clase abstracta base)
    
    // Define el CONTRATO que todos los estados deben cumplir.
    // Cada método = una acción posible sobre el AC.
    
    public abstract class EstadoAC
    {
        // Propiedades informativas de cada estado
        public abstract string Nombre      { get; }
        public abstract string Potencia    { get; }
        public abstract string Eficiencia  { get; }
        public abstract Color  ColorEstado { get; }

        // Acciones del patrón Estado — cada clase concreta decide qué es válido
        public abstract void Enfriar (AireAcondicionado ac);
        public abstract void Calentar(AireAcondicionado ac);
        public abstract void Ventilar(AireAcondicionado ac);
        public abstract void Eco     (AireAcondicionado ac);
        public abstract void Apagar  (AireAcondicionado ac);

        // Lógica física por tick (simulación de temperatura) — opcional en cada estado
        public virtual void Tick(AireAcondicionado ac) { }

        public override string ToString() => Nombre;
    }
}
