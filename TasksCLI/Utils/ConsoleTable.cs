using System;

namespace TasksCLI.Utils
{
    public class ConsoleTable
    {
        public int columnLength;
        public string[] columns;

        public ConsoleTable(string[] cols, int colLength = 10)
        {
            columns = cols;
            columnLength = colLength;
        }

        public void CreateColumn()
        {
            if (columns.Length == 0)
                return;

            GenerateSeparator();
            foreach (var col in columns)
            {
                var column = "|" + col + new string(' ', columnLength - col.Length);
                Console.Write(column);
            }
            Console.Write(" |\n");
            GenerateSeparator();
        }

        public void CreateRow(string[] rowData, bool isLastRow = false)
        {
            if (rowData.Length == 0)
                return;

            foreach (var row in rowData)
            {
                var rowValue = "|" + row + new string(' ', columnLength - row.Length);
                Console.Write(rowValue);
            }
            Console.Write(" |\n");

            if (isLastRow)
                GenerateSeparator();
        }

        // To Generator as table lines which acts a separator
        public void GenerateSeparator()
        {
            var separatorLength = 0;
            Console.Write(' ');
            foreach (var col in columns)
            {
                separatorLength += (col.Length + columnLength + 2);
            }
            Console.WriteLine(new string('-', separatorLength));
        }
    }
}
