using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSaberSongManager
{
    public class DifficultyLevel
    {
        public string difficulty { get; set; }
        public int difficultyRank { get; set; }
        public string audioPath { get; set; }
        public string jsonPath { get; set; }
        public int offset { get; set; }
        public int oldOffset { get; set; }
        public string chromaToggle { get; set; }
        public bool customColors { get; set; }
        public string characteristic { get; set; }
        public string difficultyLabel { get; set; }
    }

    public class Song : IEquatable<Song>
    {
        public string songName { get; set; }
        public string songSubName { get; set; }
        public string authorName { get; set; }
        internal List<object> contributors { get; set; }
        internal double beatsPerMinute { get; set; }
        internal double previewStartTime { get; set; }
        internal double previewDuration { get; set; }
        internal string coverImagePath { get; set; }
        internal string environmentName { get; set; }
        internal bool oneSaber { get; set; }
        internal string customEnvironment { get; set; }
        internal string customEnvironmentHash { get; set; }
        internal List<DifficultyLevel> difficultyLevels { get; set; }
        internal string path { get; set; }
        public bool isSelected { get; set; }
        public bool isProtected { get; set; }

        public bool Equals(Song other)
        {
            return songName == other.songName &&
                    songSubName == other.songSubName &&
                    authorName == other.authorName;
        }
        public static bool operator==(Song song1, Song song2)
        {

            return song1.songName == song2.songName &&
                    song1.songSubName == song2.songSubName &&
                    song1.authorName == song2.authorName;
        }
        public static bool operator !=(Song song1, Song song2)
        {
            return song1.songName != song2.songName ||
                    song1.songSubName != song2.songSubName ||
                    song1.authorName != song2.authorName;
        }
    }
}
