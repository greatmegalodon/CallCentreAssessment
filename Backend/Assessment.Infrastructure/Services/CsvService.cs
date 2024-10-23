using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Assessment.Infrastructure.Services.Interfaces;

namespace Assessment.Infrastructure.Services;

public class CsvService : ICsvService, IDisposable
{
    private MemoryStream? _memoryStream;
    private StreamWriter? _streamWriter;
    private CsvWriter? _csvWriter;
    
    private StreamReader? _streamReader;
    private CsvReader? _csvReader;

    public async Task<MemoryStream> ExportToCsvAsync<TRow>(IEnumerable<TRow>? rows = null)
    {
        _memoryStream = new MemoryStream();
        _streamWriter = new StreamWriter(_memoryStream, new UTF8Encoding(true));
        _csvWriter = new CsvWriter(_streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            TrimOptions = TrimOptions.None,
            HasHeaderRecord = true
        });
        
        _csvWriter.WriteHeader<TRow>();
        await _csvWriter.NextRecordAsync();

        if (rows != null)
            await _csvWriter.WriteRecordsAsync(rows);
        
        await _streamWriter.FlushAsync();
        _memoryStream.Seek(0, SeekOrigin.Begin);

        return _memoryStream;
    }
    
    public async Task<IEnumerable<TRow>> ImportCsvAsync<TRow>(Stream importStream)
    {
        _streamReader = new StreamReader(importStream, new UTF8Encoding(true));
        _csvReader = new CsvReader(_streamReader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            TrimOptions = TrimOptions.Trim,
            HasHeaderRecord = true,
        });

        var rows = _csvReader.GetRecords<TRow>();
        
        return rows;
    }

    public void Dispose()
    {
        _csvWriter?.Dispose();
        _csvReader?.Dispose();
        _streamWriter?.Dispose();
        _streamReader?.Dispose();
        _memoryStream?.Dispose();
    }
}