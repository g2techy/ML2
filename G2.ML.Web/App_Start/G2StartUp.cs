using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DFL = G2.Frameworks.Logging;
using DFC = G2.Frameworks.Core;
using DFME = G2.Frameworks.MVC.ExceptionHandling;
using DTBS = G2.ML.BusinessServices;

namespace G2.ML.Web
{
    public class G2StartUp
	{
        public static void Configuration()
        {

            /*Logging default settings...*/
            DFL.DefaultLogManagerFactory.LogManager.Configure("DefaultLogger");

            /*Set Encryption key...*/
            DFC.Encryption.EncryptionKey = "ML";
            DFL.DefaultLogManagerFactory.LogManager.Debug("EncryptionKey key has been set...");

            /*Register error handler attribute...*/
            DFME.ErrorHandlerAttribute.Configure(string.Empty, string.Empty, Infrastructure.Web.Common.IsDebuggingEnabled, string.Empty);
            GlobalFilters.Filters.Add(new DFME.ErrorHandlerAttribute());
            DFL.DefaultLogManagerFactory.LogManager.Debug("Error handler attribute has been set...");

			/* Set PDF export component...*/
			Infrastructure.Utilities.PDF.CurrentPDFExportComponent = Infrastructure.Utilities.PDFExportComponent.SelectPDF;
			DFL.DefaultLogManagerFactory.LogManager.Debug(string.Format("PDF export component '{0}' has been set...", Infrastructure.Utilities.PDF.CurrentPDFExportComponent));

			/*Register AutoMapper settings...*/
			DFL.DefaultLogManagerFactory.LogManager.Debug("AutoMapper settings started...");
            AutoMapper.Mapper.Initialize(cfg =>
            {
                Infrastructure.AutoMapperConfiguration.Configure(cfg);
                DTBS.AutoMapperConfiguration.Configure(cfg);                
            });
            DFL.DefaultLogManagerFactory.LogManager.Debug("AutoMapper settings initialized...");
            AutoMapper.Mapper.AssertConfigurationIsValid();
            DFL.DefaultLogManagerFactory.LogManager.Debug("AutoMapper settings validated...");
        }
    }
}