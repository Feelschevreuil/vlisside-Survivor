﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque
{
    public static class GestionPrix
    {
        public static bool UpdateLesPrix(LivreBibliotheque LivreEtatPrix, ModificationLivreVM form, ApplicationDbContext _context)
        {
            List<PrixEtatLivre> listPrixEtat = _context.PrixEtatsLivres
                .Include(x => x.LivreBibliotheque)
                .Include(x => x.EtatLivre)
                .ToList();

            PrixEtatLivre prixNeuf = listPrixEtat.Find(x => x.LivreBibliotheque == LivreEtatPrix && x.EtatLivre.Nom == NomEtatLivre.NEUF);

            PrixEtatLivre prixDigital = listPrixEtat.Find(x => x.LivreBibliotheque == LivreEtatPrix && x.EtatLivre.Nom == NomEtatLivre.DIGITAL);

            PrixEtatLivre prixUsager = listPrixEtat.Find(x => x.LivreBibliotheque == LivreEtatPrix && x.EtatLivre.Nom == NomEtatLivre.USAGE);


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
                prixNeuf.Prix = (double)form.PrixNeuf;
                _context.PrixEtatsLivres.Update(prixNeuf);
                _context.SaveChanges();
            }
            else if (form.PrixNeuf != 0)
            {
                PrixEtatLivre nouveauPrixNeuf = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.NEUF).EtatLivreId,
                    Prix = (double)form.PrixNeuf,
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
                    EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.DIGITAL).EtatLivreId,
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
                    EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.USAGE).EtatLivreId,
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

        public static List<PrixEtatLivre> AssocierPrixEtat(LivreBibliotheque LivreEtatPrix, CreationLivreVM form, ApplicationDbContext _context)
        {
            List<PrixEtatLivre> ListPrixEtat = new();

            if (form.PrixNeuf != null && form.PrixNeuf != 0)
            {
                PrixEtatLivre AssociationPrixNeuf = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.NEUF).EtatLivreId,
                    Prix = form.PrixNeuf,
                };
                ListPrixEtat.Add(AssociationPrixNeuf);
            }
            if (form.PrixNumerique != null && form.PrixNumerique != 0)
            {
                PrixEtatLivre AssociationPrixNumérique = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.DIGITAL).EtatLivreId,
                    Prix = form.PrixNumerique,
                };
                ListPrixEtat.Add(AssociationPrixNumérique);
            }
            if (form.PrixUsage != null && form.PrixUsage != 0)
            {
                PrixEtatLivre AssociationPrixUsager = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.USAGE).EtatLivreId,
                    Prix = form.PrixUsage,
                };
                ListPrixEtat.Add(AssociationPrixUsager);
            }

            return ListPrixEtat;
        }
    }
}

