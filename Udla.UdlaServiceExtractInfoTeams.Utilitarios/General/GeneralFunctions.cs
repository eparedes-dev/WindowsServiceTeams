using System;
using System.Reflection;
using System.Text;

namespace Udla.UdlaServiceExtractInfoTeams.Util.Generales
{
    /// <summary>
    /// Clase que contiene funciones generales a ser usadas en todas las capas de la aplicación
    /// </summary>
    public class GeneralFunctions
    {

        /// <summary>
        /// Función que devuelve un string con el tipo de datos de una propiedad de una clase aunque esta sea Nullable
        /// </summary>
        /// <param name="propertyInfo">Datos de la propiedad a obtner el tipo de datos</param>
        /// <returns>string con el Tipo de dato.</returns>
        public string ObtenerNombreTipoDatoPropiedad(PropertyInfo propertyInfo)
        {
            string nombreTipoDato = string.Empty;

            // si es nullable, obtengo de otra manera el nombre del tipo de dato
            if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                nombreTipoDato = propertyInfo.PropertyType.GetGenericArguments()[0].FullName;
            }
            else
            {
                nombreTipoDato = propertyInfo.PropertyType.FullName;
            }

            return nombreTipoDato;
        }


        /// <summary>
        /// Función para devolver un string randómico. Nos sirve principalmente para retornar este valor en el ViewBag a la vista y mandar como parámetro en el querystring en la llamada
        /// del archivo JS
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
        
    }
}
