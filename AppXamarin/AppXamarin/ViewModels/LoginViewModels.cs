using AppXamarin.Models;
using AppXamarin.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace AppXamarin.ViewModels
{
	public class LoginViewModels : INotifyPropertyChanged
	{
		public Command LoginCommand { get; set; }
		public LoginViewModels()
		{
			LoginCommand = new Command(loginAsync);
		}
		//commit here
		private async void loginAsync()
		{
			ServicioWebApi servicio = new ServicioWebApi();
			JsonLogin user = new JsonLogin();
			user.DNI = DNI;
			user.Contra = Contra;
			string resp = await servicio.Login(user);
			if (!string.IsNullOrEmpty(resp))
			{
				await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new MainPage()),true);
			}
			else
			{
				await Application.Current.MainPage.DisplayAlert("Error","Revise los datos de inicio","OK");
			}
		}

		private string _DNI;

		public string DNI
		{
			get { return _DNI; }
			set
			{
				if (value != null || value != _DNI) _DNI = value;
				RaisepropertyChanged("DNI");
			}
		}



		private string _Contra;
		public string Contra
		{
			get { return _Contra; }
			set
			{
				if (value != null || value != _Contra) _Contra = value;
				RaisepropertyChanged("Contra");

			}
		}


		void RaisepropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
