using System;

namespace RunAndGun.Space
{
    public static class GlobalBuffer
    {
        public static GamePoints gamePoints;
        private static DateTime timer;
        public static void Reset()
        {
            gamePoints = new GamePoints();
            timer = DateTime.Now;
        }

        // public static DateTime TimeSpent()
        // {
        //     return DateTime.Now.Subtract(timer);
        // }
    }
}