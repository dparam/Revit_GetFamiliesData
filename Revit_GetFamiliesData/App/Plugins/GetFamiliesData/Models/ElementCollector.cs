using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.IO;
using System.Windows;


namespace Revit_GetFamiliesData.App.Plugins.GetFamiliesData.Models
{
    public static class ElementCollector
    {
        public static string[] GetFamiliesByPath(string rootPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(rootPath);
            if (!directoryInfo.Exists)
            {
                MessageBox.Show($"Директория\n\n{rootPath}\n\nне найдена");
                return null;
            }

            return Directory.GetFiles(rootPath, "*.rfa", SearchOption.AllDirectories);

            //return string.Join("\n", allfiles);
        }


        public static string GetFamilyData(UIApplication uIApplication, string familyPath)
        {
            string result = "";

            if (!File.Exists(familyPath))
                return "FILE DOES NOT EXIST";

            Document familyDoc = null;

            string path = Path.GetDirectoryName(familyPath);
            string familyName = Path.GetFileName(familyPath).Replace(".rfa", "");
            string category = "category";
            string typeName = "typeName";
            string adskName = "";
            string adskTag = "";
            string adskFactoryName = "";

            try
            {
                familyDoc = uIApplication.Application.OpenDocumentFile(familyPath);
                FamilyManager familyManager = familyDoc.FamilyManager;

                category = familyDoc.OwnerFamily.FamilyCategory.Name;

                foreach (FamilyType familyType in familyManager.Types)
                {
                    if (familyType.Name == " " || string.IsNullOrEmpty(familyType.Name))
                        typeName = familyName;
                    else
                        typeName = familyType.Name;

                    foreach (FamilyParameter parameter in familyManager.Parameters)
                    {
                        if (parameter.Definition.Name == "ADSK_Наименование")
                        {
                            adskName = familyType.AsString(parameter);
                            continue;
                        }

                        if (parameter.Definition.Name == "ADSK_Марка")
                        {
                            adskTag = familyType.AsString(parameter);
                            continue;
                        }

                        if (parameter.Definition.Name == "ADSK_Завод-изготовитель")
                        {
                            adskFactoryName = familyType.AsString(parameter);
                            continue;
                        }
                    }

                    FamilyTypeDataItem familyTypeDataItem = new FamilyTypeDataItem(
                        path, familyName, category, typeName, adskName, adskTag, adskFactoryName);

                    result += familyTypeDataItem.GetFamilyTypeDataString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"GetFamilyData\n{e}");
                familyDoc.Close(saveModified: false);
            }

            if (familyDoc != null && familyDoc.IsValidObject)
                familyDoc.Close(saveModified: false);

            return result;
        }
    }
}
