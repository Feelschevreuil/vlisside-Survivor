using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
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
using System.Text.Json;
using static Humanizer.In;

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
        private readonly IDAO<ProgrammeEtude> _programmeEtudeDAO;
        private readonly IDAO<Evenement> _evenementDAO;
        private readonly IDAO<Province> _provinceDAO;
        private readonly IDAO<Professeur> _professeurDAO;
        private readonly IDAO<MaisonEdition> _maisonEditionDAO;
        private readonly IDAO<Commanditaire> _commanditaireDAO;
        private readonly IDAO<Adresse> _adresseDAO;
        private readonly IDAO<Auteur> _auteurDAO;
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
            IDAO<ProgrammeEtude> programmeEtudeDAO,
            IDAO<Evenement> evenementDAO,
            IDAO<Province> provinceDAO,
            IDAO<Professeur> professeurDAO,
            IDAO<MaisonEdition> maisonEditionDAO,
            IDAO<Commanditaire> commanditaireDAO,
            IDAO<Adresse> adresseDAO,
            IDAO<Auteur> auteurDAO,
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
            _programmeEtudeDAO = programmeEtudeDAO;
            _evenementDAO = evenementDAO;
            _provinceDAO = provinceDAO;
            _professeurDAO = professeurDAO;
            _maisonEditionDAO = maisonEditionDAO;
            _commanditaireDAO = commanditaireDAO;
            _adresseDAO = adresseDAO;
            _auteurDAO = auteurDAO;
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

            if (!ModelState.IsValid)
            {
                vm.ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom));
                vm.Provinces = new SelectList(_context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom));
                return PartialView("Views/Shared/_EtudiantPartial.cshtml", vm);
            }

            Adresse adresse = new()
            {
                App = vm.App,
                CodePostal = !string.IsNullOrEmpty(vm.CodePostal) ? vm.CodePostal.ToUpper() : vm.CodePostal,
                NumeroCivique = Convert.ToInt32(vm.NoCivique),
                Rue = vm.Rue.Trim(),
                Ville = vm.Ville.Trim(),
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

            if (!result.Succeeded)
            {
                var errorMessages = new Dictionary<string, string>
                {
                    { "PasswordTooShort", "Le mot de passe doit être d'au moins 6 caractères." },
                    { "PasswordRequiresNonAlphanumeric", "Le mot de passe doit avoir au moins un caractère spécial (Ex: !, $, %, ?, &, *, etc...)." },
                    { "PasswordRequiresLower", "Le mot de passe doit avoir au moins une lettre minuscule." },
                    { "PasswordRequiresUpper", "Le mot de passe doit avoir au moins une lettre majuscule." },
                    { "DuplicateUserName", "Le courriel que vous avez entré existe déjà." }
                };

                foreach (var error in result.Errors)
                {
                    if (errorMessages.TryGetValue(error.Code, out var errorMessage))
                        ModelState.AddModelError(string.Empty, errorMessage);
                    else
                        ModelState.AddModelError("Exception", "Erreur non gérér");
                }
                vm.ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom));
                vm.Provinces = new SelectList(_context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom));
                return PartialView("Views/Shared/_EtudiantPartial.cshtml", vm);
            }

            await _userManagerEtudiant.AddToRoleAsync(etudiant, Constante.Etudiant);
            vm.EtudiantId = etudiant.Id;
            vm.NomProgrammeEtude = _context.ProgrammesEtudes.Single(p => p.ProgrammeEtudeId == vm.ProgrammeEtudeId).Nom;
            vm.NomProvince = _context.Provinces.Single(p => p.ProvinceId == vm.ProvinceId).Nom;

            return Json(vm);
        }

        [HttpGet]
        public IActionResult ModifierEtudiant(string id)
        {
            Etudiant etudiant = _context.Etudiants.Include(e => e.ProgrammeEtude).Include(e => e.Adresse.Province).SingleOrDefault(e => e.Id == id);

            // retourner un erreur si l'étudiant n'existe pas
            if (etudiant == null)
                return NotFound();

            GestionProfilVM vm = _utilisateur.GetEtudiantProfilVM(etudiant);
            vm.EtudiantId = id;

            return PartialView("Views/Shared/_EtudiantPartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult ModifierEtudiant([FromBody] GestionProfilVM vm)
        {
            ModelState.Remove(nameof(vm.ProgrammeEtudes));
            ModelState.Remove(nameof(vm.Provinces));
            ModelState.Remove(nameof(vm.checkBoxCours));

            vm.ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom));
            vm.Provinces = new SelectList(_context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom));

            if (!ModelState.IsValid)
                return PartialView("/Views/Shared/_EtudiantPartial.cshtml", vm);

            Etudiant etudiant = _userManagerEtudiant.Users.Include(e => e.ProgrammeEtude).Include(e => e.Adresse.Province).SingleOrDefault(e => e.Id == vm.EtudiantId);

            // retourner un erreur si l'étudiant n'existe pas
            if (etudiant == null)
                return NotFound();

            if (vm.CodePostal != null)
                vm.CodePostal = vm.CodePostal.ToUpper();

            Adresse adresse = _utilisateur.GetAdresse(etudiant);

            _utilisateur.ModelBinding(etudiant, adresse, vm);

            _context.SaveChanges();

            vm.NomProgrammeEtude = etudiant.ProgrammeEtude.Nom;
            vm.NomProvince = etudiant.Adresse.Province.Nom;

            return Json(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SupprimerEtudiantAsync([FromBody] string id)
        {
            Etudiant etudiant = _userManagerEtudiant.Users.Include(etudiant => etudiant.ProgrammeEtude).Include(etudiant => etudiant.Adresse.Province)
                    .SingleOrDefault(e => e.Id == id);

            if (etudiant == null)
                return NotFound();

            await _userManagerEtudiant.DeleteAsync(etudiant);

            return Ok();
        }

        //------------------Livres------------------

        [HttpGet]
        public IActionResult Livres()
        {
            return PartialView("~/Views/TableauDeBord/Livres.cshtml", _mapper.Map<List<LivreBibliothequeDto>>(_livreDAO.GetAll().ToList()));
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

            if (!ModelState.IsValid)
            {
                vm.CheckBoxAuteurs = _CheckedBox.GetAuteurs();
                vm.MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition();
                vm.CheckBoxCours = _CheckedBox.GetCours();
                return PartialView("Views/Shared/_AjoutEditLivrePartial.cshtml", vm);
            }

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

        [HttpGet]
        public IActionResult ModifierLivre(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            LivreBibliotheque livreBibliothequeRechercher = _livreDAO.GetById(id.Value);

            if (livreBibliothequeRechercher == null)
                return NotFound();

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

            if (!ModelState.IsValid)
            {
                form.MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition();
                form.CheckBoxCours = _CheckedBox.GetCoursLivre(LivreBibliothèqueModifier);
                form.CheckBoxAuteurs = _CheckedBox.GetAuteursLivre(LivreBibliothèqueModifier);
                return PartialView("Views/Shared/_AjoutEditLivrePartial.cshtml", form);
            }

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

        [HttpPost]
        public IActionResult SupprimerLivre([FromBody] int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var livreSupprimer = _livreDAO.GetById(id.Value);

            if (livreSupprimer == null)
                return NotFound();

            List<CoursLivre> ListCoursRelier = _context.CoursLivres.Where(cl => cl.LivreBibliothequeId == livreSupprimer.LivreId).ToList();
            List<AuteurLivre> ListAuteursRelier = _context.AuteursLivres.Where(al => al.LivreBibliothequeId == livreSupprimer.LivreId).ToList();
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
            AjoutEditCoursVM cours = new()
            {
                ProgrammesEtude = _dropDownList.ListDropDownProgrammesEtude(),
            };
            return PartialView("Views/Shared/_CoursPartial.cshtml", cours);
        }
        [HttpPost]
        public IActionResult CreerCours([FromBody] AjoutEditCoursVM vm)
        {
            ModelState.Remove(nameof(vm.ProgrammeEtude));
            ModelState.Remove(nameof(vm.ProgrammesEtude));
            ModelState.Remove(nameof(vm.Id));

            if (!ModelState.IsValid)
            {
                vm.ProgrammesEtude = _dropDownList.ListDropDownProgrammesEtude();
                return PartialView("Views/Shared/_CoursPartial.cshtml", vm);
            }

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
        [HttpGet]
        public IActionResult ModifierCours(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            Cours coursRechercher = _CoursDAO.GetById(id.Value);

            if (coursRechercher == null)
                return NotFound();

            AjoutEditCoursVM vm = new()
            {
                CoursId = coursRechercher.CoursId,
                Nom = coursRechercher.Nom,
                Code = coursRechercher.Code,
                AnneeParcours = coursRechercher.AnneeParcours,
                Description = coursRechercher.Description,
                ProgrammesEtudeId = coursRechercher.ProgrammeEtudeId,
                ProgrammesEtude = _dropDownList.ListDropDownProgrammesEtude(),
            };

            return PartialView("Views/Shared/_CoursPartial.cshtml", vm);
        }
        [HttpPost]
        public IActionResult ModifierCours([FromBody] AjoutEditCoursVM vm)
        {
            ModelState.Remove(nameof(vm.ProgrammeEtude));
            ModelState.Remove(nameof(vm.ProgrammesEtude));
            ModelState.Remove(nameof(vm.Id));

            if (!ModelState.IsValid)
            {
                vm.ProgrammesEtude = _dropDownList.ListDropDownProgrammesEtude();
                return PartialView("Views/Shared/_CoursPartial.cshtml", vm);
            }

            Cours coursRechercher = _CoursDAO.GetById(vm.CoursId);

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
        [HttpPost]
        public IActionResult SupprimerCours([FromBody] int? id)
        {
            if (!id.HasValue || id == 0)
                return NotFound();

            Cours supprimerCours = _CoursDAO.GetById(id.Value);

            if (supprimerCours == null)
                return NotFound();

            _context.Cours.Remove(supprimerCours);
            _context.SaveChanges();
            return Ok();
        }


        //------------------Programmes d'études------------------

        [HttpGet]
        public IActionResult ProgrammesEtudes()
        {
            return PartialView("~/Views/TableauDeBord/ProgrammesEtudes.cshtml", _programmeEtudeDAO.GetAll().ToList());
        }

        [HttpGet]
        public IActionResult CreerProgrammeEtudes()
        {
            return PartialView("Views/Shared/_ProgrameEtudePartial.cshtml", new AjoutEditProgrammeEtudesVM());
        }
        [HttpPost]
        public IActionResult CreerProgrammeEtudes([FromBody] AjoutEditProgrammeEtudesVM vm)
        {
            ModelState.Remove(nameof(vm.Id));

            if (!ModelState.IsValid)
                return PartialView("Views/Shared/_ProgrameEtudePartial.cshtml", vm);

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

        [HttpGet]
        public IActionResult ModifierProgrammeEtudes(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            ProgrammeEtude programme = _programmeEtudeDAO.GetById(id.Value);

            if (programme == null)
                return NotFound();

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
            if (!ModelState.IsValid)
                return PartialView("Views/Shared/_ProgrameEtudePartial.cshtml", vm);

            ProgrammeEtude modifierProgramme = _programmeEtudeDAO.GetById(vm.ProgrammeEtudeId);

            if (modifierProgramme == null)
                return PartialView("Views/Shared/_ProgrameEtudePartial.cshtml", vm);

            modifierProgramme.Nom = vm.Nom;
            modifierProgramme.Code = vm.Code;
            _context.ProgrammesEtudes.Update(modifierProgramme);
            _context.SaveChanges();
            vm.Id = modifierProgramme.ProgrammeEtudeId;
            return Json(vm);
        }

        [HttpPost]
        public IActionResult SupprimerProgrammeEtudes([FromBody] int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var programmeEtudeSupprimer = _programmeEtudeDAO.GetById(id.Value);

            if (programmeEtudeSupprimer == null)
                return NotFound();

            List<Etudiant> bdListEtudiant = _context.Etudiants.Where(x => x.ProgrammeEtudeId == id.Value).ToList();

            _context.ProgrammesEtudes.Remove(programmeEtudeSupprimer);
            _context.SaveChanges();

            var programmeEtudeRandom = _context.ProgrammesEtudes.First(); //Avoir un programme quand des élèves font partie d'un programme qui est enlever ou le mettre nullable

            foreach (Etudiant etudiant in bdListEtudiant)
            {
                etudiant.ProgrammeEtudeId = programmeEtudeRandom.ProgrammeEtudeId;
                _context.Etudiants.Update(etudiant);
                _context.SaveChanges();
            }

            return Ok();
        }

        //------------------Promotions------------------

        [HttpGet]
        public IActionResult Promotions()
        {
            return PartialView("~/Views/TableauDeBord/Promotions.cshtml", _evenementDAO.GetAll().ToList());
        }

        [HttpGet]
        public IActionResult CreerPromotions()
        {
            return PartialView("Views/Shared/_PromotionPartial.cshtml", new AjoutEditPromotionVM());
        }

        [HttpPost]
        public IActionResult CreerPromotions([FromBody] AjoutEditPromotionVM vm)
        {
            if (vm.Debut.Date > vm.Fin.Date)
                ModelState.AddModelError(string.Empty, "La date de début doit être avant la date de fin");

            ModelState.Remove(nameof(vm.Id));
            ModelState.Remove(nameof(vm.dateDebutFormatter));
            ModelState.Remove(nameof(vm.dateFinFormatter));

            if (!ModelState.IsValid)
                return PartialView("Views/Shared/_PromotionPartial.cshtml", vm);

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

        [HttpGet]
        public IActionResult ModifierPromotions(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            Evenement evenement = _evenementDAO.GetById(id.Value);

            if (evenement == null)
                return NotFound();

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

            if (!ModelState.IsValid)
                return PartialView("Views/Shared/_PromotionPartial.cshtml", vm);

            Evenement modifierEvenement = _evenementDAO.GetById(vm.EvenementId);

            if (modifierEvenement == null)
                return PartialView("Views/Shared/_PromotionPartial.cshtml", vm);

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

        [HttpPost]
        public IActionResult SupprimerPromotion([FromBody] int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var promotionSupprimer = _evenementDAO.GetById(id.Value);

            if (promotionSupprimer == null)
                return NotFound();

            _context.Evenements.Remove(promotionSupprimer);
            _context.SaveChanges();
            return Ok();
        }


        //--------------------Remplir BD -----------------------------------
        [HttpGet]
        public IActionResult RemplirBd()
        {
            if (!_provinceDAO.GetAll().Any())
            {
                var province = new Province()
                {
                    ProvinceId = 0,
                    Nom = "Québec"
                };
                _provinceDAO.Insert(province);
                _provinceDAO.Save();
            }

            if (!_programmeEtudeDAO.GetAll().Any())
            {
                var programmeEtude = new ProgrammeEtude()
                {
                    ProgrammeEtudeId = 0,
                    Code = "01",
                    Nom = "Math"
                };
                _programmeEtudeDAO.Insert(programmeEtude);
                _programmeEtudeDAO.Save();
            }

            if (!_professeurDAO.GetAll().Any())
            {
                var professeur = new Professeur()
                {
                    ProfesseurId = 0,
                    Nom = "Noris",
                    Prenom = "Chuck",
                    NumeroProfesseur = 100
                };
            }

            if (!_maisonEditionDAO.GetAll().Any())
            {
                var maisonEdition = new MaisonEdition()
                {
                    MaisonEditionId = 0,
                    Nom = "Labonté"
                };
                _maisonEditionDAO.Insert(maisonEdition);
                _maisonEditionDAO.Save();
            }

            if (!_commanditaireDAO.GetAll().Any())
            {
                var commanditaire = new Commanditaire()
                {
                    CommanditaireId = 0,
                    Courriel = "pirate@hotmail.com",
                    Message = "Ceci est un message",
                    Nom = "Pirate Software",
                    Url = "https://www.twitch.tv/piratesoftware"
                };
                _commanditaireDAO.Insert(commanditaire);
                _commanditaireDAO.Save();
            }

            if (!_evenementDAO.GetAll().Any())
            {
                var evenement = new Evenement()
                {
                    EvenementId = 0,
                    CommanditaireId = _commanditaireDAO.GetAll().First().CommanditaireId,
                    Debut = DateTime.Now.AddYears(2),
                    Fin = DateTime.Now.AddYears(3),
                    Description = "Come play to the game fest",
                    Image = "",
                    Nom = "Games Fest"
                };
                _evenementDAO.Insert(evenement);
                _evenementDAO.Save();
            }

            if (!_adresseDAO.GetAll().Any())
            {
                var adresse = new Adresse()
                {
                    AdresseId = 0,
                    App = "",
                    CodePostal = "J2K2T5",
                    NumeroCivique = 1,
                    ProvinceId = _provinceDAO.GetAll().First().ProvinceId,
                    Rue = "Laroque",
                    Ville = "Longueil"
                };
                _adresseDAO.Insert(adresse);
                _adresseDAO.Save();
            }

            if (!_auteurDAO.GetAll().Any())
            {
                var auteur = new Auteur()
                {
                    AuteurId = 0,
                    Nom = "Write",
                    Prenom = "Mec"
                };
                _auteurDAO.Insert(auteur);
                _auteurDAO.Save();
            }

            if (!_CoursDAO.GetAll().Any())
            {
                var cours = new Cours()
                {
                    CoursId = 0,
                    Nom = "Science",
                    AnneeParcours = 1,
                    Code = "01",
                    Description = "On apprend des sciences ici",
                    ProgrammeEtudeId = _programmeEtudeDAO.GetAll().First().ProgrammeEtudeId,
                };
                _CoursDAO.Insert(cours);
                _CoursDAO.Save();
            }

            if (!_livreDAO.GetAll().Any())
            {
                var livre = new LivreBibliotheque()
                {
                    LivreId = 0,
                    MaisonEditionId = _maisonEditionDAO.GetAll().First().MaisonEditionId,
                    DatePublication = DateTime.Now,
                    Isbn = "0-9767736-6-X",
                    PhotoCouverture = "",
                    Resume = "A very good book resume",
                    Titre = "WallerFlower",
                    PrixJson = JsonSerializer.Serialize(new PrixEtatLivreDto() { PrixNeuf = 10.69m, PrixNumerique = 5 })
                };
                _livreDAO.Insert(livre);
                _livreDAO.Save();


                _livreDAO.Update(livre);
                _livreDAO.Save();
            }

            if (!_context.AuteursLivres.Any())
            {
                var livre = _livreDAO.GetAll().First();

                livre.Auteurs = new List<AuteurLivre>
                {
                    new AuteurLivre
                    {
                        AuteurId = _auteurDAO.GetAll().First().AuteurId,
                        LivreBibliothequeId = livre.LivreId
                    }
                };
                _livreDAO.Update(livre);
                _livreDAO.Save();
            }

            if (!_context.CoursLivres.Any())
            {
                var livre = _livreDAO.GetAll().First();

                livre.Cours = new List<CoursLivre>
                {
                    new CoursLivre
                    {
                        CoursId = _CoursDAO.GetAll().First().CoursId,
                        LivreBibliothequeId = livre.LivreId
                    }
                };
                _livreDAO.Update(livre);
                _livreDAO.Save();
            }

            return Ok();
        }
    }
}
