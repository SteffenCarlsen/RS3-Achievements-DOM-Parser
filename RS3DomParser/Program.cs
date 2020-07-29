using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace RS3DomParser
{
    class Program
    {
        private static bool OutputAsJson = true;
        private static bool PrintTableToConsole = false;
        static void Main(string[] args)
        {
            // Get DOM
            WebClient wc = new WebClient();
            var data = wc.DownloadString("https://runescape.wiki/w/List_of_achievements");
            
            // Load it into htmldocument for parsing
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(data);

            var achievements = new List<Achievement>();
            // Store all tr nodes as rows
            foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
            {
                var tableRow = row.SelectNodes("td").Select(td => td.InnerText).ToArray();
                // Sanity check (Also exists invalid rows in the data)
                if (tableRow.Length < 5)
                {
                    continue;
                }
                
                var ach = new Achievement();
                ach.Name = StringHelpers.CleanString(tableRow[0]);
                ach.Members = StringHelpers.CleanString(tableRow[1]);
                ach.Description = StringHelpers.CleanString(tableRow[2]);
                ach.Category = StringHelpers.CleanString(tableRow[3]);
                ach.SubCategory = StringHelpers.CleanString(tableRow[4]);
                ach.SubSubCategory = StringHelpers.CleanString(tableRow[5]);
                ach.RuneScore = StringHelpers.CleanString(tableRow[6]);
                
                achievements.Add(ach);
            }

            // Dont, it's works like crap :)
            if (PrintTableToConsole)
            {
                PrintTable(achievements);
            }

            if (OutputAsJson)
            {
                JsonObjectFileSaveLoad.WriteToJsonFile(Path.Combine(AppContext.BaseDirectory, "achievementsTwo.json"), achievements);
            }
            
        }

        /// <summary>
        /// Description is too long for this to be pretty :(
        /// </summary>
        /// <param name="achievements"></param>
        private static void PrintTable(List<Achievement> achievements)
        {
            var header = "| ";
            StringBuilder sb = new StringBuilder(header);
            sb.Append(StringHelpers.PadRight("Name", 60) + " | ");
            sb.Append(StringHelpers.PadRight("Members", 4) + " | ");
            sb.Append(StringHelpers.PadRight("Description", 156) + " | ");
            sb.Append(StringHelpers.PadRight("Category", 38) + " | ");
            sb.Append(StringHelpers.PadRight("SubCategory", 63) + " | ");
            sb.Append(StringHelpers.PadRight("SubSubCategory", 13) + " | ");
            sb.Append(StringHelpers.PadRight("RuneScore", 5) + " |" + Environment.NewLine);
            foreach (var achievement in achievements)
            {
                sb.Append(achievement + Environment.NewLine);
            }

            Console.WriteLine(sb.ToString());
        }

    }
    
}