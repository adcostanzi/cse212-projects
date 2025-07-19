using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        HashSet<string> check = new HashSet<string>();
        List<string> results = new List<string>();

        foreach (string word in words)
        {
            if (!check.Contains(word))
            {
                string newWord = $"{word[1]}{word[0]}";

                if (check.Contains(newWord))
                {
                    results.Add($"{newWord} & {word}");
                }
                else
                {
                    check.Add(word);
                }
            }
        }

        string[] finalResults = new string[results.Count];

        for (int i = 0; i < results.Count; i++)
        {
            finalResults[i] = results[i];
        }

        return finalResults;
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (degrees.ContainsKey(fields[3]))
            {
                degrees[fields[3]] += 1;
            }
            else
            {
                degrees.Add(fields[3], 1);
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE
        int longer = Math.Max(word1.Length, word2.Length);

        Dictionary<char, int> storage = new Dictionary<char, int>();

        for (int i = 0; i < longer; i++)
        {
            if (i < word1.Length && word1[i] != ' ')
            {
                char char1 = Char.ToLower(word1[i]);
                if (storage.ContainsKey(char1))
                {
                    storage[char1]++;
                }
                else
                {
                    storage.Add(char1, 1);
                }
            }
            if (i < word2.Length && word2[i] != ' ')
            {
                char char2 = Char.ToLower(word2[i]);

                if (storage.ContainsKey(char2))
                {
                    storage[char2]--;
                }
                else
                {
                    storage.Add(char2, -1);
                }
            }
        }
        return storage.Values.All(v => v == 0);

    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        string[] finalData = new string[featureCollection.Features.Count()];
        int i = 0;

        foreach (var feature in featureCollection.Features)
        {
            string place = feature.Properties["place"].ToString();
            var magElement = (JsonElement)feature.Properties["mag"];
            decimal mag = magElement.GetDecimal();

            string newLine = $"{place} - Mag {mag}";

            finalData[i] = newLine;

            i++;
        }
        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.
        return finalData;
    }

    public class FeatureCollection
    {
        public string Type { get; set; }
        public Metadata Metadata { get; set; }
        public Feature[] Features { get; set; }
    }

    public class Metadata
    {
        public long Generated { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string Api { get; set; }
        public int Count { get; set; }
    }
    public class Feature
    {
        public string Type { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public Geometry Geometry { get; set; }
        public string Id { get; set; }
    }

    public class Properties
    {
        // public decimal Mag => Convert.ToDecimal(Data["Mag"]);
        // public string Place => Data["Place"].ToString();
    
        public Dictionary<string, object> Data { get; set; }
    }

    public class Geometry
    {
        public string Type { get; set; }
        public double[] Coordinates { get; set; }

    }
}