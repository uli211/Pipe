using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RestSharp.Authenticators;
using RestSharp;

namespace Ucu.Poo.Twitter;

class Program
{
    static void Main(string[] args)
    {
        var twitter = new TwitterImage();
        string path = File.Exists(@"../../bill2.jpg") ? @"../../bill2.jpg" : @"bill2.jpg";
        Console.WriteLine(twitter.PublishToTwitter("New Employee 2! ", path));
        var twitterDirectMessage = new TwitterMessage();
        Console.WriteLine(twitterDirectMessage.SendMessage("Hola!", "1396065818"));
    }
}