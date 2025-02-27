using AutoMapper;
using AutoMapper.EquivalencyExpression;
using System.Linq;
using System.Text.Json;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Mapper
{
    public class Mapping : Profile
    {

        public Mapping()
        {

            CreateMap<Auteur, AuteurVM>()
                .EqualityComparison((dto, entity) => dto.AuteurId == entity.Id)
                .ForMember(a => a.Id, opt => opt.MapFrom(a => a.AuteurId));

            CreateMap<AuteurVM, Auteur>()
                .EqualityComparison((dto, entity) => dto.Id == entity.AuteurId)
                .ForMember(a => a.Livres, opt => opt.Ignore())
                .ForMember(a => a.AuteurId, opt => opt.MapFrom(a => a.Id));

            CreateMap<Commanditaire, CommanditaireDto>().EqualityComparison((dto, entity) => dto.CommanditaireId == entity.CommanditaireId).ReverseMap();
            
            CreateMap<Cours, CoursVM>()
                .ForMember(c => c.Id, opt => opt.MapFrom(c=> c.CoursId));

            CreateMap<CoursVM, Cours>()
                .ForMember(c => c.CoursId, opt => opt.MapFrom(c => c.Id));

            CreateMap<Evenement, EvenementVM>();


            CreateMap<EvenementVM, Evenement>()
                .ForMember(dest => dest.Commanditaire, opt => opt.Ignore());


            #region Etudiant

            CreateMap<Etudiant, EtudiantDto>()
                .ForMember(dest => dest.ProgrammeEtude, opt => opt.MapFrom(src => src.ProgrammeEtude.Nom))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Courriel, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.NumeroCivique, opt => opt.MapFrom(src => src.Adresse.NumeroCivique))
                .ForMember(dest => dest.Rue, opt => opt.MapFrom(src => src.Adresse.Rue))
                .ForMember(dest => dest.Ville, opt => opt.MapFrom(src => src.Adresse.Ville))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Adresse.Province.Nom))
                .ForMember(dest => dest.CodePostal, opt => opt.MapFrom(src => src.Adresse.CodePostal));

            #endregion


            #region Livre
            CreateMap<LivreBibliotheque, LivreBibliothequeDto>()
                .ForMember(dest => dest.Cours, opt => opt.MapFrom(src => src.Cours))
                .ForMember(dest => dest.MaisonEditionNom, opt => opt.MapFrom(src => src.MaisonEdition.Nom))
                .ForMember(dest => dest.Prix, opt => opt.MapFrom(src => JsonSerializer.Deserialize<PrixEtatLivreDto>(src.PrixJson, new JsonSerializerOptions())));

            CreateMap<LivreBibliotheque, AjoutEditLivreVM>()
                .ForMember(a => a.Id, opt => opt.MapFrom(l => l.LivreId))
                .ForMember(a => a.ISBN, opt => opt.MapFrom(l => l.Isbn))
                .ForMember(a => a.Prix, opt => opt.MapFrom(l => JsonSerializer.Deserialize<PrixEtatLivreDto>(l.PrixJson, new JsonSerializerOptions())))
                .ForMember(a => a.Photo, opt => opt.MapFrom(l => l.PhotoCouverture));

            CreateMap<AjoutEditLivreVM, LivreBibliotheque>()
                .EqualityComparison((dto, entity) => dto.Id == entity.LivreId)
                .ForMember(a => a.LivreId, opt => opt.MapFrom(l => l.Id))
                .ForMember(a => a.Isbn, opt => opt.MapFrom(l => l.ISBN))
                .ForMember(a => a.Auteurs, opt => opt.MapFrom(l => l.Auteurs))
                //.ForMember(a => a.Auteurs, opt => opt.Ignore())
                .ForMember(a => a.MaisonEdition, opt => opt.Ignore())
                .ForMember(a => a.PrixJson, opt => opt.MapFrom(l => JsonSerializer.Serialize(l.Prix, new JsonSerializerOptions())))
                .ForMember(a => a.PhotoCouverture, opt => opt.MapFrom(l => l.Photo));
            #endregion

            #region Evenement
            CreateMap<Evenement, EvenementDto>()
              .ForMember(dest => dest.CommanditaireNom, opt => opt.MapFrom(src => src.Commanditaire.Nom))
              .ForMember(dest => dest.CommanditaireMessage, opt => opt.MapFrom(src => src.Commanditaire.Message))
              .ForMember(dest => dest.CommanditaireCourriel, opt => opt.MapFrom(src => src.Commanditaire.Courriel));
            #endregion

            #region Cours
            CreateMap<Cours, CoursVM>()
              .ForMember(dest => dest.ProgrammeEtudeNom, opt => opt.MapFrom(src => src.ProgrammeEtude.Nom));
            #endregion

            #region LivreEtudiant
            CreateMap<LivreEtudiant, LivreEtudiantDto>()
                .ForMember(dest => dest.EtudiantEmail, opt => opt.MapFrom(scr => scr.Etudiant.Email))
                .ForMember(dest => dest.EtudiantId, opt => opt.MapFrom(scr => scr.Etudiant.Id));

            CreateMap<LivreEtudiant, AjoutEditLivreEtudiantVM>()
                .ForMember(dest => dest.EtudiantEmail, opt => opt.MapFrom(scr => scr.Etudiant.Email))
                .ForMember(dest => dest.EtudiantId, opt => opt.MapFrom(scr => scr.Etudiant.Id));
            #endregion

            #region Tuiles

            CreateMap<LivreBibliotheque, TuileLivreBibliotequeVM>()
                .ForMember(t => t.LivreBibliotheque, opt => opt.MapFrom(l => l))
                .ForMember(t => t.ProgrammeEtudeNom, opt => opt.MapFrom(l => l.Cours.Any() ? l.Cours.First().Nom : string.Empty))
                .ForMember(t => t.Quantite, opt => opt.MapFrom(l => JsonSerializer.Deserialize<PrixEtatLivreDto>(l.PrixJson, new JsonSerializerOptions()).QuantiteUsage))
                .ForMember(t => t.Auteurs, opt => opt.MapFrom(l => l.Auteurs.Select(a => a.NomComplet)));

            CreateMap<LivreBibliotheque, LivreDetailVM>()
                .ForMember(t => t.LivreBibliotheque, opt => opt.MapFrom(l => l))
                .ForMember(t => t.ProgrammeEtudeNom, opt => opt.MapFrom(l => l.Cours.Any() ? l.Cours.First().Nom : string.Empty))
                .ForMember(t => t.Quantite, opt => opt.MapFrom(l => JsonSerializer.Deserialize<PrixEtatLivreDto>(l.PrixJson, new JsonSerializerOptions()).QuantiteUsage))
                .ForMember(t => t.Auteurs, opt => opt.MapFrom(l => l.Auteurs.Select(a => a.NomComplet)));


            #endregion
        }
    }
}
