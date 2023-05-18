using Npgsql;
namespace Baby_foot
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try {
           // ApplicationConfiguration.Initialize();
                Application.Run(new SelectionJoueur());
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}