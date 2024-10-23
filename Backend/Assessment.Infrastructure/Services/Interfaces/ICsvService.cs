namespace Assessment.Infrastructure.Services.Interfaces;

public interface ICsvService
{
    Task<MemoryStream> ExportToCsvAsync<TRow>(IEnumerable<TRow>? rows = null);
    Task<IEnumerable<TRow>> ImportCsvAsync<TRow>(Stream importStream);
}