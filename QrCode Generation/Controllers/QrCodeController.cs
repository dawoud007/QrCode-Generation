using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace QrCode_Generation.Controllers
{
    public class QrCodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
            
        public IActionResult Booking()
        {
            Random random = new Random();   
            int QrCodedNumber=random.Next(0,10000);
            QRCodeGenerator QrCodeGenerator=new QRCodeGenerator();
            QRCodeData QrCodeData = QrCodeGenerator.CreateQrCode(QrCodedNumber.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode Qrcode=new QRCode(QrCodeData);

            using(MemoryStream qrStream = new MemoryStream())
            {
                using(Bitmap bitMap = Qrcode.GetGraphic(20,Color.BlueViolet,Color.Black,false))
                {
                    bitMap.Save(qrStream,ImageFormat.Png);
                    ViewBag.QRCode ="data:image/png;base64,"+Convert.ToBase64String(qrStream.ToArray());
                    ViewBag.number=QrCodedNumber.ToString();
                }
            }

            return View();
        }
    }
}
