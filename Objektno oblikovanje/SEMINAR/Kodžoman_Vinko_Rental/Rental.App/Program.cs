using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rental
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create the app controller
            AppController controller = new AppController();

            fillWithTestData();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(controller));
        }

        public static void fillWithTestData()
        {
            // Initialise NH and SQLite
            NHibernateService.Init();

            // Insert some data
            Employee employee1 = new Employee("Vinko", "Zadric");
            Employee employee2 = new Employee("Mlako", "Vader");
            Employee employee3 = new Employee("Hesimono", "Kaero");
            Client client = new Client("Marin", "Veljko", employee1);

            // Clean repos (because they are singeltons, they might still have some data left in them)
            // PersonRepository.Instance.Clear();

            // Fill the repos
            PersonRepository.Instance.Add(employee1);
            PersonRepository.Instance.Add(employee2);
            PersonRepository.Instance.Add(employee3);
            PersonRepository.Instance.Add(client);
        }
    }
}
