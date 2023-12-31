﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{
    public class AjoutEditCoursVM : Cours
    {
        public List<SelectListItem> ProgrammesEtude { get; set; }
        [DisplayName("Programmes d'étude")]
        public int ProgrammesEtudeId { get; set; }

        public int Id { get; set; }
    }
}
