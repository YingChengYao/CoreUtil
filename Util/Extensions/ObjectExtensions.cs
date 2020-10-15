using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 判断对象是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 判断对象是否不为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static T IsNotNull<T>(T value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static T NotNull<T>(T value, string parameterName, string message)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }

            return value;
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        public static void ThrowIfNull(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        public static void ThrowIfNull(this object obj, string message)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), message);
            }
        }
    }
}
