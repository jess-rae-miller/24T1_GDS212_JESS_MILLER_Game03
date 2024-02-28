public class AdventureResult
{
    public int Coins { get; set; }
    public int Food { get; set; }
    public int Water { get; set; }
    public int Toys { get; set; }
    public int RareItems { get; set; }

    // Constructor
    public AdventureResult()
    {
        Coins = 0;
        Food = 0;
        Water = 0;
        Toys = 0;
        RareItems = 0;
    }
}
