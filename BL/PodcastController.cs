﻿using DAL;
using DAL.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BL
{
    public class PodcastController
    {
        private PodcastRepository podcastRepository;

        public PodcastController()
        {
            podcastRepository = new PodcastRepository();
        }
        public async Task<Podcast> CreatePodcastObject(string url, string name, int interval, string category, IProgress<int> progress)
        {
            Podcast podcast = new Podcast();
            await Task.Run(() =>
            {
                string description = podcastRepository.reader.GetDescription(url);
                var episodesList= podcastRepository.reader.GetEpisodes(url, progress);
                int episodeAmount = podcastRepository.reader.GetAmountOfEpisodes(url);

                podcastRepository.Create(podcast = new Podcast(url, new Category(category), interval, episodesList.Result, name, episodeAmount));
            });

            return podcast;
        }
        public List<Podcast> RetrieveAllPodcasts()
        {
            return podcastRepository.GetAll();
        }
        public async Task UpdatePodcast(string URL, string name, int interval, Category category, int index, IProgress<int> progress)
        {
            await Task.Run(() =>
            {
                var episodesList = podcastRepository.reader.GetEpisodes(URL, progress);
                int episodeAmount = podcastRepository.reader.GetAmountOfEpisodes(URL);
                podcastRepository.Update(URL, name, interval, category, index, episodesList.Result, episodeAmount);
            });
        }
        public async Task UpdatePodcastBatch(List<Podcast> podcasts, IProgress<int> progress, ListBox console)
        {
             await podcastRepository.Update(podcasts, progress, console);
        }
        public void UpdatePodcast(string currentCategory, string newCategory)
        {
            podcastRepository.Update(currentCategory, newCategory); 
        }
        public void DeletePodcast(int index)
        {
            podcastRepository.Delete(index);
        }
        public void DeletePodcast(Podcast podcast)
        {
            podcastRepository.Delete(podcast);
        }

        public void SavePodcastData()
        {
            podcastRepository.SaveChanges();
        }

        public List<Podcast> GetPodcastData()
        {
            return podcastRepository.GetAllData();
        }
    }
}
