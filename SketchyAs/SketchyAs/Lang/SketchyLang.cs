using System;
using System.Collections.Generic;
namespace SketchyAs
{
   // I needed a class to generate the silly prompts so I figured I'd build it so we could possibly do translations of UI elements in the future
  public abstract class SketchyLang
  {

        public string Language { get; set; } = "none";
        public Dictionary<string, string> TransList;

        public SketchyLang()
        {
            TransList = new Dictionary<string, string>();
        }

        // responsible for loading translations in the future. Get read from localisation file as key,value pairs into TransList
        public bool loadTranslations()
        {
            if (Language.Equals("none"))
                return false;
            // load langauge here
            return true;
        }

        // translate to be used one day to supply UI translations for different languages
        public string translate(string s)
        {
            try
            {
                return TransList[s];
            }
            catch (Exception)
            {
                return "unavailable";
            }
        }

        // loadPromptWords to be overridden is subclasses as types of words changes from language to language
        public virtual bool loadPromptWords()
        {
            return true;
        }

        // getPrompt to be overridden is subclasses as the sentence structure changes from language to language
        public virtual string getPrompt(bool nsfw)
        {
            // generate the prompt here
            return "No prompt available";
        }

  }
}
