using System;
using System.Globalization;
using YourProjectName_Domain.Entity;

namespace YourProjectName_Domain.Helpers
{
    public static class UtilitiesHelper
    {
        public static Nullable<DateTime> GetDate(string date, string FieldName)
        {
            Nullable<DateTime> FinalDate = null;

            if (date.Length > 0)
            {
                try
                {
                    FinalDate = DateTime.ParseExact(date, "dd.MM.yyyy", null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Field (" + FieldName + ") - Content (" + date + "): " + ErrorHelper.GetAllErrorMessages(ex, false));
                }
            }
            return FinalDate;
        }

        public static DateTime GetTime(string time, string FieldName)
        {
            DateTime FinalDate = DateTime.ParseExact("01.01.1900", "dd.MM.yyyy", null);
            try
            {
                if (time.Length > 0)
                {
                    if (time.Equals("24:00:00"))
                    {
                        time = "00:00:00";
                    }
                    FinalDate = DateTime.ParseExact(time, "HH:mm:ss", null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Field (" + FieldName + ") - Content (" + time + "): " + ErrorHelper.GetAllErrorMessages(ex, false));
            }
            return FinalDate;
        }

        public static int GetInt(string numero, string FieldName)
        {
            int ReturnNumber = 0;
            if ((numero.Length == 0) || numero.Equals("#N/A") || numero.Equals("#REF!") || numero.Contains(".") || numero.Contains(","))
            {
                numero = "0";
            }
            try
            {
                ReturnNumber = int.Parse(numero);
            }
            catch (Exception ex)
            {
                throw new Exception("Field (" + FieldName + ") - Content (" + numero + "): " + ErrorHelper.GetAllErrorMessages(ex, false));
            }

            return ReturnNumber;
        }

        public static decimal GetDecimal(string numero, string FieldName)
        {
            NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
            CultureInfo provider = new CultureInfo("pt-BR");

            decimal ReturnNumber = 0;
            if (numero.Length == 0)
            {
                numero = "0";
            }
            try
            {
                ReturnNumber = decimal.Parse(numero, style, provider);
            }
            catch (Exception ex)
            {
                throw new Exception("Field (" + FieldName + ") - Content (" + numero + "): " + ErrorHelper.GetAllErrorMessages(ex, false));
            }

            return ReturnNumber;
        }
    }
}