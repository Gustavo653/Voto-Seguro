using Serilog;
using System.Diagnostics;

namespace VotoSeguro.DTO.Base
{
    public class ResponseDTO
    {
        public int Code { get; set; } = 200;
        public string Message { get; set; } = "Operação concluída!";
        public DateTime Date { get; } = DateTime.Now;
        public object? Object { get; set; }
        public object? Error { get; set; }
        public Stopwatch Elapsed { get; } = Stopwatch.StartNew();

        public void SetError(Exception ex)
        {
            Code = 500;
            Message = "Ocorreu um erro inesperado!";
            Error = new { message = ex.Message, innerMessage = ex.InnerException?.Message, stackTrace = ex.StackTrace };
            Log.Error(ex, Message);
        }

        public void SetBadInput(string message)
        {
            Code = 400;
            Message = $"Verifique os registros enviados! Detalhes: {message}";
            Log.Warning(Message);
        }
    }
}
