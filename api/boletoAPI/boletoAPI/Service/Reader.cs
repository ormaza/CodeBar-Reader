using ZXing;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using boletoAPI.Domain;

namespace boletoAPI.Service
{
    public static class Reader
    {
        [System.Obsolete]
        public static Retorno GetCodeBar(Stream file)
        {
            Retorno retorno = new Retorno();
            var barcodeBitmap = (Bitmap)Image.FromStream(file);
            ZXing.BarcodeReader wvBarCodeReader;

            List<BarcodeFormat> wvBarCodesList = new List<BarcodeFormat>();
            wvBarCodesList.Add(BarcodeFormat.ITF);
            wvBarCodeReader = new ZXing.BarcodeReader { AutoRotate = true };
            wvBarCodeReader.Options.PossibleFormats = wvBarCodesList;
            wvBarCodeReader.Options.TryHarder = true;
            wvBarCodeReader.TryInverted = true;

            Result r;
            r = wvBarCodeReader.Decode(barcodeBitmap);

            if (r == null)
            {
                retorno.codigo = 1;
                retorno.mensagem = "Não foram identificados códigos de barra";
            } else
            {
                retorno.codigo = 0;
                retorno.mensagem = r.Text.ToString();
            }

            return retorno;
        }
    }
}
