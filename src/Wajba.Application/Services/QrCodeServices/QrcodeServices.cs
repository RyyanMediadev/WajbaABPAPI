global using ZXing;
global using ZXing.Common;

namespace Wajba.Services.QrCodeServices;

public class QrcodeServices
{
    public string GenerateQrCodeImage(string qrCodeUrl)
    {
        var writer = new BarcodeWriterSvg
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new EncodingOptions
            {
                Width = 300,  // Width of the QR code
                Height = 300, // Height of the QR code
                Margin = 1    // Margin around the QR code
            }
        };
        var svgImage = writer.Write(qrCodeUrl);
        var svgContent = svgImage.Content;
        var base64Svg = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(svgContent));
        return $"data:image/svg+xml;base64,{base64Svg}";
    }

    public string GenerateQrCodeUrl(int branchId, string tableName)
    {
        string baseUrl = "https://api.wajba.net/branch";
        return $"{baseUrl}?branchId={branchId}&tableName={tableName}";
    }
}