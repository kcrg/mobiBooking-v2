using Itenso.TimePeriod;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using mobiBooking.Component.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace mobiBooking.Component
{
    public static class Helpers
    {

        public static string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static bool CheckDateOverlaps(DateTime AdateFrom, DateTime AdateTo, DateTime BdateFrom, DateTime BdateTo)
        {
            return new TimeRange(AdateFrom, AdateTo).OverlapsWith(new TimeRange(BdateFrom, BdateTo));
        }

        public static bool CheckDateInside(DateTime AdateFrom, DateTime AdateTo, DateTime BdateFrom, DateTime BdateTo)
        {
            return new TimeRange(AdateFrom, AdateTo).HasInside(new TimeRange(BdateFrom, BdateTo));
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime EndOfWeek(this DateTime date, DayOfWeek startOfWeek)
        {
            return date.StartOfWeek(startOfWeek).AddDays(6);
        }

        //return true if interval reservation overlaps with date
        public static bool CheckIntervalReservation(DateTime reservDateFrom, DateTime reservDateTo, DateTime dateFrom, DateTime dateTo, Intervals? interval, bool cyclicReservation)
        {

            if (!cyclicReservation)
            {
                return false;
            }

            switch (interval.Value)
            {
                case Intervals.Day:
                    return CheckDateOverlaps(DateTime.Now.Date.AddHours(reservDateFrom.Hour).AddMinutes(reservDateFrom.Minute),
                         DateTime.Now.Date.AddHours(reservDateTo.Hour).AddMinutes(reservDateTo.Minute),
                         DateTime.Now.Date.AddHours(dateFrom.Hour).AddMinutes(dateFrom.Minute),
                         DateTime.Now.Date.AddHours(dateTo.Hour).AddMinutes(dateTo.Minute));

                case Intervals.Week:

                    if (reservDateFrom.DayOfWeek == dateFrom.DayOfWeek && reservDateTo.DayOfWeek == dateTo.DayOfWeek)
                    {
                        return CheckDateOverlaps(DateTime.Now.Date.AddHours(reservDateFrom.Hour).AddMinutes(reservDateFrom.Minute),
                             DateTime.Now.Date.AddHours(reservDateTo.Hour).AddMinutes(reservDateTo.Minute),
                             DateTime.Now.Date.AddHours(dateFrom.Hour).AddMinutes(dateFrom.Minute),
                             DateTime.Now.Date.AddHours(dateTo.Hour).AddMinutes(dateTo.Minute));
                    }
                    else
                    {
                        return false;
                    }

                case Intervals.TwoWeeks:

                    if ((dateFrom - reservDateFrom).Days % 14 == 0 && (dateTo - reservDateTo).Days % 14 == 0)
                    {
                        return CheckDateOverlaps(DateTime.Now.Date.AddHours(reservDateFrom.Hour).AddMinutes(reservDateFrom.Minute),
                             DateTime.Now.Date.AddHours(reservDateTo.Hour).AddMinutes(reservDateTo.Minute),
                             DateTime.Now.Date.AddHours(dateFrom.Hour).AddMinutes(dateFrom.Minute),
                             DateTime.Now.Date.AddHours(dateTo.Hour).AddMinutes(dateTo.Minute));
                    }
                    else
                    {
                        return false;
                    }

                case Intervals.Month:

                    if (reservDateFrom.Day == dateFrom.Day && reservDateTo.Day == dateTo.Day)
                    {
                        return CheckDateOverlaps(DateTime.Now.Date.AddHours(reservDateFrom.Hour).AddMinutes(reservDateFrom.Minute),
                             DateTime.Now.Date.AddHours(reservDateTo.Hour).AddMinutes(reservDateTo.Minute),
                             DateTime.Now.Date.AddHours(dateFrom.Hour).AddMinutes(dateFrom.Minute),
                             DateTime.Now.Date.AddHours(dateTo.Hour).AddMinutes(dateTo.Minute));
                    }
                    else
                    {
                        return false;
                    }

                case Intervals.Year:
                    if (reservDateFrom.Day == dateFrom.Day && reservDateTo.Day == dateTo.Day && reservDateFrom.Month == dateFrom.Month && reservDateTo.Month == dateTo.Month)
                    {
                        return CheckDateOverlaps(DateTime.Now.Date.AddHours(reservDateFrom.Hour).AddMinutes(reservDateFrom.Minute),
                             DateTime.Now.Date.AddHours(reservDateTo.Hour).AddMinutes(reservDateTo.Minute),
                             DateTime.Now.Date.AddHours(dateFrom.Hour).AddMinutes(dateFrom.Minute),
                             DateTime.Now.Date.AddHours(dateTo.Hour).AddMinutes(dateTo.Minute));
                    }
                    else
                    {
                        return false;
                    }

                default: return false;
            }
        }
    }
}
