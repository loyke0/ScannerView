using System;
using Xamarin.Forms;
using ZXing;
using System.Runtime.CompilerServices;
using ZXing.Mobile;

namespace ScannerView
{
	public class ScannerView:BoxView
	{
		public static readonly BindableProperty IsRunningProperty =
			BindableProperty.Create("IsRunning", typeof(bool), typeof(ScannerView), false);

		public static readonly BindableProperty UseTorcheProperty =
			BindableProperty.Create("UseTorche", typeof(bool), typeof(ScannerView), false);

		public static readonly BindableProperty CancelButtonTextProperty =
			BindableProperty.Create("CancelButtonText", typeof(String), typeof(ScannerView), default(String));

		public static readonly BindableProperty TorcheButtonTextProperty =
			BindableProperty.Create("TorcheButtonText", typeof(String), typeof(ScannerView), default(String));

		public static readonly BindableProperty BottomTextProperty =
			BindableProperty.Create("BottomText", typeof(String), typeof(ScannerView), default(String));
		
		public static readonly BindableProperty CornerRadiusProperty =
			BindableProperty.Create("CornerRadius", typeof(float), typeof(ScannerView), 0f);

		public static readonly BindableProperty CustomOverlayViewProperty =
			BindableProperty.Create("CustomOverlayView", typeof(View), typeof(ScannerView), default(View));

		public static readonly BindableProperty UseCustomOverlayViewProperty =
			BindableProperty.Create("UseCustomOverlayView", typeof(bool), typeof(ScannerView), false);

		public static readonly BindableProperty MobileBarcodeScanningOptionsProperty =
			BindableProperty.Create("MobileBarcodeScanningOptions", typeof(MobileBarcodeScanningOptions), typeof(ScannerView), default(MobileBarcodeScanningOptions));

		public MobileBarcodeScanningOptions Options { 
			get { return (MobileBarcodeScanningOptions)GetValue (MobileBarcodeScanningOptionsProperty); }
			private set { SetValue(MobileBarcodeScanningOptionsProperty,value); }
		}

		public float CornerRadius{
			get{ return (float) GetValue(CornerRadiusProperty); } 
			set{ SetValue(CornerRadiusProperty,value); }
		}

		public bool UseTorche{
			get{ return (bool) GetValue(UseTorcheProperty); } 
			set{ SetValue(UseTorcheProperty,value); }
		}

		public View CustomOverlayView{
			get{ return (View) GetValue(CustomOverlayViewProperty); } 
			set{ SetValue(CustomOverlayViewProperty,value); }
		}

		public bool UseCustomOverlayView{
			get{ return (bool) GetValue(UseCustomOverlayViewProperty); } 
			set{ SetValue(UseCustomOverlayViewProperty,value); }
		}

		public void ToggleTorch(){
			this.UseTorche = !this.UseTorche;
		}
		
		public  bool IsRunning {
			get { return (bool)GetValue(IsRunningProperty); }
		}

		public  bool IsTorchOn {
			get { return (bool)GetValue(UseTorcheProperty); }
		}

		public Action<Result> OnQrFound{ get; private set; } 

		public Action OnCancelPressed { get; set; }


		public string CancelButtonText { 
			get{ return (string) GetValue(CancelButtonTextProperty); } 
			set{ SetValue(CancelButtonTextProperty,value); }
		}

		public string BottomText { 
			get{ return (string) GetValue(BottomTextProperty); } 
			set{ SetValue(BottomTextProperty,value); }
		}

		public string TorchButtonText { 
			get{ return (string) GetValue(TorcheButtonTextProperty); } 
			set{ SetValue(TorcheButtonTextProperty,value); }
		}

		public ScannerView ()
		{
			
		}


			
		public virtual void StartScanner(Action<Result> onQrFound, MobileBarcodeScanningOptions options){
			this.OnQrFound = onQrFound;
			this.Options = options;
			SetValue(IsRunningProperty, true);
		}

		public void StopScanner(){
			SetValue(IsRunningProperty, false);
		}
	}
}

