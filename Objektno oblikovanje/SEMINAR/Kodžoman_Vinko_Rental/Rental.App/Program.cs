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
            Client client = new Client("Marin", "Veljko", employee3);
            Client client1 = new Client("John", "Make", employee1);
            Client client2 = new Client("Mark", "Shannon", employee3);
            Client client3 = new Client("Laplace", "Smith", employee1);

            // Fill the repos
            PersonRepository.Instance.Add(employee1);
            PersonRepository.Instance.Add(employee2);
            PersonRepository.Instance.Add(employee3);
            PersonRepository.Instance.Add(client);
            PersonRepository.Instance.Add(client1);
            PersonRepository.Instance.Add(client2);
            PersonRepository.Instance.Add(client3);

            // Add some rentals
            // Create some features (payed and included)
            IList<SpecialFeatures> sF = new List<SpecialFeatures>();
            sF.Add(new SpecialFeatures(200, "Boat trip"));
            sF.Add(new SpecialFeatures(150, "Dinner near the sea"));

            IList<RentalInclude> rF = new List<RentalInclude>();
            rF.Add(new RentalInclude(Offer.balcony, 2));
            rF.Add(new RentalInclude(Offer.kitchen, 2));
            rF.Add(new RentalInclude(Offer.room, 4));

            // Create the apartmant via the factory and add it to the repo
            Apartment apartmant = ApartmanFactory.createApartman(client, "Vila Zrinka",
                "A beautiful vila on the sea, breathtaking view. Enjoy the warm sun on your skin and let go all your woories.",
                "12004", "Torovinkova 5", 200, rF, sF);
            RentalRepository.Instance.Add(apartmant);
        }
    }
}
