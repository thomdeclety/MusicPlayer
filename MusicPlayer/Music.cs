using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WMPLib;

namespace MusicPlayer
{
    class Music
    {
        public static List<string> Playlist { get; set; } = new List<string>();
        public static bool ON { get; set; } = false;
        public static bool First { get; set; } = true;
        public static WindowsMediaPlayer MusicPlayer { get; set; } = new WindowsMediaPlayer();
        public static int Current { get; set; } = 0;
        public static Stopwatch Watch { get; set; } = new Stopwatch();

        public static void FillPlaylist()
        {
            string path = @"D:\drive\Musique";

            foreach (string file in System.IO.Directory.GetFiles(path))
            {
                if (System.IO.Path.GetExtension(file) == ".mp3"
                    || System.IO.Path.GetExtension(file) == ".m4a"
                    || System.IO.Path.GetExtension(file) == ".wav"
                    || System.IO.Path.GetExtension(file) == ".ogg")
                {
                    Playlist.Add(file);
                }
            }

            Playlist.Sort();
        }

        public static void PlayOrPause()
        {
            if (First)
            {
                MusicPlayer.URL = Playlist[Screen.Cursor];
                MusicPlayer.controls.play();
                First = false;
                ON = true;
                Current = Screen.Cursor;
                Screen.PlayIcon = 'H';
                Watch.Start();
            }
            else
            {
                if (Current == Screen.Cursor)
                {
                    if (ON)
                    {
                        MusicPlayer.controls.pause();
                        ON = false;
                        Screen.PlayIcon = '>';
                        Current = Screen.Cursor;
                        Watch.Stop();
                    }
                    else if (!ON)
                    {
                        MusicPlayer.controls.play();
                        ON = true;
                        Screen.PlayIcon = 'H';
                        Current = Screen.Cursor;
                        Watch.Start();
                    }
                }
                else
                {
                    MusicPlayer.URL = Playlist[Screen.Cursor];
                    MusicPlayer.controls.play();
                    ON = true;
                    Screen.PlayIcon = 'H';
                    Current = Screen.Cursor;
                    Watch.Restart();
                }
            }
            Screen.Print();
        }

        public static void PlayNext()
        {
            Screen.Cursor++;
            MusicPlayer.URL = Playlist[Screen.Cursor];
            MusicPlayer.controls.play();
            Screen.PlayIcon = 'H';
            Current = Screen.Cursor;
            Screen.Print();
            Watch.Restart();
        }

        public static void DisplayMusicCount()
        {
            while(true)
            {
                Thread.Sleep(1000);
                if(ON && Watch.ElapsedMilliseconds / 1000 > MusicPlayer.currentMedia.duration)
                {
                    PlayNext();
                }
            }
        }
    }
}
