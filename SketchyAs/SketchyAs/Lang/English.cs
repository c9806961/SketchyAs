using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SketchyAs
{
    public class English : SketchyLang
    {
        List<string> quantity;
        List<string> modifier;
        List<string> colour;
        List<string> places;
        List<string> commonnounsthatcandothings;
        List<string> descriptor;
        List<string> verbs;
        List<string> commonnounsthatcanbedoneto;
        List<string> propernouns;

        public English(List<string> packs)
        {
            Language = "English";
            ShortName = "EN";
            loadTranslations();
            loadPromptWords(packs);
        }

        public override string getPrompt()
        {
            // generate a prompt, else return base value
            List<int> inputs = new List<int>(){ 50, 50, 15, 5, 15, 5, 95, 20, 15, 15, 2, 15 };
            // q1, q2, m1, m2, c1, c2, v, d, n1, n2, maxextras, places
            inputs[0] = inputs[0] > 0 ? inputs[0] : 1; //q1
            inputs[1] = inputs[1] > 0 ? inputs[1] : 1; //q2
            inputs[2] = inputs[2] > 0 ? inputs[2] : 1; //m1
            inputs[3] = inputs[3] > 0 ? inputs[3] : 1; //m2
            inputs[4] = inputs[4] > 0 ? inputs[4] : 1; //c1
            inputs[5] = inputs[5] > 0 ? inputs[5] : 1; //c2
            inputs[6] = inputs[6] > 0 ? inputs[6] : 1; //v
            inputs[7] = inputs[7] > 0 ? inputs[7] : 1; //d
            inputs[8] = inputs[8] > 0 ? inputs[8] : 1; //n1
            inputs[9] = inputs[9] > 0 ? inputs[9] : 1; //n2
            inputs[11] = inputs[11] > 0 ? inputs[11] : 1; //places

            inputs[0] = inputs[0] < 100 ? inputs[0] : 100;
            inputs[1] = inputs[1] < 100 ? inputs[1] : 100;
            inputs[2] = inputs[2] < 100 ? inputs[2] : 100;
            inputs[3] = inputs[3] < 100 ? inputs[3] : 100;
            inputs[4] = inputs[4] < 100 ? inputs[4] : 100;
            inputs[5] = inputs[5] < 100 ? inputs[5] : 100;
            inputs[6] = inputs[6] < 100 ? inputs[6] : 100;
            inputs[7] = inputs[7] < 100 ? inputs[7] : 100;
            inputs[8] = inputs[8] < 100 ? inputs[8] : 100;
            inputs[9] = inputs[9] < 100 ? inputs[9] : 100;
            inputs[11] = inputs[11] < 100 ? inputs[11] : 100;

            List<string> output = new List<string>();
            int extras = 0;
            bool hadquantity = false;
            bool haveVerb = false;
            string quantity1;
            string quantity2;
            string lastword = "";
            List<string> mynoun = new List<string>();
            Random rnd = new Random();
            //rnd.Next(1,101); 

            // proper or common
            if (rnd.Next(1, 101) <= inputs[8])
            {
                lastword = propernouns[rnd.Next(0, propernouns.Count - 1)];
                output.Add(lastword);
            }
            else
            {
                // first quantity
                if (rnd.Next(1, 101) <= inputs[0]) //chance of singular
                {
                    quantity1 = quantity[0];
                }
                else
                {
                    quantity1 = quantity[rnd.Next(1, quantity.Count - 1)];
                    hadquantity = true;
                }
                if (quantity1 != "")
                {
                    output.Add(quantity1);
                }
                // modifier
                if (rnd.Next(1, 101) <= inputs[2])
                {
                    lastword = modifier[rnd.Next(0, modifier.Count - 1)];
                    output.Add(lastword);
                    extras++;
                }
                // colour
                if (rnd.Next(1, 101) <= inputs[4])
                {
                    lastword = colour[rnd.Next(0, colour.Count - 1)];
                    output.Add(lastword);
                    extras++;
                }
                // first nouns
                mynoun = new List<string>();
                mynoun.AddRange(commonnounsthatcandothings[rnd.Next(0, commonnounsthatcandothings.Count - 1)].Split('/'));
                if (quantity1 == "a")
                {
                    output.Add(mynoun[0]);
                }
                else
                {
                    if (mynoun.Count == 1)
                    {
                        output.Add(mynoun[0] + "s");
                    }
                    else
                    {
                        output.Add(mynoun[1]);
                    }
                }
            }
            // places
            if (rnd.Next(1, 101) <= inputs[11])
            {
                lastword = places[rnd.Next(0, places.Count - 1)];
                output.Add(lastword);
                extras++;
            }
            // verbs
            if (rnd.Next(1, 101) <= inputs[6])
            {
                haveVerb = true;
            }
            if (haveVerb) // if you have no verb, don't bother with a descriptor or anything after
            {
                //descriptor
                if (rnd.Next(1, 101) <= inputs[7])
                {
                    lastword = descriptor[rnd.Next(0, descriptor.Count - 1)];
                    output.Add(lastword);
                    extras++;
                }
            }   // add the verb
            output.Add(verbs[rnd.Next(0, verbs.Count - 1)]);
            // proper or common
            if (rnd.Next(1, 101) <= inputs[9])
            {
                output.Add(propernouns[rnd.Next(0, propernouns.Count - 1)]);
            }
            else // common
            {
                // second Quantity
                quantity2 = "";
                if (rnd.Next(1, 101) <= inputs[1])
                {
                    quantity2 = quantity[0];
                }
                else if (!hadquantity)
                {
                    quantity2 = quantity[rnd.Next(1, quantity.Count - 1)];
                }
                if (quantity2 != "")
                {
                    output.Add(quantity2);
                }
                // second modifer
                if (rnd.Next(1, 101) <= inputs[3] & extras < inputs[10])
                {
                    lastword = modifier[rnd.Next(0, modifier.Count - 1)];
                    output.Add(lastword);
                    extras++;
                }
                // second colour
                if (rnd.Next(1, 101) <= inputs[5] & extras < inputs[10])
                {
                    lastword = colour[rnd.Next(0, colour.Count - 1)];
                    output.Add(lastword);
                    extras++;
                }
                // second noun
                mynoun = new List<string>();
                mynoun.AddRange(commonnounsthatcanbedoneto[rnd.Next(0, commonnounsthatcanbedoneto.Count - 1)].Split('/'));
                if (quantity2 == "a")
                {
                    output.Add(mynoun[0]);
                }
                else
                {
                    if (mynoun.Count == 1)
                    {
                        output.Add(mynoun[0] + "s");
                    }
                    else
                    {
                        output.Add(mynoun[1]);
                    }
                }
            }
            // convert "a" to "an" if required (mostly, things like "hour" won't be picked up
            for (int i = 0; i < output.Count-1 ; i++)
            {
                if (output[i] == "a" & "aeiouAEIOU".Contains(output[i + 1][0].ToString()))
                {
                    output[i] = "an";
                }
            }
            string outputString = String.Join(" ", output.ToArray());
            // capitalise first letter
            outputString  = outputString.Substring(0, 1).ToUpper() + outputString.Substring(1);
            return outputString;
        }
        public override bool loadPromptWords(List<string> packs)
        {
            quantity = new List<string>()
            {
                "a",
                "two",
                ""
            };
            modifier = new List<string>();
            colour = new List<string>();
            places = new List<string>();
            commonnounsthatcandothings = new List<string>();
            descriptor = new List<string>();
            verbs = new List<string>();
            commonnounsthatcanbedoneto = new List<string>();
            propernouns = new List<string>();
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (string file in packs)
            {
                Stream stream = assembly.GetManifestResourceStream("SketchyAs.Lang." + ShortName + "-" + file + ".txt");
                string line = "";
                using (var reader = new System.IO.StreamReader(stream))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains("modifier:"))
                        {
                            line = line.Replace("modifier:", "");
                            line = line = Regex.Replace(line, @"\t|\n|\r", "");
                            modifier.AddRange(line.Split(','));
                        }
                        else if (line.Contains("colour:"))
                        {
                            line = line.Replace("colour:", "");
                            line = line = Regex.Replace(line, @"\t|\n|\r", "");
                            colour.AddRange(line.Split(','));
                        }
                        else if (line.Contains("commonnounsthatcandothings:"))
                        {
                            line = line.Replace("commonnounsthatcandothings:", "");
                            line = line = Regex.Replace(line, @"\t|\n|\r", "");
                            commonnounsthatcandothings.AddRange(line.Split(','));
                        }
                        else if (line.Contains("descriptor:"))
                        {
                            line = line.Replace("descriptor:", "");
                            line = line = Regex.Replace(line, @"\t|\n|\r", "");
                            descriptor.AddRange(line.Split(','));
                        }
                        else if (line.Contains("verbs:"))
                        {
                            line = line.Replace("verbs:", "");
                            line = line = Regex.Replace(line, @"\t|\n|\r", "");
                            verbs.AddRange(line.Split(','));
                        }
                        else if (line.Contains("commonnounsthatcanbedoneto:"))
                        {
                            line = line.Replace("commonnounsthatcanbedoneto:", "");
                            line = line = Regex.Replace(line, @"\t|\n|\r", "");
                            commonnounsthatcanbedoneto.AddRange(line.Split(','));
                        }
                        else if (line.Contains("propernouns:"))
                        {
                            line = line.Replace("propernouns:", "");
                            line = line = Regex.Replace(line, @"\t|\n|\r", "");
                            propernouns.AddRange(line.Split(','));
                        }
                        else if (line.Contains("places:"))
                        {
                            line = line.Replace("places:", "");
                            line = line = Regex.Replace(line, @"\t|\n|\r", "");
                            places.AddRange(line.Split(','));
                        }
                    }
                }
            }
            return true;
        }
    }
}
