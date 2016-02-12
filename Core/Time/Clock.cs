using System;

namespace DotOriko.Core.Time {
	public class Clock {

		private DateTime now;
		private TimeSpan timeNow;
		private TimeSpan gameTime;
		private int minutesPerDay; //Realtime minutes per game-day (1440 would be realtime)
		
		public Clock(int minPerDay) {
			minutesPerDay = minPerDay;
		}
		
		public TimeSpan GetTime() {
			this.now      = DateTime.Now;
			this.timeNow  = this.now.TimeOfDay;

			double   hours = timeNow.TotalMinutes % minutesPerDay;
			double minutes = (hours % 1) * 60;
			double seconds = (minutes % 1) * 60;

			this.gameTime = new TimeSpan((int)hours,(int)minutes,(int)seconds);
			
			return gameTime;
		}
	}
}
