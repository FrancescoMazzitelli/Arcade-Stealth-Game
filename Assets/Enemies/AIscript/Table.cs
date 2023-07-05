using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Table
{
    private List<List<string>> data;
    private int numRows;
    private int numColumns;

    public Table()
    {
        data = new List<List<string>>();
        numRows = 0;
        numColumns = 0;
    }

    public void InsertFromCSV(string filePath)
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            numRows = lines.Length;
            numColumns = 0;

            foreach (string line in lines)
            {
                string[] values = line.Split(',');

                if (values.Length > numColumns)
                {
                    numColumns = values.Length;
                }

                List<string> row = new List<string>(values);
                data.Add(row);
            }
        }
        else
        {
            Console.WriteLine("File CSV non trovato: " + filePath);
        }
    }

    public string[] GetHeader()
    {
        if (numRows > 0)
        {
            return data[0].ToArray();
        }
        else
        {
            return new string[0];
        }
    }

    public List<string> GetRow(int rowIndex)
    {
        if (rowIndex >= 0 && rowIndex < numRows)
        {
            return data[rowIndex];
        }
        else
        {
            return null;
        }
    }

    public List<List<string>> GetAllRows()
    {
        return data;
    }

    public int[] FindRow(string searchValue)
    {
        for (int i = 0; i < numRows; i++)
        {
            List<string> row = data[i];
            int columnIndex = row.IndexOf(searchValue);

            if (columnIndex != -1)
            {
                int[] result = { i, columnIndex };
                return result;
            }
        }

        return null; // Valore non trovato
    }

    public void PrintTable()
    {
        foreach (List<string> row in data)
        {
            foreach (string value in row)
            {
                Debug.Log(value + "\t");
            }
            Debug.Log("");
        }
    }


    public int NumRows
    {
        get { return numRows; }
        set { numRows = value; }
    }

    public int NumColumns
    {
        get { return numColumns; }
        set { numColumns = value; }
    }
}
