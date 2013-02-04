using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
namespace TicketHelper.Test
{
    public class BinaryTest
    {

        public static void main()
        {
            Bitmap bufferedImage = (Bitmap)Bitmap.FromFile("code.gif");
            int h = bufferedImage.Height;
            int w = bufferedImage.Width;

            #region  // 灰度化
            int[,] gray = new int[w, h];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    int argb = bufferedImage.GetPixel(x, y).ToArgb();
                    // 图像加亮（调整亮度识别率非常高）
                    int r = (int)(((argb >> 16) & 0xFF) * 1.1 + 30);
                    int g = (int)(((argb >> 8) & 0xFF) * 1.1 + 30);
                    int b = (int)(((argb >> 0) & 0xFF) * 1.1 + 30);
                    if (r >= 255)
                    {
                        r = 255;
                    }
                    if (g >= 255)
                    {
                        g = 255;
                    }
                    if (b >= 255)
                    {
                        b = 255;
                    }
                    gray[x, y] = (int)Math.Pow((Math.Pow(r, 2.2) * 0.2973 + Math.Pow(g, 2.2) * 0.6274 + Math.Pow(b, 2.2) * 0.0753), 1 / 2.2);
                }
            }

            #endregion
            Bitmap vimg = new Bitmap(w, h);
            // 二值化
            int threshold = ostu(gray, w, h);
            BitmapData dataOut = vimg.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            // BufferedImage binaryBufferedImage = new BufferedImage(w, h, BufferedImage.TYPE_BYTE_BINARY);
            unsafe
            {
                byte* pOut = (byte*)(dataOut.Scan0.ToPointer());
                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        if (gray[x, y] > threshold)
                        {
                            gray[x, y] |= 0x00FFFF;
                        }
                        else
                        {
                            gray[x, y] &= 0xFF0000;
                        }
                        //binaryBufferedImage.s(x, y, gray[x, y]);
                    }
                }
            }
            vimg.UnlockBits(dataOut);
            vimg.Save("aa.gif");
            //// 矩阵打印
            //for (int y = 0; y < h; y++) {
            //    for (int x = 0; x < w; x++) {
            //        if (isBlack(binaryBufferedImage.getRGB(x, y))) {
            //            System.out.print("*");
            //        } else {
            //            System.out.print(" ");
            //        }
            //    }
            //    System.out.println();
            //}

            //ImageIO.write(binaryBufferedImage, "jpg", new File("D:/code.jpg"));

        }

        public static bool isBlack(int colorInt)
        {
            Color color = Color.FromArgb(colorInt);
            if (color.R + color.G + color.B <= 300)
            {
                return true;
            }
            return false;
        }

        public static bool isWhite(int colorInt)
        {
            Color color = Color.FromArgb(colorInt);
            if (color.R + color.G + color.B > 300)
            {
                return true;
            }
            return false;
        }

        public static int isBlackOrWhite(int colorInt)
        {
            if (getColorBright(colorInt) < 30 || getColorBright(colorInt) > 730)
            {
                return 1;
            }
            return 0;
        }

        public static int getColorBright(int colorInt)
        {
            Color color = Color.FromArgb(colorInt);
            return color.R + color.G + color.B;
        }

        public static int ostu(int[,] gray, int w, int h)
        {
            int[] histData = new int[w * h];
            // Calculate histogram
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    int red = 0xFF & gray[x, y];
                    histData[red]++;
                }
            }

            // Total number of pixels
            int total = w * h;

            float sum = 0;
            for (int t = 0; t < 256; t++)
                sum += t * histData[t];

            float sumB = 0;
            int wB = 0;
            int wF = 0;

            float varMax = 0;
            int threshold = 0;

            for (int t = 0; t < 256; t++)
            {
                wB += histData[t]; // Weight Background
                if (wB == 0)
                    continue;

                wF = total - wB; // Weight Foreground
                if (wF == 0)
                    break;

                sumB += (float)(t * histData[t]);

                float mB = sumB / wB; // Mean Background
                float mF = (sum - sumB) / wF; // Mean Foreground

                // Calculate Between Class Variance
                float varBetween = (float)wB * (float)wF * (mB - mF) * (mB - mF);

                // Check if new maximum found
                if (varBetween > varMax)
                {
                    varMax = varBetween;
                    threshold = t;
                }
            }

            return threshold;
        }
    }
}
