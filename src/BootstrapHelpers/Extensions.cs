using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BootstrapHelpers
{
    public static class Extensions
    {
        public static MvcForm BeginBootstrapForm(this HtmlHelper helper, string actionName, string controllerName)
        {
            var mvcForm = helper.BeginForm(actionName, controllerName, FormMethod.Post, new { role = "form" });
            return mvcForm;
        }

        public static MvcHtmlString BootstrapTextBox<TModel, TProperty>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            string title,
            string id,
            string placeholder,
            bool usePlaceholder = true)
        {
            var label = GetLabel(helper, expression, title);
            var input = GetTextInput(helper, expression, id, title, placeholder, usePlaceholder);
            var validationMessages = GetValidationMessage(helper, expression);

            return BuildEditor(label, input, validationMessages);
        }

        public static MvcHtmlString BootstrapTextBox<TModel, TProperty>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            string title,
            string id,
            bool usePlaceholder = true)
        {
            return BootstrapTextBox(helper, expression, title, id, string.Empty, usePlaceholder);
        }

        public static MvcHtmlString BootstrapEmailAddress<TModel, TProperty>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            string title,
            string id,
            bool usePlaceholder = true)
        {
            var label = GetLabel(helper, expression, title);
            var input = GetEmailAddressInput(helper, expression, id, title, usePlaceholder);
            var validationMessages = GetValidationMessage(helper, expression);

            return BuildEditor(label, input, validationMessages);
        }

        public static MvcHtmlString BootstrapDateTimePicker<TModel, TProperty>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            string title,
            string id)
        {
            var label = GetLabel(helper, expression, title);
            var input = GetDateTimePicker(helper, expression, id);
            var validationMessages = GetValidationMessage(helper, expression);

            return BuildEditor(label, input, validationMessages);
        }

        public static MvcHtmlString BootstrapDropDownList<TModel, TProperty>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> values,
            string title,
            string id)
        {
            var label = GetLabel(helper, expression, title);
            var input = GetDropdownList(helper, expression, values, title, id);
            var validationMessages = GetValidationMessage(helper, expression);

            return BuildEditor(label, input, validationMessages);
        }

        private static string GetLabel<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string title)
        {
            var label = helper.LabelFor(expression, title);
            return label.ToString();
        }

        private static string GetValidationMessage<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var validationMessages = helper.ValidationMessageFor(expression);
            return validationMessages.ToString();
        }

        private static string GetTextInput<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string id, string title, string placeholder, bool usePlaceholder)
        {
            var ph = string.IsNullOrEmpty(placeholder) ? title : placeholder;
            var textbox = helper.TextBoxFor(expression, new { @class = "form-control", id = id, placeholder = usePlaceholder ? ph : string.Empty });
            return textbox.ToString();
        }

        private static string GetEmailAddressInput<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string id, string title, bool usePlaceholder)
        {
            var textbox = helper.TextBoxFor(expression, new { @type = "email", @class = "form-control", id = id, placeholder = usePlaceholder ? title : string.Empty });
            return textbox.ToString();
        }

        private static string GetDateTimePicker<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string id)
        {
            var textInput = GetTextInput(helper, expression, id, string.Empty, string.Empty, false);
            var sb = new StringBuilder();
            sb.Append("<div class='input-group date'>");
            sb.Append(textInput);
            sb.Append("<span class='input-group-addon'>");
            sb.Append("<span class='glyphicon glyphicon-calendar'></span>");
            sb.Append("</span>");
            sb.Append("</div>");

            return sb.ToString();
        }

        private static string GetDropdownList<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> values, string title, string id)
        {
            var list = helper.DropDownListFor(expression, values, new { @class = "form-control", id = id });
            return list.ToString();
        }

        private static MvcHtmlString BuildEditor(string label, string input, string validationMessages)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<div class='form-group'>");
            sb.AppendLine(label);
            sb.AppendLine(input);
            sb.AppendLine(validationMessages);
            sb.AppendLine("</div>");

            return new MvcHtmlString(sb.ToString());
        }
    }
}