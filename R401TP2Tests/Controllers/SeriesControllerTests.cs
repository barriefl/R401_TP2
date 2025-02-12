using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using R401TP2.Controllers;
using R401TP2.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R401TP2.Controllers.Tests
{
    [TestClass()]
    public class SeriesControllerTests
    {
        [TestMethod()]
        public void SeriesControllerTest()
        {
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost; port=5432; Database=SeriesDB; uid=postgres; password=postgres;"); // Chaine de connexion à mettre dans les ( )

            SeriesDbContext context = new SeriesDbContext(builder.Options);
            SeriesController controller = new SeriesController(context);
        }

        [TestMethod()]
        public void GetSeries_OK()
        {
            // Arrange.
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost; port=5432; Database=SeriesDB; uid=postgres; password=postgres;"); // Chaine de connexion à mettre dans les ( )
            SeriesDbContext context = new SeriesDbContext(builder.Options);
            SeriesController controller = new SeriesController(context);

            List<Serie> listeSeriesAttendues = new List<Serie>();
            listeSeriesAttendues.Add(new Serie(1, "Scrubs", "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !", 9, 184, 2001, "ABC (US)"));
            listeSeriesAttendues.Add(new Serie(2, "James May's 20th Century", "The world in 1999 would have been unrecognisable to anyone from 1900. James May takes a look at some of the greatest developments of the 20th century, and reveals how they shaped the times we live in now.", 1, 6, 2007, "BBC Two"));
            listeSeriesAttendues.Add(new Serie(3, "True Blood", "Ayant trouvé un substitut pour se nourrir sans tuer (du sang synthétique), les vampires vivent désormais parmi les humains. Sookie, une serveuse capable de lire dans les esprits, tombe sous le charme de Bill, un mystérieux vampire. Une rencontre qui bouleverse la vie de la jeune femme...", 7, 81, 2008, "HBO"));

            List<Serie> listeSeriesRecuperees = new List<Serie>();

            // Act.
            var resultat = controller.GetSeries();
            listeSeriesRecuperees.Where(s => s.Serieid <= 3).ToList();
            // PAS DE LISTE RECUPEREES.

            // Assert.
            CollectionAssert.AreEqual(listeSeriesAttendues, listeSeriesRecuperees);
        }
    }
}