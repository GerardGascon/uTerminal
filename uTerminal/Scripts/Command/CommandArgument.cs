using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace uTerminal
{
    public class uArgument
    {
        public string t;
        public T GetParameter<T>()
        {
            Type parameterType = typeof(T);

            if (parameterType == typeof(Vector3) || parameterType == typeof(Vector2))
                return (T)ToVector(t.ToString());
            else
                return (T)Convert.ChangeType(t, parameterType);
        } 

        private static object ToVector(string arg)
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

            return null;
        } 
    }
}
