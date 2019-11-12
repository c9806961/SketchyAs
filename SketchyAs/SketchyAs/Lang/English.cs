using System;

namespace SketchyAs
{
    public class English : SketchyLang
    {
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
            output = "This is an english prompt";
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
