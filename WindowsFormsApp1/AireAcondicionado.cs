using System;

namespace WindowsFormsApp1
{
    
    // CONTEXTO — AireAcondicionado
    //
    // Delega todas las acciones al estado actual.
    // Notifica a la UI mediante el evento EstadoCambiado.
    
    public class AireAcondicionado
    {
        private EstadoAC _estadoActual;

        public double TempAmbiente { get; set; } = 28.0;
        public double TempObjetivo { get; set; } = 21.0;

        // Evento para notificar a la UI cuando algo cambia
        public event EventHandler EstadoCambiado;

        public AireAcondicionado()
        {
            _estadoActual = new EstadoApagado();
        }

        public EstadoAC GetEstado() => _estadoActual;

        public void SetEstado(EstadoAC nuevoEstado)
        {
            _estadoActual = nuevoEstado;
            NotificarCambio();
        }

        // Acciones delegadas al estado actual
        public void Enfriar()  => _estadoActual.Enfriar(this);
        public void Calentar() => _estadoActual.Calentar(this);
        public void Ventilar() => _estadoActual.Ventilar(this);
        public void Eco()      => _estadoActual.Eco(this);
        public void Apagar()   => _estadoActual.Apagar(this);

        // Tick llamado por el Timer de la Form
        public void Tick()
        {
            _estadoActual.Tick(this);
            NotificarCambio();
        }

        private void NotificarCambio()
        {
            if (EstadoCambiado != null)
                EstadoCambiado(this, EventArgs.Empty);
        }
    }
}
