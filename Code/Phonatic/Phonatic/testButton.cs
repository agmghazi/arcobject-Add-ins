using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Phonatic
{
    public class testButton : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public testButton()
        {

        }

        protected override void OnUpdate()
        {

        }

        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            int x = arg.X;
            int y = arg.Y;

            IMxDocument mxDocument = (IMxDocument)ArcMap.Application.Document;
            IPoint pPoint = (IPoint)mxDocument.ActivatedView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

            MessageBox.Show("Mouse X: " + x + Environment.NewLine + "Y: " + y + "Map Point X: "+ pPoint.X + Environment.NewLine + "Y: " + pPoint.Y);


        }
    }

}
