using System;
using System.Drawing;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class PrintController : ControllerBase
{

    private const string PrinterName = "EPSON TM-T20III Receipt6"; // Asegúrate de que el nombre es correcto


     [HttpPost]
    public IActionResult PrintTicket([FromBody] PrintRequest2 request)
    {
       try
    {
        if (request == null || request.Lines == null || request.Lines.Length == 0)
        {
            return BadRequest("El cuerpo de la solicitud está vacío o mal formado.");
        }

        string ticketContent = string.Join("\n", request.Lines);
        bool printed = RawPrinterHelper.SendStringToPrinter("EPSON TM-T20III Receipt6", ticketContent);

        if (!printed)
            return StatusCode(500, "Error al enviar la impresión a la impresora.");

        return Ok("Impresión enviada correctamente.");
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error al imprimir: {ex.Message}");
    }
    }

    [HttpPost("print")]
    public IActionResult PrintTicket([FromBody] PrintRequest request)
    {
        try
        {
            string printerName = "EPSON TM-T20III Receipt6"; // Asegúrate de que sea el nombre correcto en tu sistema
            byte[] bytes = Encoding.UTF8.GetBytes(request.Text + "\n\n\n\n\n"); // Espacios para corte automático

            bool result = RawPrinterHelper.SendBytesToPrinter(printerName, bytes);
            if (result)
                return Ok(new { message = "Impresión enviada correctamente" });

            return StatusCode(500, new { message = "Error al enviar la impresión" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error interno", error = ex.Message });
        }
    }
}

public class PrintRequest
{
    public string Text { get; set; }
}

public class PrintRequest2
{
    public string[] Lines { get; set; }
}

