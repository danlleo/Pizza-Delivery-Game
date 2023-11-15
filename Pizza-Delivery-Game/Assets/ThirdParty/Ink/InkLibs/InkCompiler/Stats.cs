
using Ink.Parsed;

namespace Ink {
    public struct Stats {

        public int words;
        public int knots;
        public int stitches;
        public int functions;
        public int choices;
        public int gathers;
        public int diverts;

        public static Stats Generate(Story story) {
            var stats = new Stats();

            var allText = story.FindAll<Text>();

            // Count all the words across all strings
            stats.words = 0;
            foreach(var text in allText) {

                var wordsInThisStr = 0;
                var wasWhiteSpace = true;
                foreach(var c in text.text) {
                    if( c == ' ' || c == '\t' || c == '\n' || c == '\r' ) {
                        wasWhiteSpace = true;
                    } else if( wasWhiteSpace ) {
                        wordsInThisStr++;
                        wasWhiteSpace = false;
                    }
                }

                stats.words += wordsInThisStr;
            }

            var knots = story.FindAll<Knot>();
            stats.knots = knots.Count;

            stats.functions = 0;
            foreach(var knot in knots)
                if (knot.isFunction) stats.functions++;

            var stitches = story.FindAll<Stitch>();
            stats.stitches = stitches.Count;

            var choices = story.FindAll<Choice>();
            stats.choices = choices.Count;

            // Skip implicit gather that's generated at top of story
            // (we know which it is because it isn't assigned debug metadata)
            var gathers = story.FindAll<Gather>(g => g.debugMetadata != null);
            stats.gathers = gathers.Count;

            // May not be entirely what you expect.
            // Does it nevertheless have value?
            // Includes:
            //  - DONE, END
            //  - Function calls
            //  - Some implicitly generated weave diverts
            // But we subtract one for the implicit DONE
            // at the end of the main flow outside of knots.
            var diverts = story.FindAll<Divert>();
            stats.diverts = diverts.Count - 1;

            return stats;
        }
    }
}