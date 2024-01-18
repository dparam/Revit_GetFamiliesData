using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revit_GetFamiliesData.App.Helpers;
using Revit_GetFamiliesData.App.Plugins.GetFamiliesData.Models;
using Revit_GetFamiliesData.App.Views;
using System.Windows;


namespace Revit_GetFamiliesData.App.Plugins.CreateSpaces.Commands
{
    [Transaction(TransactionMode.Manual)]

    public class Command_GetFamiliesData : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApplication = commandData.Application;
            Document document = uiApplication.ActiveUIDocument.Document;

            string report = "";

            //

            var familiesArray = ElementCollector.GetFamiliesByPath(@"\\family-directory-path");
            if (familiesArray == null) return Result.Failed;

            foreach (string familyPath in familiesArray)
            {
                report += ElementCollector.GetFamilyData(uiApplication, familyPath);
            }


            DebugWindow debugWindow = new DebugWindow(report);
            debugWindow.ShowDialog();

            //

            return Result.Succeeded;
        }
    }
}
