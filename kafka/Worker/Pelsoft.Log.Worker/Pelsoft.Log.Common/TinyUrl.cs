using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pelsoft.Log.Common
{
    public class TinyUrl
    {
        /// <summary>
        /// Minifica una url usando la API TINIURL
        /// Api: https://tinyurl.com/app/
        /// </summary>
        /// <param name="pStrURL"></param>
        /// <returns></returns>
        public static async Task<string> ShrinkURL(string pStrURL)
        {
            string wResponseString;
            string url = "http://tinyurl.com/api-create.php?url=" + pStrURL;
            HttpResponseMessage wResponse = null;

            using (var httpClient = new HttpClient())
            {
                wResponse = await httpClient.GetAsync(url);
                if (wResponse.StatusCode == HttpStatusCode.OK)
                    wResponseString = await wResponse.Content.ReadAsStringAsync();
                else
                {
                    // Aquí podemos retornar un error solo es un demo
                    wResponseString = await wResponse.Content.ReadAsStringAsync();
                    return "error : " + wResponseString;
                }

                return wResponseString;
            }
        }
    }
}
