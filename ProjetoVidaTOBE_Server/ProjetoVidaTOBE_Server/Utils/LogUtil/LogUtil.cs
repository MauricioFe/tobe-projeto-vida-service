using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Utils.Log
{
    public class LogUtil
    {
        public static void Create(Exception ex, EventLogEntryType tipoLog = EventLogEntryType.Error)
        {
            Create(ex.Source, ex.StackTrace, tipoLog);
        }
        public static void Create(string source, string message, EventLogEntryType tipoLog = EventLogEntryType.Error)
        {
            Create(null, source, message, null, tipoLog);
        }

        public static void Create(string source, string message, string stackTrace, EventLogEntryType tipoLog = EventLogEntryType.Error)
        {
            Create(null, source, message, stackTrace, tipoLog);
        }

        public static void Create(string additional, string source, string message, string stackTrace, EventLogEntryType tipoLog = EventLogEntryType.Error)
        {
            var body = new StringBuilder();

            if (!string.IsNullOrEmpty(additional))
            {
                body.AppendLine("Additional:");
                body.AppendLine(additional);
            }

            if (!string.IsNullOrEmpty(source))
            {
                body.AppendLine("Source:");
                body.AppendLine(source);
            }

            if (!string.IsNullOrEmpty(message))
            {
                body.AppendLine("Message:");
                body.AppendLine(message);
            }

            if (!string.IsNullOrEmpty(stackTrace))
            {
                body.AppendLine("StackTrace:");
                body.AppendLine(stackTrace);
            }

            CreateEventViewer(body, "Api Projeto Vida", tipoLog);
        }
        public static void CreateEventViewer(StringBuilder message, string application, EventLogEntryType tipoLog = EventLogEntryType.Error)
        {
            try
            {
                const string logName = "ToBe Api";

                if (!EventLog.SourceExists(application))
                    EventLog.CreateEventSource(application, logName);

                using (var eventLog = new EventLog(logName))
                {
                    eventLog.Source = application;
                    eventLog.WriteEntry(message.ToString(), tipoLog, 1001);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///     Cria o arquivo de log em linha
        /// </summary>
        public static void AddLine(string message, string repositoryName, string fileName)
        {
            try
            {
                //Pasta onde será armazedado os logs
                var path = Path.Combine(@"C:\PROJETO_VIDA_LOG", repositoryName);
                //Caso não exista cria o diretório onde será armazenado os logs
                FolderUtil.Create(path);

                //Caminho completo do arquivo de log
                var filePath = Path.Combine(path, string.Format("{0}.log", fileName));

                //Cria o arquivo caso ele não exista
                if (!File.Exists(filePath))
                    File.Create(filePath).Close();

                //Cria um novo bloco de mensagem no arquivo de log 
                using (var fw = File.AppendText(filePath))
                    fw.WriteLine(message);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
