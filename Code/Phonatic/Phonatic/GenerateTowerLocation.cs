using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;


namespace Phonatic
{
    public class GenerateTowerLocation : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public GenerateTowerLocation()
        {
        }

        protected override void OnClick()
        {
            IMxDocument pMxdoc = (IMxDocument)ArcMap.Application.Document;
            IFeatureLayer pfeaturelayer = (IFeatureLayer)pMxdoc.ActiveView.FocusMap.Layer[0];
            IDataset pDS = (IDataset)pfeaturelayer.FeatureClass;
            
            TowerManager tm = new TowerManager(pDS.Workspace);
            
            Tower pTower = tm.GetTowerByID("T04");
           
            double towerRange = 100;

            ITopologicalOperator pTopo = (ITopologicalOperator) pTower.TowerLocation;

            IPolygon range3Bars = (IPolygon) pTopo.Buffer(towerRange / 3);
            IPolygon range2BarsWhole = (IPolygon)pTopo.Buffer((towerRange*2) / 3);
            IPolygon range1BarsWhole = (IPolygon)pTopo.Buffer(towerRange);

            ITopologicalOperator pIntTopo = (ITopologicalOperator) range2BarsWhole;

            ITopologicalOperator pInTopo1 = (ITopologicalOperator)range1BarsWhole;

            IPolygon range2BarsDount = (IPolygon)pIntTopo.SymmetricDifference(range3Bars);
            IPolygon range1BarsDount = (IPolygon)pIntTopo.SymmetricDifference(range2BarsWhole);

            IWorkspaceEdit pWorkspaceEdit = (IWorkspaceEdit)pDS.Workspace;

            pWorkspaceEdit.StartEditing(true);
            pWorkspaceEdit.StartEditOperation();

            IFeatureWorkspace pFWorkspace =(IFeatureWorkspace) pWorkspaceEdit;
            IFeatureClass pTowerRangeFC = pFWorkspace.OpenFeatureClass("TowerRange");

            IFeature pFeature3Bar = pTowerRangeFC.CreateFeature();
            pFeature3Bar.set_Value(pFeature3Bar.Fields.FindField("TOWERID"), "T04");
            pFeature3Bar.set_Value(pFeature3Bar.Fields.FindField("RANGE"), 3);

            pFeature3Bar.Shape = range3Bars;
            pFeature3Bar.Store();

            IFeature pFeature2Bar = pTowerRangeFC.CreateFeature();
            pFeature2Bar.set_Value(pFeature2Bar.Fields.FindField("TOWERID"), "T04");
            pFeature2Bar.set_Value(pFeature2Bar.Fields.FindField("RANGE"), 2);

            pFeature2Bar.Shape = range2BarsDount;
            pFeature2Bar.Store();

            IFeature pFeature1Bar = pTowerRangeFC.CreateFeature();
            pFeature1Bar.set_Value(pFeature1Bar.Fields.FindField("TOWERID"), "T04");
            pFeature1Bar.set_Value(pFeature1Bar.Fields.FindField("RANGE"), 1);

            pFeature1Bar.Shape = range1BarsDount;
            pFeature1Bar.Store();


            pWorkspaceEdit.StopEditOperation();
            pWorkspaceEdit.StopEditing(true);


        }

        protected override void OnUpdate()
        {
        }
    }
}
