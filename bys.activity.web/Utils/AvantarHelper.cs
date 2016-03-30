using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace bys.activity.web.Utils
{
    public class AvantarHelper
    {
        private static System.Text.RegularExpressions.Regex CHARS_REGEX = new System.Text.RegularExpressions.Regex(@"(\w)(\w)", System.Text.RegularExpressions.RegexOptions.Compiled);
        private static List<Color> COLORS = new List<Color> {
                                                 Color.FromArgb(196, 108, 168),
                                                 Color.FromArgb(127, 219, 107),
                                                 Color.FromArgb(83, 89, 215),
                                                 Color.FromArgb(111, 198, 148),
                                                 Color.FromArgb(186, 130, 82),
                                                 Color.FromArgb(113, 66, 197),
                                                 Color.FromArgb(201, 186, 84),
                                                 Color.FromArgb(114, 153, 205),
                                                 Color.FromArgb(141, 108, 204),
                                                 Color.FromArgb(75, 183, 114),
                                                 Color.FromArgb(192, 86, 82),
                                                 Color.FromArgb(199, 115, 209),
                                                 Color.FromArgb(206, 77, 94),
                                                 Color.FromArgb(211, 135, 162),
                                                 Color.FromArgb(71, 194, 197),
                                                 Color.FromArgb(188, 110, 93)
                                            };

        public static string SaveAvantarImage(string alias) {
            var img = GenerateDefaultAvatar(alias, 80);
            // save the image
            var relativePath = $"/Images/Avantar/{alias}.png";
            img.Save(HttpContext.Current.Server.MapPath("~/"+relativePath));
            return relativePath;
        }

        public static Bitmap GenerateDefaultAvatar(string data, int size = 420, string bgcolor = "")
        {
            // hash the given data
            var hash = GetMd5Hash(data);

            // using regex to find out valid chars
            var charMatchs = CHARS_REGEX.Matches(hash);

            // init attrs
            var ini_color = Color.FromArgb(0, 0, 0);
            var ini_background = GetBgColor(bgcolor);
            var pixel_ratio = (int)Math.Round((double)size / 6);
            var square_array = new System.Collections.Hashtable();

            // convert hash
            ConvertHash(charMatchs, square_array, out ini_color);

            // final step, generate the img
            return InnerCreateGravatar(size, size, ini_background, ini_color, pixel_ratio, square_array);
        }

        private static Bitmap InnerCreateGravatar(int width, int height, Color bgcolor, Color blockcolor, int blocksize, System.Collections.Hashtable hashtable)
        {
            Bitmap img = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            using (Graphics ps = Graphics.FromImage(img))
            {
                ps.Clear(bgcolor);
                foreach (System.Collections.DictionaryEntry line in hashtable)
                {
                    var outerkey = (int)line.Key;
                    var innerhashtable = line.Value as System.Collections.Hashtable;
                    foreach (System.Collections.DictionaryEntry innerline in innerhashtable)
                    {
                        var innerkey = (int)innerline.Key;
                        if ((bool)innerline.Value == true)
                        {
                            ps.FillRegion(new SolidBrush(blockcolor), new Region(new Rectangle(innerkey * blocksize + (blocksize / 2), (outerkey * blocksize) + (blocksize / 2),
                                (blocksize), (blocksize))));
                        }
                        else
                        {
                            ps.FillRegion(new SolidBrush(bgcolor), new Region(new Rectangle(innerkey * blocksize + (blocksize / 2), (outerkey * blocksize) + (blocksize / 2),
                               (blocksize), (blocksize))));
                        }
                    }
                }
            }

            return img;
        }

        private static Color GetBgColor(string bgcolor)
        {
            var defaultcolor = Color.FromArgb(240, 240, 240);
            var color = defaultcolor;

            if (!string.IsNullOrEmpty(bgcolor))
            {
                try
                {
                    if (bgcolor.StartsWith("#"))
                    {
                        color = (Color)(new ColorConverter().ConvertFromString(bgcolor));
                    }
                    else
                    {
                        var csplits = bgcolor.Trim().Split(',');
                        color = Color.FromArgb(int.Parse(csplits[0]), int.Parse(csplits[1]), int.Parse(csplits[2]));
                    }
                }
                catch
                {
                    color = defaultcolor;
                }
            }

            return color;
        }

        private static void ConvertHash(System.Text.RegularExpressions.MatchCollection matchs, System.Collections.Hashtable hashtable, out Color ini_color)
        {
            var chars = new List<char>();
            foreach (System.Text.RegularExpressions.Match m in matchs)
            {
                chars.Add(m.Value[1]);
            }

            for (var i = 0; i < chars.Count - 1; i++)
            {
                var ch = chars[i];
                var sqindex = i / 3;
                if (!hashtable.ContainsKey(sqindex))
                {
                    hashtable[i / 3] = new System.Collections.Hashtable();
                }
                if (i % 3 == 0) // mirror, nice trick!
                {
                    ((System.Collections.Hashtable)hashtable[i / 3])[0] = H2B(ch);
                    ((System.Collections.Hashtable)hashtable[i / 3])[4] = H2B(ch);
                }
                else if (i % 3 == 1)
                {
                    ((System.Collections.Hashtable)hashtable[i / 3])[1] = H2B(ch);
                    ((System.Collections.Hashtable)hashtable[i / 3])[3] = H2B(ch);
                }
                else
                {
                    ((System.Collections.Hashtable)hashtable[i / 3])[2] = H2B(ch);
                }
            }

            // pick a random color
            var randomindex = new Random().Next(0, 16);
            var tmp_color = COLORS[randomindex];
            ini_color = tmp_color;
        }

        private static bool H2B(char ch)
        {
            return System.Text.Encoding.UTF8.GetBytes(new char[] { ch }).ToList()[0] % 2 == 0;
        }

        private static string GetMd5Hash(string input)
        {
            using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }


    }
}