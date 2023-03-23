namespace Core;

using Repo;

public class Core
{
    public string getDatas()
    {
        return new Repo().getJsonDatas();
    }
}