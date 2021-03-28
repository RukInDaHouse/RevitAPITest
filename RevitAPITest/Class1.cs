using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
[TransactionAttribute(TransactionMode.Manual)]
[RegenerationAttribute(RegenerationOption.Manual)]
public class Lab1PlaceGroup : IExternalCommand
{
    public Result Execute(
    ExternalCommandData commandData,
    ref string message,
    ElementSet elements)
    {
        UIApplication uiApp = commandData.Application;
        Document doc = uiApp.ActiveUIDocument.Document;
        Reference pickedRef = null;
        Selection sel = uiApp.ActiveUIDocument.Selection;
        pickedRef = sel.PickObject(ObjectType.Element,
        "Выберите группу");
        Element elem = doc.GetElement(pickedRef);
        Group group = elem as Group;
        //Указание точки
        XYZ point = sel.PickPoint("Укажите точку для размещения группы");
        //Размещение группы
        Transaction trans = new Transaction(doc);
        trans.Start("Lab");
        doc.Create.PlaceGroup(point, group.GroupType);
        trans.Commit();
        return Result.Succeeded;
    }
}
