using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Convergys.Assist.WebUI.HtmlExtensions
{
    public static class MVCTextBox
    {
        //public static MvcHtmlString TextBox(this HtmlHelper, string name, object value, object htmlAttributes, bool required, string requiredMsg, string group)
        //{
        //}

        /// <summary>
        /// Last parameter will add a "val-" to the attribute
        /// </summary>
        public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes, object dataAttributes)
        {
            RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);
            attributes.AddDataAttributes(dataAttributes);

            return htmlHelper.TextBox(name, value, ((IDictionary<string, object>)attributes));
        }

        private static void AddDataAttributes(this RouteValueDictionary dictionary, object values)
        {
            if (values != null)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
                {
                    object obj2 = descriptor.GetValue(values);
                    dictionary.Add("data-" + descriptor.Name, obj2);
                }
            }
        }
    }
}