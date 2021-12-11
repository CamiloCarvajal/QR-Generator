using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodigoQR
{
    public partial class gencode : Form
    {
        public gencode()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
        }

        
        private void generar_Click(object sender, EventArgs e)
        {
           
                    QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
                    QrCode qrCode = new QrCode();
                    qrEncoder.TryEncode(tbValor.Text, out qrCode);
                    GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(500, QuietZoneModules.Zero), Brushes.Black, Brushes.White);

                    MemoryStream ms = new MemoryStream();
                    renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
                    var imageTemporal = new Bitmap(ms);
                    var imagen = new Bitmap(imageTemporal, new Size(new Point(240, 240)));
                    panelResultado.BackgroundImage = imagen;

            //imagen.Save("imagen.png", ImageFormat.Png);
            guardar.Enabled = true;
        }

        private void guardar_Click(object sender, EventArgs e)
        {
            Image imgFinal = (Image)panelResultado.BackgroundImage.Clone();

            SaveFileDialog CajaDeDiaologoGuardar = new SaveFileDialog();
            CajaDeDiaologoGuardar.AddExtension = true;
            CajaDeDiaologoGuardar.Filter = "Image PNG (*.png)|*.png";
            CajaDeDiaologoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(CajaDeDiaologoGuardar.FileName))
            {
                imgFinal.Save(CajaDeDiaologoGuardar.FileName, ImageFormat.Png);
            }
            imgFinal.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbValor.Clear();
            tbValor.Focus();
        }
    }
}
