using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Helper
    {
        #region Members

        private static DateTime dtMinDate = new DateTime(1753, 1, 1);
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

        /// <summary>
        /// Gets the message header of the program.
        /// </summary>
        public static string MessageHeader
        {
            get
            {
                return Resources.ProgramMessages.MesHeader;
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
            if (SubInput.IndexOf('.') != -1) return false;
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
        /// Gets the suitable option for the message in the message box according to the current language.
        /// </summary>
        /// <returns>Returns the option of the message in the message box according to the current language.</returns>
        public static MessageBoxOptions GetMessageBoxOptions()
        {
            return MessageBoxOptions.RtlReading;
        }
        /// <summary>
        /// Displays a message box to the user.
        /// </summary>
        /// <param name="Message">The message that will appear to the user in the message box.</param>
        /// <returns>Returns the button that the user pressed in the message box.</returns>
        public static DialogResult ShowMessage(string Message)
        {
            
            return MessageBox.Show(Message, MessageHeader, MessageBoxButtons.OK
                   , MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                   GetMessageBoxOptions());
        }
        /// <summary>
        /// Displays a message box to the user.
        /// </summary>
        /// <param name="Message">The message that will appear to the user in the message box.</param>
        /// <param name="MessageButtons">The buttons that will appear to the user in the message box.</param>
        /// <returns>Returns the button that the user pressed in the message box.</returns>
        public static DialogResult ShowMessage(string Message, MessageBoxButtons MessageButtons)
        {
            return MessageBox.Show(Message, MessageHeader, MessageButtons
                   , MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                   GetMessageBoxOptions());
        }
        /// <summary>
        /// Displays a message box to the user.
        /// </summary>
        /// <param name="Message">The message that will appear to the user in the message box.</param>
        /// <param name="MessageButtons">The buttons that will appear to the user in the message box.</param>
        /// <param name="MessageIcon">The icon that will appear to the user in the message box.</param>
        /// <returns>Returns the button that the user pressed in the message box.</returns>
        public static DialogResult ShowMessage(string Message, MessageBoxButtons MessageButtons, MessageBoxIcon MessageIcon)
        {
            return MessageBox.Show(Message, MessageHeader, MessageButtons, MessageIcon
                , MessageBoxDefaultButton.Button1, GetMessageBoxOptions());
        }
        /// <summary>
        /// Displays a message box to the user.
        /// </summary>
        /// <param name="Message">The message that will appear to the user in the message box.</param>
        /// <param name="MessageButtons">The buttons that will appear to the user in the message box.</param>
        /// <param name="MessageIcon">The icon that will appear to the user in the message box.</param>
        /// <param name="MessageDefBtn">The default button that it is focused when the message box appears.</param>
        /// <returns>Returns the button that the user pressed in the message box.</returns>
        public static DialogResult ShowMessage(string Message, MessageBoxButtons MessageButtons, MessageBoxIcon MessageIcon, MessageBoxDefaultButton MessageDefBtn)
        {
            return MessageBox.Show(Message, MessageHeader, MessageButtons, MessageIcon
                , MessageDefBtn, GetMessageBoxOptions());
        }
        /// <summary>
        /// Gets the error icon alignment according to the language in the
        /// configuration file.
        /// </summary>
        /// <returns>Returns the icon alignment.</returns>
        public static ErrorIconAlignment GetErrorIconAlignment()
        {
            return ErrorIconAlignment.MiddleLeft;
        }
        /// <summary>
        /// Tests if the supplied byte array represents an image or not.
        /// </summary>
        /// <param name="data">The byte array to test.</param>
        /// <returns>Returns true if the supplied byte array is an image other wise it returns false.</returns>
        public static bool IsImage(byte[] data)
        {
            //read 64 bytes of the stream only to determine the type
            string myStr = System.Text.Encoding.ASCII.GetString(data).Substring(0, 16);
            //check if its definately an image.
            if (myStr.Substring(8, 2).ToString().ToLower() != "if")
            {         //its not a jpeg
                if (myStr.Substring(0, 3).ToString().ToLower() != "gif")
                {                //its not a gif 
                    if (myStr.Substring(0, 2).ToString().ToLower() != "bm")
                    {                   //its not a .bmp                   
                        if (myStr.Substring(0, 2).ToString().ToLower() != "ii")
                        {
                            myStr = null;
                            return false;
                        }
                    }
                }
            }
            myStr = null;
            return true;
        }
        /// <summary>
        /// Gets the input language corresponding to the supplied culture name.
        /// </summary>
        /// <param name="strRequiredLanguage">The culture name of the required input language (it is "ar" or "en").
        /// </param>
        /// <returns>Returns the input language of the supplied culture name if it was not installed it returns the
        /// current input language.</returns>
        public static InputLanguage GetInputLanguage(string strRequiredLanguage)
        {
            foreach (InputLanguage inplang in InputLanguage.InstalledInputLanguages)
                if (inplang.Culture.Name.Contains(strRequiredLanguage))
                    return inplang;
            //If the code reached here then there is no approperiate installed language.
            string strRequiredLanguageAbsent = Resources.ProgramMessages.MesNoArabicInput;
            if (strRequiredLanguage.ToLower().Contains("en"))
                strRequiredLanguageAbsent = Resources.ProgramMessages.MesNoEnglishInput;
            if (Helper.ShowMessage(strRequiredLanguageAbsent, MessageBoxButtons.RetryCancel,
                MessageBoxIcon.Question) == DialogResult.Retry)
                return GetInputLanguage(strRequiredLanguage);
            else
                Helper.ShowMessage(Resources.ProgramMessages.MesCantWriteInField);
            //Return the current input language.
            return InputLanguage.CurrentInputLanguage;
        }
        /// <summary>
        /// Converts a string in arabic language to it's finger print to avoid arabic dictation errors.
        /// </summary>
        /// <param name="inString">The string that we want to calculate it's finger print.</param>
        /// <returns>Returns a finger print for the string.</returns>
        public static string FingerPrintString(string inString)
        {
            string FpString = inString;
            if (FpString.IndexOf("Çááå") != -1) FpString = FpString.Replace("Çááå", "ááå");
            if (FpString.IndexOf('Ã') != -1) FpString = FpString.Replace('Ã', 'Ç');
            if (FpString.IndexOf('Å') != -1) FpString = FpString.Replace('Å', 'Ç');
            if (FpString.IndexOf('Â') != -1) FpString = FpString.Replace('Â', 'Ç');
            if (FpString.IndexOf('ì') != -1) FpString = FpString.Replace('ì', 'í');
            if (FpString.IndexOf('É') != -1) FpString = FpString.Replace('É', 'å');
            if (FpString.IndexOf('-') != -1) FpString = FpString.Replace("-", "");
            if (FpString.IndexOf(" ") != -1) FpString = FpString.Replace(" ", "");
            return FpString;
        }
        public static System.Data.DataTable DecryptColumns(System.Data.DataTable tblSource, params string[] strColumns)
        {
            try
            {
                System.Data.DataTable tblResult = tblSource;
                if (SqlAdoWrapper.CheckColumns(tblSource, strColumns) && tblSource.Rows.Count > 0)
                {
                    for (int i = 0; i < tblSource.Rows.Count; i++)
                        for (int j = 0; j < strColumns.Length; j++)
                            tblResult.Rows[i][strColumns[j]] = ERFT.Rtgy(tblSource.Rows[i][strColumns[j]].ToString());
                }
                return tblResult;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex);
            }
            return null;
        }
        /// <summary>
        /// Decrypts the data of the supplied tables.
        /// </summary>
        /// <param name="tblSource"></param>
        /// <param name="strColumns"></param>
        /// <param name="strSuff"></param>
        /// <returns></returns>
        public static System.Data.DataTable DecryptColumns(System.Data.DataTable tblSource, string[] strColumns, string strSuff)
        {
            try
            {
                System.Data.DataTable tblResult = tblSource;
                if (SqlAdoWrapper.CheckColumns(tblSource, strColumns) && tblSource.Rows.Count > 0)
                {
                    for (int i = 0; i < tblSource.Rows.Count; i++)
                        for (int j = 0; j < strColumns.Length; j++)
                            tblResult.Rows[i][strColumns[j]] = strSuff + ERFT.Rtgy(tblSource.Rows[i][strColumns[j]].ToString()) + strSuff;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex);
            }
            return null;
        }
        public static string DisplayParkingPeriod(TimeSpan tsPeriod)
        {
            int iHours = 0, iMinitues = 0, iSeconds = 0;
            if (tsPeriod.Days > 0)
                iHours = tsPeriod.Days * 24;
            iHours += tsPeriod.Hours;
            iMinitues = tsPeriod.Minutes;
            iSeconds = tsPeriod.Seconds;
            return string.Format("{0} h: {1} : m : {2} s", iHours, iMinitues, iSeconds);
        }
        public static DateTime GetCurrentServerDate()
        {
            object objCurrentDate = SqlAdoWrapper.ExecuteScalarCommand("select getdate()", null, true);
            if (objCurrentDate != null && objCurrentDate != DBNull.Value)
                return Convert.ToDateTime(objCurrentDate);
            return dtMinDate;
        }

        #endregion
    }
}
