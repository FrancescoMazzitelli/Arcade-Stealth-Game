using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

    public class FileReader
    {
        private Dictionary<string, string> enemiesParams = new Dictionary<string, string>();
        private Dictionary<string, string> environmentParams = new Dictionary<string, string>();
        private Dictionary<string, string> mainCharacterParams = new Dictionary<string, string>();

        private string textfile;

        public FileReader(string textFile) 
        {
            textfile = textFile;
        }

        public ParametersStructure readParams()
        {
            string[] lines = File.ReadAllLines(textfile);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.StartsWith("Enemies"))
                {
                    i++;
                    line = lines[i];
                    while (line != null)
                    {
                        if (string.IsNullOrEmpty(line)) break;
                        line = lines[i];
                        try
                        {
                            string[] keyValue = line.Split(' ');
                            enemiesParams.Add(keyValue[0], keyValue[1]);
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            break;
                        }
                        i++;
                    }
                }

                if (line.StartsWith("Environment"))
                {
                    i++;
                    line = lines[i];
                    while (line != null)
                    {
                        if (string.IsNullOrEmpty(line)) break;
                        line = lines[i];
                        try
                        {
                            string[] keyValue = line.Split(' ');
                            environmentParams.Add(keyValue[0], keyValue[1]);
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            break;
                        }
                        i++;
                    }
                }

                if (line.StartsWith("MainCharacter"))
                {
                    i++;
                    line = lines[i];
                    while (line != null)
                    {
                        if (string.IsNullOrEmpty(line)) break;
                        line = lines[i];
                        try
                        {
                            string[] keyValue = line.Split(' ');
                            mainCharacterParams.Add(keyValue[0], keyValue[1]);
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            break;
                        }
                        i++;
                    }
                }
            }
            ParametersStructure parameters = new ParametersStructure(enemiesParams, environmentParams, mainCharacterParams);
            return parameters;
        }

        public void debugPrint(Dictionary<string, string> dictionary)
        {
            foreach (KeyValuePair<string, string> entry in dictionary)
            {
                Debug.Log("Chiave: " + entry.Key + ", Valore: " + entry.Value);
            }
        }
    }
    