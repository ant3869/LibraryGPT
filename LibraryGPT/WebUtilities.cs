using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HAP = HtmlAgilityPack; // Alias for HtmlAgilityPack

namespace LibraryGPT
{
    public static class WebUtilities
    {
        private static readonly HttpClient _httpClient;
        private static readonly HttpClient _client = new();

        public static HttpClient Client
        {
            get { return _httpClient; }
        }

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

        ///// <summary>
        ///// Reloads the current page by redirecting to the current URL.
        ///// </summary>
        ///// <param name="response">The HttpResponse instance.</param>
        //public static void Reload(this HttpResponse response)
        //{
        //    response.Redirect(HttpContext =>  RequestUrl.Reload(response);
        //    RequestUrl.Reload(response);
        //    response.Redirect(HttpContext.RequestUrl().ToString(), true);
        //}

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

        ///// <summary>
        ///// Sets a specific HTTP status code and description for the response.
        ///// </summary>
        ///// <param name="response">The HttpResponse instance.</param>
        ///// <param name="code">The HTTP status code.</param>
        ///// <param name="description">The status description.</param>
        ///// <param name="endResponse">Indicates whether to terminate the response.</param>
        //public static void SetStatus(this HttpResponse response, int code, string description, bool endResponse)
        //{
        //    response.StatusCode = code;
        //    response.StatusDescription = description;

        //    if (endResponse)
        //        throw new HttpException(code, description); // Replace response.End() with an exception
        //}

        // Commented out due to UriQueryString

        //public static async Task<List<Customer>> ScrapeCustomerQueueAsync(string url)
        //{
        //    var customerQueue = new List<Customer>();

        //    try
        //    {
        //        var response = await _httpClient.GetAsync(url);
        //        response.EnsureSuccessStatusCode();
        //        var content = await response.Content.ReadAsStringAsync();

        //        var document = new HtmlDocument();
        //        document.LoadHtml(content);

        //        // Assuming that customer data is in a table and each customer is a row <tr>
        //        var nodes = document.DocumentNode.SelectNodes("//tr[contains(@class, 'customer-row-class')]");

        //        if (nodes != null)
        //        {
        //            foreach (var node in nodes)
        //            {
        //                // Adjust the XPath queries according to the actual HTML structure
        //                var name = node.SelectSingleNode(".//td[1]").InnerText.Trim();
        //                var reason = node.SelectSingleNode(".//td[2]").InnerText.Trim();

        //                customerQueue.Add(new Customer { Name = name, ReasonForVisit = reason });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions that occur during the fetch
        //        Console.WriteLine($"An error occurred while scraping the customer queue: {ex.Message}");
        //    }

        //    return customerQueue;
        //}
    }

}
