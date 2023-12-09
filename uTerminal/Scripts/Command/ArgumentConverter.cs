using System; 
using System.Reflection;
using UnityEngine;

namespace uTerminal
{
    public static class ArgumentConverter
    {
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

        public static Vector3 ToVector3(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return StringToVector(obj.ToString());
        } 

        public static Vector3 ToVector2(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return StringToVector(obj.ToString());
        } 

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

        public static string ToString(this object obj)
        {
            return obj?.ToString() ?? string.Empty;
        }

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
