using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Extensions;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using Microsoft.AspNetCore.Hosting;
using vlissides_bibliotheque.Extensions.Interface;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.DAO.Interface;
using AutoMapper;
using vlissides_bibliotheque.DTO.Ajax;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using vlissides_bibliotheque.Commun;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize(Roles = Constante.Admin)]
    public class TableauDeBordController : Controller
    {
        private readonly SignInManager<Etudiant> _signInManager;
        private readonly UserManager<Utilisateur> _userManager;
        private readonly UserManager<Etudiant> _userManagerEtudiant;
        private readonly ApplicationDbContext _context;
        private readonly IDAO<LivreBibliotheque> _livreDAO;
        private readonly IDAO<Cours> _CoursDAO;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ICheckedBox _CheckedBox;
        private readonly IDropDownList _dropDownList;
        private readonly IUtilisateur _utilisateur;
        private readonly IMapper _mapper;

        public TableauDeBordController(
            SignInManager<Etudiant> signInManager,
            UserManager<Utilisateur> userManager,
            UserManager<Etudiant> userManagerEtudiant,
            ApplicationDbContext context,
            IDAO<LivreBibliotheque> livreDAO,
            IDAO<Cours> coursDAO,
            IWebHostEnvironment hostingEnvironment,
            ICheckedBox CheckedBox,
            IDropDownList dropDownList,
            IUtilisateur utilisateur,
            IMapper mapper
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userManagerEtudiant = userManagerEtudiant;
            _context = context;
            _livreDAO = livreDAO;
            _CoursDAO = coursDAO;
            _hostingEnvironment = hostingEnvironment;
            _CheckedBox = CheckedBox;
            _dropDownList = dropDownList;
            _utilisateur = utilisateur;
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
                    vm.NomProvince = _context.Provinces.Single(p => p.ProvinceId == vm.ProvinceId).Nom;

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
            Etudiant etudiant = _context.Etudiants
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
                Etudiant etudiant = _userManagerEtudiant.Users
                    .Where(etudiant => etudiant.Id == vm.EtudiantId)
                    .Include(etudiant => etudiant.ProgrammeEtude)
                    .Include(etudiant => etudiant.Adresse.Province)
                    .SingleOrDefault();

                // retourner un erreur si l'étudiant n'existe pas
                if (etudiant == null) return NotFound();

                Adresse adresse = _utilisateur.GetAdresse(etudiant);

                _utilisateur.ModelBinding(etudiant, adresse, vm);

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
            Etudiant etudiant = _userManagerEtudiant.Users
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
            List<LivreBibliothequeDto> livres = _mapper.Map<List<LivreBibliothequeDto>>(_context.LivresBibliotheque
                .Include(livre => livre.MaisonEdition)
                .ToList());

            return PartialView("~/Views/TableauDeBord/Livres.cshtml", livres);
        }

        [HttpGet]
        public IActionResult CreerLivre()
        {
            AjoutEditLivreVM vm = new AjoutEditLivreVM
            {
                MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition(),
                CheckBoxCours = _CheckedBox.GetCours(),
                CheckBoxAuteurs = _CheckedBox.GetAuteurs()
            };
            return PartialView("Views/Shared/_AjoutEditLivrePartial.cshtml", vm);
        }

        [HttpPost]
        public ActionResult CreerLivre([FromBody] AjoutEditLivreVM vm)
        {
            ModelState.Remove(nameof(vm.MaisonsDeditions));
            ModelState.Remove(nameof(vm.CheckBoxCours));
            ModelState.Remove(nameof(vm.CheckBoxAuteurs));
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

                _context.SaveChanges();
                vm.Id = nouveauLivreBibliothèque.LivreId;
                vm.DateFormater = vm.DatePublication.ToString("dd MMMM yyyy");
                return Json(vm);
            }
            vm.CheckBoxAuteurs = _CheckedBox.GetAuteurs();
            vm.MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition();
            vm.CheckBoxCours = _CheckedBox.GetCours();
            return PartialView("Views/Shared/_AjoutEditLivrePartial.cshtml", vm);
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

            AjoutEditLivreVM vm = new()
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
                CheckBoxCours = _CheckedBox.GetCoursLivre(livreBibliothequeRechercher),
                CheckBoxAuteurs = _CheckedBox.GetAuteursLivre(livreBibliothequeRechercher)

            };

            return PartialView("Views/Shared/_AjoutEditLivrePartial.cshtml", vm);
        }

        [HttpPost]
        public ActionResult ModifierLivre([FromBody] ModifierLivreCours form)
        {
            ModelState.Remove(nameof(form.MaisonsDeditions));
            ModelState.Remove(nameof(form.CheckBoxCours));
            ModelState.Remove(nameof(form.DateFormater));
            if (form == null)
            {
                AjoutEditLivreVM Emptyform = new();
                return PartialView("Views/Shared/_AjoutEditLivrePartial.cshtml", Emptyform);
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


                form.DateFormater = form.DatePublication.ToString("dd MMMM yyyy");
                return Json(form);
            }

            form.MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition();
            form.CheckBoxCours = _CheckedBox.GetCoursLivre(LivreBibliothèqueModifier);
            form.CheckBoxAuteurs = _CheckedBox.GetAuteursLivre(LivreBibliothèqueModifier);
            return PartialView("Views/Shared/_AjoutEditLivrePartial.cshtml", form);
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
            List<CoursDto> cours = _mapper.Map<List<CoursDto>>(_CoursDAO.GetAll()).ToList();
            return PartialView("~/Views/TableauDeBord/Cours.cshtml", cours);
        }

        [HttpGet]
        public IActionResult CreerCours()
        {
            AjoutEditCoursVM cours = new();
            cours.ProgrammesEtude = _dropDownList.ListDropDownProgrammesEtude();
            return PartialView("Views/Shared/_CoursPartial.cshtml", cours);
        }
        [HttpPost]
        public IActionResult CreerCours([FromBody] AjoutEditCoursVM vm)
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

            AjoutEditCoursVM vm = new()
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
        public IActionResult ModifierCours([FromBody] AjoutEditCoursVM vm)
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
            AjoutEditProgrammeEtudesVM vm = new();

            return PartialView("Views/Shared/_ProgrameEtudePartial.cshtml", vm);
        }
        [HttpPost]
        public IActionResult CreerProgrammeEtudes([FromBody] AjoutEditProgrammeEtudesVM vm)
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
            AjoutEditProgrammeEtudesVM vm = new()
            {
                ProgrammeEtudeId = programme.ProgrammeEtudeId,
                Nom = programme.Nom,
                Code = programme.Code
            };

            return PartialView("Views/Shared/_ProgrameEtudePartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult ModifierProgrammeEtudes([FromBody] AjoutEditProgrammeEtudesVM vm)
        {
            ModelState.Remove(nameof(AjoutEditProgrammeEtudesVM.ProgrammeEtudeId));
            ModelState.Remove(nameof(AjoutEditProgrammeEtudesVM.Id));
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
            AjoutEditPromotionVM vm = new();
            return PartialView("Views/Shared/_PromotionPartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult CreerPromotions([FromBody] AjoutEditPromotionVM vm)
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
            AjoutEditPromotionVM vm = new()
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
        public IActionResult ModifierPromotions([FromBody] AjoutEditPromotionVM vm)
        {
            ModelState.Remove(nameof(AjoutEditPromotionVM.Id));
            ModelState.Remove(nameof(AjoutEditPromotionVM.dateDebutFormatter));
            ModelState.Remove(nameof(AjoutEditPromotionVM.dateFinFormatter));
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
    }
}
