using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace GameApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> errors = new List<string>();
            try
            {

                // Check arguments
                if (args[0] == null)
                    errors.Add("Game settings file not found.");
                else if (!File.Exists(args[0]))
                    errors.Add($"{args[0]} is not found.");

                if (args[1] == null)
                    errors.Add("Moves file not found.");
                else if (!File.Exists(args[1]))
                    errors.Add($"{args[1]} file not found.");

                if (errors.Count > 0)
                    goto EndOfProgramm;

                // Load file setting map
                string jsonStringGameSetting = FileLoader(args[0]);

                // Load Sequence move for the tortule
                string jsonStringMoves = FileLoader(args[1]);

                // Initialize Json object
                // Map
                Map Map = null;
                try
                {
                    Map = JsonConvert.DeserializeObject<Map>(jsonStringGameSetting);
                }
                catch (JsonException)
                {
                    errors.Add("Game settings file is not a correct JSON object.");
                    goto EndOfProgramm;
                }
                Map.TriggerExit += DisplayMessage.DisplayExit;
                Map.TriggerHitMine += DisplayMessage.DisplayHitMine;
                Map.ForwardSuccess += DisplayMessage.DisplayForward;
                Map.TriggerOutsideMap += DisplayMessage.DisplayFallDown;
                

                // Moves
                string[] jsonObjMoves = null;                
                try
                {
                    jsonObjMoves = JsonConvert.DeserializeObject<string[]>(jsonStringMoves);                    
                }
                catch (JsonException)
                {
                    errors.Add("Move file is not a correct JSON object.");
                    goto EndOfProgramm;
                }

                List<Tortle.Move> Moves;
                try
                {
                    Moves = jsonObjMoves.Select(a => (Tortle.Move)Enum.Parse(typeof(Tortle.Move), a)).ToList(); 
                }
                catch
                {
                    errors.Add("The value in move file are incorrect.");
                    goto EndOfProgramm;
                }

                // Tortule
                var Tortle = new Tortle(Map.Start, Map.DirectionStart);
                Map.TriggerOutsideMap += Tortle.InstantDead;

                var ind = 0;
                foreach (Tortle.Move m in Moves)
                {
                    Console.Write($"Sequence {(ind + 1)}: ");
                    if (!Tortle.IsAlive())
                    {
                        DisplayMessage.DisplayTortleIsDead();
                        break;
                    }
                    else
                    {
                        switch(m)
                        {
                            case Tortle.Move.r:
                                Tortle.Rotate();
                                DisplayMessage.DisplayTortleFacing(Tortle.Facing.ToString("f"));
                                break;

                            case Tortle.Move.m:
                                Tortle.Forward();                            
                                Map.TriggerAction(Tortle.Position);                            
                                break;
                        }
                    }
                    Console.Write("\r\n");
                    ind++;
                }                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        EndOfProgramm:
            errors.ForEach(e =>
            {
                Console.WriteLine(e);
            });
            Console.ReadLine();
        }
       


        static private string FileLoader(string path)
        {
            string text = File.ReadAllText(path);

            // Remove whitespace
            Regex REnoWhiteCharacter = new Regex("\\s");
            text = REnoWhiteCharacter.Replace(text, "");

            return text;
        }
    }
}
