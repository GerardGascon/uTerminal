using System;
using System.Reflection;
using UnityEngine;

namespace uTerminal
{
    /// <summary>
    /// Static class that defines built-in commands for converting arguments in uTerminal.
    /// </summary>
    public static class ArgumentConverter
    {
        /// <summary>
        /// Creates adjusted parameters based on the provided parameters and their information.
        /// </summary>
        /// <param name="parameters">The array of parameters to be adjusted.</param>
        /// <param name="parametersInfo">The information about the parameters.</param>
        /// <returns>The adjusted parameters.</returns>
        public static object[] CreateParameters(object[] parameters, ParameterInfo[] parametersInfo)
        {
            object[] adjustedParameters = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                Type parameterType = parametersInfo[i].ParameterType;

                if (parameterType == typeof(Vector3) || parameterType == typeof(Vector2))
                    adjustedParameters[i] = StringToVector(parameters[i].ToString());
                else
                    adjustedParameters[i] = Convert.ChangeType(parameters[i], parameterType);
            }

            return adjustedParameters;
        }

        private static Vector3 StringToVector(string arg)
        {
            string[] split = arg.Replace("(", "").Replace(")", "").Split(',');

            if (float.TryParse(split[0], out float x) && float.TryParse(split[1], out float y))
            {
                if (split.Length == 3)
                {
                    if (float.TryParse(split[2], out float z))
                    {
                        return new Vector3(x, y, z);
                    }
                }

                return new Vector2(x, y);
            }

            return Vector2.zero;
        }

        /// <summary>
        /// Converts the object to a Vector3.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The converted Vector3.</returns>
        public static Vector3 ToVector3(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return StringToVector(obj.ToString());
        }

        /// <summary>
        /// Converts the object to a Vector2.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The converted Vector2.</returns>
        public static Vector3 ToVector2(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return StringToVector(obj.ToString());
        }

        /// <summary>
        /// Converts the object to an integer.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The converted integer.</returns>
        public static int ToInt(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (int.TryParse(obj.ToString(), out int resultado))
            {
                return resultado;
            }

            return 0;
        }

        /// <summary>
        /// Converts the object to a string.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The converted string.</returns>
        public static string ToString(this object obj)
        {
            return obj?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Converts the object to a float.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The converted float.</returns>
        public static float ToFloat(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (float.TryParse(obj.ToString(), out float resultado))
            {
                return resultado;
            }

            return 0;
        }
    }
}
