using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMovil.ViewModels
{
    public class IngresarFeedbackViewModel : INotifyPropertyChanged
    {
        // Campo privado para almacenar el valor del ID del producto
        private int _idProducto;

        // Propiedad pública que se puede enlazar (binding) desde la vista (XAML)
        public int IdProducto
        {
            get => _idProducto; // Devuelve el valor actual
            set
            {
                // Solo actualiza si el valor es diferente
                if (_idProducto != value)
                {
                    _idProducto = value;
                    OnPropertyChanged(nameof(IdProducto));
                }
            }
        }

        // Constructor que recibe el ID del producto y lo asigna a la propiedad
        public IngresarFeedbackViewModel(int idProducto)
        {
            IdProducto = idProducto;
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
