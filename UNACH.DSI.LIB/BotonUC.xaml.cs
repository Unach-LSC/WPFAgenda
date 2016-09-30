using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace UNACH.DSI.LIB
{
    /// <summary>
    /// Lógica de interacción para BotonUC.xaml
    /// </summary>
    public partial class BotonUC : UserControl
    {
        public event EventHandler miClick;
        public BotonUC()
        {
            InitializeComponent();
        }


        [DefaultValue("Titulo boton")]
        public string Titulo
        {
            get { return (string)GetValue(TituloProperty); }
            set { SetValue(TituloProperty, value); }
        }
        public static readonly DependencyProperty TituloProperty=
            DependencyProperty.Register("Titulo",typeof(string),
            typeof (BotonUC),
           new PropertyMetadata((d, e) =>
           { if (e.NewValue != null)
               ((BotonUC)d).TituloTextBlock.Text = e.NewValue.ToString(); }
               ));
        /// <summary>
        /// imagen central del Tile
        /// </summary>
        public ImageSource ImagenBoton
        {
            get { return GetValue(ImagenTileProperty) as ImageSource; }
            set { SetValue(ImagenTileProperty, value); }
        }
        public static readonly DependencyProperty ImagenTileProperty =
               DependencyProperty.Register("Icono", typeof(ImageSource), typeof(BotonUC),
               new PropertyMetadata((d, e) =>
               {
                   var tile = ((BotonUC)d);
                   tile.IconoImage.Source = e.NewValue as ImageSource;
               }));
        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (miClick != null) miClick(this, new EventArgs());
        }
        
    }
}
