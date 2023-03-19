namespace Repo;

public class Repo
{
    public string getJsonDatas()
    {
        string json = string.Empty;
        using (System.Net.WebClient wc = new System.Net.WebClient())
        {
            wc.Headers.Add("x-functions-key", "lprgi_api_key_2023");
            json = wc.DownloadString("https://api-lprgi.natono.biz/api/GetConfig");
        }
        return json;
    }
}