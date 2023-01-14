namespace FacileBudget.Models.Services.Infrastructure;

public interface IDatabaseAccessor
{
    Task<DataSet> QueryAsync(FormattableString query);
}