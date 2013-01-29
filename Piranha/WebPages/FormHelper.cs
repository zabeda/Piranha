using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.WebPages.Html;

namespace Piranha.WebPages
{
	/// <summary>
	/// The Piranha form helpers for model binder.
	/// </summary>
	/// <typeparam name="TModel">The model type</typeparam>
	public class FormHelper<TModel>
	{
		#region Members
		private ContentPage<TModel> Page ;
		#endregion

		/// <summary>
		/// Default constructor. Creates a new form helper.
		/// </summary>
		/// <param name="page">The current page.</param>
		public FormHelper(ContentPage<TModel> page) {
			Page = page ;
		}

		/// <summary>
		/// Gets a textbox for the specified model property.
		/// </summary>
		/// <typeparam name="TProperty">The property type</typeparam>
		/// <param name="expr">The model expression</param>
		/// <returns>The textbox</returns>
		public IHtmlString TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expr) {
			return Page.Html.TextBox(expr.Body.ToString(), GetValue(expr)) ;
		}

		/// <summary>
		/// Gets a textbox for the specified model property.
		/// </summary>
		/// <typeparam name="TProperty">The property type</typeparam>
		/// <param name="expr">The model expression</param>
		/// <param name="htmlAttributes">Additional html attributes</param>
		/// <returns>The textbox</returns>
		public IHtmlString TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expr, object htmlAttributes) {
			return Page.Html.TextBox(expr.Body.ToString(), GetValue(expr), htmlAttributes) ;
		}

		/// <summary>
		/// Gets a textarea for the specified model property.
		/// </summary>
		/// <typeparam name="TProperty">The property type</typeparam>
		/// <param name="expr">The model expression</param>
		/// <returns>The textbox</returns>
		public IHtmlString TextAreaFor<TProperty>(Expression<Func<TModel, TProperty>> expr) {
			return Page.Html.TextArea(expr.Body.ToString(), GetValue(expr).ToString()) ;
		}

		/// <summary>
		/// Gets a textarea for the specified model property.
		/// </summary>
		/// <typeparam name="TProperty">The property type</typeparam>
		/// <param name="expr">The model expression</param>
		/// <param name="htmlAttributes">Additional html attributes</param>
		/// <returns>The textbox</returns>
		public IHtmlString TextAreaFor<TProperty>(Expression<Func<TModel, TProperty>> expr, object htmlAttributes) {
			return Page.Html.TextArea(expr.Body.ToString(), GetValue(expr).ToString(), htmlAttributes) ;
		}

		/// <summary>
		/// Gets a checkbox for the specified model property.
		/// </summary>
		/// <typeparam name="TProperty">The property type</typeparam>
		/// <param name="expr">The model expression</param>
		/// <returns>The checkbox</returns>
		public IHtmlString CheckBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expr) {
			return Page.Html.CheckBox(expr.Body.ToString(), GetValue(expr)) ;
		}

		/// <summary>
		/// Gets a password for the specified model property.
		/// </summary>
		/// <typeparam name="TProperty">The property type</typeparam>
		/// <param name="expr">The model expression</param>
		/// <returns>The password</returns>
		public IHtmlString PasswordFor<TProperty>(Expression<Func<TModel, TProperty>> expr) {
			return Page.Html.Password(expr.Body.ToString()) ;
		}

		/// <summary>
		/// Gets a hidden field for the specified model property.
		/// </summary>
		/// <typeparam name="TProperty">The property type</typeparam>
		/// <param name="expr">The model expression</param>
		/// <returns>The hidden field</returns>
		public IHtmlString HiddenFor<TProperty>(Expression<Func<TModel, TProperty>> expr) {
			return Page.Html.Hidden(expr.Body.ToString(), GetValue(expr)) ;
		}

		/// <summary>
		/// Gets a validation message for the specified model property.
		/// </summary>
		/// <typeparam name="TProperty">The property type</typeparam>
		/// <param name="expr">The model expression</param>
		/// <returns>The validation message</returns>
		public IHtmlString ValidationMessageFor<TProperty>(Expression<Func<TModel, TProperty>> expr) {
			return Page.Html.ValidationMessage(expr.Body.ToString()) ;
		}

		/// <summary>
		/// Gets a sekect list for the specified model property.
		/// </summary>
		/// <typeparam name="TProperty">The property type</typeparam>
		/// <param name="expr">The model expression</param>
		/// <returns>The select list</returns>
		public IHtmlString DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expr, IList<SelectListItem> selectList) {
			return Page.Html.DropDownList(expr.Body.ToString(), selectList) ;
		}

		/// <summary>
		/// Gets a sekect list for the specified model property.
		/// </summary>
		/// <typeparam name="TProperty">The property type</typeparam>
		/// <param name="expr">The model expression</param>
		/// <param name="htmlAttributes">Additional html attributes</param>
		/// <returns>The select list</returns>
		public IHtmlString DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expr, IList<SelectListItem> selectList, object htmlAttributes) {
			return Page.Html.DropDownList(expr.Body.ToString(), selectList, htmlAttributes) ;
		}

		#region Private methods
		/// <summary>
		/// Gets the model value for the given expression, either from the current
		/// ModelState or from the current model.
		/// </summary>
		/// <typeparam name="TProperty">The property type</typeparam>
		/// <param name="expr">The expression</param>
		/// <returns>The expression value</returns>
		private object GetValue<TProperty>(Expression<Func<TModel, TProperty>> expr) {
			var name = expr.Body.ToString() ;
			var state = Page.ModelState[name] ;

			if (state != null)
				return Page.ModelState[name].Value ;
			return expr.Compile()(Page.Model) ;
		}
		#endregion
	}
}