using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Synergy_HW.Lesson
{
    internal class Schedule
    {
        static Dictionary<DateTime, LessonData[]> Lessons;
        public Schedule()
        {
            
        }
        public static void Initialize(string data)
        {
            Lessons = JsonSerializer.Deserialize<Dictionary<DateTime, LessonData[]>>(data, new JsonSerializerOptions() { WriteIndented = true}) ?? new();
            Console.WriteLine(Lessons.Count);
        }
    }
}
