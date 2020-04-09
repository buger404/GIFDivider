using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIFDivider
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("please drop file(s) to the .exe file");
                Console.ReadLine();
                return;
            }
            if (!Directory.Exists("D:\\GIFDivider")) Directory.CreateDirectory("D:\\GIFDivider");
            foreach(string file in args)
            {
                Console.WriteLine("convert : " + file);
                Bitmap b = new Bitmap(file);
                Bitmap o = new Bitmap(b.Width, b.Height);
                Graphics g = Graphics.FromImage(o);
                Guid guid = (Guid)b.FrameDimensionsList.GetValue(0);
                FrameDimension dimension = new FrameDimension(guid);

                int ticks = b.GetFrameCount(dimension);
                int ws = 0;int ttick = ticks;
                do
                {
                    ws++;
                    ttick /= 10;
                } while (ttick > 0);
                string[] temp = file.Split('\\');
                string path = "D:\\GIFDivider\\" + temp[temp.Length - 1];
                Console.WriteLine(ticks + " frames found , created :" + path);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                for (int i = 0;i < ticks; i++)
                {
                    b.SelectActiveFrame(dimension, i);
                    g.Clear(Color.Transparent);
                    g.DrawImage(b, 0, 0);
                    o.Save(path + "\\tick_" + FixLength(i.ToString(),ws) + ".png");
                    Console.WriteLine("Outputed (" + i + ") : " + path + "\\tick_" + FixLength(i.ToString(), ws) + ".png");
                }
                g.Dispose(); b.Dispose();o.Dispose();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ReadLine();
            return;
        }
        public static string FixLength(string num,int ws)
        {
            string ret = num;
            for(int i = num.Length;i < ws; i++)
            {
                ret = "0" + ret;
            }
            return ret;
        }
    }
}
