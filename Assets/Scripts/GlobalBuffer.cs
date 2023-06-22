using System;
using UnityEngine;

namespace RunAndGun.Space
{
    public static class GlobalBuffer
    {
        public static GamePoints gamePoints;
        public static TimeSpan TimeSpent;
        public static bool failed;
        private static DateTime timer;
        public static void Reset()
        {
            gamePoints = new GamePoints();
            timer = DateTime.Now;
            failed = false;
        }

        public static void CalculateTimeSpent()
        {
            TimeSpent = DateTime.Now - timer;
        }
    }
}