namespace Apprendi.Web.Client.Services
{
    public interface ILocalTimeZoneService
    {   
        Task<LocalTimeZone> GetTimeZone();
    }

    public interface ILocalTimeZoneSetterService
    {
        void SetZoneInfo(string locale, string timeZone, int timeZoneOffset);
    }

    public record struct LocalTimeZone(string TimeZoneId, string Locale, int TimeZoneOffset);

    public class LocalTimeZoneService : ILocalTimeZoneSetterService, ILocalTimeZoneService, IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource = new();
        private readonly ManualResetEvent _manualReset = new(false);
        private string _timeZone;
        private string _locale;
        private int _timeZoneOffset;

        public void SetZoneInfo(string locale, string timeZone, int timeZoneOffset)
        {
            _locale = locale;
            _timeZone = timeZone;
            _timeZoneOffset = timeZoneOffset;
            _manualReset.Set();
        }

        public async Task<LocalTimeZone> GetTimeZone()
        {
            await Task.Run(_manualReset.WaitOne, _cancellationTokenSource.Token);
            
            return new LocalTimeZone(_timeZone, _locale, _timeZoneOffset);
        }

        void IDisposable.Dispose()
        {
            _cancellationTokenSource.Cancel();
            _manualReset.Dispose();
        }
    }
}
