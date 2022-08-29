using Newtonsoft.Json;

namespace BookingHero.UseCase.Execution
{
    public class ExecutionLog
    {
        public ExecutionLog(string message, DateTime loggedAt, ExecutionLogType logType)
        {
            Message = message;
            LoggedAt = loggedAt;
            LogType = logType;
        }

        public string Message { get; set; }
        public DateTime LoggedAt { get; set; }
        public ExecutionLogType LogType { get; set; }

        public override string? ToString()
        {
            return $"{LogType}: [{LoggedAt}] - {Message}";
        }

        public static ExecutionLog CreateInfoLog(string message)
        => new ExecutionLog(message, DateTime.UtcNow, ExecutionLogType.info);

        public static ExecutionLog CreateDebugLog(string message)
        => new ExecutionLog(message, DateTime.UtcNow, ExecutionLogType.debug);

        public static ExecutionLog CreateErrorLog(string message)
        => new ExecutionLog(message, DateTime.UtcNow, ExecutionLogType.error);

        public static ExecutionLog CreateWarningLog(string message)
        => new ExecutionLog(message, DateTime.UtcNow, ExecutionLogType.warning);
    }

    public class ExecutionLogList : List<ExecutionLog>
    {
        public new ExecutionLogList Add(ExecutionLog item)
        {
            base.Add(item);
            return this;
        }

        public ExecutionLogList AddInfo(string message) => this.Add(ExecutionLog.CreateInfoLog(message));
        public ExecutionLogList AddDebug(string message) => this.Add(ExecutionLog.CreateDebugLog(message));
        public ExecutionLogList AddError(string message) => this.Add(ExecutionLog.CreateErrorLog(message));
        public ExecutionLogList AddWarning(string message) => this.Add(ExecutionLog.CreateWarningLog(message));

        public override string? ToString() => JsonConvert.SerializeObject(this);
    }
}
