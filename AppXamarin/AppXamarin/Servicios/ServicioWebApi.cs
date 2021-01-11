using AppXamarin.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppXamarin.Servicios
{
	public class ServicioWebApi
	{
		public string ruta = "http://localhost:54688/"; 

		HttpClient cliente;
		public ServicioWebApi()
		{
			cliente = new HttpClient();
		}
		public string WebApiUrl { get; private set; }

		public async Task<string> Login(JsonLogin usu)
		{
			string respuesta = "";
			var json = JsonConvert.SerializeObject(usu); //serializo el objeto
			HttpContent httpContent = new StringContent(json);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");//declaro el header del tipo app/Json

			WebApiUrl = ruta + "api/Login";
			var uri = new Uri(WebApiUrl);

			//try
			//{
				var response = await cliente.PostAsync(uri, httpContent);//es la union del localhost:44339/api/Lognin + lo que manda el objeto
				if (response.IsSuccessStatusCode)
				{
					var LoginJsonString = await response.Content.ReadAsStringAsync();
					respuesta = JsonConvert.DeserializeObject<string>(LoginJsonString);
				}
				else
				{
					await Application.Current.MainPage.DisplayAlert("Error", "Error en los datos", "OK");
				}
			//}
			//catch (Exception ex)
			//{

			//	await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			//}
			return respuesta;


		}


	}
}
