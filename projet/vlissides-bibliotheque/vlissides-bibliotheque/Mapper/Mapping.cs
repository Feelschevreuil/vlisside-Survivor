using vlissides_bibliotheque.Models;
using AutoMapper;
using vlissides_bibliotheque.ViewModels;
using vlissides_bibliotheque.DTO;

namespace vlissides_bibliotheque.Mapper
{
    public class Mapping : Profile
    {

        public Mapping()
        {

            CreateMap<Evenement, EvenementVM>().ReverseMap();

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
                .ForMember(dest => dest.MaisonEditionNom, opt => opt.MapFrom(src => src.MaisonEdition.Nom));

            #endregion

            #region Evenement
            CreateMap<Evenement, EvenementDto>()
              .ForMember(dest => dest.CommanditaireNom, opt => opt.MapFrom(src => src.Commanditaire.Nom))
              .ForMember(dest => dest.CommanditaireMessage, opt => opt.MapFrom(src => src.Commanditaire.Message))
              .ForMember(dest => dest.CommanditaireCourriel, opt => opt.MapFrom(src => src.Commanditaire.Courriel));
            #endregion

            #region Cours
            CreateMap<Cours, CoursDto>()
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
        }
    }
}
