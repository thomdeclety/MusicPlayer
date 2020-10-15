using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    class Screen
    {
        public static int X { get; set; } = 41;
        public static int Y { get; set; } = 21;
        public static char[,] Frame { get; set; } = new char[X, Y];
        public static int Cursor { get; set; } = 0;
        public static char PlayIcon { get; set; } = '>';

        public static void Init()
        {
            for (int i = 0; i < Y; i++)
            {
                for (int j = 0; j < X; j++)
                {
                    if(i == 3 || i == 7 || i == 11 || i == 15)
                    {
                        Frame[j, i] = '-';
                    }
                    else if (j == 2 && i < 19)
                    {
                        Frame[j, i] = '|';
                    }
                    else
                    {
                        Frame[j, i] = ' ';
                    }
                }
            }
        }

        public static void Print()
        {
            Console.Clear();
            Showlist();

            for (int i = 0; i < Y; i++)
            {
                for (int j = 0; j < X; j++)
                {
                    Console.Write(Frame[j, i]);
                }
                if (i< Y-1)
                {
                    Console.WriteLine();
                }
            }
        }

        public static void Showlist()
        {
            for (int i = 0; i < 5; i++)
            {
                Frame[0, 1 + 4 * i] = ' ';
            }

            for (int i = 0; i < X; i++)
            {
                Frame[i, 20] = ' ';
            }

            int start = 0;

            if(Cursor < 2)
            {
                if(Cursor == 0)
                {
                    Frame[0, 1] = PlayIcon;
                }
                else if(Cursor == 1)
                {
                    Frame[0, 5] = PlayIcon;
                }
                start = 0;
            }
            else if(Cursor > Music.Playlist.Count - 3)
            {
                if (Cursor == Music.Playlist.Count - 2)
                {
                    Frame[0, 13] = PlayIcon;
                }
                else if (Cursor == Music.Playlist.Count - 1)
                {
                    Frame[0, 17] = PlayIcon;
                }
                start = Music.Playlist.Count - 5;
            }
            else
            {
                Frame[0, 9] = PlayIcon;
                start = Cursor - 2;
            }

            for (int i = 0; i < 5; i++)
            {
                FillTitles(start + i, i);
            }
            if(!Music.First && Music.MusicPlayer.currentMedia.duration > 0)
            {
                FillTimer();
            }
        }

        public static void FillTitles(int start, int compt)
        {
            string music = Music.Playlist[start].Substring(17);

            for (int i = 0; i < 36; i++)
            {
                Frame[4 + i, 1 + 4 * compt] = ' ';
            }

            if (music.Length > 36)
            {
                for (int i = 0; i < 36; i++)
                {
                    Frame[4 + i, 1 + 4 * compt] = music[i];
                }
            }
            else
            {
                for (int i = 0; i < music.Length; i++)
                {
                    Frame[4 + i, 1 + 4 * compt] = music[i];
                }
            }
        }

        public static void FillTimer()
        {
            string duration = "";

            int sec = (int)Music.Watch.ElapsedMilliseconds / 1000;
            string temp = sec.ToString();
            string temp2 = "";

            if (temp.Length < 2)
            {
                temp2 = "00:0" + temp;
                duration = temp2;
            }
            else if(sec < 60)
            {
                temp2 = "00:" + temp;
                duration = temp2;
            }
            else
            {
                string t = (sec / 60).ToString();
                if(t.Length < 2)
                {
                    t = "0" + (sec / 60).ToString();
                }
                string t2 = (sec % 60).ToString();
                if (t2.Length < 2)
                {
                    t2 = "0" + (sec % 60).ToString();
                }
                duration = t + ":" + t2;
            }

            duration += " / " + Music.MusicPlayer.currentMedia.durationString;

            for (int i = 0; i < duration.Length; i++)
            {
                Frame[i, 19] = duration[i];
            }

            double percent = sec / Music.MusicPlayer.currentMedia.duration * 41;
            if(percent == 0)
            {
                percent = 1;
            }
            if(percent > 41)
            {
                percent = 41;
            }

            for (int i = 0; i < percent; i++)
            {
                Frame[i, 20] = '>';
            }
        }
    }
}
