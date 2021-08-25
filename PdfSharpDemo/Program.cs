using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Diagnostics;
using System.IO;

namespace PdfSharpDemo
{
    class Program
    {
        static string russianText = "Russian\n" +
"PDFsharp это .NET библиотека для создания и обработки PDF документов 'налету'. " +
"Библиотека полностью написана на языке C# и базируется исключительно на безопасном, управляемом коде. " +
"PDFsharp использует два мощных абстрактных уровня для создания и обработки PDF документов.\n" +
"Для рисования текста, графики, и изображений в ней используется набор классов, которые разработаны аналогично с" +
"пакетом System.Drawing, библиотеки .NET framework. С помощью этих классов возможно не только создавать" +
"содержимое PDF страниц очень легко, но они так же позволяют рисовать напрямую в окне приложения или на принтере.\n" +
"Дополнительно PDFsharp имеет полноценные модели структурированных базовых элементов PDF. Они позволяют работать с существующим PDF документами " +
"для изменения их содержимого, склеивания документов, или разделения на части.\n" +
"Исходный код PDFsharp библиотеки это Open Source распространяемый под лицензией MIT (http://ru.wikipedia.org/wiki/MIT_License). " +
"Теоретически она позволяет использовать PDFsharp без ограничений в не open source проектах или коммерческих проектах/продуктах.";

        [Obsolete]
        static void Main(string[] args)
        {
            // register doce page provider
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new PdfDocument();
            document.Info.Title = "PdfSharp Test 01";

            // new page
            PdfPage page = document.AddPage();

            // get xgraphics
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // save initial gfx settings (before rotation)
            gfx.Save();

            // create font
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

            // draw the text
            gfx.DrawString("Hello world!", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height), XStringFormat.Center);

            // rotate graphics
            gfx.RotateAtTransform(90, new XPoint(page.Width/2, page.Height/2));

            // draw text again (while graphics rotation is applied)
            gfx.DrawString("Hello world!", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height), XStringFormat.Center);

            // reset gfx
            gfx.Restore();

            // draw image
            // Get an XGraphics object for drawing
            DrawImage(gfx, "image_test.jpg", Convert.ToInt32(page.Width/2), 50, 250, 250);

            // new font
            XFont normalFont = new XFont("Arial", 11);

            // text formatter
            XTextFormatter tf = new XTextFormatter(gfx);

            // draw russian text
            tf.DrawString(russianText, normalFont, XBrushes.Black,
                new XRect(0, 0, page.Width / 2, page.Height), XStringFormat.TopLeft);

            // save document
            const string filename = "HelloWorld.pdf";
            document.Save(filename);

            return;
        }

        static void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
        }
    }
}
