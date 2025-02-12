using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R401TP2.Models.EntityFramework;

namespace R401TP2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly SeriesDbContext _context;

        /// <summary>
        /// Constructeur pour le contrôleur SeriesController.
        /// </summary>
        /// <param name="context">Le contexte de la base de données utilisé pour accéder aux séries.</param>
        public SeriesController(SeriesDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère toutes les séries.
        /// </summary>
        /// <returns>Une liste de séries sous forme de réponse HTTP 200 OK.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Serie>>> GetSeries()
        {
            return await _context.Series.ToListAsync();
        }

        /// <summary>
        /// Récupère une série spécifique en fonction de son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de la série à récupérer.</param>
        /// <returns>Une série sous forme de réponse HTTP 200 OK ou une erreur 404 Not Found si la série n'existe pas.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Serie>> GetSerie(int id)
        {
            var serie = await _context.Series.FindAsync(id);

            if (serie == null)
            {
                return NotFound();
            }

            return serie;
        }

        /// <summary>
        /// Met à jour une série spécifique en fonction de son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de la série à mettre à jour.</param>
        /// <param name="serie">Les nouvelles données de la série à mettre à jour.</param>
        /// <returns>Une réponse HTTP 204 No Content si la mise à jour est réussie, ou des erreurs spécifiques selon le cas (400, 404, 409).</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PutSerie(int id, Serie serie)
        {
            if (id != serie.Serieid)
            {
                return BadRequest();
            }

            _context.Entry(serie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SerieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Crée une nouvelle série.
        /// </summary>
        /// <param name="serie">Les données de la série à créer.</param>
        /// <returns>La série créée avec une réponse HTTP 201 Created ou une erreur 400 Bad Request.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Serie>> PostSerie(Serie serie)
        {
            _context.Series.Add(serie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSerie", new { id = serie.Serieid }, serie);
        }

        /// <summary>
        /// Supprime une série spécifique en fonction de son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de la série à supprimer.</param>
        /// <returns>Une réponse HTTP 204 No Content si la suppression est réussie, ou une erreur 404 Not Found si la série n'existe pas.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSerie(int id)
        {
            var serie = await _context.Series.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }

            _context.Series.Remove(serie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Vérifie si une série existe dans la base de données par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de la série à vérifier.</param>
        /// <returns>Vrai si la série existe, sinon faux.</returns>
        private bool SerieExists(int id)
        {
            return _context.Series.Any(e => e.Serieid == id);
        }
    }
}
