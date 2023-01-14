namespace FacileBudget.Controllers;

public class SpeseController : Controller
{
    private readonly ISpeseService spesaService;
    public SpeseController(ISpeseService spesaService)
    {
        this.spesaService = spesaService;
    }

    public async Task<IActionResult> Index(SpeseListInputModel input)
    {
        ViewData["Title"] = "Elenco delle spese";
        ListViewModel<SpeseViewModel> spese = await spesaService.GetSpeseAsync(input);

        SpeseListViewModel viewModel = new SpeseListViewModel
        {
            Spese = spese,
            Input = input
        };

        return View(viewModel);
    }
    public IActionResult Create()
    {
        ViewData["Title"] = "Nuova spesa";
        var inputModel = new SpeseCreateInputModel();
        return View(inputModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SpeseCreateInputModel inputModel)
    {
        if (ModelState.IsValid)
        {
            bool spesa = await spesaService.CreateSpesaAsync(inputModel);
            if (spesa == true)
            {
                TempData["ConfirmationMessage"] = "Ok! La tua spesa è stata aggiunta!";
                return RedirectToAction("Index", "Spese");
            }
        }
        ViewData["Title"] = "Nuova spesa";
        return View(inputModel);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int IdSpesa)
    {
        bool spesa = await spesaService.DeleteSpesaAsync(IdSpesa);
        if (spesa == true)
        {
            TempData["ConfirmationMessage"] = "Ok! La tua spesa è stata cancellata!";
            return RedirectToAction("Index", "Spese");
        }
        else
        {
            TempData["WarningMessage"] = "Attenzione! La tua spesa non è stata cancellata!";
            return RedirectToAction("Index", "Spese");
        }
    }

    [HttpPost]
    public async Task<FileResult> ExportDatiMese(string mese, string anno)
    {
        DataSet dsSpese = await spesaService.ExportCsvMese(mese, anno);
        StringBuilder sb = new StringBuilder();

        string SelMese = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(mese)).ToString();
        string FileName = "Report_spese_" + SelMese + ".csv";

        sb.AppendLine("Descrizione" + ";" + "Importo");

        foreach (DataRow dr in dsSpese.Tables[0].Rows)
        {
            sb.AppendLine(dr["Descrizione"].ToString() + ";" + dr["Importo"].ToString().Replace(".", ","));
        }
        return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", FileName);
    }
}