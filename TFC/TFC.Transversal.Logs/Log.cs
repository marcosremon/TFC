using NLog;

namespace TFC.Transversal.Logs
{

    public class Log
    {
        #region FileName
        private static string _fileName = "Atlas.txt";
        public static void SetLogFileName(string fileName)
        {
            if (_showLogInConsole) Console.WriteLine("FileName: " + fileName);
            _fileName = fileName;

            // Reinicio el Log para que pille el nuevo fichero
            _instance = null;
        }
        public static string GetLogFileName()
        {
            return _fileName;
        }
        #endregion

        #region ShowLogInConsole
        private static bool _showLogInConsole = false;
        public static void SetShowLogInConsole(bool showLogInConsole)
        {
            _showLogInConsole = showLogInConsole;
        }
        public static bool GetShowLogInConsole()
        {
            return _showLogInConsole;
        }
        #endregion

        #region Instance
        private static Log? _instance = null;
        public static Log Instance
        {
            get
            {
                if (_instance == null) _instance = new Log();

                return _instance;
            }
        }
        private static Logger? _logger;
        private Log()
        {
            try
            {
                var config = new NLog.Config.LoggingConfiguration();

                // Targets where to log to: File and Console
                var logfile = new NLog.Targets.FileTarget("logfile")
                {
                    FileName = _fileName,
                    ArchiveAboveSize = 100000000,
                    MaxArchiveFiles = 20,
                    ArchiveNumbering = NLog.Targets.ArchiveNumberingMode.Rolling,
                };

                // Rules for mapping loggers to targets            
                config.AddRule(LogLevel.Trace, LogLevel.Fatal, logfile);

                // Apply config           
                NLog.LogManager.Configuration = config;

                _logger = NLog.LogManager.GetLogger("logfile");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Unable to create the log file. ", ex.Message));
            }
        }
        #endregion

        #region Trace
        public void TraceBBDD(string message)
        {
            try
            {
                if (_showLogInConsole) Console.WriteLine(message);
                _logger?.Trace(message);
            }
            catch { }
        }

        public void Trace(string message, params object[] args)
        {
            try
            {
                if (_showLogInConsole) Console.WriteLine(message, args);
                _logger?.Trace(message, args);
            }
            catch { }
        }
        public void Trace(string message, Exception ex)
        {
            try
            {
                if (_showLogInConsole) Console.WriteLine(message, ex);
                _logger?.Trace(message, ex);
            }
            catch { }
        }
        #endregion

        #region Debug
        public void Debug(string message, params object[] args)
        {
            try
            {
                if (_showLogInConsole) Console.WriteLine(message, args);
                _logger?.Debug(message, args);
            }
            catch { }
        }
        public void Debug(string message, Exception ex)
        {
            try
            {
                if (_showLogInConsole) Console.WriteLine(message, ex);
                _logger?.Debug(message, ex);
            }
            catch { }
        }
        #endregion

        #region Info
        public void Info(string message, params object[] args)
        {
            try
            {
                if (_showLogInConsole) Console.WriteLine(message, args);
                _logger?.Info(message, args);
            }
            catch { }
        }
        public void Info(string message, Exception ex)
        {
            try
            {
                if (_showLogInConsole) Console.WriteLine(message, ex);
                _logger?.Info(message, ex);
            }
            catch { }
        }
        #endregion

        #region Warn
        public void Warn(string message, params object[] args)
        {
            try
            {
                if (_showLogInConsole) Console.WriteLine(message, args);
                _logger?.Warn(message, args);
            }
            catch { }
        }
        public void Warn(string message, Exception ex)
        {
            try
            {
                if (_showLogInConsole) Console.WriteLine(message, ex);
                _logger?.Warn(message, ex);
            }
            catch { }
        }
        #endregion

        #region Error
        public void Error(string message, params object[] args)
        {
            try
            {
                if (_showLogInConsole) Console.WriteLine(message, args);
                _logger?.Error(message, args);
            }
            catch { }
        }
        public void Error(string message, Exception ex)
        {
            try
            {
                if (_showLogInConsole) Console.WriteLine(message, ex);
                _logger?.Error(message, ex);
            }
            catch { }
        }
        #endregion

        #region Fatal
        public void Fatal(string message, params object[] args)
        {
            try
            {
                if (_showLogInConsole) Console.WriteLine(message, args);
                _logger?.Fatal(message, args);
            }
            catch { }
        }
        public void Fatal(string message, Exception ex)
        {
            try
            {
                if (_showLogInConsole) Console.WriteLine(message, ex);
                _logger?.Fatal(message, ex);
            }
            catch { }
        }
        #endregion

        #region IsDebugEnabled
        public bool IsDebugEnabled { get { return _logger == null ? false : _logger.IsDebugEnabled; } }
        #endregion

    }

}

