using System.Text;

namespace ParseCsv.Splitters
{
    internal class ColorCsvSplitter
    {
        internal static string[] ManualSplitCsvLine(string line)
        {
            int commaCount = 0;
            int indexToRemove = -1;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ',')
                {
                    commaCount++;
                    if (commaCount == 2)
                    {
                        indexToRemove = i;
                        break;
                    }
                }
            }

            if (indexToRemove != -1)
            {
                line = line.Remove(indexToRemove, 1);
            }

            var fields = new List<string>();
            var currentField = new StringBuilder();
            commaCount = 0;

            foreach (char c in line)
            {
                if (c == ',' && commaCount < 2)
                {
                    fields.Add(currentField.ToString());
                    currentField.Clear();
                    commaCount++;
                }
                else
                {
                    currentField.Append(c);
                }
            }

            fields.Add(currentField.ToString());

            return [.. fields];
        }
    }
}
