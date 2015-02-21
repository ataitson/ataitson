using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Substitua pelo endereço local da imagem.
            Bitmap temp = new Bitmap(@"C:\Users\Alexandre\Downloads\testImage.bmp");
            Response.Write(extractText(temp));
        }

        public static string extractText(Bitmap bmp)
        {
            int pixelId = 0;
            string texto = "";
            string stringTemp = "";

            string textoFinal = String.Empty;
                // For dentro de For para varrer as linhas e colunas, iniciando do Pixel 0 da ultima linha.
                for (int i = 333; i > 0; i--)
                {
                    for (int j = 0; j < bmp.Width; j++)
                    {
                        Color pixel = bmp.GetPixel(j, i);
                        for (int n = 0; n < 3; n++)
                        {
                            switch (pixelId % 3)
                            {
                                //Ordem invertida do RGB para esteganografia - BGR
                                case 0:
                                    {
                                        int binaryValue = pixel.B;
                                        texto = texto + Convert.ToString(binaryValue, 2) + " ";
                                        stringTemp = texto.Substring(texto.Length - 2).Replace(" ", "") + stringTemp;
                                    } break;
                                case 1:
                                    {
                                        int binaryValue = pixel.G;
                                        texto = texto + Convert.ToString(binaryValue, 2) + " ";
                                        stringTemp = texto.Substring(texto.Length - 2).Replace(" ", "") + stringTemp;
                                    } break;
                                case 2:
                                    {
                                        int binaryValue = pixel.R;
                                        texto = texto + Convert.ToString(binaryValue, 2) + " ";
                                        stringTemp = texto.Substring(texto.Length - 2).Replace(" ", "") + stringTemp;
                                    } break;
                            }

                            if (stringTemp.Length == 8)
                            {
                                int output = Convert.ToInt32(stringTemp, 2);
                                if (output > 32 && output < 128)
                                {
                                    char char2 = Convert.ToChar(output);
                                    textoFinal = textoFinal + char2;
                                }
                                else
                                {
                                    if (stringTemp.Equals("00000000"))
                                    { i = 0; j = 500; }
                                }
                                stringTemp = "";
                            }
                            pixelId++;
                        }
                    }
                }
            return textoFinal;
        }
    } 
}
