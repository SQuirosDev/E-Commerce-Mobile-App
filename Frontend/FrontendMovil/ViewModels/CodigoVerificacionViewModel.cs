using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMovil.ViewModels
{
    public class CodigoVerificacionViewModel : INotifyPropertyChanged
    {
        // Campo privado para almacenar el valor del correo
        private string _correo;

        // Propiedad pública que se puede enlazar (binding) desde la vista (XAML)
        public string Correo
        {
            get => _correo; // Devuelve el valor actual
            set
            {
                // Solo actualiza si el valor es diferente
                if (_correo != value)
                {
                    _correo = value;
                    OnPropertyChanged(nameof(Correo));
                }
            }
        }

        // Constructor que recibe el correo y lo asigna a la propiedad
        public CodigoVerificacionViewModel(string correo)
        {
            Correo = correo;
        }

        // Evento requerido por INotifyPropertyChanged para que la vista sepa cuándo actualizar
        public event PropertyChangedEventHandler PropertyChanged;

        // Método que dispara el evento de cambio de propiedad
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
