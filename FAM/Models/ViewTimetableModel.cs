namespace FAM.Models
{
    public class ViewTimetableModel
    {
        public ViewTimetableModel(DateTime whichdaybois)
        {
            this.Monday = ConvertToMonday(whichdaybois);
            this.Tuesday = ConvertToTuesday(whichdaybois);
            this.Wednesday = ItIsWednesdayMyDudeAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA(whichdaybois);
            this.Thursday = ConvertToThurstyDay(whichdaybois);
            this.Friday = LastFridayNight(whichdaybois);
            this.Saturday = SaturdayNightIsAlright(whichdaybois);
            this.Sunday = SaveYourTearsTheWeeknd(whichdaybois);
        }

        public DateTime Monday { get; set; }
        public DateTime Tuesday { get; set; }
        public DateTime Wednesday { get; set; }
        public DateTime Thursday { get; set; }
        public DateTime Friday { get; set; }
        public DateTime Saturday { get; set; }
        public DateTime Sunday { get; set; }

        private DateTime ConvertToMonday(DateTime date)
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date.AddDays(-6).Date;
                case 1: return date.Date;
                case 2: return date.AddDays(-1).Date;
                case 3: return date.AddDays(-2).Date;
                case 4: return date.AddDays(-3).Date;
                case 5: return date.AddDays(-4).Date;
                case 6: return date.AddDays(-5).Date;
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }

        private DateTime ConvertToTuesday(DateTime date)
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date.AddDays(-5).Date;
                case 1: return date.AddDays(1).Date;
                case 2: return date.Date;
                case 3: return date.AddDays(-1).Date;
                case 4: return date.AddDays(-2).Date;
                case 5: return date.AddDays(-3).Date;
                case 6: return date.AddDays(-4).Date;
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }

        private DateTime ItIsWednesdayMyDudeAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA(DateTime date)
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date.AddDays(-4).Date;
                case 1: return date.AddDays(2).Date;
                case 2: return date.AddDays(1).Date;
                case 3: return date.Date;
                case 4: return date.AddDays(-1).Date;
                case 5: return date.AddDays(-2).Date;
                case 6: return date.AddDays(-3).Date;
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }

        private DateTime ConvertToThurstyDay(DateTime date)
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date.AddDays(-3).Date;
                case 1: return date.AddDays(3).Date;
                case 2: return date.AddDays(2).Date;
                case 3: return date.AddDays(1).Date;
                case 4: return date.Date;
                case 5: return date.AddDays(-1).Date;
                case 6: return date.AddDays(-2).Date;
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }

        private DateTime LastFridayNight(DateTime date)
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date.AddDays(-2).Date;
                case 1: return date.AddDays(4).Date;
                case 2: return date.AddDays(3).Date;
                case 3: return date.AddDays(2).Date;
                case 4: return date.AddDays(1).Date;
                case 5: return date.Date;
                case 6: return date.AddDays(-1).Date;
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }

        private DateTime SaturdayNightIsAlright(DateTime date)
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date.AddDays(-1).Date;
                case 1: return date.AddDays(5).Date;
                case 2: return date.AddDays(4).Date;
                case 3: return date.AddDays(3).Date;
                case 4: return date.AddDays(2).Date;
                case 5: return date.AddDays(1).Date;
                case 6: return date.Date;
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }

        private DateTime SaveYourTearsTheWeeknd(DateTime date)
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date.Date;
                case 1: return date.AddDays(6).Date;
                case 2: return date.AddDays(5).Date;
                case 3: return date.AddDays(4).Date;
                case 4: return date.AddDays(3).Date;
                case 5: return date.AddDays(2).Date;
                case 6: return date.AddDays(1).Date;
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }
    }
}
