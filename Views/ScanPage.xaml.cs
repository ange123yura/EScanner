using System;
using System.Security.Cryptography;
using Microsoft.Maui.Controls;
using EScanner.Models;
using IronSoftware.Drawing;
using IronBarCode;
using static EScanner.Includes.GlobalVariables;
//using MetalPerformanceShadersGraph;
//using MetalPerformanceShadersGraph;
namespace EScanner.Views;
 
public partial class ScanPage : ContentPage
{
    private string _barcodeId;
    private string _barcodeName;
    private Label _resultLabel;
    private DateTime _timeIn;
    private DateTime _timeOut;
    private DateTime _datetime;
    Visitor _visitor = new();
    Event _event = new();
    Student _student = new();
    public ScanPage()
    {
        InitializeComponent();
        cameraView.BarCodeOptions = new()
        {
            PossibleFormats = { ZXing.BarcodeFormat.QR_CODE }
        };
        txteventname.Text = _en;
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
            });
        }
    }

    private async void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        try
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                //var barcodeText = $"{args.Result[0].BarcodeFormat}: {args.Result[0].Text}";
                var barcodeText = args.Result[0].Text;
                var (id, name) = ExtractBarcodeId(barcodeText);
                var currentTime = DateTime.Now;
                var currentDate = DateTime.Now;



                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(name))
                {

                    _barcodeId = id;
                    barcodeResultID.Text = id;
                    _barcodeName = name;
                    barcodeResultName.Text = name;


                    _datetime = currentDate;
                    barcodeResultDate.Text = $"{_datetime.ToString("yyyy-MM-dd")}";

                    _timeIn = currentTime;

                    barcodeResult.Text = $"{_timeIn.ToString(@"h\:mm")}";

                    barcodeResultOut.Text = $"Student Didnt CheckedOut Yet";

                }
                //await DisplayAlert("", id, "OK");
                //var visitor = await _visitor.GetVisitor(id);
                //if (visitor != null)
                //{
                //    {
                //        barcodeResultID.Text = visitor.BarcodeId;

                //        barcodeResultName.Text = visitor.FullName;
                //        barcodeResult.Text = visitor.TimeIn;
                //        barcodeResultDate.Text = visitor.Date;

                //        _timeOut = currentTime;

                //        barcodeResultOut.Text = $"{_timeOut.ToString(@"h\:mm")}";
                //    }



                //}



            });

        }
        catch
        {

        }

    }

    private (string, string) ExtractBarcodeId(string barcodeText)
    {
        // Assuming the barcode ID is in the format "Date: <ID>"
        var parts = barcodeText.Split('\n');
        if (parts.Length > 1)
        {
            var id = parts[0].Split(':')[1].Trim();
            var name = parts[1].Split(':')[1].Trim();
            return (id, name);
        }
        return (string.Empty, string.Empty);
    }

    private async void btnadd_Clicked(object sender, EventArgs e)
    {
        await _visitor.GetStudKey(barcodeResultID.Text);
        if (string.IsNullOrEmpty(barcodeResultID.Text))
        {
            await DisplayAlert("Data validation", "Please Enter Purpose of Visit", "Got it");
            return;
        }
        
        else if (string.IsNullOrEmpty(barcodeResultName.Text))
        {
            await DisplayAlert("Data validation", "Please Enter Name", "Got it");
            return;
        }

        bool a;
        a = await _visitor.GetVisID(barcodeResultID.Text);
       
        if (!a)
        {
            await DisplayAlert("Validation", "This student has already Timed In", "Got it");
        }
        else {
            bool b;
            b = await _visitor.GetStudID(barcodeResultID.Text);
            if (b)
            {
                await DisplayAlert("Validation", "This student has not Registered", "Got it");
            }
*-0   
                else
                {

                    await _visitor._AddVisitors(barcodeResultID.Text, barcodeResultName.Text, barcodeResultDate.Text, barcodeResult.Text, barcodeResultOut.Text);
                    barcodeResultID.Text = string.Empty;
                    barcodeResultName.Text = string.Empty;
                    barcodeResultDate.Text = string.Empty;
                    barcodeResult.Text = string.Empty;
                    barcodeResultOut.Text = string.Empty;

                    await DisplayAlert(" ", "Time In Successfully!!!", "OK");

                    //Application.Current!.MainPage = new ScanPage();
                    return;


                }
            
            
        }
        

    }


    private async void btntimeOut_Clicked(object sender, EventArgs e)
    {
        try
        {
bool a;
        a = await _visitor.GetVisID(barcodeResultOut.Text);
        if (!a)
        {
            await DisplayAlert("Time Out validation", "You Have Already Timed Out", "Got it");

         
        }

        else
        {

           await _visitor.GetVisitorKey(barcodeResultID.Text);
            if (timeout == "Visitor Didnt CheckedOut Yet")
            {
                var TimeOuts = DateTime.Now.ToString(@"h\:mm");
            await _visitor._OutVisitors(barcodeResultID.Text, barcodeResultName.Text, barcodeResultDate.Text, barcodeResult.Text, TimeOuts);
            //await _visitor.EditVisitor(visitor.BarcodeId, visitor.Purpose, visitor.FullName, visitor.Date, visitor.TimeIn, visitor.TimeOut);
            //barcodeResultOut.Text = visitor.TimeOut;
            await DisplayAlert(" ", "Time Out Successfully!!!", "OK");
                    barcodeResultID.Text = "";

                    barcodeResultName.Text = "";
                    barcodeResult.Text = "";
                    barcodeResultDate.Text = "";
                   // Application.Current!.MainPage = new ScanPage();
                    return;
            }else
            {
                await DisplayAlert("Time Out validation", "You Have Already Timed Out", "Got it");
                    barcodeResultID.Text = "";

                    barcodeResultName.Text = "";
                    barcodeResult.Text = "";
                    barcodeResultDate.Text = "";
                }
          

        }
        }
        catch { 
        
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        Application.Current!.MainPage = new ViewEvent();
    }
}

