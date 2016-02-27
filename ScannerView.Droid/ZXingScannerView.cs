using System;
using Android.Views;
using ZXing.Mobile;
using Android.OS;
using Android.Content;
using Android.Widget;
using ZXing;
using Android.App;

namespace ScannerView.Droid
{
	public class ZXingScannerView : RelativeLayout , IZXingScanner<View>
	{
		public View CustomOverlayView { get;set; }
		public bool UseCustomOverlayView { get; set ; }
		public MobileBarcodeScanningOptions ScanningOptions { get;set; }
		public string TopText { get;set; }
		public string BottomText { get;set; }
		Context _Context { get; set; }
		ZXingSurfaceView scanner;
		ZxingOverlayView zxingOverlay;

		
		public ZXingScannerView(Context context) :base(context)
		{
			this._Context = context;
			UseCustomOverlayView = false;
			scanner = new ZXingSurfaceView (this._Context as Activity);
			var layoutParams = new LinearLayout.LayoutParams (ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent);
			layoutParams.Weight = 1;
			this.AddView(scanner, layoutParams);
			if (!UseCustomOverlayView)
			{
				zxingOverlay = new ZxingOverlayView (this._Context);
				zxingOverlay.TopText = TopText ?? "";
				zxingOverlay.BottomText = BottomText ?? "";

				this.AddView (zxingOverlay, layoutParams);
			}
			else if (CustomOverlayView != null)
			{
				this.AddView(CustomOverlayView, layoutParams);
			}
		}
			
		public void SetTorch(bool on)
		{
			scanner.Torch(on);
		}

		public void AutoFocus()
		{
			scanner.AutoFocus();
		}

		Action<ZXing.Result> scanCallback;
		bool scanImmediately = false;

		public void StartScanning (MobileBarcodeScanningOptions options, Action<ZXing.Result> callback)
		{            
			ScanningOptions = options;
			scanCallback = callback;

			if (scanner == null) {
				scanImmediately = true;
				return;
			}

			scan ();
		}

		void scan ()
		{
			scanner.StartScanning (scanCallback,ScanningOptions);
		}


		public void StartScanning (Action<ZXing.Result> callback)
		{
			StartScanning (MobileBarcodeScanningOptions.Default, callback);
		}

		public void StopScanning ()
		{
			scanner.StopScanning ();
		}

		public void PauseAnalysis ()
		{
			scanner.PauseAnalysis ();
		}

		public void ResumeAnalysis ()
		{
			scanner.ResumeAnalysis ();
		}

		public void ToggleTorch ()
		{
			scanner.ToggleTorch ();
		}

		public bool IsTorchOn {
			get {
				return scanner.IsTorchOn;
			}
		}

		public bool IsAnalyzing {
			get {
				return scanner.IsAnalyzing;
			}
		}

		public void StartScanning (Action<ZXing.Result> scanResultHandler, MobileBarcodeScanningOptions options = null)
		{
			StartScanning (MobileBarcodeScanningOptions.Default, scanResultHandler);
		}

		public void Torch (bool on)
		{
			throw new NotImplementedException ();
		}

		public void AutoFocus (int x, int y)
		{
			throw new NotImplementedException ();
		}

		public bool HasTorch {
			get {
				throw new NotImplementedException ();
			}
		}
	}
}

