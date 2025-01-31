// using System.Drawing;
// using System.IO;
// using QRCoder;

// public class QRCodeService
// {
//     public byte[] GenerarQRCode(string contenido)
//     {
//         try
//         {
//             // Inicializar el generador de QR
//             using var qrGenerator = new QRCodeGenerator();
//             using var qrCodeData = qrGenerator.CreateQrCode(contenido, QRCodeGenerator.ECCLevel.Q);

//             // Generar la imagen QR
//             using var qrCode = new QRCode(qrCodeData);
//             using var bitmap = qrCode.GetGraphic(20); // Tamaño del pixel

//             // Convertir la imagen a un arreglo de bytes
//             using var stream = new MemoryStream();
//             bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
//             return stream.ToArray();
//         }
//         catch (Exception ex)
//         {
//             throw new Exception("Error al generar el código QR: " + ex.Message, ex);
//         }
//     }
// }
