using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Fwk.HelperFunctions
{
	/// <summary>
	/// Funciones para envío de correo electrúnico.
	/// </summary>
	public class EMailFunctions
	{
		

		#region --[MailAddressValidate]--
		/// <summary>
		/// Valida una direccion de correo electronico.
		/// </summary>
		/// <param name="pMailAddress">Direccion de correo electronico.</param>
		/// <returns>Indica si la validacion fue exitosa.</returns>
		public static bool MailAddressValidate(string pMailAddress)
		{
			return Regex.IsMatch(pMailAddress, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"); 
		}
		#endregion
	}
}
