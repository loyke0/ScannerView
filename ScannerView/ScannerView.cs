using System;
using Xamarin.Forms;
using ZXing;
using System.Runtime.CompilerServices;

namespace ScannerView
{
	public class ScannerView:BoxView
	{
		public static readonly BindableProperty IsRunningProperty =
			BindableProperty.Create("IsRunning", typeof(bool), typeof(ScannerView), false);

		public static readonly BindableProperty CancelButtonTextProperty =
			BindableProperty.Create("CancelButtonText", typeof(String), typeof(ScannerView), default(String));

		public static readonly BindableProperty CornerRadiusProperty =
			BindableProperty.Create("CornerRadius", typeof(float), typeof(ScannerView), 0f);

		public float CornerRadius{
			get{ return (float) GetValue(CornerRadiusProperty); } 
			set{ SetValue(CornerRadiusProperty,value); }
		}
		
		public  bool IsRunning {
			get { return (bool)GetValue(IsRunningProperty); }
		}

		public Action<Result> OnQrFound{ get; internal set; } 

		public string CancelButtonText { 
			get{ return (string) GetValue(CancelButtonTextProperty); } 
			set{ SetValue(CancelButtonTextProperty,value); }
		}

		public ScannerView ()
		{
			
		}
			
		public virtual void StartScanner(Action<Result> onQrFound){
			this.OnQrFound = onQrFound;
			SetValue(IsRunningProperty, true);
		}

		public void StopScanner(){
			SetValue(IsRunningProperty, false);
		}
	}
}

