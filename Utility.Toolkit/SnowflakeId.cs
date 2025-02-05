using System;

namespace Utility.Toolkit
{
    /// <summary>
    /// 雪花算法ID
    /// </summary>
    public readonly struct SnowflakeId : IEquatable<SnowflakeId>
    {
        /// <summary>
        /// 空
        /// </summary>
        public static readonly SnowflakeId Empty = new SnowflakeId(0);



        private readonly Int64 m_value = 0;

        private SnowflakeId(Int64 value)
        {
            this.m_value = value;
        }
        /// <inheritdoc/>


        public override string ToString()
        {
            return this.m_value.ToString("X").ToUpper();
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return this.m_value == 0;
            }
        }

        /// <summary>
        /// 类别
        /// </summary>
        public Int32 Category
        {
            get
            {
                return (Int32)((this.m_value >> 48) & 0x0FFF);
            }
        }

        /// <summary>
        /// 时间戳
        /// </summary>
        public Int32 Timestamp
        {
            get
            {
                return (Int32)((this.m_value >> 16) & 0xFFFFFFFF);
            }
        }

        /// <summary>
        /// UTC时间
        /// </summary>
        public DateTime UTCTime
        {
            get
            {
                return STARTDATE_DEFINE.AddSeconds((this.m_value >> 16) & 0xFFFFFFFF);
            }
        }
        /// <summary>
        /// 本地时间
        /// </summary>
        public DateTime LocalTime
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(STARTDATE_DEFINE.AddSeconds((this.m_value >> 16) & 0xFFFFFFFF), TimeZoneInfo.Local);
            }
        }

        /// <summary>
        /// 序列
        /// </summary>
        public UInt16 Sequence
        {
            get
            {
                return (UInt16)(this.m_value & 0xFFFF);
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        public Int64 Value
        {
            get
            {
                return this.m_value;
            }
        }

        /// <summary>
        /// ==
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool operator ==(SnowflakeId s1, SnowflakeId s2)
        {
            return s1.m_value == s2.m_value;
        }

        /// <summary>
        /// !=
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool operator !=(SnowflakeId s1, SnowflakeId s2)
        {
            return s1.m_value != s2.m_value;
        }
        /// <inheritdoc/>

        public bool Equals(SnowflakeId other)
        {
            return other.m_value == this.m_value;
        }
        /// <inheritdoc/>

        public override int GetHashCode()
        {
            return unchecked((int)((long)m_value)) ^ (int)(m_value >> 32);
        }
        /// <inheritdoc/>

        public override bool Equals(object obj)
        {
            if (obj is SnowflakeId id)
            {
                return id.m_value == this.m_value;
            }
            return false;
        }
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator SnowflakeId(String value)
        {
            long result = Convert.ToInt64(value, 16);
            return new SnowflakeId(result);
        }
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator SnowflakeId(Int64 value)
        {
            return new SnowflakeId(value);
        }




        #region Static 


        private static readonly DateTime STARTDATE_DEFINE = new DateTime(2024, 1, 1, 12, 34, 56, DateTimeKind.Utc);
        private static long _lastTimestamp = -1L;
        private static long _sequence = 1L;
        private static readonly object _lock = new object();


        /// <summary>
        /// 生成一个ID
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static SnowflakeId Generate(Int32 category)
        {
            if (category < 0 || category > 0xFFF)
            {
                throw new ArgumentOutOfRangeException(nameof(category), $"Type must be between 0 and 0xFFF");
            }
            lock (_lock)
            {
                var timestamp = GetCurrentTimestamp();
                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & 0xFFFF;
                    if (_sequence == 0L)
                    {
                        timestamp = WaitNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 1L;
                }
                _lastTimestamp = timestamp;
                long id =
                    ((long)0x7 << 60) |
                    ((long)(category & 0xFFF) << 48) |
                    (timestamp << 16) |
                    _sequence;
                return new SnowflakeId(id);
            }


        }


        private static long GetCurrentTimestamp()
        {
            return (long)(DateTime.UtcNow - STARTDATE_DEFINE).TotalSeconds;
        }

        private static long WaitNextMillis(long lastTimestamp)
        {
            var timestamp = GetCurrentTimestamp();
            while (timestamp <= lastTimestamp)
            {
                timestamp = GetCurrentTimestamp();
            }
            return timestamp;
        }

        #endregion

    }
}
