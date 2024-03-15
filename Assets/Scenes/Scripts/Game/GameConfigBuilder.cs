public class GameConfigBuilder : IService
{
    private GameConfig config;
    public GameConfigBuilder() 
    {
        config = new GameConfig();
    }
    public GameConfigBuilder SetSubmitter(IWordSubmitter submitter)
    {
        config.Submitter = submitter;
        return this;
    }
    public GameConfigBuilder SetRows(int rows)
    {
        config.Rows = rows;
        return this;
    }
    public GameConfigBuilder SetCols(int cols)
    {
        config.Columns = cols;
        return this;
    }
    public GameConfigBuilder SetGrid(int rows, int cols)
    {
        config.Rows = rows;
        config.Columns = cols;
        return this;
    }
    public GameConfig GetConfig()
    {
        return config;
    }
}
