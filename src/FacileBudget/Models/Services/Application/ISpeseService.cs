using System.Data;
using System.Threading.Tasks;
using FacileBudget.Models.InputModels;
using FacileBudget.Models.ViewModels;

namespace FacileBudget.Models.Services.Application
{
    public interface ISpeseService
    {
        Task<ListViewModel<SpeseViewModel>> GetSpeseAsync(SpeseListInputModel model);
        Task<bool> CreateSpesaAsync(SpeseCreateInputModel inputModel);
        Task<bool> DeleteSpesaAsync(int IdSpesa);
        Task<DataSet> ExportCsvMese(string mese, string anno);
    }
}