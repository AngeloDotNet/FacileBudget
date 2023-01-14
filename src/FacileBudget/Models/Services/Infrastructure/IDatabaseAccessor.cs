using System;
using System.Data;
using System.Threading.Tasks;

namespace FacileBudget.Models.Services.Infrastructure
{
    public interface IDatabaseAccessor
    {
        Task<DataSet> QueryAsync(FormattableString query);
    }
}