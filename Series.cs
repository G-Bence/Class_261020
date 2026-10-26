using System;
using System.Collections.Generic;
using System.Text;

namespace Class_261020
{
    class Series: IContent
    {
        private string title;
        private List<Video> episodes;

        public List<Video> Episodes { get => episodes; set => episodes = value; }
        public string Title { get => title; set => title = value; }
        public int Length { get => getLength(); }
        public string ContentType { get => "Series"; }

        public Series(string title, List<Video> episodes)
        {
            this.title = title;
            this.episodes = episodes;
        }


        public string getTitle()
        {
            return this.title;
        }

        public int getLength()
        {
            int length = 0;
            foreach (var episode in this.episodes)
            {
                length += episode.Length;
            }
            return length;
        }
    }
}
