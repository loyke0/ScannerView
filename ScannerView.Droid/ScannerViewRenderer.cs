using System;
using Xamarin.Forms.Platform.Android;
using ZXing.Mobile;
using System.Collections.Generic;
using Xamarin.Forms;
using ScannerView.Droid;

[assembly: ExportRenderer (typeof (ScannerView.ScannerView), typeof (ScannerViewRenderer))]

namespace ScannerView.Droid
{
	public class ScannerViewRenderer:ViewRenderer<ScannerView,ZXingScannerView>
	{
		ZXingScannerView XZScannerView { get; set; }

		protected override void OnElementChanged (ElementChangedEventArgs<ScannerView> e)
		{

			if (Control == null && e.NewElement != null) {
				XZScannerView= new ZXingScannerView(this.Context);

				//ScannerView.CancelButtonText = e.NewElement.CancelButtonText;
				XZScannerView.UseCustomOverlayView = false;

				var options = new MobileBarcodeScanningOptions ();
				options.UseFrontCameraIfAvailable = true;
				options.PossibleFormats = new List<ZXing.BarcodeFormat> () { 
					ZXing.BarcodeFormat.QR_CODE 
				};
				this.SetNativeControl (XZScannerView);
			}
			base.OnElementChanged(e);
		}


		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			if (e.PropertyName==ScannerView.IsRunningProperty.PropertyName) {
				if (Element.IsRunning) {
					if (Element.OnQrFound != null) {
						this.Control.StartScanning (Element.Options, Element.OnQrFound);
					} 
				} else {
					this.Control.StopScanning ();
				}
			}
			else if (e.PropertyName == ScannerView.CancelButtonTextProperty.PropertyName) {
				//this.Control.CancelButtonText = Element.CancelButtonText;
			}

		}
	}
}

