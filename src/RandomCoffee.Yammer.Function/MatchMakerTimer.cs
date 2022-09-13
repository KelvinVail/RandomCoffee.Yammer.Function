using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using RandomCoffee.Core;
using RandomCoffee.Core.Sorters;

namespace RandomCoffee.Yammer.Function
{
    public class MatchMakerTimer
    {
        [Function(nameof(MakeMatches))]
        public async Task MakeMatches([TimerTrigger("0 0 8 12 * *", UseMonitor = false)] FunctionContext context)
        {
            using var rng = new RNGCryptoServiceProvider();
            using var client = new HttpClient();

            var groupId = long.Parse(Environment.GetEnvironmentVariable("GroupId"), NumberStyles.Integer, new NumberFormatInfo());
            var token = Environment.GetEnvironmentVariable("BearerToken");
            var group = new YammerGroup(groupId, client, token, new PostFormatter("Random Coffee"));

            var matchMaker = new MatchMaker(
                group,
                new RandomSorter(rng));

            await matchMaker.MakeMatches();
        }
    }
}
