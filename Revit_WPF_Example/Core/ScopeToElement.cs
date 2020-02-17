using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_WPF_Example.Core
{
    internal class ScopeToElement
    {
        private RevitActions rvtActions;

        public ScopeToElement(RevitActions rvtActions)
        {
            this.rvtActions = rvtActions;
        }

        public void ScopeToRvtElement(Element wall)
        {
            using (Transaction t = new Transaction(rvtActions.Doc, "Scope To Element"))
            {
                t.Start();

                var bbx = wall.get_BoundingBox(null);
                var scopebbx = new BoundingBoxXYZ();
                scopebbx.Min = bbx.Min;
                scopebbx.Max = bbx.Max;

                var trans = bbx.Transform;
                scopebbx.Max = trans.OfPoint(bbx.Max).Add(new XYZ(1, 1, 1));
                scopebbx.Min = trans.OfPoint(bbx.Min).Subtract(new XYZ(1, 1, 1));

                try
                {
                    if (rvtActions.UIDoc.ActiveGraphicalView is View3D)
                    {
                        var v3d = ((View3D)rvtActions.UIDoc.ActiveGraphicalView);
                        v3d.IsSectionBoxActive = true;
                        v3d.SetSectionBox(scopebbx);
                    }
                    else
                    {
                        rvtActions.UIDoc.ActiveGraphicalView.CropBoxActive = true;
                        rvtActions.UIDoc.ActiveGraphicalView.CropBox = scopebbx;
                    }

                    rvtActions.Doc.Regenerate();

                    GetUiView(rvtActions.UIDoc.ActiveGraphicalView).ZoomAndCenterRectangle(scopebbx.Min, scopebbx.Max);
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Error Navigate to Element", ex.Message);
                }

                t.Commit();
            }
        }

        private UIView GetUiView(Autodesk.Revit.DB.View view)
        {
            try
            {
                foreach (UIView uiv in rvtActions.UIDoc.GetOpenUIViews())
                {
                    IList<UIView> uiviews = rvtActions.UIDoc.GetOpenUIViews();
                    foreach (var item in uiviews)
                    {
                        if (item.ViewId == view.Id)
                        {
                            return item;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error Finding UIView", ex.Message);
            }
            return null;
        }
    }
}