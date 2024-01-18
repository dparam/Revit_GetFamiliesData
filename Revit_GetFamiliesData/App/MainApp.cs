using Autodesk.Revit.UI;
using Revit_GetFamiliesData.App.Ribbons;
using System.Collections.Generic;


namespace Revit_GetFamiliesData.App
{
    public class MainApp : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication uiControlledApp)
        {
            string tabName = "Revit_GetFamiliesData";

            RibbonPanel ribbonPanel = uiControlledApp.CreateRibbonPanel(tabName);
            CreateRibbonPanel(uiControlledApp, ribbonPanel);
            return Result.Succeeded;
        }


        public Result OnShutdown(UIControlledApplication uiControlledApp)
        {
            return Result.Succeeded;
        }


        //


        public void CreateRibbonPanel(UIControlledApplication uiControlledApp, RibbonPanel ribbonPanel)
        {
            var b1 = RibbonPanelHelpers.CreateButton(
                "BTN_GetFamiliesData",
                "Get Families Data",
                "Сбор информации из всех семейств по выбранному пути и выдача отчёта для дальнейшей обработки",
                "Revit_GetFamiliesData.App.Plugins.GetFamiliesData.Commands.Command_GetFamiliesData",
                "icon_Default.ico");

            ribbonPanel.AddItem(b1);
        }
    }
}
