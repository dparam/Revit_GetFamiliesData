using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_GetFamiliesData.App.Plugins.GetFamiliesData.Models
{
    public class FamilyTypeDataItem
    {
        public string Path { get; set; }
        public string FamilyName { get; set; }
        public string Category { get; set; }
        public string TypeName { get; set; }
        public string ADSKName { get; set; }
        public string ADSKTag { get; set; }
        public string ADSKFactoryName { get; set; }


        public FamilyTypeDataItem(string path, string familyName, string category, string typeName, string adskName, string adskTag, string adskFactoryName)
        {
            Path = path;
            FamilyName = familyName;
            Category = category;
            TypeName = typeName;
            ADSKName = adskName;
            ADSKTag = adskTag;
            ADSKFactoryName = adskFactoryName;
        }


        public string GetFamilyTypeDataString()
        {
            return $"{Path}%%%{FamilyName}%%%{Category}%%%{TypeName}%%%{ADSKName}%%%{ADSKTag}%%%{ADSKFactoryName}\n";
        }
    }
}
