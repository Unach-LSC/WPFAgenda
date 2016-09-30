using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UNACH.DSI.AGENDA.WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        public int estadoEjecucion { get; set; }
        public Contacto miContacto = new Contacto();
        AgendaBDEntities _conexion = new AgendaBDEntities();
        private void NuevoBoton_miClick_1(object sender, EventArgs e)
        {
            estadoEjecucion = 1;
            FormularioGrid.Visibility=Visibility.Visible;
         //   MessageBox.Show("nuevo");
        }

        private void EditarBoton_miClick_1(object sender, EventArgs e)
        {
            estadoEjecucion = 2;
            FormularioGrid.Visibility = Visibility.Visible;
        }

        private void EliminarBoton_miClick_1(object sender, EventArgs e)
        {
            estadoEjecucion = 3;
            FormularioGrid.Visibility = Visibility.Visible;
        }

        private void SalirBoton_miClick_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            cargarDatosCompletos();
        }

        private void cargarDatosCompletos()
        {
            var _contactos = (from c in _conexion.Contactos select c);
            ContactosDataGrid.ItemsSource = _contactos.ToList();
        }



        private void BuscarButton_Click_1(object sender, RoutedEventArgs e)
        {
            var _contactos = (from c in _conexion.Contactos 
            where c.NombreCompleto.Contains
            (BusquedaTextBox.Text )
            select c);
            ContactosDataGrid.ItemsSource = _contactos.ToList();
        }

        private void GuardarButton_Click_1(object sender, RoutedEventArgs e)
        {
            switch(estadoEjecucion)
            {
                case 1:      //nuevo
                    Contacto nuevoContacto = new Contacto();
                    nuevoContacto.NombreCompleto = NombreTextBox.Text;
                    nuevoContacto.Telefono = TelefonoTextBox.Text;
                    nuevoContacto.CorreoElectronico = CorreoTextBox.Text;
                    nuevoContacto.FechaNacimiento=FechaNacimientoDatePicker.SelectedDate;
                    nuevoContacto.Direccion = DireccionTextBox.Text;
                    if (MasculinoRadioButton.IsChecked == true)
                    {
                        nuevoContacto.Sexo = "M";
                    }
                    else
                    {
                        nuevoContacto.Sexo = "F";
                    }
                    using (var context = new AgendaBDEntities())
                    {
                        context.Contactos.Add(nuevoContacto);
                        context.SaveChanges();
                        cargarDatosCompletos();
                        FormularioGrid.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 2:      //edita
                    var buscarContacto=(from c in _conexion.Contactos 
                                        where c.Id==miContacto.Id
                                            select c).FirstOrDefault();
                    if (buscarContacto != null)
                    {
                        buscarContacto.NombreCompleto = NombreTextBox.Text;
                        buscarContacto.Telefono = TelefonoTextBox.Text;
                        buscarContacto.CorreoElectronico = CorreoTextBox.Text;
                        buscarContacto.FechaNacimiento = FechaNacimientoDatePicker.SelectedDate;
                        buscarContacto.Direccion = DireccionTextBox.Text;
                        if (MasculinoRadioButton.IsChecked == true)
                        {
                            buscarContacto.Sexo = "M";
                        }
                        else
                        {
                            buscarContacto.Sexo = "F";
                        }
                        _conexion.SaveChanges();
                        cargarDatosCompletos();
                        FormularioGrid.Visibility = Visibility.Collapsed;
                    }

                    break;
                case 3:      //elimina
                          var eliminarContacto=(from c in _conexion.Contactos 
                                        where c.Id==miContacto.Id
                                            select c).FirstOrDefault();
                          if (eliminarContacto != null)
                    {
                        _conexion.Contactos.Remove(eliminarContacto);
                        _conexion.SaveChanges();
                        cargarDatosCompletos();
                        FormularioGrid.Visibility = Visibility.Collapsed;
                    }
                    break;
                default:
                    break;
            }
        }

        private void CancelarButton_Click_1(object sender, RoutedEventArgs e)
        {
            FormularioGrid.Visibility = Visibility.Collapsed;
        }

        private void ContactosDataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            Contacto contactoSeleccionado=new Contacto();
            contactoSeleccionado = (Contacto)ContactosDataGrid.SelectedItem;
            miContacto = contactoSeleccionado;
            NombreTextBox.Text = contactoSeleccionado.NombreCompleto;
            TelefonoTextBox.Text = contactoSeleccionado.Telefono;
            CorreoTextBox.Text = contactoSeleccionado.CorreoElectronico;
            DireccionTextBox.Text = contactoSeleccionado.Direccion;
            FechaNacimientoDatePicker.SelectedDate = contactoSeleccionado.FechaNacimiento;
            if (contactoSeleccionado.Sexo == "M")
            { MasculinoRadioButton.IsChecked = true; }
            else
            { FemeninoRadioButton.IsChecked = true; }
        }

       
    }
}
