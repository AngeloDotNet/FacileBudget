namespace FacileBudget.Models.ViewModels;

public class SpeseViewModel
{
    public int IdSpesa { get; set; }
    public string Descrizione { get; set; }
    public string Importo { get; set; }
    public string Valuta { get; set; }
    public string Mese { get; set; }
    public string Anno { get; set; }

    public static SpeseViewModel FromDataRow(DataRow Row)
    {
        var speseViewModel = new SpeseViewModel
        {
            Descrizione = Convert.ToString(Row["Descrizione"]),
            Importo = Convert.ToString(Row["Importo"]),
            Valuta = Convert.ToString(Row["Valuta"]),
            Mese = Convert.ToString(Row["Mese"]),
            Anno = Convert.ToString(Row["Anno"]),
            IdSpesa = Convert.ToInt32(Row["IdSpesa"])
        };
        return speseViewModel;
    }
}