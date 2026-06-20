using System;
using System.Windows.Forms;

namespace CapaPresentacion
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // CAMBIA ESTA LÍNEA CON EL NOMBRE DE TU FORMULARIO:
            Application.Run(new FrmPanelGerenteGeneral());
        }
    }
}