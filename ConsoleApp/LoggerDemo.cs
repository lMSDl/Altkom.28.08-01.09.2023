using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    internal class LoggerDemo
    {
        private ILogger _logger;

        public LoggerDemo(ILogger<LoggerDemo> logger)
        {
            _logger = logger;
        }

        public void Work()
        {
            try
            {
                using (var scope2 = _logger.BeginScope(nameof(Work)))
                using (var scope3 = _logger.BeginScope("Ala ma kota"))
                using (var scope4 = _logger.BeginScope("my format {0}...", GetType().Name))
                using (var scope5 = _logger.BeginScope(new Dictionary<string, string>(){ { "a", "1" }, { "b", "2" } }))
                {

                    for (int i = 0; i < 10; i++)
                    {
                        using (var scope1 = _logger.BeginScope(i.ToString()))
                        {
                            try
                            {
                                _logger.LogInformation(i.ToString());

                                if (i == 5)
                                {
                                    throw new IndexOutOfRangeException();
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, i.ToString());
                            }

                            if (i == 9)
                                throw new Exception("Opss..");
                        }
                    }
                }
            }
            catch (Exception ex) when (LogError(ex))
            {

                _logger.LogError(ex, ex.Message);
            }
        }

        private bool LogError(Exception e)
        {
            _logger.LogError(e, e.Message);
            return true;
        }
    }
}
