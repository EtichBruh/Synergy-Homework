using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Synergy_HW.Lesson
{
    internal class Schedule
    {
        public static Dictionary<DateTime, LessonData> Lessons;
        public Schedule()
        {

        }
        public static void Initialize(byte[] data)
        {
            using (var stream = new MemoryStream(data))
                Lessons = JsonSerializer.Deserialize<Dictionary<DateTime, LessonData>>(stream, new JsonSerializerOptions() { WriteIndented = true, Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic) }) ?? new();
            Console.WriteLine(Lessons.Count);
        }
    }
}
