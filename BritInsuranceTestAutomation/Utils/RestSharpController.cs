//

using RestSharp;


namespace BritInsuranceTestAutomation.Utils
{
    public class RestSharpController
    {
        /// <summary>
        /// Performs a GET request
        /// </summary>
        /// <param name="url">Complete URL for the request</param>
        /// <returns>Response as string</returns>
        public async Task<string> GetAsync(string url)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(url, Method.Get);

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                    return response.Content;

                throw new Exception($"Error: {response.StatusCode} - {response.Content}");
            }
            catch (Exception ex)
            {
                throw new Exception($"GET Request Failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Performs a POST request
        /// </summary>
        /// <param name="url">Complete URL for the request</param>
        /// <param name="body">The request body</param>
        /// <returns>Response as string</returns>
        public async Task<string> PostAsync(string url, string body)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(url, Method.Post);

                request.AddHeader("Content-Type", "application/json");
                request.AddStringBody(body, DataFormat.Json);

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                    return response.Content;

                throw new Exception($"Error: {response.StatusCode} - {response.Content}");
            }
            catch (Exception ex)
            {
                throw new Exception($"POST Request Failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Performs a PUT request
        /// </summary>
        /// <param name="url">Complete URL for the request</param>
        /// <param name="body">The request body</param>
        /// <returns>Response as string</returns>
        public async Task<string> PutAsync(string url, string body)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(url, Method.Put);

                request.AddHeader("Content-Type", "application/json");
                request.AddStringBody(body, DataFormat.Json);

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                    return response.Content;

                throw new Exception($"Error: {response.StatusCode} - {response.Content}");
            }
            catch (Exception ex)
            {
                throw new Exception($"PUT Request Failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Performs a PATCH request
        /// </summary>
        /// <param name="url">Complete URL for the request</param>
        /// <param name="body">The request body</param>
        /// <returns>Response as string</returns>
        public async Task<string> PatchAsync(string url, string body)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(url, Method.Patch);

                request.AddHeader("Content-Type", "application/json");
                request.AddStringBody(body, DataFormat.Json);

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                    return response.Content;

                throw new Exception($"Error: {response.StatusCode} - {response.Content}");
            }
            catch (Exception ex)
            {
                throw new Exception($"PATCH Request Failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Performs a DELETE request
        /// </summary>
        /// <param name="url">Complete URL for the request</param>
        /// <returns>Response as string</returns>
        public async Task<string> DeleteAsync(string url)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(url, Method.Delete);

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                    return response.Content;

                throw new Exception($"Error: {response.StatusCode} - {response.Content}");
            }
            catch (Exception ex)
            {
                throw new Exception($"DELETE Request Failed: {ex.Message}");
            }
        }
    }
}
