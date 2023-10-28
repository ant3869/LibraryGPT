using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Web;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;

namespace LibraryGPT
{
    public static class WebUtilities
    {
        private static readonly HttpClient _client = new HttpClient();

        // Fetch a web page as a string
        public static async Task<string> FetchPageAsync(string url)
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
        public static async Task<HtmlDocument> LoadPageAsync(string url)
        {
            var pageContent = await FetchPageAsync(url);
            if (pageContent == null) return null;

            var doc = new HtmlDocument();
            doc.LoadHtml(pageContent);
            return doc;
        }

        // Extract all links from a web page
        public static async Task<string[]> ExtractLinksAsync(string url)
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
        public static async Task<string> PostDataAsync(string url, HttpContent content)
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
        /// Redirects to a formatted URL.
        /// </summary>
        /// <param name="response">The HttpResponse instance.</param>
        /// <param name="urlFormat">The URL format with placeholders.</param>
        /// <param name="values">Values for the placeholders.</param>
        public static void Redirect(this HttpResponse response, string urlFormat, params object[] values)
        {
            var url = string.Format(urlFormat, values);
            response.Redirect(url, true);
        }

        /// <summary>
        /// Redirects to a URL with an appended query string.
        /// </summary>
        /// <param name="response">The HttpResponse instance.</param>
        /// <param name="url">The base URL.</param>
        /// <param name="queryString">The query string to append.</param>
        public static void Redirect(this HttpResponse response, string url)
        {
            //url = queryString.ToString(url);
            response.Redirect(url, true);
        }

        /// <summary>
        /// Sets the HTTP response status code to 404 (Not Found).
        /// </summary>
        /// <param name="response">The HttpResponse instance.</param>
        public static void SetFileNotFound(this HttpResponse response)
        {
            response.SetFileNotFound();
            //response.SetStatus(404, "Not Found", true);
        }

        /// <summary>
        /// Sets the HTTP response status code to 500 (Internal Server Error).
        /// </summary>
        /// <param name="response">The HttpResponse instance.</param>
        public static void SetInternalServerError(this HttpResponse response)
        {
            response.SetInternalServerError()
;            //response.SetStatus(500, "Internal Server Error", true);
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
    }

}
