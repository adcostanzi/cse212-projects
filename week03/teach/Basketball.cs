/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData)
        {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);

            if (players.ContainsKey(playerId))
            {
                players[playerId] += points;
            }
            else
            {
                players.Add(playerId, points);
            }
        }

        var sortedDict = (from entry in players orderby entry.Value descending select entry).Take(10);



        var topPlayers = new string[10];
        int i = 0;
        foreach (var kvp in sortedDict)
        {
            topPlayers[i] = $"{kvp.Key} - {kvp.Value}";

            i++;
        }
        
        Console.WriteLine($"Players: {{{string.Join(", ", topPlayers)}}}");
    }
}