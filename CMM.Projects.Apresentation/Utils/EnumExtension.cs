using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CMM.Projects.Apresentation.Utils
{
    public static class DescriptionExtension
    {
        public static string Descricao<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(
            typeof(DisplayAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Name;
            else return source.ToString();
        }
    }


}