using System.Text.RegularExpressions;
using Xunit;

namespace Duplicate.Removal.Tests
{
    public class TryOuts
    {
        [Fact]
        public void AnnotationExtractionFromPattern()
        {
            var pattern =
                "\\<p id=\"annotation-(?<num>\\d*)\" class=\"annotation\"\\>\\<a href=\"(?<htmlfile>[^\\.]*\\.html)#anchor-(?<num>\\d*)\"\\>(?<num>\\d*)\\<\\/a\\>\\. \\<em class=\"foreign-word\"\\>(?<word>[^\\<]*)\\<\\/em\\> — (?<definition>[^\\<]*)\\<\\/p\\>";

            var text =
                "Lorem ipsum something something <p id=\"annotation-3280\" class=\"annotation\"><a href=\"part54.html#anchor-3280\">3280</a>. <em class=\"foreign-word\">word4</em> — something</p> Lorem lorem";

            var result = Regex.Match(text, pattern);

            var capturedNum = GetCapturedValue(result, "num");
            var capturedHtmlFile = GetCapturedValue(result, "htmlfile");
            var capturedDefinition = GetCapturedValue(result, "definition");
            var capturedWord = GetCapturedValue(result, "word");

            Assert.Equal("3280", capturedNum);
            Assert.Equal("part54.html", capturedHtmlFile);
            Assert.Equal("word4", capturedWord);
            Assert.Equal("something", capturedDefinition);
        }

        private string GetCapturedValue(Match match, string captureGroupName)
        {
            return match.Groups[captureGroupName].Captures[0].Value;
        }

        [Fact]
        public void MultipleAnnotationsExtraction()
        {
            var pattern =
                "\\<p id=\"annotation-(?<num>\\d*)\" class=\"annotation\"\\>\\<a href=\"(?<htmlfile>[^\\.]*\\.html)#anchor-(?<num>\\d*)\"\\>(?<num>\\d*)\\<\\/a\\>\\. \\<em class=\"foreign-word\"\\>(?<word>[^\\<]*)\\<\\/em\\> — (?<definition>[^\\<]*)\\<\\/p\\>";

            var text =
                "Lorem ipsum something something <p id=\"annotation-3280\" class=\"annotation\"><a href=\"part54.html#anchor-3280\">3280</a>. <em class=\"foreign-word\">word4</em> — something</p> Lorem lorem Lorem ipsum something something <p id=\"annotation-3281\" class=\"annotation\"><a href=\"part55.html#anchor-3281\">3281</a>. <em class=\"foreign-word\">super word</em> — its definition</p> Lorem lorem";

            var result = Regex.Matches(text, pattern);

            Assert.Equal(2, result.Count);

            var firstMatch = result[0];

            var capturedNum = GetCapturedValue(firstMatch, "num");
            var capturedHtmlFile = GetCapturedValue(firstMatch, "htmlfile");
            var capturedDefinition = GetCapturedValue(firstMatch, "definition");
            var capturedWord = GetCapturedValue(firstMatch, "word");

            Assert.Equal("3280", capturedNum);
            Assert.Equal("part54.html", capturedHtmlFile);
            Assert.Equal("word4", capturedWord);
            Assert.Equal("something", capturedDefinition);

            var secondMatch = result[1];

            capturedNum = GetCapturedValue(secondMatch, "num");
            capturedHtmlFile = GetCapturedValue(secondMatch, "htmlfile");
            capturedDefinition = GetCapturedValue(secondMatch, "definition");
            capturedWord = GetCapturedValue(secondMatch, "word");

            Assert.Equal("3281", capturedNum);
            Assert.Equal("part55.html", capturedHtmlFile);
            Assert.Equal("super word", capturedWord);
            Assert.Equal("its definition", capturedDefinition);
        }

        [Fact]
        public void MultiplePatternMatches()
        {
            var pattern = "lorem(?<num>\\d*)";
            var text = "lorem1 ipsum lorem2 lorem3 ipsum";

            var matches = Regex.Matches(text, pattern);

            Assert.Equal(3, matches.Count);
        }
    }
}
