using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Class_261020
{
    class Movie: IContent
    {
        private Video video;

        public string Title { get => getTitle(); }
        public int Length { get => getLength();}
        public string ContentType { get => "Movie"; }
        public Video Video { get => video; set => video = value; }
        public Movie(string title, int length)
        {
            this.video = new Video(title, length);
            
        }

        public string getTitle()
        {
            return this.video.Title;
        }

        public int getLength()
        {
            return this.video.Length;
        }

    }
}
