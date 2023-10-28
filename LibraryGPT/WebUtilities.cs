using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HAP = HtmlAgilityPack; // Alias for HtmlAgilityPack

namespace LibraryGPT
{
    public static class WebUtilities
    {
        private static readonly HttpClient _client = new();

        // Fetch a web page as a string
        public static async Task<string?> FetchPageAsync(string url)
        {
            try
            {
                return await _client.GetStringAsync(url);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error fetching the URL: {e.Message}");
                return null;
            }
        }

        // Load a web page into an HtmlDocument (using HtmlAgilityPack)
        public static async Task<HAP.HtmlDocument?> LoadPageAsync(string url)
        {
            var pageContent = await FetchPageAsync(url);
            if (pageContent == null) return null;

            var doc = new HAP.HtmlDocument();
            doc.LoadHtml(pageContent);
            return doc;
        }

        // Extract all links from a web page
        public static async Task<string[]?> ExtractLinksAsync(string url)
        {
            var doc = await LoadPageAsync(url);
            if (doc == null) return null;

            var links = doc.DocumentNode.SelectNodes("//a[@href]");
            if (links == null) return null;

            return links.Select(link => link.GetAttributeValue("href", string.Empty)).ToArray();
        }

        // Extract text content from a specific element by its ID
        public static async Task<string?> ExtractTextByIdAsync(string url, string elementId)
        {
            var doc = await LoadPageAsync(url);
            if (doc == null) return null;

            var node = doc.GetElementbyId(elementId);
            return node?.InnerText.Trim();
        }

        // Post data to a web page and get the response
        public static async Task<string?> PostDataAsync(string url, HttpContent content)
        {
            try
            {
                var response = await _client.PostAsync(url, content);
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error posting data: {e.Message}");
                return null;
            }
        }

        // Download a file from the web
        public static async Task DownloadFileAsync(string url, string destinationPath)
        {
            try
            {
                var response = await _client.GetAsync(url);
                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                await System.IO.File.WriteAllBytesAsync(destinationPath, fileBytes);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error downloading file: {e.Message}");
            }
        }

        // Advanced: Set custom headers or modify the HttpClient before a request
        public static void SetCustomHeader(string headerName, string headerValue)
        {
            if (_client.DefaultRequestHeaders.Contains(headerName))
                _client.DefaultRequestHeaders.Remove(headerName);

            _client.DefaultRequestHeaders.Add(headerName, headerValue);
        }

        /// <summary>
        /// Checks if there is an active internet connection.
        /// Ex: bool isInternetAvailable = await IsInternetAvailableAsync();
        /// </summary>
        /// <returns>True if there is an internet connection, otherwise false.</returns>
        public static async Task<bool> IsInternetAvailableAsync()
        {
            try
            {
                var response = await _client.GetAsync("http://www.google.com");
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }
    }
}
