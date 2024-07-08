using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Helper
    {
        #region Members

        private static DateTime dtMinDate = new(1753, 1, 1);
        public enum StampActions
        {
            EditedBy = 1,
            DateTime = 2,
            Custom = 4
        }

        #endregion

        #region Properties
        /// <summary>
        /// Get the minimum date in the program.
        /// </summary>
        public static DateTime MinDate
        {
            get
            {
                return dtMinDate;
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Checks whether a string is an integer number or not.
        /// </summary>
        /// <param name="Input">The string to check whether it is an integer number or not.</param>
        /// <returns>Returns true if the input string is an integer number otherwise it returns false.</returns>
        public static bool CheckNumberInt(string strInput)
        {
            if (strInput.Length == 0)
                return false;
            if (strInput[0] == '-')
                strInput = strInput.Substring(1);
            foreach (char A in strInput.ToCharArray())
            {
                if (A == '0' || A == '1' || A == '2' || A == '3' ||
                    A == '4' || A == '5' || A == '6' || A == '7' ||
                    A == '8' || A == '9')
                    continue;
                else
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Checks whether an object  is an integer number or not.
        /// </summary>
        /// <param name="objInput">The object to check whether it is an integer number or not.</param>
        /// <returns>Returns true if the input object is an integer number otherwise it returns false.</returns>
        public static bool CheckNumberInt(object objInput)
        {
            if (objInput == null)
                return false;
            else
                return CheckNumberInt(objInput.ToString());
        }
        /// <summary>
        /// Checks whether a string is a double number or not.
        /// </summary>
        /// <param name="Input">The string to check whether it is a double number or not.</param>
        /// <returns>Returns true if the input string is a double number otherwise it returns false.</returns>
        public static bool CheckNumberDouble(string strInput)
        {
            if (strInput.Length == 0)
                return false;
            if (strInput.StartsWith(".")) return false;
            if (strInput.EndsWith(".")) return false;
            string SubInput = strInput.Substring(strInput.IndexOf('.') + 1);
            if (SubInput.Contains('.')) return false;
            foreach (char A in strInput.ToCharArray())
            {
                if (A == '0' || A == '1' || A == '2' || A == '3' ||
                    A == '4' || A == '5' || A == '6' || A == '7' ||
                    A == '8' || A == '9' || A == '.')
                    continue;
                else
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Checks whether an object is a double number or not.
        /// </summary>
        /// <param name="Input">The object to check whether it is a double number or not.</param>
        /// <returns>Returns true if the input object is a double number otherwise it returns false.</returns>
        public static bool CheckNumberDouble(object objInput)
        {
            if (objInput == null)
                return false;
            else
                return CheckNumberDouble(objInput.ToString());
        }
        /// <summary>
        /// Tests if the supplied byte array represents an image or not.
        /// </summary>
        /// <param name="data">The byte array to test.</param>
        /// <returns>Returns true if the supplied byte array is an image other wise it returns false.</returns>
        public static bool IsImage(byte[] data)
        {
            //read 64 bytes of the stream only to determine the type
            string myStr = Encoding.ASCII.GetString(data).Substring(0, 16);
            //check if its definately an image.
            if (myStr.Substring(8, 2).ToString().ToLower() != "if")
            {         //its not a jpeg
                if (myStr.Substring(0, 3).ToString().ToLower() != "gif")
                {                //its not a gif 
                    if (myStr.Substring(0, 2).ToString().ToLower() != "bm")
                    {                   //its not a .bmp                   
                        if (myStr.Substring(0, 2).ToString().ToLower() != "ii")
                            return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Converts a string in arabic language to it's finger print to avoid arabic dictation errors.
        /// </summary>
        /// <param name="inString">The string that we want to calculate it's finger print.</param>
        /// <returns>Returns a finger print for the string.</returns>
        public static string FingerPrintString(string inString)
        {
            string FpString = inString;
            if (FpString.IndexOf("اللة") != -1) FpString = FpString.Replace("اللة", "الله");
            if (FpString.IndexOf('أ') != -1) FpString = FpString.Replace('أ', 'ا');
            if (FpString.IndexOf('إ') != -1) FpString = FpString.Replace('إ', 'ا');
            if (FpString.IndexOf('آ') != -1) FpString = FpString.Replace('آ', 'ا');
            if (FpString.IndexOf('ì') != -1) FpString = FpString.Replace('ì', 'í');
            if (FpString.IndexOf('ة') != -1) FpString = FpString.Replace('ة', 'ه');
            if (FpString.IndexOf('-') != -1) FpString = FpString.Replace("-", "");
            if (FpString.IndexOf(" ") != -1) FpString = FpString.Replace(" ", "");
            return FpString;
        }
         static bool CheckColumns(DataTable tblSource, string[] strColumns)
        {
            foreach (string strColumn in strColumns)
                if (!tblSource.Columns.Contains(strColumn))
                    return false;
            return true;
        }
        public static DataTable DecryptColumns(DataTable tblSource, string EncryptionKey, params string[] strColumns)
        {
            try
            {
                DataTable tblResult = tblSource;
                if (CheckColumns(tblSource, strColumns) && tblSource.Rows.Count > 0)
                {
                    for (int i = 0; i < tblSource.Rows.Count; i++)
                        for (int j = 0; j < strColumns.Length; j++)
                        {
                            if (tblSource.Rows[i][strColumns[j]] != null && tblSource.Rows[i][strColumns[j]] != DBNull.Value)
                            {
                                string SourceData = tblSource.Rows[i][strColumns[j]].ToString();
                                (bool, string) Result = Downloader.Download(SourceData ?? string.Empty, EncryptionKey);
                                if (Result.Item1)
                                    tblResult.Rows[i][strColumns[j]] = Result.Item2;
                            }
                        }
                }
                return tblResult;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex);
            }
            return new DataTable();
        }
        /// <summary>
        /// Decrypts the data of the supplied tables.
        /// </summary>
        /// <param name="tblSource"></param>
        /// <param name="strColumns"></param>
        /// <param name="strSuff"></param>
        /// <returns></returns>
        public static DataTable DecryptColumns(DataTable tblSource, string strSuff, string EncryptionKey, params string[] strColumns)
        {
            try
            {
                DataTable tblResult = tblSource;
                if (CheckColumns(tblSource, strColumns) && tblSource.Rows.Count > 0)
                {
                    for (int i = 0; i < tblSource.Rows.Count; i++)
                        for (int j = 0; j < strColumns.Length; j++)
                            if (tblSource.Rows[i][strColumns[j]] != null && tblSource.Rows[i][strColumns[j]] != DBNull.Value)
                            {
                                string SourceData = tblSource.Rows[i][strColumns[j]].ToString();
                                (bool, string) Result = Downloader.Upload(SourceData ?? string.Empty, EncryptionKey);
                                if (Result.Item1)
                                    tblResult.Rows[i][strColumns[j]] = strSuff + Result.Item2;
                            }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex);
            }
            return new DataTable();
        }
        public static string DisplayParkingPeriod(TimeSpan tsPeriod)
        {
            int iHours = 0, iMinitues, iSeconds;
            if (tsPeriod.Days > 0)
                iHours = tsPeriod.Days * 24;
            iHours += tsPeriod.Hours;
            iMinitues = tsPeriod.Minutes;
            iSeconds = tsPeriod.Seconds;
            return string.Format("{0} h: {1} : m : {2} s", iHours, iMinitues, iSeconds);
        }

        #endregion
    }
}
