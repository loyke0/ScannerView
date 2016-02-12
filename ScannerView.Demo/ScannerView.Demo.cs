using System;
using Xamarin.Forms;

namespace ScannerView.Demo
{
	public class App : Application
	{
		public App ()
		{
			var scannedLabel = new Label {
				Text = "Scanned Value",
				HeightRequest = 50,
			};
			var scan = new ScannerView {
				HeightRequest = 400,
				WidthRequest = 400,
			};

			var btn = new Button {
				Text = "Push To start scanner",
				HeightRequest=50,
			};
			btn.Clicked += (s, e) => {
				scan.StartScanner((res)=>{
					scan.StopScanner();
					scannedLabel.Text=res.Text;
				});
			};
			// The root page of your application
			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					HorizontalOptions=LayoutOptions.Center,
					Children = {
						scan,
						scannedLabel,
						btn
					}
				}
			};
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

