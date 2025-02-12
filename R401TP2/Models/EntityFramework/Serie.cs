using System;
using System.Collections.Generic;

namespace R401TP2.Models.EntityFramework;

public partial class Serie
{
    public int Serieid { get; set; }

    public string Titre { get; set; } = null!;

    public string? Resume { get; set; }

    public int? Nbsaisons { get; set; }

    public int? Nbepisodes { get; set; }

    public int? Anneecreation { get; set; }

    public string? Network { get; set; }
}
