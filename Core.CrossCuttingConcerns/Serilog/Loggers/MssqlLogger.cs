using Core.CrossCuttingConcerns.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.Serilog.Loggers
{
    public class MssqlLogger : LoggerServiceBase
    {
        private readonly IConfiguration _configuration;

        public MssqlLogger(IConfiguration configuration)
        {
            _configuration = configuration;

            MssqlConfiguration logger =
                configuration.GetSection("SerilogConfigurations:MsSqlConfiguration").Get<MssqlConfiguration>() ?? throw new Exception(SerilogMessages.NullOptionsMessage);

            MSSqlServerSinkOptions sinkOptions = new()
            {
                TableName = logger.Table,
                AutoCreateSqlDatabase = logger.AutoCreateSqlTable,
            };

            ColumnOptions columnOptions = new();

            Logger = new LoggerConfiguration()
                .WriteTo
                .MSSqlServer(logger.ConnectionString, sinkOptions: sinkOptions, columnOptions: columnOptions)
                .CreateLogger();
        }
    }
}
