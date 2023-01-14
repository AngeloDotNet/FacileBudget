using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using FacileBudget.Models.InputModels;
using FacileBudget.Models.Options;

namespace FacileBudget.Customizations.ModelBinders
{
    public class SpeseListInputModelBinder : IModelBinder
    {
        private readonly IOptionsMonitor<SpeseOptions> speseOptions;
        public SpeseListInputModelBinder(IOptionsMonitor<SpeseOptions> speseOptions)
        {
            this.speseOptions = speseOptions;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            int.TryParse(bindingContext.ValueProvider.GetValue("Page").FirstValue, out int page);

            SpeseOptions options = speseOptions.CurrentValue;
            var inputModel = new SpeseListInputModel(page, options.PerPage);

            bindingContext.Result = ModelBindingResult.Success(inputModel);

            return Task.CompletedTask;
        }
    }
}