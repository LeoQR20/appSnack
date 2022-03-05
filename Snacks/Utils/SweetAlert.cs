using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snacks.Util
{
    public class SweetAlert
    {
        public static string Mensaje(string titulo, string mensaje, SweetAlertMessageType tipoMensaje)
        {
            return "swal({title: '" + titulo + "',text: '" + mensaje + "',type: '" + tipoMensaje + "'});";
        }

    }
    public enum SweetAlertMessageType
    {
        warning, error, success, info

    }
}
