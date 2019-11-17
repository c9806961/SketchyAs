using System;

namespace SketchyAs
{
    public class English : SketchyLang
    {
        bool first = true; // for testing remove
        public English()
        {
            Language = "English";
            loadTranslations();
            loadPromptWords();
        }

        public override string getPrompt(bool nsfw)
        {
            // generate a prompt, else return base value
            string output = "";
            //START fortesting remove after
            if (first)
                output = "This is an english prompt";
            else
                output = "This is  a different english prompt";
            first = !first;
            //END fortesting remove after
            // if it fails to generate a prompt for some reason
            if (output.Equals(""))
                output = base.getPrompt(nsfw);
            return output;
        }
        public override bool loadPromptWords()
        {
            return true;
        }
    }
}
