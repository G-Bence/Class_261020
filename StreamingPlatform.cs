using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Class_261020
{
    class StreamingPlatform: IContent
    {
        private List<IContent> contents = new List<IContent>();

        public List<IContent> Contents { get => contents; set => contents = value; }

        public void addContent(IContent content)
        {
            this.contents.Add(content);
        }

        public void loadFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);
            List<IContent> loadedContents = new List<IContent>();

            foreach (var item in lines)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    string[] data = item.Split('|');
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = data[i].Trim();
                    }

                    if (data.Length == 2 && !string.IsNullOrEmpty(data[0]) && !string.IsNullOrEmpty(data[1]))
                    {
                        loadedContents.Add(new Movie(data[0], Convert.ToInt32(data[1])));
                    }

                    else if (data[1].Equals("Movie") && data.Length == 3)
                    {
                        loadedContents.Add(new Movie(data[0], Convert.ToInt32(data[2])));
                    }
                    else if (data[1].Equals("Series") && data.Length >= 4 && data.Length % 2 == 0)
                    {
                        List<Video> episodes = new List<Video>();
                        for (int i = 2; i < data.Length; i += 2)
                        {
                            episodes.Add(new Video(data[i], Convert.ToInt32(data[i + 1])));
                        }
                        loadedContents.Add(new Series(data[0], episodes));
                    }
                }
            }

            this.contents = loadedContents;
        }

        public List<IContent> filterContents(string search)
        {
            string filteredSearch = search.ToLower().Trim();
            List<IContent> filteredContents = new List<IContent>();
            foreach (var content in this.contents)
            {
                if(string.IsNullOrEmpty(filteredSearch))
                {
                    filteredContents.Add(content);
                }
                else if (content.getTitle().ToLower().Contains(filteredSearch))
                {
                    filteredContents.Add(content);
                }
            }
            return filteredContents;
        }

        public IContent getLongestContent()
        {
            IContent longest = contents[0];
            foreach (var item in contents)
            {
                if (item.getLength() > longest.getLength())
                {
                    longest = item;
                }

            }

            return longest;
        }
            
        public int getLength()
        {
            throw new NotImplementedException();
        }

        public string getTitle()
        {
            throw new NotImplementedException();
        }
    }
}
