using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace Ucu.Poo.Twitter;

public class TwitterImage : TwitterApi
{
    private readonly string twitterUploadApiUrl = "https://upload.twitter.com/1.1/media/upload.json";

    /// <summary>
    /// Publish a post with image
    /// </summary>
    /// <returns>result</returns>
    /// <param name="post">post to publish</param>
    /// <param name="pathToImage">image to attach</param>
    public string PublishToTwitter(string post, string pathToImage)
    {
        try
        {
            // Primero, subir la imagen
            string mediaID = string.Empty;
            var rezImage = Task.Run(async () =>
            {
                var response = await TweetImage(pathToImage);
                return response;
            });
            var rezImageJson = JObject.Parse(rezImage.Result.Item2);

            if (rezImage.Result.Item1 != HttpStatusCode.OK)
            {
                try // return error from JSON
                {
                    return $"Error uploading image to Twitter. {rezImageJson["errors"][0]["message"].Value<string>()}";
                }
                catch (Exception) // return unknown error
                {
                    // log exception somewhere
                    return "Unknown error uploading image to Twitter";
                }
            }
            mediaID = rezImageJson["media_id_string"].Value<string>();

            var data = new {
                text = post,
                media = new {
                    media_ids = new [] { mediaID }
                },
            };

            return this.PostRequest("tweets", data);
        }
        catch (Exception)
        {
            // log exception somewhere
            return "Unknown error publishing to Twitter";
        }
    }

    /// <summary>
    /// Upload some image to Twitter
    /// </summary>
    /// <returns>HTTP StatusCode and response</returns>
    /// <param name="pathToImage">Path to the image to send</param>
    private Task<(HttpStatusCode, string)> TweetImage(string pathToImage)
    {
        byte[] imgdata = System.IO.File.ReadAllBytes(pathToImage);
        var imageContent = new ByteArrayContent(imgdata);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

        var multipartContent = new MultipartFormDataContent();
        multipartContent.Add(imageContent, "media");

        return SendImage(twitterUploadApiUrl, multipartContent);
    }

    private async Task<(HttpStatusCode, string)> SendImage(string url, MultipartFormDataContent multipartContent)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", PrepareOAuth(url, null));

            var httpResponse = await httpClient.PostAsync(url, multipartContent);
            var httpContent = await httpResponse.Content.ReadAsStringAsync();

            return new (httpResponse.StatusCode, httpContent);
        }
    }
}