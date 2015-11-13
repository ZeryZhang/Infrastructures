using System;
namespace Hk.Infrastructures.Common.Utility
{
    internal class IdWorker
    {
        public const long Twepoch = 1288834974657L;
        //机器标识位数
        const int WorkerIdBits = 4;//5
        const int DatacenterIdBits = 4;//5
        //毫秒内自增位
        const int SequenceBits = 1; //12
        const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);
        const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);

        private const int WorkerIdShift = SequenceBits;
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);

        private long _sequence = 0L;
        private long _lastTimestamp = -1L;

        public IdWorker(long workerId, long datacenterId, long sequence = 0L)
        {
            WorkerId = workerId;
            DatacenterId = datacenterId;
            _sequence = sequence;

            // sanity check for workerId
            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException(String.Format("worker Id can't be greater than {0} or less than 0", MaxWorkerId));
            }

            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException(String.Format("datacenter Id can't be greater than {0} or less than 0", MaxDatacenterId));
            }	
        }

        public long WorkerId { get; protected set; }
        public long DatacenterId { get; protected set; }

        public long Sequence
        {
            get { return _sequence; }
            internal set { _sequence = value; }
        }

        readonly object _lock = new Object();

        public virtual long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();

                if (timestamp < _lastTimestamp)
                {
                    throw new InvalidSystemClock(String.Format(
                        "Clock moved backwards.  Refusing to generate id for {0} milliseconds", _lastTimestamp - timestamp));
                }

                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & SequenceMask;
                    if (_sequence == 0)
                    {
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0;
                }

                _lastTimestamp = timestamp;
                var id = ((timestamp - Twepoch) << TimestampLeftShift) |
                         (DatacenterId << DatacenterIdShift) |
                         (WorkerId << WorkerIdShift) | _sequence;

                return id;
            }
        }

        protected virtual long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        protected virtual long TimeGen()
        {
            return SystemFunction.CurrentTimeMillis();
        }
    }

    internal class DisposableAction : IDisposable
    {
        readonly Action _action;

        public DisposableAction(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            _action = action;
        }

        public void Dispose()
        {
            _action();
        }
    }

    internal class InvalidSystemClock : Exception
    {
        public InvalidSystemClock(string message) : base(message) { }
    }

    internal static class SystemFunction
    {
        public static Func<long> CurrentTimeFunc = InternalCurrentTimeMillis;

        public static long CurrentTimeMillis()
        {
            return CurrentTimeFunc();
        }

        public static IDisposable StubCurrentTime(Func<long> func)
        {
            CurrentTimeFunc = func;
            return new DisposableAction(() =>
            {
                CurrentTimeFunc = InternalCurrentTimeMillis;
            });
        }

        public static IDisposable StubCurrentTime(long millis)
        {
            CurrentTimeFunc = () => millis;
            return new DisposableAction(() =>
            {
                CurrentTimeFunc = InternalCurrentTimeMillis;
            });
        }

        private static readonly DateTime DateTime197011 = new DateTime
           (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static long InternalCurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - DateTime197011).TotalMilliseconds;
        }
    }
}
