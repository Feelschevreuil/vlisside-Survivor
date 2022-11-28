using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Extensions;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using vlissides_bibliotheque.Enums;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using static Humanizer.In;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Humanizer;
using System.Diagnostics;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize(Roles = RolesName.Admin)]
    public class TableauDeBordController : Controller
    {
        private readonly SignInManager<Etudiant> _signInManager;
        private readonly UserManager<Utilisateur> _userManager;
        private readonly UserManager<Etudiant> _userManagerEtudiant;
        private readonly ApplicationDbContext _context;
        private readonly LivresBibliothequeDAO _livresBibliothequeDAO;

        public TableauDeBordController(
            SignInManager<Etudiant> signInManager,
            UserManager<Utilisateur> userManager,
            UserManager<Etudiant> userManagerEtudiant,
            ApplicationDbContext context,
            LivresBibliothequeDAO livresBibliothequeDAO
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userManagerEtudiant = userManagerEtudiant;
            _context = context;
            _livresBibliothequeDAO = livresBibliothequeDAO;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //------------------Cours------------------

        [HttpGet]
        public IActionResult Cours()
        {
            List<Cours> cours = _context.Cours
                .Include(cours => cours.ProgrammeEtude)
                .ToList();

            return PartialView("~/Views/TableauDeBord/Cours.cshtml", cours);
        }

        [HttpGet]
        public IActionResult CreerCours()
        {
            GestionCoursVM cours = new();
            cours.ProgrammesEtude = ListDropDown.ListDropDownProgrammesEtude(_context);
            return PartialView("Views/Shared/_CoursPartial.cshtml", cours);
        }
        [HttpPost]
        public IActionResult CreerCours([FromBody] GestionCoursVM vm)
        {
            ModelState.Remove(nameof(vm.ProgrammeEtude));
            ModelState.Remove(nameof(vm.ProgrammesEtude));
            ModelState.Remove(nameof(vm.Id));

            if (ModelState.IsValid)
            {
                Cours nouveauCours = new()
                {
                    CoursId = 0,
                    ProgrammeEtudeId = vm.ProgrammesEtudeId,
                    Nom = vm.Nom,
                    Code = vm.Code,
                    AnneeParcours = vm.AnneeParcours,
                    Description = vm.Description,
                };
                _context.Cours.Add(nouveauCours);
                _context.SaveChanges();
                vm.Id = nouveauCours.CoursId;
                return Json(vm);
            }

            vm.ProgrammesEtude = ListDropDown.ListDropDownProgrammesEtude(_context);
            return PartialView("Views/Shared/_CoursPartial.cshtml", vm);
        }
        [HttpGet]
        public IActionResult ModifierCours(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Cours coursRechercher = _context.Cours.Where(x => x.CoursId == id).FirstOrDefault();
            if (coursRechercher == null)
            {
                return NotFound();
            }

            GestionCoursVM vm = new()
            {
                CoursId = coursRechercher.CoursId,
                Nom = coursRechercher.Nom,
                Code = coursRechercher.Code,
                AnneeParcours = coursRechercher.AnneeParcours,
                Description = coursRechercher.Description,
                ProgrammesEtudeId = coursRechercher.ProgrammeEtudeId,
            };
            vm.ProgrammesEtude = ListDropDown.ListDropDownProgrammesEtude(_context);
            return PartialView("Views/Shared/_CoursPartial.cshtml", vm);

        }
        [HttpPost]
        public IActionResult ModifierCours([FromBody] GestionCoursVM vm)
        {
            ModelState.Remove(nameof(vm.ProgrammeEtude));
            ModelState.Remove(nameof(vm.ProgrammesEtude));
            ModelState.Remove(nameof(vm.Id));

            if (ModelState.IsValid)
            {
                Cours coursRechercher = _context.Cours.Where(x => x.CoursId == vm.CoursId).FirstOrDefault();

                coursRechercher.ProgrammeEtudeId = vm.ProgrammesEtudeId;
                coursRechercher.Nom = vm.Nom;
                coursRechercher.Code = vm.Code;
                coursRechercher.AnneeParcours = vm.AnneeParcours;
                coursRechercher.Description = vm.Description;

                _context.Cours.Update(coursRechercher);
                _context.SaveChanges();
                vm.Id = coursRechercher.CoursId;
                return Json(vm);
            }

            vm.ProgrammesEtude = ListDropDown.ListDropDownProgrammesEtude(_context);
            return PartialView("Views/Shared/_CoursPartial.cshtml", vm);

        }
        [HttpPost]
        public IActionResult SupprimerCours([FromBody] int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Cours supprimerCours = _context.Cours.Where(x => x.CoursId == id).FirstOrDefault();
            if (supprimerCours == null)
            {
                return NotFound();
            }
            _context.Cours.Remove(supprimerCours);
            _context.SaveChanges();
            return Ok();
        }

        //------------------Étudiants------------------

        [HttpGet]
        public IActionResult Etudiants()
        {
            List<Etudiant> etudiants = _userManagerEtudiant.Users
                .Include(etudiant => etudiant.ProgrammeEtude)
                .Include(etudiant => etudiant.Adresse)
                .Include(etudiant => etudiant.Adresse.Province)
                .ToList();

            return PartialView("~/Views/TableauDeBord/Etudiants.cshtml", etudiants);
        }

        [HttpGet]
        public IActionResult CreerEtudiant()
        {
            return PartialView("Views/Shared/_EtudiantPartial.cshtml", _context.NewGestionProfilVM());
        }

        [HttpPost]
        public async Task<IActionResult> CreerEtudiantAsync([FromBody] GestionProfilVM vm)
        {
            ModelState.Remove(nameof(vm.ProgrammeEtudes));
            ModelState.Remove(nameof(vm.Provinces));
            ModelState.Remove(nameof(vm.checkBoxCours));

            if (vm.CodePostal != null)
            {
                vm.CodePostal = vm.CodePostal.ToUpper();
            }

            if (ModelState.IsValid)
            {

                Adresse adresse = new()
                {
                    App = vm.App,
                    CodePostal = vm.CodePostal,
                    NumeroCivique = Convert.ToInt32(vm.NoCivique),
                    Rue = vm.Rue,
                    Ville = vm.Ville,
                    ProvinceId = (int)vm.ProvinceId,
                };

                _context.Adresses.Add(adresse);
                _context.SaveChanges();

                // model binding
                Etudiant etudiant = new()
                {
                    Email = vm.Courriel,
                    UserName = vm.Courriel,
                    Nom = vm.Nom,
                    Prenom = vm.Prenom,
                    PhoneNumber = vm.NoTelephone,
                    ProgrammeEtudeId = (int)vm.ProgrammeEtudeId,
                    AdresseId = adresse.AdresseId,
                    Adresse = adresse,
                    EmailConfirmed = true
                };

                // Obj pour HASHER un mot de passe
                PasswordHasher<Etudiant> passwordHasher = new();

                // création
                var result = await _userManagerEtudiant.CreateAsync(etudiant, passwordHasher.HashPassword(null, "Jaimelaprog1!"));

                if (result.Succeeded)
                {

                    // ajouter rôle
                    await _userManagerEtudiant.AddToRoleAsync(etudiant, "Etudiant");

                    vm.NomProgrammeEtude = _context.ProgrammesEtudes.Find(vm.ProgrammeEtudeId).Nom;
                    vm.NomProvince = _context.Provinces.Find(vm.ProvinceId).Nom;

                    return Json(vm);
                }

                foreach (var error in result.Errors)
                {
                    if (error.Code == "PasswordTooShort")
                    {
                        ModelState.AddModelError(string.Empty, "Le mot de passe doit être d'au moins 6 caractères.");
                    }
                    if (error.Code == "PasswordRequiresNonAlphanumeric")
                    {
                        ModelState.AddModelError(string.Empty, "Le mot de passe doit avoir au moins un caractère spécial (Ex: !, $, %, ?, &, *, etc...).");
                    }
                    if (error.Code == "PasswordRequiresLower")
                    {
                        ModelState.AddModelError(string.Empty, "Le mot de passe doit avoir au moins une lettre minuscule.");
                    }
                    if (error.Code == "PasswordRequiresUpper")
                    {
                        ModelState.AddModelError(string.Empty, "Le mot de passe doit avoir au moins une lettre majuscule.");
                    }
                    if (error.Code == "DuplicateUserName")
                    {
                        ModelState.AddModelError(string.Empty, "Le courriel que vous avez entré existe déjà.");
                    }
                }
            }

            vm.ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom));
            vm.Provinces = new SelectList(_context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom));

            return PartialView("Views/Shared/_EtudiantPartial.cshtml", vm);
        }

        [HttpGet]
        public IActionResult ModifierEtudiant(string Id)
        {
            Etudiant? etudiant = _userManagerEtudiant.Users
                .Where(etudiant => etudiant.Id == Id)
                .Include(etudiant => etudiant.ProgrammeEtude)
                .Include(etudiant => etudiant.Adresse.Province)
                .FirstOrDefault();

            // retourner un erreur si l'étudiant n'existe pas
            if (etudiant == null) return NotFound();

            GestionProfilVM vm = etudiant.GetEtudiantProfilVM(_context);

            vm.EtudiantId = Id;

            return PartialView("Views/Shared/_EtudiantPartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult ModifierEtudiant([FromBody] GestionProfilVM vm)
        {
            ModelState.Remove(nameof(vm.ProgrammeEtudes));
            ModelState.Remove(nameof(vm.Provinces));
            ModelState.Remove(nameof(vm.checkBoxCours));

            if (vm.CodePostal != null)
            {
                vm.CodePostal = vm.CodePostal.ToUpper();
            }

            vm.ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom));
            vm.Provinces = new SelectList(_context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom));

            if (ModelState.IsValid)
            {
                Etudiant? etudiant = _userManagerEtudiant.Users
                    .Where(etudiant => etudiant.Id == vm.EtudiantId)
                    .Include(etudiant => etudiant.ProgrammeEtude)
                    .Include(etudiant => etudiant.Adresse.Province)
                    .FirstOrDefault();

                // retourner un erreur si l'étudiant n'existe pas
                if (etudiant == null) return NotFound();

                Adresse adresse = etudiant.GetAdresse(_context);

                etudiant.ModelBinding(adresse, vm);

                _context.SaveChanges();

                vm.NomProgrammeEtude = etudiant.ProgrammeEtude.Nom;
                vm.NomProvince = etudiant.Adresse.Province.Nom;

                return Json(vm);
            }
            else
            {

                return PartialView("/Views/Shared/_EtudiantPartial.cshtml", vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SupprimerEtudiantAsync([FromBody] string Id)
        {
            Etudiant? etudiant = _userManagerEtudiant.Users
                    .Where(etudiant => etudiant.Id == Id)
                    .Include(etudiant => etudiant.ProgrammeEtude)
                    .Include(etudiant => etudiant.Adresse.Province)
                    .FirstOrDefault();
            if (etudiant == null)
            {
                return NotFound();
            }

            await _userManagerEtudiant.DeleteAsync(etudiant);

            return Ok();
        }

        //------------------Livres------------------

        [HttpGet]
        public IActionResult Livres()
        {
            List<LivreBibliotheque> livres = _context.LivresBibliotheque
                .Include(livre => livre.MaisonEdition)
                .ToList();

            return PartialView("~/Views/TableauDeBord/Livres.cshtml", livres);
        }

        [HttpGet]
        public IActionResult CreerLivre()
        {
            AssocierLivreCours vm = new AssocierLivreCours
            {
                Auteurs = ListDropDown.ListDropDownAuteurs(_context),
                MaisonsDeditions = ListDropDown.ListDropDownMaisonDedition(_context),
                checkBoxCours = CheckedBox.GetCours(_context)
            };
            return PartialView("Views/Shared/_AjouterLivrePartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult CreerLivre([FromBody] AssocierLivreCours vm)
        {
            ModelState.Remove(nameof(vm.Auteurs));
            ModelState.Remove(nameof(vm.MaisonsDeditions));
            ModelState.Remove(nameof(vm.checkBoxCours));
            ModelState.Remove(nameof(vm.Id));

            if (ModelState.IsValid)
            {
                LivreBibliotheque nouveauLivreBibliothèque = new LivreBibliotheque()
                {
                    LivreId = 0,
                    MaisonEditionId = (int)vm.MaisonDeditionId,
                    Isbn = vm.ISBN,
                    Titre = vm.Titre,
                    Resume = vm.Resume,
                    PhotoCouverture = vm.Photo,
                    DatePublication = vm.DatePublication,
                };

                _context.LivresBibliotheque.Add(nouveauLivreBibliothèque);
                _context.SaveChanges();

                List<Cours> coursBD = _context.Cours.ToList();
                foreach (int coursId in vm.Cours)
                {
                    Cours idCoursRechercher = coursBD.Find(x => x.CoursId == coursId);

                    CoursLivre nouvelleAssociation = new()
                    {
                        CoursLivreId = 0,
                        CoursId = idCoursRechercher.CoursId,
                        LivreBibliothequeId = nouveauLivreBibliothèque.LivreId,
                    };

                    _context.CoursLivres.Add(nouvelleAssociation);
                    _context.SaveChanges();
                }

                AuteurLivre auteurLivre = new AuteurLivre()
                {
                    AuteurId = (int)vm.AuteurId,
                    LivreBibliothequeId = nouveauLivreBibliothèque.LivreId,
                };
                _context.AuteursLivres.Add(auteurLivre);
                _context.SaveChanges();

                _context.PrixEtatsLivres.AddRange(GestionPrix.AssocierPrixEtat(nouveauLivreBibliothèque, vm, _context));
                _context.SaveChanges();
                vm.Id = nouveauLivreBibliothèque.LivreId;
                return Json(vm);
            }
            vm.Auteurs = ListDropDown.ListDropDownAuteurs(_context);
            vm.MaisonsDeditions = ListDropDown.ListDropDownMaisonDedition(_context);
            vm.checkBoxCours = CheckedBox.GetCours(_context);
            return PartialView("Views/Shared/_AjouterLivrePartial.cshtml", vm);
        }

        [HttpGet]
        public IActionResult ModifierLivre(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LivreBibliotheque livreBibliothequeRechercher = _livresBibliothequeDAO.Get(id);

            if (livreBibliothequeRechercher == null)
            {
                return NotFound();
            }

            AuteurLivre auteurLivre = _context.AuteursLivres
                .ToList()
                .Find(x => x.LivreBibliothequeId == livreBibliothequeRechercher.LivreId);
            List<PrixEtatLivre> prixEtatLivre = _context.PrixEtatsLivres
                .ToList()
                .FindAll(x => x.LivreBibliothequeId == livreBibliothequeRechercher.LivreId);


            ModificationLivreVM vm = new()
            {
                IdDuLivre = livreBibliothequeRechercher.LivreId,
                AuteurId = auteurLivre.AuteurId,
                MaisonDeditionId = livreBibliothequeRechercher.MaisonEditionId,
                Auteurs = ListDropDown.ListDropDownAuteurs(_context),
                MaisonsDeditions = ListDropDown.ListDropDownMaisonDedition(_context),
                DatePublication = livreBibliothequeRechercher.DatePublication,
                ISBN = livreBibliothequeRechercher.Isbn,
                Titre = livreBibliothequeRechercher.Titre,
                Resume = livreBibliothequeRechercher.Resume,
                Photo = livreBibliothequeRechercher.PhotoCouverture,
                PossedeNeuf = true,
                PossedeNumerique = true,
                PossedeUsagee = true,
                checkBoxCours = CheckedBox.GetCoursLivre(_context, livreBibliothequeRechercher),

            };

            var prixNeuf = prixEtatLivre.Find(x => x.EtatLivre == EtatLivreEnum.NEUF);
            var prixNumerique = prixEtatLivre.Find(x => x.EtatLivre == EtatLivreEnum.NUMERIQUE);
            var prixUsage = prixEtatLivre.Find(x => x.EtatLivre == EtatLivreEnum.USAGE);

            if (prixNeuf != null) { vm.PrixNeuf = prixNeuf.Prix; } else { vm.PrixNeuf = 0; vm.PossedeNeuf = false; };
            if (prixNumerique != null) { vm.PrixNumerique = prixNumerique.Prix; } else { vm.PrixNumerique = 0; vm.PossedeNumerique = false; };
            if (prixUsage != null) { vm.PrixUsage = prixUsage.Prix; vm.QuantiteUsagee = prixUsage.QuantiteUsage; } else { vm.PrixUsage = 0; vm.QuantiteUsagee = 0; vm.PossedeUsagee = false; };


            return PartialView("Views/Shared/_ModifierLivrePartial.cshtml", vm);
        }

        [HttpPost]
        public async Task<ActionResult> ModifierLivre([FromBody] ModifierLivreCours form)
        {
            ModelState.Remove(nameof(form.Auteurs));
            ModelState.Remove(nameof(form.MaisonsDeditions));
            ModelState.Remove(nameof(form.checkBoxCours));
            if (form == null)
            {
                ModificationLivreVM Emptyform = new();
                return PartialView("Views/Shared/_ModifierLivrePartial.cshtml", Emptyform);
            };

            LivreBibliotheque LivreBibliothèqueModifier = _livresBibliothequeDAO.Get(form.IdDuLivre);
            if (ModelState.IsValid)
            {

                LivreBibliothèqueModifier.MaisonEditionId = (int)form.MaisonDeditionId;
                LivreBibliothèqueModifier.Isbn = form.ISBN;
                LivreBibliothèqueModifier.Titre = form.Titre;
                LivreBibliothèqueModifier.Resume = form.Resume;
                LivreBibliothèqueModifier.PhotoCouverture = form.Photo;
                LivreBibliothèqueModifier.DatePublication = form.DatePublication;

                _context.LivresBibliotheque.Update(LivreBibliothèqueModifier);
                _context.SaveChanges();

                AuteurLivre auteurLivre = _context.AuteursLivres.Where(x => x.LivreBibliothequeId == form.IdDuLivre).FirstOrDefault();
                if (auteurLivre != null && auteurLivre.AuteurId != form.AuteurId)
                {
                    _context.AuteursLivres.Remove(auteurLivre);
                    _context.SaveChanges();

                    AuteurLivre nouveauAuteurLivre = new()
                    {
                        AuteurId = (int)form.AuteurId,
                        LivreBibliothequeId = LivreBibliothèqueModifier.LivreId
                    };
                    _context.AuteursLivres.Add(nouveauAuteurLivre);
                    _context.SaveChanges();
                }

                GestionPrix.UpdateLesPrix(LivreBibliothèqueModifier, form, _context);
                return Json(form);
            }

            form.Auteurs = ListDropDown.ListDropDownAuteurs(_context);
            form.MaisonsDeditions = ListDropDown.ListDropDownMaisonDedition(_context);
            form.checkBoxCours = CheckedBox.GetCoursLivre(_context, LivreBibliothèqueModifier);
            return PartialView("Views/Shared/_ModifierLivrePartial.cshtml", form);
        }

        [HttpPost]
        public IActionResult SupprimerLivre([FromBody] int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livreSupprimer = _livresBibliothequeDAO.Get(id);
            if (livreSupprimer == null)
            {
                return NotFound();
            };

            List<CoursLivre> ListCoursRelier = _context.CoursLivres.ToList().FindAll(x => x.LivreBibliothequeId == livreSupprimer.LivreId);
            _context.CoursLivres.RemoveRange(ListCoursRelier);
            _context.LivresBibliotheque.Remove(livreSupprimer);
            _context.SaveChanges();

            return Ok();
        }

        //------------------Programmes d'études------------------

        [HttpGet]
        public IActionResult ProgrammesEtudes()
        {
            List<ProgrammeEtude> programmesEtudes = _context.ProgrammesEtudes.ToList();

            return PartialView("~/Views/TableauDeBord/ProgrammesEtudes.cshtml", programmesEtudes);
        }

        [HttpGet]
        public IActionResult CreerProgrammeEtudes()
        {
            GestionProgrammeEtudesVM vm = new();

            return PartialView("Views/Shared/_ProgrameEtudePartial.cshtml", vm);
        }
        [HttpPost]
        public IActionResult CreerProgrammeEtudes([FromBody] GestionProgrammeEtudesVM vm)
        {
            ModelState.Remove(nameof(vm.Id));
            if (ModelState.IsValid)
            {
                ProgrammeEtude nouveauProgramme = new()
                {
                    ProgrammeEtudeId = 0,
                    Nom = vm.Nom,
                    Code = vm.Code
                };
                _context.ProgrammesEtudes.Add(nouveauProgramme);
                _context.SaveChanges();
                vm.Id = nouveauProgramme.ProgrammeEtudeId;
                return Json(vm);
            }

            return PartialView("Views/Shared/_ProgrameEtudePartial.cshtml", vm);
        }

        [HttpGet]
        public IActionResult ModifierProgrammeEtudes(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProgrammeEtude programme = _context.ProgrammesEtudes.Where(x => x.ProgrammeEtudeId == id).FirstOrDefault();
            if (programme == null)
            {
                return NotFound();
            }
            GestionProgrammeEtudesVM vm = new()
            {
                ProgrammeEtudeId = programme.ProgrammeEtudeId,
                Nom = programme.Nom,
                Code = programme.Code
            };

            return PartialView("Views/Shared/_ProgrameEtudePartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult ModifierProgrammeEtudes([FromBody] GestionProgrammeEtudesVM vm)
        {
            ModelState.Remove(nameof(GestionProgrammeEtudesVM.ProgrammeEtudeId));
            ModelState.Remove(nameof(GestionProgrammeEtudesVM.Id));
            if (ModelState.IsValid)
            {
                ProgrammeEtude modifierProgramme = _context.ProgrammesEtudes.Where(x => x.ProgrammeEtudeId == vm.ProgrammeEtudeId).FirstOrDefault();
                if (modifierProgramme != null)
                {
                    modifierProgramme.Nom = vm.Nom;
                    modifierProgramme.Code = vm.Code;
                    _context.ProgrammesEtudes.Update(modifierProgramme);
                    _context.SaveChanges();
                    vm.Id = modifierProgramme.ProgrammeEtudeId;
                    return Json(vm);
                }
            }
            return PartialView("Views/Shared/_ProgrameEtudePartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult SupprimerProgrammeEtudes([FromBody] int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmeEtudeSupprimer = _context.ProgrammesEtudes.Where(x => x.ProgrammeEtudeId == id).FirstOrDefault();
            if (programmeEtudeSupprimer == null)
            {
                return NotFound();
            };

            _context.ProgrammesEtudes.Remove(programmeEtudeSupprimer);
            _context.SaveChanges();
            return Ok();
        }

        //------------------Promotions------------------

        [HttpGet]
        public IActionResult Promotions()
        {
            List<Evenement> evenements = _context.Evenements
                .Include(commande => commande.Commanditaire)
                .ToList();

            return PartialView("~/Views/TableauDeBord/Promotions.cshtml", evenements);
        }

        [HttpGet]
        public IActionResult CreerPromotions()
        {
            GestionPromotionVM vm = new();
            return PartialView("Views/Shared/_PromotionPartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult CreerPromotions([FromBody] GestionPromotionVM vm)
        {
            if (!DateEvenement.CompareDate(vm.Debut, vm.Fin))
            {
                ModelState.AddModelError(string.Empty, "La date de début doit être avant la date de fin");
            }
            ModelState.Remove(nameof(vm.Id));
            if (ModelState.IsValid)
            {
                Commanditaire commanditaire = new()
                {
                    CommanditaireId = 0,
                    Nom = vm.CommanditaireNom,
                    Courriel = vm.CommanditaireCourriel,
                    Message = vm.CommanditaireMessage,
                    Url = vm.Url
                };
                _context.Commanditaires.Add(commanditaire);
                _context.SaveChanges();

                Evenement nouveauEvenement = new()
                {
                    EvenementId = vm.EvenementId,
                    Commanditaire = commanditaire,
                    CommanditaireId = vm.CommanditaireId,
                    Debut = vm.Debut,
                    Fin = vm.Fin,
                    Image = vm.Photo,
                    Nom = vm.Nom,
                    Description = vm.Description,
                };
                _context.Evenements.Add(nouveauEvenement);
                _context.SaveChanges();
                vm.Id = nouveauEvenement.EvenementId;
                return Json(vm);

            }
            return PartialView("Views/Shared/_PromotionPartial.cshtml", vm);
        }

        [HttpGet]
        public IActionResult ModifierPromotions(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Evenement evenement = _context.Evenements
                .Include(x => x.Commanditaire)
                .Where(x => x.EvenementId == id)
                .FirstOrDefault();
            if (evenement == null)
            {
                return NotFound();
            }
            GestionPromotionVM vm = new()
            {
                EvenementId = evenement.EvenementId,
                Nom = evenement.Nom,
                Debut = evenement.Debut,
                Fin = evenement.Fin,
                Description = evenement.Description,
                Photo = evenement.Image,
                CommanditaireId = evenement.CommanditaireId,
                CommanditaireNom = evenement.Commanditaire.Nom,
                CommanditaireCourriel = evenement.Commanditaire.Courriel,
                Url = evenement.Commanditaire.Url,
                CommanditaireMessage = evenement.Commanditaire.Message

            };

            return PartialView("Views/Shared/_PromotionPartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult ModifierPromotions([FromBody] GestionPromotionVM vm)
        {
            ModelState.Remove(nameof(GestionPromotionVM.Id));
            if (ModelState.IsValid)
            {
                Evenement modifierEvenement = _context.Evenements
                    .Include(x => x.Commanditaire)
                    .Where(x => x.EvenementId == vm.EvenementId)
                    .FirstOrDefault();
                if (modifierEvenement != null)
                {
                    modifierEvenement.EvenementId = vm.EvenementId;
                    modifierEvenement.Nom = vm.Nom;
                    modifierEvenement.Debut = vm.Debut;
                    modifierEvenement.Fin = vm.Fin;
                    modifierEvenement.Description = vm.Description;
                    modifierEvenement.Image = vm.Photo;
                    modifierEvenement.CommanditaireId = vm.CommanditaireId;
                    modifierEvenement.Commanditaire.Nom = vm.CommanditaireNom;
                    modifierEvenement.Commanditaire.Courriel = vm.CommanditaireCourriel;
                    modifierEvenement.Commanditaire.Url = vm.Url;
                    modifierEvenement.Commanditaire.Message = vm.CommanditaireMessage;
                    _context.Evenements.Update(modifierEvenement);
                    _context.SaveChanges();
                    vm.Id = modifierEvenement.EvenementId;
                    return Json(vm);
                }
            }
            return PartialView("Views/Shared/_PromotionPartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult supprimerPromotion([FromBody] int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotionSupprimer = _context.Evenements.Where(x => x.EvenementId == id).FirstOrDefault();
            if (promotionSupprimer == null)
            {
                return NotFound();
            };

            _context.Evenements.Remove(promotionSupprimer);
            _context.SaveChanges();
            return Ok();
        }


        //------------------Commandes------------------

        [HttpGet]
        public IActionResult Commandes()
        {
            List<FactureEtudiant> commandes = _context.FacturesEtudiants
                .Include(commande => commande.Etudiant)
                .ToList();

            return PartialView("~/Views/TableauDeBord/Commandes.cshtml", commandes);
        }

        [HttpGet]
        public IActionResult CreerCommandes()
        {
            GestionCommandeVM vm = new();
            vm.listEtatLivre = ListDropDown.ListDropDownEtatsLivre();
            return PartialView("Views/Shared/_CommandePartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult CreerCommandes([FromBody] GestionCommandeVM vm)
        {
            ModelState.Remove(nameof(vm.Id));
            ModelState.Remove(nameof(vm.FactureEtudiantId));
            if (ModelState.IsValid)
            {
                Etudiant etudiant = _context.Etudiants.Where(x => x.Id == vm.EtudiantId).FirstOrDefault();
                FactureEtudiant facture = new()
                {
                    FactureEtudiantId = 0,
                    PaymentIntentId = "Test-PaymentIntentId",
                    ClientSecret = "Test-ClientSecret",
                    EtudiantId = etudiant.Id,
                    Etudiant = etudiant,
                    AdresseLivraison = "123 balker",
                    DateFacturation = DateTime.Now,
                    Statut = StatusFacture.TRANSIT,
                    Tps = (decimal)Taxes.TPS,
                    Tvq = (decimal)Taxes.TVQ
                };
                _context.FacturesEtudiants.Add(facture);
                _context.SaveChanges();

                vm.Id = facture.FactureEtudiantId;
                return Json(vm);

            }
            vm.listEtatLivre = ListDropDown.ListDropDownEtatsLivre();
            return PartialView("Views/Shared/_CommandePartial.cshtml", vm);
        }

        //[HttpGet]
        //public IActionResult ModifierCommandes(int id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    Evenement evenement = _context.Evenements
        //        .Include(x => x.Commanditaire)
        //        .Where(x => x.EvenementId == id)
        //        .FirstOrDefault();
        //    if (evenement == null)
        //    {
        //        return NotFound();
        //    }
        //    GestionPromotionVM vm = new()
        //    {
        //        EvenementId = evenement.EvenementId,
        //        Nom = evenement.Nom,
        //        Debut = evenement.Debut,
        //        Fin = evenement.Fin,
        //        Description = evenement.Description,
        //        Photo = evenement.Image,
        //        CommanditaireId = evenement.CommanditaireId,
        //        CommanditaireNom = evenement.Commanditaire.Nom,
        //        CommanditaireCourriel = evenement.Commanditaire.Courriel,
        //        Url = evenement.Commanditaire.Url,
        //        CommanditaireMessage = evenement.Commanditaire.Message

        //    };

        //    return PartialView("Views/Shared/_PromotionPartial.cshtml", vm);
        //}

        //[HttpPost]
        //public IActionResult ModifierCommandes([FromBody] GestionCommandeVM vm)
        //{
        //    ModelState.Remove(nameof(GestionPromotionVM.Id));
        //    if (ModelState.IsValid)
        //    {
        //        Evenement modifierEvenement = _context.Evenements
        //            .Include(x => x.Commanditaire)
        //            .Where(x => x.EvenementId == vm.EvenementId)
        //            .FirstOrDefault();
        //        if (modifierEvenement != null)
        //        {
        //            modifierEvenement.EvenementId = vm.EvenementId;
        //            modifierEvenement.Nom = vm.Nom;
        //            modifierEvenement.Debut = vm.Debut;
        //            modifierEvenement.Fin = vm.Fin;
        //            modifierEvenement.Description = vm.Description;
        //            modifierEvenement.Image = vm.Photo;
        //            modifierEvenement.CommanditaireId = vm.CommanditaireId;
        //            modifierEvenement.Commanditaire.Nom = vm.CommanditaireNom;
        //            modifierEvenement.Commanditaire.Courriel = vm.CommanditaireCourriel;
        //            modifierEvenement.Commanditaire.Url = vm.Url;
        //            modifierEvenement.Commanditaire.Message = vm.CommanditaireMessage;
        //            _context.Evenements.Update(modifierEvenement);
        //            _context.SaveChanges();
        //            vm.Id = modifierEvenement.EvenementId;
        //            return Json(vm);
        //        }
        //    }
        //    return PartialView("Views/Shared/_PromotionPartial.cshtml", vm);
        //}

        public IActionResult ModifierListeLivres(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CommandeEtudiant commande = _context.CommandesEtudiants
                .Include(x => x.PrixEtatLivre.LivreBibliotheque)
                .Where(x => x.FactureEtudiantId == id)
                .FirstOrDefault();
            if (commande == null)
            {
                return NotFound();
            }

            List<LivreBibliotheque> list = new()
            {
                _context.LivresBibliotheque.Include(x => x.MaisonEdition).ToList().FirstOrDefault()
            };
            FactureLivreVM factureLivreVM = new()
            {
                Livres = list,
                checkBoxLivres = CheckedBox.GetLivres(_livresBibliothequeDAO)
            };
            return PartialView("Views/Shared/_LivresFactureEtudiantPartial.cshtml", factureLivreVM);

        }

        [HttpPost]
        public IActionResult supprimerCommande([FromBody] int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeSupprimer = _context.CommandesEtudiants.Where(x => x.FactureEtudiantId == id).FirstOrDefault();
            if (commandeSupprimer == null)
            {
                return NotFound();
            };

            _context.CommandesEtudiants.Remove(commandeSupprimer);
            _context.SaveChanges();
            return Ok();
        }

    }
}
