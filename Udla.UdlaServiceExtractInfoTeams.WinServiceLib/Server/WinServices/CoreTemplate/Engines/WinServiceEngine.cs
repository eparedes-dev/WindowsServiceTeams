using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Udla.UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines.Configuration;
using Udla.UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines;
using Udla.UdlaServiceExtractInfoTeams.Util.Mail;

namespace Udla.UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines
{
    public class WinServiceEngine : ApplicationException
    {
        #region log declaration
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region private members
        private System.Timers.Timer _timer = null;
        #endregion

        #region public functions
        public void Start()
        {
            try
            {
                string buildMode = "RELEASE";

#if DEBUG
                buildMode = "DEBUG";
#endif

                // 1. Log the start (begin).
                log.InfoFormat("Engine start Mode {0} - begin ({1}).", buildMode, this.GetType().FullName);

                // 2. Create the operation context.  This validates the server settings 
                //    (makes sure that the output culture name is valid, the required
                //    folders exist, the multilanguage rules are valid).
                OperationContext operationContext = new OperationContext();

                // 3. Initialize timer.
                _timer = new System.Timers.Timer();
                _timer.Enabled = false;
                _timer.Interval = operationContext.ServiceSettings.TimerInterval;
                _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
                _timer.Enabled = true;

                operationContext.ServiceSettings.ImprimirProximaHoraEjecucion();

                // Notifico vía mail el inicio del servicio
                bool seEnvioMail = new MailNotification().SendSingleMail_AvisoIntegracion(
                  "Se inició el servicio de sincronizacion Teams SDS.",
                  operationContext.ServiceSettings.MailMessageSubject
                  );
            }
            catch (Exception exception)
            {
                // Log and throw the exception.
                string errorMessage = string.Format("Engine start - error ({0}).", this.GetType().FullName);
                log.Error(errorMessage, exception);
                throw;
            }
            finally
            {
                // Log the stop (end).
                log.InfoFormat("Engine start - end ({0}).", this.GetType().FullName);
            }
        }


        public void Stop()
        {
            try
            {
                // 1. Log the start (begin).
                log.InfoFormat("Engine stop - begin ({0}).", this.GetType().FullName);

                // 2. Release structures.
                _timer.Enabled = false;
                _timer = null;

                // Notifico vía mail el fin del servicio
                OperationContext operationContext = new OperationContext();
                bool seEnvioMail = new MailNotification().SendSingleMail_AvisoIntegracion(
                    "Se detuvo el servicio de sincronizacion Teams SDS.",
                    operationContext.ServiceSettings.MailMessageSubject
                    );
            }
            catch (Exception exception)
            {
                // Log and throw the exception.
                string errorMessage = string.Format("Engine stop - error ({0}).", this.GetType().FullName);
                log.Error(errorMessage, exception);
                throw;
            }
            finally
            {
                // Log the stop (end).
                log.InfoFormat("Engine stop - end ({0}).", this.GetType().FullName);
            }
        }


        public void ExecuteEngineOperation()
        {
            try
            {
                // 1. Log the start of the engine operation.

                log.Info("ExecuteEngineOperation - begin.");

                // 2. Create the operation context.  This validates the server settings 
                //    (makes sure that the output culture name is valid, the required
                //    folders exist, the multilanguage rules are valid).
                OperationContext operationContext = new OperationContext();

                // 3. Set the culture info to be used on the service.
                if (operationContext.ServiceConfiguration.ServiceCultureInfo != null)
                {
                    Thread.CurrentThread.CurrentCulture = operationContext.ServiceConfiguration.ServiceCultureInfo;
                }

                // 4. List all the processors.
                IList<BaseProcessor> processorList = new List<BaseProcessor>();
                //processorList.Add(new Processor());
                processorList.Add(new UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines.Processors.Test.Processor());
                processorList.Add(new UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines.Processors.ExtractUsers.Processor());
                // 5. Invoke all the processors.
                foreach (BaseProcessor processor in processorList)
                {
                    InvokeProcessor(operationContext, processor);
                }

                // 6. Envío el mail con la información recogida desde los procesadores de envio de Info desde EBilling a BX
                string bodyMail = operationContext.ContenidoMail_Servicio.ToString();
                if (!string.IsNullOrEmpty(bodyMail))
                {
                    bool seEnvioMail = new MailNotification().SendSingleMail_AvisoIntegracion(bodyMail, operationContext.ServiceSettings.MailMessageSubject);
                }

                // 7. Update the interval on the timer (maybe the interval setting was 
                //    changed since the last time that the timer fired).  We check if
                //    _timer was set to null because this function may be invoked by a
                //    test program, and that test program may not have called the Start
                //    function (which creates and initializes the timer).
                operationContext = new OperationContext();
                if (_timer != null)
                {
                    _timer.Interval = operationContext.ServiceSettings.TimerInterval;
                    operationContext.ServiceSettings.ImprimirProximaHoraEjecucion();
                }

            }
            catch (Exception exception)
            {
                // Log the error but don't throw an exception.  Even if this operation
                // did not execute successfully, we don't want to raise a service 
                // exception.
                string errorMessage = string.Format("ExecuteEngineOperation - error ({0}).", this.GetType().FullName);
                log.Error(errorMessage, exception);
            }
            finally
            {
                // Log the end of the engine operation.
                log.Info("ExecuteEngineOperation - end.");
            }
        }

        #endregion


        #region event handlers
        private void _timer_Elapsed(
            object sender,
            ElapsedEventArgs e)
        {
            // 1. Disable the timer (to stop other Elapsed events from entering this 
            //    code).
            _timer.Enabled = false;

            // 2. Process the input folder.
            ExecuteEngineOperation();

            // 3. Enable back the timer.
            _timer.Enabled = true;
        }
        #endregion


        #region private functions
        private void InvokeProcessor(
            OperationContext operationContext,
            BaseProcessor processor)
        {
            string processorName = "";
            try
            {
                processorName = processor.Name;
                log.DebugFormat("Invoking processor (Name={0}) - begin.", processorName);
                processor.Execute(operationContext);
            }
            catch (Exception exception)
            {
                // Log the error but don't throw an exception.  Even if this processor
                // did not execute successfully, we don't want to stop the other 
                // processors from running.
                string errorMessage = string.Format("Invoking processor (Name={0}) - error.", processorName);
                log.Error(errorMessage, exception);
            }
            finally
            {
                log.DebugFormat("Invoking processor (Name={0}) - end.", processorName);
            }
        }
        #endregion


    }
}
