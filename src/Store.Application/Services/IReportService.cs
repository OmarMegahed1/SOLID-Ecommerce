using Store.Application.Models;
using Store.Common.Results;

public interface IReportService
{
    Task<Result<IEnumerable<OrderReport>>> GetOrderReportAsync(
        DateTime from, 
        DateTime to, 
        ReportInterval reportInterval, 
        CancellationToken cancellationToken);
}