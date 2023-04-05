using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System;

namespace M726Raytracing2 {
    public class CSV {
        // Stores the parsed data from the CSV file as a list of columns
        public Dictionary<float, float[]> data;

        public CSV(string filename) {
            // Open the CSV file
            using (StreamReader reader = new StreamReader(filename)) {
                data = new Dictionary<float, float[]>();

                string[] headers = reader.ReadLine().Split(',');

                
                while (!reader.EndOfStream) {
                    string[] line = reader.ReadLine().Split(',');

                    if(float.TryParse(line[0], out float key)) {
                        List<float> vs = new List<float>();
                        for (int i = 1; i < headers.Length; i++) {
                            if (i < line.Length) {
                                if (float.TryParse(line[i].Trim(), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint,NumberFormatInfo.InvariantInfo,out float value)) {
                                    vs.Add(value);
                                } else {
                                    throw new System.Exception(string.Concat("CSV Value Read Error: ", line[i]));
                                }
                            } else {
                                vs.Add(0);
                            }
                        }
                        data.Add(key,vs.ToArray());
                    } else {
                        throw new System.Exception("CSV Key Read Error");
                    }
                }
            }
            
        }

        public Dictionary<float, float[]> GetData() {
            return data;
        }
        public SortedList<float, float[]> GetSortedList() {
            return new SortedList<float, float[]>(data);
        }
        public float[] Interpolate(float targetKey) {
            SortedList<float, float[]> sortedList = GetSortedList();

            int index1 = 0;
            int index2 = 0;

            // Check if the target key is outside the range of the sorted list
            if (targetKey < sortedList.Keys[0]) {
                index1 = 0;
                index2 = index1;
                //throw new ArgumentOutOfRangeException("targetKey");
            } else if(targetKey > sortedList.Keys[sortedList.Count - 1]) {
                index1 = sortedList.Count - 1;
                index2 = index1;
            } else {
                for (int i = 1; i < sortedList.Count - 1; i++) {
                    if (sortedList.Keys[i] >= targetKey) {
                        index1 = i - 1;
                        index2 = i;
                        break;
                    }
                }
            }

            // Interpolate values from the nearest key-value pairs
            float key1 = sortedList.Keys[index1];
            float key2 = sortedList.Keys[index2];
            float[] value1 = sortedList.Values[index1];
            float[] value2 = sortedList.Values[index2];
            float t = (targetKey - key1) / (key2 - key1);
            float[] result = new float[value1.Length];
            for (int i = 0; i < result.Length; i++) {
                result[i] = value1[i] + t * (value2[i] - value1[i]);
            }
            return result;
        }



        public override string ToString() {
            string s = "";
            foreach (KeyValuePair<float, float[]> pair in data) {
                s = string.Concat(s, pair.Key.ToString(), ":");
                foreach (float value in pair.Value) {
                    s = string.Concat(s, " ", value.ToString());
                }
                s = string.Concat(s, "\n");
            }
            return s;
        }
    }
}
