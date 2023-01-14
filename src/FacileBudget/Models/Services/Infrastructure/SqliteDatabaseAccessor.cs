namespace FacileBudget.Models.Services.Infrastructure;

public class SqliteDatabaseAccessor : IDatabaseAccessor
{
    private readonly ILogger<SqliteDatabaseAccessor> logger;
    private readonly IOptionsMonitor<ConnectionStringsOptions> connectionStringOptions;

    public SqliteDatabaseAccessor(ILogger<SqliteDatabaseAccessor> logger, IOptionsMonitor<ConnectionStringsOptions> connectionStringOptions)
    {
        this.logger = logger;
        this.connectionStringOptions = connectionStringOptions;
    }
    public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
    {
        logger.LogInformation(formattableQuery.Format, formattableQuery.GetArguments());

        var queryArguments = formattableQuery.GetArguments();
        var sqliteParameters = new List<SqliteParameter>();
        for (var i = 0; i < queryArguments.Length; i++)
        {
            if (queryArguments[i] is Sql)
            {
                continue;
            }
            var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
            sqliteParameters.Add(parameter);
            queryArguments[i] = "@" + i;
        }
        string query = formattableQuery.ToString();
        string connectionString = connectionStringOptions.CurrentValue.Default;

        using (var conn = new SqliteConnection(connectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new SqliteCommand(query, conn))
            {
                cmd.Parameters.AddRange(sqliteParameters);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var dataSet = new DataSet();

                    do
                    {
                        var dataTable = new DataTable();
                        dataSet.Tables.Add(dataTable);
                        dataTable.Load(reader);
                    } while (!reader.IsClosed);

                    return dataSet;
                }
            }
        }
    }
}