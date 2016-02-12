using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using ZXing.Mobile;
using UIKit;
using ScannerView.iOS;
using ScannerView;
using System.Collections.Generic;

[assembly: ExportRenderer (typeof (ScannerView.ScannerView), typeof (ScannerViewRenderer))]
namespace ScannerView.iOS
{
	public class ScannerViewRenderer:ViewRenderer<ScannerView,AVCaptureScannerView>
	{
		AVCaptureScannerView AvScannerView { get; set; }

		public static void Register () {}

		protected override void OnElementChanged (ElementChangedEventArgs<ScannerView> e)
		{
			if (e.OldElement == null && e.NewElement != null) {
				AvScannerView= new AVCaptureScannerView();
				AvScannerView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
				AvScannerView.UseCustomOverlayView = false;
				AvScannerView.Layer.CornerRadius =((ScannerView) e.NewElement).CornerRadius;
				AvScannerView.CancelButtonText=((ScannerView) e.NewElement).CancelButtonText;
				AvScannerView.ClipsToBounds = true;
				var options = new MobileBarcodeScanningOptions ();
				options.UseFrontCameraIfAvailable = true;
				options.PossibleFormats = new List<ZXing.BarcodeFormat> { 
					ZXing.BarcodeFormat.QR_CODE 
				};
				SetNativeControl (AvScannerView);
			}
			base.OnElementChanged(e);
		}


		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			if (e.PropertyName==ScannerView.IsRunningProperty.PropertyName) {
				if (Element.IsRunning) {
					var options = new MobileBarcodeScanningOptions ();
					options.UseFrontCameraIfAvailable = true;
					options.PossibleFormats = new List<ZXing.BarcodeFormat> { 
						ZXing.BarcodeFormat.QR_CODE 
					};
					if (Element.OnQrFound != null) {
						if (NativeView.Frame!=Control.Frame) {
							Control.Frame = NativeView.Frame;
						}
						Control.StartScanning (Element.OnQrFound, options);
						Control.ResizePreview(UIApplication.SharedApplication.StatusBarOrientation);
					} 
				} else {
					Control.StopScanning ();
				}
			}
			else if (e.PropertyName == ScannerView.CancelButtonTextProperty.PropertyName) {
				Control.CancelButtonText = Element.CancelButtonText;
			}
			else if (e.PropertyName == ScannerView.CornerRadiusProperty.PropertyName) {
				Control.Layer.CornerRadius = Element.CornerRadius;
			}
		}
	}
}

