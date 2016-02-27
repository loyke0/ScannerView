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
				AvScannerView.FlashButtonText = ((ScannerView)e.NewElement).TorchButtonText;
				AvScannerView.Layer.CornerRadius =((ScannerView) e.NewElement).CornerRadius;
				AvScannerView.CancelButtonText=((ScannerView) e.NewElement).CancelButtonText;
				AvScannerView.OnCancelButtonPressed += () => {
					if (((ScannerView)e.NewElement).OnCancelPressed!=null) {
						((ScannerView)e.NewElement).OnCancelPressed();
					}
				};
				AvScannerView.BottomText = ((ScannerView)e.NewElement).BottomText;
				//AvScannerView.CustomOverlayView = ((ScannerView)e.NewElement).CustomOverlayView;
				AvScannerView.UseCustomOverlayView = ((ScannerView)e.NewElement).UseCustomOverlayView;
				AvScannerView.Torch (((ScannerView)e.NewElement).UseTorche);
				AvScannerView.ClipsToBounds = true;
				SetNativeControl (AvScannerView);
			}
			base.OnElementChanged(e);
		}


		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			if (e.PropertyName==ScannerView.IsRunningProperty.PropertyName) {
				if (Element.IsRunning) {
					if (Element.OnQrFound != null) {
						if (NativeView.Frame!=Control.Frame) {
							Control.Frame = NativeView.Frame;
						}
						Control.StartScanning (Element.OnQrFound, Element.Options);
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
			else if (e.PropertyName == ScannerView.UseTorcheProperty.PropertyName) {
				Control.Torch(Element.UseTorche);
			}
			else if (e.PropertyName == ScannerView.TorcheButtonTextProperty.PropertyName) {
				Control.FlashButtonText = Element.TorchButtonText;
			}
			else if (e.PropertyName == ScannerView.BottomTextProperty.PropertyName) {
				Control.BottomText = Element.BottomText;
			}
			else if (e.PropertyName == ScannerView.UseCustomOverlayViewProperty.PropertyName) {
				Control.UseCustomOverlayView = Element.UseCustomOverlayView;
			}
			else if (e.PropertyName == ScannerView.CustomOverlayViewProperty.PropertyName) {
				//Control.CustomOverlayView = Element.CustomOverlayView;
			}
		}
	}
}

