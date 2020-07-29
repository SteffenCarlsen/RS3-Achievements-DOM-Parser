using System.Net.NetworkInformation;
using System.Text;

namespace RS3DomParser
{
    public class Achievement
    {
        public string Name { get; set; }
        public string Members { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string SubSubCategory { get; set; }
        public string RuneScore { get; set; }

        public override string ToString()
        {
            string achievement = "| ";
            StringBuilder sb = new StringBuilder(achievement);
            sb.Append(StringHelpers.PadRight(Name, 60) + " | ");
            sb.Append(StringHelpers.PadRight(Members, 4) + " | ");
            sb.Append(StringHelpers.PadRight(Description, 156) + " | ");
            sb.Append(StringHelpers.PadRight(Category, 38) + " | ");
            sb.Append(StringHelpers.PadRight(SubCategory, 63) + " | ");
            sb.Append(StringHelpers.PadRight(SubSubCategory, 13) + " | ");
            sb.Append(StringHelpers.PadRight(RuneScore, 5) + " |");
            return sb.ToString();
        }


    }
}