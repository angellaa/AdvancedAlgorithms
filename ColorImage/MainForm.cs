using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ColorImage.Properties;
using ConnectivityProblem;

namespace ColorImage
{
    public partial class MainForm : Form
    {
        private WeightedQuickUnionPathCompression finder;

        private readonly Bitmap bitmap;
        private readonly int bitmapWidth;
        private readonly int bitmapHeight;
        private readonly int sensitivity;

        const int PixelSize = 4;

        private bool buildCompleted;

        public MainForm()
        {
            InitializeComponent();

            bitmap = (Bitmap)pbImage.Image;
            bitmapWidth = bitmap.Width;
            bitmapHeight = bitmap.Height;
            sensitivity = tbSensitivity.Value;

            Task.Run(() => BuildConnectivity());
        }

        // More details for low level bitmap manipulation: http://bobpowell.net/lockingbits.aspx
        private void BuildConnectivity()
        {
            finder = new WeightedQuickUnionPathCompression(bitmapWidth * bitmapHeight);

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmapWidth, bitmapHeight),
                                             ImageLockMode.ReadWrite,
                                             bitmap.PixelFormat);

            unsafe
            {
                for (int y = 1; y < bitmapData.Height - 2; y++)
                {
                    byte* rowUp = (byte*)bitmapData.Scan0 + ((y - 1) * bitmapData.Stride);
                    byte* row = (byte*)bitmapData.Scan0 + (y * bitmapData.Stride);
                    byte* rowDown = (byte*)bitmapData.Scan0 + ((y + 1) * bitmapData.Stride);

                    for (int x = 1; x < bitmapData.Width - 1; x++)
                    {
                        int* value = (int*)(row + x * PixelSize);
                        int* valueUp = (int*)(rowUp + x * PixelSize);
                        int* valueDown = (int*)(rowDown + x * PixelSize);
                        int* valueLeft = (int*)(row + (x - 1) * PixelSize);
                        int* valueRight = (int*)(row + (x + 1) * PixelSize);
                        
                        // Connect pixels that have a similar color (based on sensitivity)
                        if (Math.Abs(*value - *valueUp) < sensitivity)    finder.Connect(x + y * bitmapWidth, x + (y - 1) * bitmapWidth);
                        if (Math.Abs(*value - *valueDown) < sensitivity)  finder.Connect(x + y * bitmapWidth, x + (y + 1) * bitmapWidth);
                        if (Math.Abs(*value - *valueLeft) < sensitivity)  finder.Connect(x + y * bitmapWidth, (x - 1) + y * bitmapWidth);
                        if (Math.Abs(*value - *valueRight) < sensitivity) finder.Connect(x + y * bitmapWidth, (x + 1) + y * bitmapWidth);
                    }
                }
            }

            bitmap.UnlockBits(bitmapData);
            buildCompleted = true;
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pbColor.BackColor = colorDialog1.Color;
            }           
        }

        private void pbImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (!buildCompleted) return;

            // Convert mouse coordinates to bitmap coordinates
            int bx = (int)(e.X / (double)pbImage.Width * bitmapWidth);
            int by = (int)(e.Y / (double)pbImage.Height * bitmapHeight);

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmapWidth, bitmapHeight),
                                             ImageLockMode.WriteOnly,
                                             bitmap.PixelFormat);

            unsafe
            {
                int newColor = pbColor.BackColor.ToArgb();

                finder.ForEachConnected(bx + by * bitmapWidth, p =>
                {
                    // Convert from finder point to bitmap coordinates
                    int x = p % bitmapWidth;
                    int y = p / bitmapWidth;

                    byte* row = (byte*)bitmapData.Scan0 + (y * bitmapData.Stride);
                    int* v = (int*)(row + x * PixelSize);

                    // Set the color
                    *v = newColor;
                });
            }

            bitmap.UnlockBits(bitmapData);           
            pbImage.Refresh();
        }
    }
}
