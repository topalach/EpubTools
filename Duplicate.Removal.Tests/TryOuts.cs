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
                "\\<p id=\"annotation-(?<num>\\d*)\" class=\"annotation\"\\>\\<a href=\"(?<htmlfile>.*\\.html)#anchor-(?<num>\\d*)\"\\>(?<num>\\d*)\\<\\/a\\>\\. \\<em class=\"foreign-word\"\\>word4\\<\\/em\\> — something\\<\\/p\\>";

            var text =
                "<p id=\"annotation-3280\" class=\"annotation\"><a href=\"part54.html#anchor-3280\">3280</a>. <em class=\"foreign-word\">word4</em> — something</p>";

            var result = Regex.Match(text, pattern);

            var capturedNum = GetCapturedValue(result, "num");
            var capturedHtmlFile = GetCapturedValue(result, "htmlfile");

            Assert.Equal("3280", capturedNum);
            Assert.Equal("part54.html", capturedHtmlFile);
        }

        private string GetCapturedValue(Match match, string captureGroupName)
        {
            return match.Groups[captureGroupName].Captures[0].Value;
        }
    }
}
