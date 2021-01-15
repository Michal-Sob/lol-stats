﻿using MatchStatistic.WebApi.Models.Matches;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MatchStatistic.WebApi.Services
{
	public class LiveMatchesService
	{
		public HttpClient Client { get; }

		public LiveMatchesService(HttpClient client)
		{
			client.DefaultRequestHeaders.Add("X-Riot-Token", ConfigurationManager.AppSettings.Get("X-Riot-Token")); //Put your API key

			Client = client;
		}

		public async Task<CurrentGameInfo> GetCurrentGameBySummId(string server, string summId)
		{
			var response = await Client.GetAsync($"https://{server}.api.riotgames.com/lol/spectator/v4/active-games/by-summoner/{summId}");

			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsAsync<CurrentGameInfo>();
		}

		public async Task<FeaturedGames> GetFeaturedLiveGames(string server)
		{
			var response = await Client.GetAsync($"https://{server}.api.riotgames.com/lol/spectator/v4/featured-games");

			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsAsync<FeaturedGames>();
		}
	}
}
