using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Udla.UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines.Configuration;

namespace Udla.UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines
{
	internal abstract class BaseProcessor
	{

		#region log declaration
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		#endregion


		#region public abstract properties
		public abstract string Name { get; }
		#endregion

		#region protected abstract functions
		protected abstract void ExecuteInternal(OperationContext operationContext);

		#endregion

		#region public function

		public void Execute(
			OperationContext operationContext)
		{
			// 1. Valido si es un día de ejecución configurado
			if (!IsExecutionDay())
			{
				log.Info("{DiaEjecución}, NO está en los días configurados de ejecución.");
				return;
			}

			// cgomez. activar la propiedad "ProcessorIsEnabledByProcessorName" que se lee de un XML.
			// 2. Validate if we need to run this processor.
			if (!ProcessorIsEnabled(operationContext))
			{
				// This processor is not enabled, we don't anything else on this 
				// operation.
				log.DebugFormat("Will not invoke processor logic because the processor is not enabled (Name={0}).", this.Name);
				return;
			}

			// 3. Execute the business logic.
			log.DebugFormat("The processor is enabled so the processor logic will be invoked (Name={0}).", this.Name);
			this.ExecuteInternal(operationContext);
		}
		#endregion


		#region private functions

		private bool IsExecutionDay()
		{
			try
			{
				// 1. Leo el modo de ejecución
				string modeExecution = ConfigurationManager.AppSettings.Get("ModeExecution").ToLower();
				if (!modeExecution.Equals("dialy"))
				{
					return true;
				}

				// 2. Leo los días de ejecución del servicio en el archivo de configuración
				string executionDays = ConfigurationManager.AppSettings.Get("ExecutionDays").ToUpper();
				if (string.IsNullOrEmpty(executionDays))
				{
					return true;
				}

				// 3. Valido si el día actual está dentro de la configuración
				var diasConfiguracion = executionDays.Split(',').ToList();
				string diaActual = DateTime.Now.ToString("dddd", new CultureInfo("es-ES"));
				if (diasConfiguracion.Contains(diaActual.ToUpper()))
				{
					return true;
				}

				return false;
			}
			catch (System.Exception ex)
			{
				log.Error("Error al validar día de ejecución", ex);
			}
			return false;
		}



		private bool ProcessorIsEnabled(
			OperationContext operationContext)
		{
			return operationContext.ServiceConfiguration.ProcessorIsEnabledByProcessorName[this.Name];
		}

		#endregion


	}
}
