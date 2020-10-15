using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Screen.X+1, Screen.Y);
            Console.Title = "Music Player";
            Music.FillPlaylist();
            Screen.Init();
            Screen.Print();

            Parallel.Invoke(
            // THREAD 1
            () =>
            {
                GetKeyPressed();
            },

            // THREAD 2
            () =>
            {
                Music.DisplayMusicCount();
            },

            // THREAD 3
            () =>
            {
                while(true)
                {
                    Screen.Print();
                    Thread.Sleep(1000);
                }
            }
            );
        }


        public static void GetKeyPressed()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(false);

                if (key.Key == ConsoleKey.DownArrow && Screen.Cursor < Music.Playlist.Count - 1)
                {
                    Screen.Cursor++;
                    Screen.Print();
                }
                else if (key.Key == ConsoleKey.UpArrow && Screen.Cursor > 0)
                {
                    Screen.Cursor--;
                    Screen.Print();
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    Music.PlayOrPause();
                }
            }
        }
    }
}
