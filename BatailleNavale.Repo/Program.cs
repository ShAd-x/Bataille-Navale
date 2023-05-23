namespace Repo;

public class Repo
{
    public async Task<string> GetJsonDatas()
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("x-functions-key", "lprgi_api_key_2023");
        return await client.GetStringAsync("https://api-lprgi.natono.biz/api/GetConfig");
    }
}