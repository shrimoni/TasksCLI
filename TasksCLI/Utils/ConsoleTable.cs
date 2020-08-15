using System;
using System.Collections.Generic;

namespace TasksCLI.Utils
{
    public class ConsoleTable
    {
        public int columnLength;
        const int maximumTableWidth = 100;
        public int tableWidth = 0;
        public string[] columns;
        public List<List<string>> tableData = new List<List<string>>();

        public void AddColumn(List<string> columnData)
        {
            if (tableData.Count == 0)
            {
                tableData.Add(columnData);
            }

            CalculateMaxTableWidth();
        }

        public void AddRow(List<string> rowData)
        {
            if (tableData.Count == 0)
                return;

            tableData.Add(rowData);

            UpdateDataForTableView();
            CalculateMaxTableWidth();
        }

        public void ShowTable()
        {
            if (tableData.Count == 0)
                return;

            Console.WriteLine("Maximum table width: " + tableWidth);
            Console.WriteLine(new string('-', tableWidth));
            foreach (var data in tableData)
            {
                foreach (var val in data)
                {
                    Console.Write("|" + val);
                }
                Console.Write("|\n");
                Console.WriteLine(new string('-', tableWidth));
            }
        }

        void UpdateDataForTableView()
        {
            if (tableData.Count <= 1)
                return;

            for (var i = 1; i < tableData.Count; i++)
            {
                for (var j = 0; j < tableData[i].Count; j++)
                {
                    var length = tableData[i][j].Length;
                    if (length < tableData[i - 1][j].Length)
                    {
                        length = tableData[i - 1][j].Length;
                    }
                    UpdateOtherRowValues(j, i, length);
                }
            }
        }

        void UpdateOtherRowValues(int colIndex, int rowIndexToStart, int length)
        {
            for (var i = rowIndexToStart; i >= 0; i--)
            {
                var difference = length - tableData[i][colIndex].Length;
                tableData[i][colIndex] = tableData[i][colIndex] + new string(' ', difference);
            }
        }

        public void CalculateMaxTableWidth()
        {
            if (tableData.Count == 0)
                return;

            foreach (var data in tableData)
            {
                var width = 0;
                foreach (var val in data)
                {
                    width += val.Length;
                }

                width += data.Count + 1;

                if (width < maximumTableWidth && width > tableWidth)
                    tableWidth = width;
            }
        }
    }
}
