using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Utilitarios.Imagens
{
    /// <summary>
    /// Classe para manipular imagens
    /// </summary>
    public class ManipularImagens
    {

        public void GerarImagensPMG(string pathImagemServ, Size p, Size m, Size g)
        {
            FileInfo file = new FileInfo(pathImagemServ);
            if (file.Exists)
            {
                this.RedimensionarImagem(file.FullName, this.RetornarNomeArquivo(file, "P"), p);
                this.RedimensionarImagem(file.FullName, this.RetornarNomeArquivo(file, "M"), m);
                this.RedimensionarImagem(file.FullName, this.RetornarNomeArquivo(file, "G"), g);
                System.Drawing.Image image = System.Drawing.Image.FromFile(file.FullName);
                this.RedimensionarImagem(file.FullName, this.RetornarNomeArquivo(file, "O"), image.Size);
                if (image.Size.Width > 640 || image.Size.Height > 480)
                {
                    image.Dispose();

                    string path = file.FullName;
                    string name = file.Name;
                    string extensao = file.Extension;

                    this.RedimensionarImagem(file.FullName, this.RetornarNomeArquivo(file, "_Temp_000954376_temp"), new Size(640, 480));

                    file.Delete();

                    FileInfo fileNovo = new FileInfo(this.RetornarNomeArquivo(path, "_Temp_000954376_temp", name, extensao));
                    fileNovo.MoveTo(fileNovo.FullName.Replace("__Temp_000954376_temp", ""));
                }
                else
                {
                    image.Dispose();
                }
            }
        }

        public string RetornarNomeArquivo(FileInfo image, string TipoPMG)
        {
            string patPasta = image.FullName.Substring(0, image.FullName.LastIndexOf("\\"));
            return patPasta + "\\" + image.Name.Substring(0, image.Name.LastIndexOf('.')) + "_" + TipoPMG + image.Extension;
        }

        public string RetornarNomeArquivo(string path, string TipoPMG, string name, string extensao)
        {
            string patPasta = path.Substring(0, path.LastIndexOf("\\"));
            return patPasta + "\\" + name.Substring(0, name.LastIndexOf('.')) + "_" + TipoPMG + extensao;
        }

        public void RedimensionarImagem(string pathImagemEntrada, string pathImagemSaida, Size size)
        {
            DirectoryInfo dir = new DirectoryInfo(pathImagemSaida.Substring(0, pathImagemSaida.LastIndexOf("\\")));
            if (!dir.Exists)
                dir.Create();

            FileInfo file = new FileInfo(pathImagemEntrada);
            if (file.Exists)
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(pathImagemEntrada);
                Image im = this.ResizeImage(ref image, size);
                im.Save(pathImagemSaida, ImageFormat.Jpeg);
                im.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// Resize image
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Image ResizeImage(ref Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentW > 1)
                nPercentW = 1;

            if (nPercentH > 1)
                nPercentH = 1;

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            b.SetResolution(100, 100);


            Graphics g = Graphics.FromImage(b);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            int l = imgToResize.Size.Width;
            int a = imgToResize.Size.Height;
            g.DrawImage(imgToResize, new Rectangle(0, 0, destWidth, destHeight), 0, 0, l, a, GraphicsUnit.Pixel);

            g.Dispose();

            return (Image)b;
        }
    }
}