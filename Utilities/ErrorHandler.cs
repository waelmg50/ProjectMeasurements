using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Utilities
{
    public class ErrorHandler
    {
        #region Public Procedures

        /// <summary>
        /// Display a message to the user with the error and writes it in the error file.
        /// </summary>
        /// <param name="e">The Exception object that raised in the application.</param>
        public static void LogError(IndexOutOfRangeException e)
        {
            ShowErrorMessageToUser(e.Message);
            WriteError(new string[] {"Error Message : " + e.Message , "Error Source : " + e.Source , "Error Trace : "
                + e.StackTrace});
        }
        /// <summary>
        /// Display a message to the user with the error and writes it in the error file.
        /// </summary>
        /// <param name="e">The Exception object that raised in the application.</param>
        public static void LogError(InvalidCastException e)
        {
            ShowErrorMessageToUser(e.Message);
            WriteError(new string[] {"Error Message : " + e.Message, "Error Type : " + e.GetType().Name,
                "Error Source : " + e.Source, "Error Trace : " + e.StackTrace});
        }
        /// <summary>
        /// Display a message to the user with the error and writes it in the error file.
        /// </summary>
        /// <param name="e">The Exception object that raised in the application.</param>
        public static void LogError(ArgumentNullException e)
        {
            ShowErrorMessageToUser(e.Message);
            WriteError(new string[] {"Param Name : " + e.ParamName, "Error Message : " + e.Message, "Error Type : "
                + e.GetType().Name, "Error Source : " + e.Source, "Error Trace : " + e.StackTrace});
        }
        /// <summary>
        /// Display a message to the user with the error and writes it in the error file.
        /// </summary>
        /// <param name="e">The Exception object that raised in the application.</param>
        public static void LogError(ArgumentException e)
        {
            ShowErrorMessageToUser(e.Message);
            WriteError(new string[] {"Param Name : " + e.ParamName, "Error Message : " + e.Message, "Error Type : "
                + e.GetType().Name, "Error Source : " + e.Source, "Error Trace : " + e.StackTrace});
        }
        /// <summary>
        /// Display a message to the user with the error and writes it in the error file.
        /// </summary>
        /// <param name="e">The Exception object that raised in the application.</param>
        public static void LogError(ArgumentOutOfRangeException e)
        {
            ShowErrorMessageToUser(e.Message);
            WriteError(new string[] {"Actual Value : " + e.ActualValue, "Param Name : " + e.ParamName,
                "Error Message : " + e.Message, "Error Type : " + e.GetType().Name, "Error Source : " + e.Source,
                "Error Trace : " + e.StackTrace});
        }
        /// <summary>
        /// Display a message to the user with the error and writes it in the error file.
        /// </summary>
        /// <param name="e">The Exception object that raised in the application.</param>
        public static void LogError(ArithmeticException e)
        {
            ShowErrorMessageToUser(e.Message);
            WriteError(new string[] {"Error Message : " + e.Message, "Error Type : " + e.GetType().Name,
                "Error Source : " + e.Source, "Error Trace : " + e.StackTrace});
        }
        /// <summary>
        /// Display a message to the user with the error and writes it in the error file.
        /// </summary>
        /// <param name="e">The Exception object that raised in the application.</param>
        public static void LogError(BadImageFormatException e)
        {
            ShowErrorMessageToUser(e.Message);
            WriteError(new string[] {"File Name : " + e.FileName, "Fusion Log : " + e.FusionLog, "Error Message : "
                + e.Message, "Error Source : " + e.Source, "Error Trace : " + e.StackTrace});
        }
        /// <summary>
        /// Display a message to the user with the error and writes it in the error file.
        /// </summary>
        /// <param name="e">The Exception object that raised in the application.</param>
        public static void LogError(NullReferenceException e)
        {
            ShowErrorMessageToUser(e.Message);
            WriteError(new string[] {"Error Message : " + e.Message, "Error Type : " + e.GetType().Name,
                "Error Source : " + e.Source, "Error Trace : " + e.StackTrace});
        }
        /// <summary>
        /// Display a message to the user with the error and writes it in the error file.
        /// </summary>
        /// <param name="e">The Exception object that raised in the application.</param>
        public static void LogError(UnauthorizedAccessException e)
        {
            ShowErrorMessageToUser(e.Message);
            WriteError(new string[] {"Error Message : " + e.Message, "Error Type : " + e.GetType().Name,
                "Error Source : " + e.Source, "Error Trace : " + e.StackTrace});
        }
        /// <summary>
        /// Display a message to the user with the error and writes it in the error file.
        /// </summary>
        /// <param name="e">The Exception object that raised in the application.</param>
        public static void LogError(Exception e)
        {
            if (e is OutOfMemoryException)
            {
                //Helper.ShowMessage(Resources.ProgramMessages.ErrMesMemoryFull);
                WriteError(new string[] {"Error Message : " + e.Message, "Error Type : " + e.GetType().Name,
                    "Error Type : " + e.GetType().Name, "Error Source : " + e.Source, "Error Trace : " + e.StackTrace});
                GC.Collect();
                //Application.Exit();
                return;
            }
            if (e is ArgumentNullException)
            {
                ArgumentNullException ex = (ArgumentNullException)e;
                LogError(ex);
                return;
            }
            if (e is ArgumentOutOfRangeException)
            {
                ArgumentOutOfRangeException ex = (ArgumentOutOfRangeException)e;
                LogError(ex);
                return;
            }
            if (e is BadImageFormatException)
            {
                BadImageFormatException ex = (BadImageFormatException)e;
                LogError(ex);
                return;
            }
            if (e is ArgumentException)
            {
                ArgumentException ex = (ArgumentException)e;
                LogError(ex);
                return;
            }
            ShowErrorMessageToUser(e.Message);
            WriteError(new string[] {"Error Message : " + e.Message, "Error Type : " + e.GetType().Name,
                "Error Source : " + e.Source, "Error Trace : " + e.StackTrace});
            if (e.InnerException != null)
                LogError(e.InnerException);
        }
        /// <summary>
        /// Display a message to the user with the error and writes it in the error file.
        /// </summary>
        /// <param name="e">The Exception object that raised in the application.</param>
        /// <param name="strCommandName">The sql command text that caused the exception.</param>
        public static void LogError(Exception e, string strCommandName)
        {
            
            if (e is OutOfMemoryException)
            {
               WriteError(new string[] {"Error Message : " + e.Message, "Error Type : " + e.GetType().Name,
                    "Error Type : " + e.GetType().Name, "Error Source : " + e.Source, "Error Trace : " + e.StackTrace});
                GC.Collect();
                return;
            }
            if (e is ArgumentNullException)
            {
                ArgumentNullException ex = (ArgumentNullException)e;
                LogError(ex);
                return;
            }
            if (e is ArgumentOutOfRangeException)
            {
                ArgumentOutOfRangeException ex = (ArgumentOutOfRangeException)e;
                LogError(ex);
                return;
            }
            if (e is BadImageFormatException)
            {
                BadImageFormatException ex = (BadImageFormatException)e;
                LogError(ex);
                return;
            }
            if (e is ArgumentException)
            {
                ArgumentException ex = (ArgumentException)e;
                LogError(ex);
                return;
            }
            ShowErrorMessageToUser(e.Message);
            WriteError(new string[] {"Procedure Name or Command Text : " + strCommandName, "Error Message : " + e.Message, "Error Type : "
                + e.GetType().Name, "Error Source : " + e.Source, "Error Trace : " + e.StackTrace});
            if (e.InnerException != null)
                LogError(e.InnerException, strCommandName);
        }
        /// <summary>
        /// Display a message to the user with the error and writes it in the error file.
        /// </summary>
        /// <param name="e">The Exception object that raised in the application.</param>
        public static void LogError(IOException e)
        {
            ShowErrorMessageToUser(e.Message);
            WriteError(new string[] {"Error Message : " + e.Message, "Error Type : " + e.GetType().Name,
                "Error Source : " + e.Source, "Error Trace : " + e.StackTrace});
            if (e.InnerException != null)
                LogError(e.InnerException);
        }
        /// <summary>
        /// Writes a string to the Error file.
        /// </summary>
        /// <param name="ErrorMessage">The message that will be saved in the Error file.</param>
        public static void WriteError(string[] ErrorMessage)
        {
            try
            {
                string ErrorFilePath = @"\Error.txt";
                if (!File.Exists(ErrorFilePath))
                {
                    FileStream fsErrorFileCreator = File.Create(ErrorFilePath);
                    fsErrorFileCreator.Close();
                }
                StreamWriter swError = new StreamWriter(ErrorFilePath, true);
                swError.WriteLine();
                swError.WriteLine("***************************** ***************************** *****************************");
                foreach (string strErrorDetail in ErrorMessage)
                    swError.WriteLine(strErrorDetail);
                swError.WriteLine("Date of Error :\t" + DateTime.Now.ToShortDateString());
                swError.WriteLine("Time of Error :\t" + DateTime.Now.ToLongTimeString());
                //swError.WriteLine("Current User ID :\t" + UserLogin.LoggedUserID);
                swError.WriteLine("***************************** ***************************** *****************************");
                swError.WriteLine();
                swError.Close();
            }
            catch //(Exception ex)
            {
                //Helper.ShowMessage(ex.Message);
            }
        }
        /// <summary>
        /// Display a message to the user with the error and writes it in the error file.
        /// </summary>
        /// <param name="Message">The string that will appear in the application as an error message.</param>
        public static void LogError(string Message)
        {
            //Helper.ShowMessage(Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            WriteError(new string[] { Message });
        }
        /// <summary>
        /// Show error message to the user.
        /// </summary>
        /// <param name="strMessage">The Error message to show to the user.</param>
        static void ShowErrorMessageToUser(string strMessage)
        {
            //MessageBox.Show(Resources.ProgramMessages.ErrMesErrorOccured + "\n\n" + strMessage, Helper.MessageHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}
