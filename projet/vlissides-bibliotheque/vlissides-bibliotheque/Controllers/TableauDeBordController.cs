﻿using Microsoft.AspNetCore.Authorization;
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
using Stripe;
using System.Formats.Asn1;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using Microsoft.Extensions.Hosting.Internal;
using vlissides_bibliotheque.Extentions;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;
using vlissides_bibliotheque.Services;
using vlissides_bibliotheque.Extensions.Interface;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.DAO.Interface;
using AutoMapper;
using vlissides_bibliotheque.DTO.Ajax;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize(Roles = RolesName.Admin)]
    public class TableauDeBordController : Controller
    {
        private readonly SignInManager<Etudiant> _signInManager;
        private readonly UserManager<Utilisateur> _userManager;
        private readonly UserManager<Etudiant> _userManagerEtudiant;
        private readonly ApplicationDbContext _context;
        private readonly IDAO<LivreBibliotheque> _livreDAO;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ICheckedBox _CheckedBox;
        private readonly IDropDownList _dropDownList;
        private readonly IUtilisateur _utilisateur;
        private readonly IPrix _prix;
        private readonly IMapper _mapper;

        public TableauDeBordController(
            SignInManager<Etudiant> signInManager,
            UserManager<Utilisateur> userManager,
            UserManager<Etudiant> userManagerEtudiant,
            ApplicationDbContext context,
            IDAO<LivreBibliotheque> livreDAO,
            IWebHostEnvironment hostingEnvironment,
            ICheckedBox CheckedBox,
            IDropDownList dropDownList,
            IUtilisateur utilisateur,
            IPrix prix,
            IMapper mapper
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userManagerEtudiant = userManagerEtudiant;
            _context = context;
            _livreDAO = livreDAO;
            _hostingEnvironment = hostingEnvironment;
            _CheckedBox = CheckedBox;
            _dropDownList = dropDownList;
            _utilisateur = utilisateur;
            _prix = prix;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<EtudiantDto> etudiants = _userManagerEtudiant.Users
               .Include(etudiant => etudiant.ProgrammeEtude)
               .Include(etudiant => etudiant.Adresse.Province)
               .Select(e => _mapper.Map<EtudiantDto>(e))
               .ToList();
            return View(etudiants);
        }

        //------------------Étudiants------------------

        [HttpGet]
        public IActionResult Etudiants()
        {
            List<EtudiantDto> etudiants = _userManagerEtudiant.Users
                .Include(etudiant => etudiant.ProgrammeEtude)
                .Include(etudiant => etudiant.Adresse.Province)
                .Select(e => _mapper.Map<EtudiantDto>(e))
                .ToList();

            return PartialView("~/Views/TableauDeBord/Etudiants.cshtml", etudiants);
        }

        [HttpGet]
        public IActionResult CreerEtudiant()
        {
            return PartialView("Views/Shared/_EtudiantPartial.cshtml", _utilisateur.NewGestionProfilVM());
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

                var result = await _userManagerEtudiant.CreateAsync(etudiant, "Jaimelaprog1!");

                if (result.Succeeded)
                {
                    await _userManagerEtudiant.AddToRoleAsync(etudiant, "Etudiant");
                    vm.EtudiantId = etudiant.Id;
                    vm.NomProgrammeEtude = _context.ProgrammesEtudes.Single(p => p.ProgrammeEtudeId == vm.ProgrammeEtudeId).Nom;
                    vm.NomProvince = _context.Provinces.Single(p=> p.ProvinceId == vm.ProvinceId).Nom;

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
            Etudiant? etudiant = _context.Etudiants
                .Where(etudiant => etudiant.Id == Id)
                .Include(etudiant => etudiant.ProgrammeEtude)
                .Include(etudiant => etudiant.Adresse.Province)
                .SingleOrDefault();

            // retourner un erreur si l'étudiant n'existe pas
            if (etudiant == null)
            {
                return NotFound();
            }
            GestionProfilVM vm = _utilisateur.GetEtudiantProfilVM(etudiant);

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
                    .SingleOrDefault();

                // retourner un erreur si l'étudiant n'existe pas
                if (etudiant == null) return NotFound();

                Adresse adresse = _utilisateur.GetAdresse(etudiant);

                _utilisateur.ModelBinding(etudiant,adresse, vm);

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
                MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition(),
                checkBoxCours = _CheckedBox.GetCours(),
                checkBoxAuteurs = _CheckedBox.GetAuteurs()
            };
            return PartialView("Views/Shared/_AjouterLivrePartial.cshtml", vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreerLivre([FromBody] AssocierLivreCours vm)
        {
            ModelState.Remove(nameof(vm.MaisonsDeditions));
            ModelState.Remove(nameof(vm.checkBoxCours));
            ModelState.Remove(nameof(vm.checkBoxAuteurs));
            ModelState.Remove(nameof(vm.Id));
            ModelState.Remove(nameof(vm.DateFormater));

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

                List<CoursLivre> nouveauCoursLivre = vm.Cours.Select(c => new CoursLivre()
                {
                    CoursLivreId = 0,
                    CoursId = c,
                    LivreBibliothequeId = nouveauLivreBibliothèque.LivreId,
                }).ToList();

                if (nouveauCoursLivre.Any())
                {
                    _context.CoursLivres.AddRange(nouveauCoursLivre);
                    _context.SaveChanges();
                }


                List<AuteurLivre> nouveauAuteurLivre = vm.Auteurs.Select(a => new AuteurLivre()
                {
                    AuteurId = a,
                    LivreBibliothequeId = nouveauLivreBibliothèque.LivreId,
                }).ToList();

                if (nouveauAuteurLivre.Any())
                {
                    _context.AuteursLivres.AddRange(nouveauAuteurLivre);
                    _context.SaveChanges();
                }

                await _context.PrixEtatsLivres.AddRangeAsync(_prix.AssocierPrixEtat(nouveauLivreBibliothèque, vm));
                _context.SaveChanges();
                vm.Id = nouveauLivreBibliothèque.LivreId;
                vm.DateFormater = vm.DatePublication.ToString("dd MMMM yyyy");
                return Json(vm);
            }
            vm.checkBoxAuteurs = _CheckedBox.GetAuteurs();
            vm.MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition();
            vm.checkBoxCours = _CheckedBox.GetCours();
            return PartialView("Views/Shared/_AjouterLivrePartial.cshtml", vm);
        }

        [HttpGet]
        public IActionResult ModifierLivre(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LivreBibliotheque livreBibliothequeRechercher = _livreDAO.GetById(id.Value);

            if (livreBibliothequeRechercher == null)
            {
                return NotFound();
            }

            List<PrixEtatLivre> prixEtatLivre = _context.PrixEtatsLivres
                .ToList()
                .FindAll(x => x.LivreBibliothequeId == livreBibliothequeRechercher.LivreId);


            ModificationLivreVM vm = new()
            {
                IdDuLivre = livreBibliothequeRechercher.LivreId,
                MaisonDeditionId = livreBibliothequeRechercher.MaisonEditionId,
                MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition(),
                DatePublication = livreBibliothequeRechercher.DatePublication,
                ISBN = livreBibliothequeRechercher.Isbn,
                Titre = livreBibliothequeRechercher.Titre,
                Resume = livreBibliothequeRechercher.Resume,
                Photo = livreBibliothequeRechercher.PhotoCouverture,
                PossedeNeuf = true,
                PossedeNumerique = true,
                PossedeUsagee = true,
                checkBoxCours = _CheckedBox.GetCoursLivre(livreBibliothequeRechercher),
                checkBoxAuteurs = _CheckedBox.GetAuteursLivre(livreBibliothequeRechercher)

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
            ModelState.Remove(nameof(form.MaisonsDeditions));
            ModelState.Remove(nameof(form.checkBoxCours));
            ModelState.Remove(nameof(form.DateFormater));
            if (form == null)
            {
                ModificationLivreVM Emptyform = new();
                return PartialView("Views/Shared/_ModifierLivrePartial.cshtml", Emptyform);
            };

            LivreBibliotheque LivreBibliothèqueModifier = _livreDAO.GetById(form.IdDuLivre);
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


                await _prix.UpdateLesPrix(LivreBibliothèqueModifier, form);
                form.DateFormater = form.DatePublication.ToString("dd MMMM yyyy");
                return Json(form);
            }

            form.MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition();
            form.checkBoxCours = _CheckedBox.GetCoursLivre(LivreBibliothèqueModifier);
            form.checkBoxAuteurs = _CheckedBox.GetAuteursLivre(LivreBibliothèqueModifier);
            return PartialView("Views/Shared/_ModifierLivrePartial.cshtml", form);
        }

        [HttpPost]
        public IActionResult SupprimerLivre([FromBody] int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var livreSupprimer = _livreDAO.GetById(id.Value);
            if (livreSupprimer == null)
            {
                return NotFound();
            };

            List<CoursLivre> ListCoursRelier = _context.CoursLivres.ToList().FindAll(x => x.LivreBibliothequeId == livreSupprimer.LivreId);
            List<AuteurLivre> ListAuteursRelier = _context.AuteursLivres.ToList().FindAll(x => x.LivreBibliothequeId == livreSupprimer.LivreId);
            _context.CoursLivres.RemoveRange(ListCoursRelier);
            _context.AuteursLivres.RemoveRange(ListAuteursRelier);
            _context.LivresBibliotheque.Remove(livreSupprimer);
            _context.SaveChanges();

            return Ok();
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
            cours.ProgrammesEtude = _dropDownList.ListDropDownProgrammesEtude();
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

            vm.ProgrammesEtude = _dropDownList.ListDropDownProgrammesEtude();
            return PartialView("Views/Shared/_CoursPartial.cshtml", vm);
        }
        [HttpGet]
        public IActionResult ModifierCours(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Cours coursRechercher = _context.Cours.Single(x => x.CoursId == id);
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
            vm.ProgrammesEtude = _dropDownList.ListDropDownProgrammesEtude();
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
                Cours coursRechercher = _context.Cours.Single(x => x.CoursId == vm.CoursId);

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

            vm.ProgrammesEtude = _dropDownList.ListDropDownProgrammesEtude();
            return PartialView("Views/Shared/_CoursPartial.cshtml", vm);

        }
        [HttpPost]
        public IActionResult SupprimerCours([FromBody] int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Cours supprimerCours = _context.Cours.Single(x => x.CoursId == id);
            if (supprimerCours == null)
            {
                return NotFound();
            }
            _context.Cours.Remove(supprimerCours);
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
        public IActionResult ModifierProgrammeEtudes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProgrammeEtude programme = _context.ProgrammesEtudes.Single(x => x.ProgrammeEtudeId == id);
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
                ProgrammeEtude modifierProgramme = _context.ProgrammesEtudes.Single(x => x.ProgrammeEtudeId == vm.ProgrammeEtudeId);
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
        public IActionResult SupprimerProgrammeEtudes([FromBody] int? id)
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
            List<Etudiant> bdListEtudiant = _context.Etudiants
                .Where(x => x.ProgrammeEtudeId == programmeEtudeSupprimer.ProgrammeEtudeId)
                .ToList();
            var programmeEtudeRandom = _context.ProgrammesEtudes.Where(x => x.ProgrammeEtudeId != id).First();
            foreach (Etudiant etudiant in bdListEtudiant)
            {
                etudiant.ProgrammeEtudeId = programmeEtudeRandom.ProgrammeEtudeId;
                _context.Etudiants.Update(etudiant);
                _context.SaveChanges();

            }
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
            ModelState.Remove(nameof(vm.dateDebutFormatter));
            ModelState.Remove(nameof(vm.dateFinFormatter));
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
                vm.dateDebutFormatter = vm.Debut.ToString("dd MMMM yyyy");
                vm.dateFinFormatter = vm.Fin.ToString("dd MMMM yyyy");
                return Json(vm);

            }
            return PartialView("Views/Shared/_PromotionPartial.cshtml", vm);
        }

        [HttpGet]
        public IActionResult ModifierPromotions(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Evenement evenement = _context.Evenements
                .Include(x => x.Commanditaire)
                .Single(x => x.EvenementId == id);

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
            ModelState.Remove(nameof(GestionPromotionVM.dateDebutFormatter));
            ModelState.Remove(nameof(GestionPromotionVM.dateFinFormatter));
            if (ModelState.IsValid)
            {
                Evenement modifierEvenement = _context.Evenements.Include(x => x.Commanditaire).Single(x => x.EvenementId == vm.EvenementId);
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
                    vm.dateDebutFormatter = vm.Debut.ToString("dd MMMM yyyy");
                    vm.dateFinFormatter = vm.Fin.ToString("dd MMMM yyyy");
                    return Json(vm);
                }
            }
            return PartialView("Views/Shared/_PromotionPartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult supprimerPromotion([FromBody] int? id)
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
        public IActionResult CreerCommande()
        {
            GestionCommandeVM vm = new();
            vm.listStatut = _dropDownList.ListDropDownStatutCommande();
            vm.listEtudiant = _dropDownList.ListDropDownEtudiant();
            return PartialView("Views/Shared/_CommandePartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult CreerCommande([FromBody] GestionCommandeVM vm)
        {
            ModelState.Remove(nameof(vm.FactureEtudiantId));
            ModelState.Remove(nameof(vm.listEtudiant));
            ModelState.Remove(nameof(vm.listStatut));
            ModelState.Remove(nameof(vm.NomStatut));
            ModelState.Remove(nameof(vm.formaterDateFacturation));
            if (ModelState.IsValid)
            {
                Etudiant etudiant = _context.Etudiants.Single(x => x.Id == vm.EtudiantId);
                FactureEtudiant facture = new()
                {
                    FactureEtudiantId = 0,
                    PaymentIntentId = vm.PaymentIntentId,
                    ClientSecret = "Test-ClientSecret",
                    EtudiantId = etudiant.Id,
                    Etudiant = etudiant,
                    DateFacturation = vm.DateFacturation,
                    Statut = (StatutFactureEnum)vm.ValeurEnumStatut,
                    Tps = (decimal)Taxes.TPS,
                    Tvq = (decimal)Taxes.TVQ
                };
                _context.FacturesEtudiants.Add(facture);
                _context.SaveChanges();
                vm.FactureEtudiantId = facture.FactureEtudiantId;
                vm.NomStatut = Enum.GetName(typeof(StatutFactureEnum), vm.Statut);
                vm.formaterDateFacturation = vm.DateFacturation.ToString("dd MMMM yyyy");
                return Json(vm);

            }
            vm.listStatut = _dropDownList.ListDropDownStatutCommande();
            vm.listEtudiant = _dropDownList.ListDropDownEtudiant();
            return PartialView("Views/Shared/_CommandePartial.cshtml", vm);
        }

        [HttpGet]
        public IActionResult ModifierCommande(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FactureEtudiant facture = _context.FacturesEtudiants.Include(x => x.Etudiant).Single(x => x.FactureEtudiantId == id);
            if (facture == null)
            {
                return NotFound();
            }
            GestionCommandeVM vm = new()
            {
                FactureEtudiantId = facture.FactureEtudiantId,
                PaymentIntentId = facture.PaymentIntentId,
                EtudiantId = facture.EtudiantId,
                DateFacturation = facture.DateFacturation,
                ValeurEnumStatut = (int)facture.Statut
            };

            vm.listStatut = _dropDownList.ListDropDownStatutCommande();
            vm.listEtudiant = _dropDownList.ListDropDownEtudiant();
            return PartialView("Views/Shared/_CommandePartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult ModifierCommande([FromBody] GestionCommandeVM vm)
        {
            ModelState.Remove(nameof(vm.FactureEtudiantId));
            ModelState.Remove(nameof(vm.listEtudiant));
            ModelState.Remove(nameof(vm.listStatut));
            ModelState.Remove(nameof(vm.NomStatut));
            ModelState.Remove(nameof(vm.formaterDateFacturation));
            if (ModelState.IsValid)
            {
                FactureEtudiant facture = _context.FacturesEtudiants.Include(x => x.Etudiant).Single(x => x.FactureEtudiantId == vm.FactureEtudiantId);
                Etudiant etudiant = _context.Etudiants.Single(x => x.Id == vm.EtudiantId);

                facture.PaymentIntentId = vm.PaymentIntentId;
                facture.EtudiantId = etudiant.Id;
                facture.Etudiant = etudiant;
                facture.DateFacturation = vm.DateFacturation;
                facture.Statut = (StatutFactureEnum)vm.ValeurEnumStatut;

                _context.FacturesEtudiants.Update(facture);
                _context.SaveChanges();
                vm.FactureEtudiantId = facture.FactureEtudiantId;
                vm.NomStatut = Enum.GetName(typeof(StatutFactureEnum), vm.ValeurEnumStatut);
                vm.formaterDateFacturation = vm.DateFacturation.ToString("dd MMMM yyyy");
                return Json(vm);

            }
            vm.listStatut = _dropDownList.ListDropDownStatutCommande();
            vm.listEtudiant = _dropDownList.ListDropDownEtudiant();
            return PartialView("Views/Shared/_CommandePartial.cshtml", vm);
        }

        public IActionResult AfficherListeCommandes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FactureCommandeVM vm = new()
            {
                CommandesEtudiant = _context.CommandesEtudiants
                .Include(x => x.PrixEtatLivre.LivreBibliotheque)
                .Where(x => x.FactureEtudiantId == id)
                .ToList()
            };
            return PartialView("Views/Shared/_CommandeFactureEtudiantPartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult supprimerCommande([FromBody] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeSupprimer = _context.FacturesEtudiants.Where(x => x.FactureEtudiantId == id).FirstOrDefault();
            if (commandeSupprimer == null)
            {
                return NotFound();
            };

            _context.FacturesEtudiants.Remove(commandeSupprimer);
            _context.SaveChanges();
            return Ok();
        }

        public async Task<IActionResult> csvToListEtudiant()
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "csv", "liste-etudiants.csv");
            string[] readText = System.IO.File.ReadAllLines(path);
            List<string> csvSansCharaSpecial = new();
            foreach (string line in readText)
            {
                string newline = Regex.Replace(line, "[�]+", "é");
                newline = newline.Replace(",", "");
                csvSansCharaSpecial.Add(newline);
            }
            List<CsvEtudiantVM> csvEnVm = csvSansCharaSpecial
               .Skip(1)
               .Where(l => l.Length > 1)
               .CsvEnEtudiantVm()
               .ToList();
            List<Etudiant> csvEnEtudiants = GetEtudiantsFromCSV(csvEnVm);
            List<Etudiant> etudiantsBD = _context.Etudiants.ToList();
            List<Etudiant> etudiantsAjoutes = new();

            foreach (Etudiant etudiant in csvEnEtudiants)
            {
                if (etudiantsBD.Find(x => x.Id == etudiant.Id) == null)
                {
                    var result = await _userManagerEtudiant.CreateAsync(etudiant, etudiant.PasswordHash);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(etudiant, RolesName.Etudiant);
                        etudiantsAjoutes.Add(etudiant);
                    }
                }
            }
            return View("SuccesEtudiantCsv", etudiantsAjoutes);
        }

        public List<Etudiant> GetEtudiantsFromCSV(List<CsvEtudiantVM> list)
        {
            string numeroCivique = "";
            string rue = "";
            string ville = "";
            string nomDeRue = "";
            int provinceIdParDefaut = _context.Provinces.First().ProvinceId;
            List<ProgrammeEtude> programmes = _context.ProgrammesEtudes.ToList();
            List<Etudiant> etudiants = new();

            foreach (CsvEtudiantVM vm in list)
            {
                string app = "";
                ProgrammeEtude programmeEtude = programmes.Where(x => x.Nom.ToLower() == vm.ProgrammeEtude.ToLower()).Single();
                List<string> contenuAdresse = vm.Adresse.Trim().Split(" ").ToList();
                numeroCivique = contenuAdresse[0];
                contenuAdresse.RemoveAt(0);
                ville = contenuAdresse[contenuAdresse.Count() - 1];
                contenuAdresse.RemoveAt(contenuAdresse.Count() - 1);


                if (contenuAdresse[contenuAdresse.Count() - 1].Contains("#"))
                {
                    app = contenuAdresse[contenuAdresse.Count() - 1];
                    contenuAdresse.RemoveAt(contenuAdresse.Count() - 1);
                    nomDeRue = string.Join(" ", contenuAdresse);
                    rue = nomDeRue;
                }
                else
                {
                    nomDeRue = string.Join(" ", contenuAdresse);
                    rue = nomDeRue;
                }

                Adresse adresse = new()
                {
                    AdresseId = 0,
                    App = app,
                    CodePostal = "",
                    NumeroCivique = Convert.ToInt32(numeroCivique),
                    Rue = rue.Trim(),
                    Ville = ville.Trim(),
                    ProvinceId = provinceIdParDefaut,
                };
                _context.Adresses.Add(adresse);
                _context.SaveChanges();

                Etudiant etudiant = new()
                {
                    Email = vm.Courriel.Trim(),
                    UserName = vm.Courriel.Trim(),
                    Nom = vm.Nom.Trim(),
                    Prenom = vm.Prenom.Trim(),
                    ProgrammeEtudeId = programmeEtude.ProgrammeEtudeId,
                    AdresseId = adresse.AdresseId,
                    Adresse = adresse,
                    EmailConfirmed = true,
                    PasswordHash = vm.MotDePasse.Trim(),
                    NumeroEtudiant = Convert.ToInt32(vm.Matricule)
                };
                etudiants.Add(etudiant);

            }
            return etudiants;
        }
    }
}
