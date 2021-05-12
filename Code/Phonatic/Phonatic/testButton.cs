using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

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
            IMxDocument pMxdoc = (IMxDocument)ArcMap.Application.Document;

            IFeatureLayer pfeaturelayer = (IFeatureLayer)pMxdoc.ActiveView.FocusMap.Layer[0];
            IDataset pDS = (IDataset)pfeaturelayer.FeatureClass;
            TowerManager tm = new TowerManager(pDS.Workspace);

            int x = arg.X;
            int y = arg.Y;
            IPoint pPoint = pMxdoc.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

            Tower t = tm.GetNearestTower(pPoint,10);

            if (t == null)
            {
                MessageBox.Show("No towers where found within the area");
                return;
            }
            //  Tower t = tm.GetTowerByID("T04");


            MessageBox.Show("Tower id  " + t.ID + Environment.NewLine + "Type " + t.TowerType + Environment.NewLine + "Networkband: " + t.NetworkBand);


        }
    }

}
