namespace User.Helpers
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using User.Models;

    public static class UserManagementHttpClient
    {
        public static async Task<UserServiceResponse> PostApiCallAsync<T>(string baseAddress, string apiAddress, T user)
        {
            using (var client = new HttpClient())
            {
                var postTask = client.PostAsJsonAsync(Path.Combine(baseAddress, apiAddress), user);
                await postTask.ConfigureAwait(false);

                var result = await postTask.ConfigureAwait(false);

                if (!result.IsSuccessStatusCode) return null;

                var readTask = result.Content.ReadAsAsync<UserServiceResponse>();
                await readTask.ConfigureAwait(false);

                var response = await readTask.ConfigureAwait(false);

                return response;
            }
        }
    }
}
