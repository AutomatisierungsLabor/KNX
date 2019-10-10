using System;
using System.Diagnostics;
using System.Windows;

namespace KNX
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Rb_Alle_Projekte_Loeschen_Checked(object sender, RoutedEventArgs e)
        {
            if (txt_Box != null) { txt_Box.Text = "Alle KNX Projekte löschen und die ETS5 starten"; }

        }

        private void Rb_MDT_verwenden_Checked(object sender, RoutedEventArgs e)
        {
            if (txt_Box != null) { txt_Box.Text = "MDT Produktdatenbank verwenden und ETS5 starten"; }
        }

        private void Rb_Siemens_verwenden_Checked(object sender, RoutedEventArgs e)
        {
            if (txt_Box != null) { txt_Box.Text = "Siemens Produktdatenbank verwenden und ETS5 starten"; }
        }

        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
            OrdnerStrukturAnpassen();
        }

        private void Btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("ets5"))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
