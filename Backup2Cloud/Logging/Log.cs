using System;

namespace Backup2Cloud.Logging
{
    /// <summary>
    /// 日志打印类
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// 打印DEBUG类型日志
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="module">模块</param>
        public static void Debug(string msg, string module = null)
        {
            msg = "[DEBUG]" + msg;
            if (string.IsNullOrWhiteSpace(module) == false)
            {
                msg = "[" + module + "]" + msg;
            }

            Print(msg);
        }

        /// <summary>
        /// 打印INFO类型日志
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="module">模块</param>
        public static void Info(string msg, string module = null)
        {
            msg = "[INFO]" + msg;
            if (string.IsNullOrWhiteSpace(module) == false)
            {
                msg = "[" + module + "]" + msg;
            }

            Print(msg);
        }

        /// <summary>
        /// 打印WARN类型日志
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="module">模块</param>
        public static void Warn(string msg, string module = null)
        {
            msg = "[WARN]" + msg;
            if (string.IsNullOrWhiteSpace(module) == false)
            {
                msg = "[" + module + "]" + msg;
            }

            Print(msg);
        }

        /// <summary>
        /// 打印ERROR类型日志
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="module">模块</param>
        public static void Error(string msg, string module = null)
        {
            msg = "[ERROR]" + msg;
            if (string.IsNullOrWhiteSpace(module) == false)
            {
                msg = "[" + module + "]" + msg;
            }

            Print(msg);
        }

        /// <summary>
        /// 打印FATAL类型日志
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="module">模块</param>
        public static void Fatal(string msg, string module = null)
        {
            msg = "[FATAL]" + msg;
            if (string.IsNullOrWhiteSpace(module) == false)
            {
                msg = "[" + module + "]" + msg;
            }

            Print(msg);
        }

        /// <summary>
        /// 打印日志。
        /// </summary>
        /// <param name="msg">日志消息</param>
        public static void Print(string msg)
        {
            string prefix = string.Format("[{0}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff zz"));
            Console.WriteLine(prefix + msg);
        }
    }
}
