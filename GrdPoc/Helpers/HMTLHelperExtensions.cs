using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace GrdPoc
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string IsCollapsed(this HtmlHelper html, string element, string status)
        {
            string answer = "";
            if (status == "True")
            {
                if (element == "wrapper")
	            {
                    answer = "full-width";
                }
                if (element == "navbar")
	            {
                    answer = "colapse";
                }
            }
            return answer;
        }

        public static string IsDisabled(this HtmlHelper html, ProjectStatus projectStatus , string field = null)
        {
            switch (projectStatus)
            {
                case ProjectStatus.Unconfigured:
                    if (field == "ExecutionProjectActualEnd") return "";
                    if (field == "ExecutionProjectActualStart") return "";
                    if (field == "ExecutionProjectDeliveranceConfirmation") return "";
                    if (field == "ExecutionProjectDeliveranceDate") return "";
                    if (field == "ExecutionProjectSchedulledEnd") return "";
                    if (field == "ExecutionProjectSchedulledStart") return "";
                    if (field == "ExecutionProjectTitle") return "";
                    break;
                case ProjectStatus.Configured:
                    if (field == "") return "";
                    if (field == "") return "";
                    if (field == "") return "";
                    if (field == "") return "";
                   break;
                case ProjectStatus.Executing:
                    if (field == "") return "";
                    if (field == "") return "";
                    if (field == "") return "";
                    if (field == "") return "";
                    break;
                case ProjectStatus.Delivered:
                    if (field == "") return "";
                    if (field == "") return "";
                    if (field == "") return "";
                    if (field == "") return "";
                    break;
                case ProjectStatus.Confirmed:
                    if (field == "") return "";
                    if (field == "") return "";
                    if (field == "") return "";
                    if (field == "") return "";
                    break;
                case ProjectStatus.Faulty:
                    if (field == "") return "";
                    if (field == "") return "";
                    if (field == "") return "";
                    if (field == "") return "";
                    break;
                default:
                    break;
            }
            return String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }
    }
}
