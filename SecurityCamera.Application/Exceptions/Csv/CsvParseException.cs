namespace SecurityCamera.Application.Exceptions.Csv;

public class CsvParseException : Exception
{
    public CsvParseException(string message, Exception? inner = null)
        : base(message, inner)
    {
    }
}
