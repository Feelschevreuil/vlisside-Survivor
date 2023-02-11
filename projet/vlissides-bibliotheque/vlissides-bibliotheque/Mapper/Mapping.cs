using Exercice_Ajax.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using vlissides_bibliotheque.Controllers;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using AutoMapper;
using vlissides_bibliotheque.ViewModels;
using System.Security.Cryptography.X509Certificates;

namespace vlissides_bibliotheque.MethodeGlobal
{
    public class Mapping : Profile
    {

        public Mapping()
        {
            
          // Evenement Evenement = (Evenement)CreateMap<Evenement, EvenementVM>().ReverseMap();
        }
    }
}
