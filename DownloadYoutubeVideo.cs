using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Exceptions;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Videos;
using System.Diagnostics;

namespace TikTokClipRip
{
    public class YouTube
    {
        private readonly YoutubeClient _youtube;

        public YouTube()
        {
            _youtube = new YoutubeClient();
        }

        public async Task DownloadVideoAsync(string videoUrl)
        {
            try
            {
                var videoId = ExtractVideoId(videoUrl);
                var streamManifest = await _youtube.Videos.Streams.GetManifestAsync(videoId);

                // Get the best quality muxed stream (combined audio and video)
                var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();

                if (streamInfo != null)
                {
                    // Download the stream to a file
                    var videoFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), $"{videoId}.{streamInfo.Container}");
                    Debug.WriteLine($"Downloading video to: {videoFilePath}");
                    await _youtube.Videos.Streams.DownloadAsync(streamInfo, videoFilePath);

                    Debug.WriteLine("Video downloaded successfully!");
                }
                else
                {
                    Debug.WriteLine("No suitable streams found.");
                }
            }
            catch (VideoUnavailableException)
            {
                Debug.WriteLine("The video is unavailable.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error downloading video: {ex.Message}");
            }
        }

        private string ExtractVideoId(string videoUrl)
        {
            var uri = new Uri(videoUrl);
            var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
            return queryParams["v"];
        }
    }
}
