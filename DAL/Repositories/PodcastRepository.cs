﻿using DAL.Serialize;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL.Repositories
{
    public class PodcastRepository : Feed<Podcast>, IDataHandler<Podcast>
    {
        public PodcastRepository() : base(new List<Podcast>())
        {
            objectSerializer = new XMLSerializer<Podcast>();
        }
        public override void Create(Podcast entity)
        {
            base.Create(entity);
            Console.WriteLine("Length: " + list.Count);
        }
        public override void Delete(Podcast entity)
        {
            base.Delete(entity);
        }
        public override void Delete(int index)
        {
            base.Delete(index);
        }
        public override List<Podcast> GetAll()
        {
            return base.GetAll();
        }
        public void Update(string URL, string newName, int interval, Category category, int index)
        {
            list[index].URL = URL;
            list[index].name = newName;
            list[index].category = category;
            list[index].updatingInterval = interval;
        }
        public async Task Update(List<Podcast> podcasts, IProgress<int> progress, ListBox console)
        {
            List<List<Episode>> episodes = new List<List<Episode>>();
            List<Episode> updatedEpisodes = new List<Episode>();
            foreach(Podcast podcast in podcasts)
            {
                updatedEpisodes = await Task.Run (() => reader.GetEpisodes(podcast.URL, progress));
                episodes.Add(updatedEpisodes);
                console.Items.Add($"{podcast.URL} downloaded: {updatedEpisodes.Count} characters long.\n");
                progress.Report(episodes.Count * 100 / podcasts.Count);
                foreach(Podcast aPodcast in GetAll())
                {
                    if (aPodcast.URL.Equals(podcast.URL))
                    {
                        aPodcast.episodes = updatedEpisodes;
                        aPodcast.amountOfEpisodes = reader.GetAmountOfEpisodes(podcast.URL);
                    }
                }
                SaveChanges();
                Console.WriteLine("Name: " + podcast.name + "Interval " + podcast.updatingInterval);
            }
        }
        public async void Update(string URL, IProgress<int> progress)
        {
            List<Episode> updatedEpisodes = await reader.GetEpisodes(URL, progress);

            foreach (Podcast podcast in GetAll())
            {
                if (podcast.URL.Equals(URL))
                {
                    podcast.episodes = updatedEpisodes;
                    podcast.amountOfEpisodes = reader.GetAmountOfEpisodes(URL);
                }
            }
        }

        public void Update(string currentCategory, string newCategory)
        {
            foreach(Podcast podcast in list)
            {
                if (podcast.category.namn.Equals(currentCategory))
                {
                    podcast.category.namn = newCategory;
                }
            }
        }

        public void SaveChanges()
        {
            foreach(Podcast podcast in list)
            {
                Console.WriteLine(podcast.URL);
            }
            objectSerializer.Serialize(list, Constants.podcast.Value, true);
        }

        public List<Podcast> GetAllData()
        {
            List<Podcast> test = objectSerializer.DeserializeList(Constants.podcast.Value);
            list = test.ToList();

            return test;
        }
    }
}
