namespace ViisApp
{
    public class BoWData
    {
        public Dictionary<string, int> BoW { get; set; }

        public const int defBlack = 1;
        public const int defWhite = -1;

        public BoWData()
        {
            BoW = new Dictionary<string, int>();
        }
    }
}