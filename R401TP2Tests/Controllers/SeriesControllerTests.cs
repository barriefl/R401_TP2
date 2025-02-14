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
        private SeriesDbContext context;
        private SeriesController controller;

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test.
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost; port=5432; Database=SeriesDB; uid=postgres; password=postgres;"); // Chaine de connexion à mettre dans les ( )

            context = new SeriesDbContext(builder.Options);
            controller = new SeriesController(context);
        }

        [TestMethod()]
        public void SeriesControllerTest()
        {
            
        }

        [TestMethod()]
        public void GetSeries_OK()
        {
            // Arrange.
            List<Serie> listeSeriesAttendues = new List<Serie>();
            listeSeriesAttendues.Add(new Serie(1, "Scrubs", "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !", 9, 184, 2001, "ABC (US)"));
            listeSeriesAttendues.Add(new Serie(2, "James May's 20th Century", "The world in 1999 would have been unrecognisable to anyone from 1900. James May takes a look at some of the greatest developments of the 20th century, and reveals how they shaped the times we live in now.", 1, 6, 2007, "BBC Two"));
            listeSeriesAttendues.Add(new Serie(3, "True Blood", "Ayant trouvé un substitut pour se nourrir sans tuer (du sang synthétique), les vampires vivent désormais parmi les humains. Sookie, une serveuse capable de lire dans les esprits, tombe sous le charme de Bill, un mystérieux vampire. Une rencontre qui bouleverse la vie de la jeune femme...", 7, 81, 2008, "HBO"));

            // Act.
            var resultat = controller.GetSeries();
            List<Serie> listeSeriesRecuperees = resultat.Result.Value.Where(s => s.Serieid <= 3).OrderBy(s => s.Serieid).ToList();

            // Assert.
            CollectionAssert.AreEqual(listeSeriesAttendues, listeSeriesRecuperees, "La liste de séries attendues n'est pas la même que la liste de séries récupérées.");
        }

        [TestMethod()]
        public void GetSerie_OK()
        {
            // Arrange.
            Serie serieAttendue = new Serie(2, "James May's 20th Century", "The world in 1999 would have been unrecognisable to anyone from 1900. James May takes a look at some of the greatest developments of the 20th century, and reveals how they shaped the times we live in now.", 1, 6, 2007, "BBC Two");

            // Act.
            var resultat = controller.GetSerie(2);
            Serie serieRecuperee = resultat.Result.Value;

            // Assert.
            Assert.AreEqual(serieAttendue, serieRecuperee, "La série attendue n'est pas la même que la série récupérée.");
        }

        [TestMethod()]
        public void GetSerie_NonOK()
        {
            // Arrange.
            Serie serieAttendue = new Serie(2, "James May's 20th Century", "The world in 3000 would have been unrecognisable to anyone from 1900. James May takes a look at some of the greatest developments of the 20th century, and reveals how they shaped the times we live in now.", 1, 6, 2007, "BBC Two");

            // Act.
            var resultat = controller.GetSerie(2);
            Serie serieRecuperee = resultat.Result.Value;

            // Assert.
            Assert.AreNotEqual(serieAttendue, serieRecuperee, "La série attendue est la même que la série récupérée.");
        }

        [TestMethod()]
        public void DeleteSerie_OK()
        {
            // Arrange.
            Serie serieAttendue = new Serie(1000000, "James May's 20th Century", "The world in 1999 would have been unrecognisable to anyone from 1900. James May takes a look at some of the greatest developments of the 20th century, and reveals how they shaped the times we live in now.", 1, 6, 2007, "BBC Two");

            // Act.
            controller.PostSerie(serieAttendue);
            var resultat = controller.DeleteSerie(1000000);

            // Assert.
            Assert.IsInstanceOfType(resultat.Result, typeof(NoContentResult), "Le retour n'est pas un NoContent.");
        }

        [TestMethod()]
        public void DeleteSerie_NonOK()
        {
            // Arrange.

            // Act.
            var resultat = controller.DeleteSerie(9999999);

            // Assert.
            Assert.IsInstanceOfType(resultat.Result, typeof(NotFoundResult), "Le retour n'est pas un NotFound.");
        }
    }
}