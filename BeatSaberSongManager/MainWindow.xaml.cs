﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeatSaberSongManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Song> SongList { get; set; } = new List<Song>();
        public MainWindow()
        {
            InitializeComponent();
            UpdateSongCollection();
            Console.WriteLine("Done");
        }

        private void DeleteButtonOnClick(object sender, RoutedEventArgs e)
        {
            var songsToDelete = new List<string>();
            foreach (var song in SongList)
            {
                if (song.isSelected)
                {
                    if (!song.isProtected)
                    {
                        var dir = System.IO.Directory.GetParent(System.IO.Directory.GetParent(song.path).ToString());
                        Console.WriteLine(dir);
                        songsToDelete.Add(dir.ToString());
                    }
                }
            }
            if (MessageBox.Show(string.Join(Environment.NewLine, songsToDelete), "Are you sure you want to delete these folders?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var song in songsToDelete)
                {
                    System.IO.Directory.Delete(song, true);
                    Console.WriteLine($"Deleted {song}");
                }
                //delete
            }
        }
        private void UpdateSongCollection()
        {
            SongList = new List<Song>();
            var fileList = System.IO.Directory.GetFiles(@"C:\Program Files (x86)\Steam\steamapps\common\Beat Saber\CustomSongs", "info.json", SearchOption.AllDirectories);
            foreach (var file in fileList)
            {
                using (var sr = new StreamReader(file))
                {
                    var songInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<Song>(sr.ReadToEnd());
                    songInfo.path = file;
                    SongList.Add(songInfo);
                }
            }

            if (System.IO.File.Exists("songcache.json"))
            {
                List<Song> cachedList = null;
                using (var sr = new StreamReader("songcache.json"))
                {
                    cachedList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Song>>(sr.ReadToEnd());
                }
                foreach (var song in cachedList)
                {
                    foreach (var newSong in SongList)
                    {
                        if (song == newSong)
                        {
                            newSong.isProtected = song.isProtected;
                            break;
                        }
                    }
                }
            }
            dataGrid.ItemsSource = SongList;
        }

        private void UpdateButtonOnClick(object sender, RoutedEventArgs e)
        {
            UpdateSongCollection();
        }

        private void SaveButtonOnClick(object sender, RoutedEventArgs e)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(SongList);
            System.IO.File.WriteAllText("songcache.json", json);
        }

        private void SelectAllButtonOnClick(object sender, RoutedEventArgs e)
        {
            foreach (var song in SongList)
            {
                if (!song.isProtected)
                {
                    song.isSelected = true;
                }
            }
            dataGrid.ItemsSource = SongList;
        }
    }
}
