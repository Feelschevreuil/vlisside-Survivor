using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.Services.Interface;

namespace vlissides_bibliotheque.Services
{
    public class PrixService : IPrix
    {
        private readonly ApplicationDbContext _context;
        public PrixService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> UpdateLesPrix(LivreBibliotheque LivreEtatPrix, AjoutEditLivreVM form)
        {
            List<PrixEtatLivre> listPrixEtat = await GetPrixLivre(LivreEtatPrix.LivreId);

            PrixEtatLivre prixNeuf = listPrixEtat.SingleOrDefault(x => x.EtatLivre == EtatLivreEnum.NEUF);
            PrixEtatLivre prixDigital = listPrixEtat.SingleOrDefault(x => x.EtatLivre == EtatLivreEnum.NUMERIQUE);
            PrixEtatLivre prixUsager = listPrixEtat.SingleOrDefault(x => x.EtatLivre == EtatLivreEnum.USAGE);


            if (form.PrixNeuf == null || form.PrixNeuf == 0 && prixNeuf != null)
            {
                _context.PrixEtatsLivres.Remove(prixNeuf);
                _context.SaveChanges();
            };

            if (form.PrixNumerique == null || form.PrixNumerique == 0 && prixDigital != null)
            {
                _context.PrixEtatsLivres.Remove(prixDigital);
                _context.SaveChanges();
            };
            if (form.PrixUsage == null || form.PrixUsage == 0 && prixUsager != null)
            {
                _context.PrixEtatsLivres.Remove(prixUsager);
                _context.SaveChanges();
            };


            if (prixNeuf != null && form.PrixNeuf != 0)
            {
                prixNeuf.Prix = (double)form?.PrixNeuf;
                _context.PrixEtatsLivres.Update(prixNeuf);
                _context.SaveChanges();
            }
            else if (form.PrixNeuf != 0)
            {
                PrixEtatLivre nouveauPrixNeuf = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivre = EtatLivreEnum.NEUF,
                    Prix = (double)form?.PrixNeuf,
                };
                _context.PrixEtatsLivres.Add(nouveauPrixNeuf);
                _context.SaveChanges();
            }


            if (prixDigital != null && form.PrixNumerique != 0)
            {
                prixDigital.Prix = (double)form.PrixNumerique;
                _context.PrixEtatsLivres.Update(prixDigital);
                _context.SaveChanges();
            }
            else if (form.PrixNumerique != 0)
            {
                PrixEtatLivre nouveauPrixDigital = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivre = EtatLivreEnum.NUMERIQUE,
                    Prix = (double)form.PrixNumerique,
                };
                _context.PrixEtatsLivres.Add(nouveauPrixDigital);
                _context.SaveChanges();
            }

            if (prixUsager != null && form.PrixUsage != 0)
            {
                prixUsager.Prix = (double)form.PrixUsage;
                prixUsager.QuantiteUsage = (int)form.QuantiteUsagee;
                _context.PrixEtatsLivres.Update(prixUsager);
                _context.SaveChanges();
            }
            else if (form.PrixUsage != 0)
            {
                PrixEtatLivre nouveauPrixUsage = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivre = EtatLivreEnum.USAGE,
                    Prix = (double)form.PrixUsage,
                    QuantiteUsage = 0
                };
                if (form.QuantiteUsagee != null)
                {
                    nouveauPrixUsage.QuantiteUsage = (int)form.QuantiteUsagee;
                }
                _context.PrixEtatsLivres.Add(nouveauPrixUsage);
                _context.SaveChanges();
            }


            return true;
        }

        public List<PrixEtatLivre> AssocierPrixEtat(LivreBibliotheque LivreEtatPrix, AjoutEditLivreVM form)
        {
            List<PrixEtatLivre> ListPrixEtat = new();

            if (form?.PrixNeuf != null && form.PrixNeuf != 0)
            {
                PrixEtatLivre AssociationPrixNeuf = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivre = EtatLivreEnum.NEUF,
                    Prix = form.PrixNeuf.Value + 0.00,
                };
                ListPrixEtat.Add(AssociationPrixNeuf);
            }
            if (form?.PrixNumerique != null && form.PrixNumerique != 0)
            {
                PrixEtatLivre AssociationPrixNumérique = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivre = EtatLivreEnum.NUMERIQUE,
                    Prix = form.PrixNumerique.Value + 0.00,
                };
                ListPrixEtat.Add(AssociationPrixNumérique);
            }
            if (form?.PrixUsage != null && form.PrixUsage != 0)
            {
                PrixEtatLivre AssociationPrixUsager = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivre = EtatLivreEnum.USAGE,
                    Prix = form.PrixUsage.Value + 0.00,
                };
                ListPrixEtat.Add(AssociationPrixUsager);
            }

            return ListPrixEtat;
        }

        public async Task<List<PrixEtatLivre>> GetPrixLivre(int id)
        {
            return await _context.PrixEtatsLivres
                .Include(x => x.LivreBibliotheque)
                .Where(l => l.LivreBibliothequeId == id)
                .ToListAsync();
        }
    }
}

