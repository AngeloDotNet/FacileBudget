namespace FacileBudget.Models.Services.Application;

public interface ISpeseService
{
    Task<ListViewModel<SpeseViewModel>> GetSpeseAsync(SpeseListInputModel model);
    Task<bool> CreateSpesaAsync(SpeseCreateInputModel inputModel);
    Task<bool> DeleteSpesaAsync(int IdSpesa);
    Task<DataSet> ExportCsvMese(string mese, string anno);
}