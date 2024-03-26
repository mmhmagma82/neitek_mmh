using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Common.Model
{
    public enum ResponseCode
    {
        Success = 1,
        Error = 2,
        Failure = 3
    }

    public enum TaskState
    {
        [Description("Abierta")]
        Open = 1,
        [Description("Completada")]
        Closed = 2
    }

    public enum OperationsList
    {
        [Description("Nuevo registro")]
        New = 1,
        [Description("Actualizar registro")]
        Update = 2,
        [Description("Eliminar registro")]
        Delete = 3
    }

    public enum MessageList
    {
        [Description("¡Muy bien!")]
        Success = 1,
        [Description("¡Algo salió mal!")]
        Error = 2
    }

    public static class EnumData
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}