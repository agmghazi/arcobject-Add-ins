using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonatic
{
    public class TowerManager
    {

        private IWorkspace _workspace;

        public TowerManager(IWorkspace pWorkspace)
        {
            _workspace = pWorkspace;

        }

        public Tower GetTowerByID(string towerid)
        {
            //query the geodatabase.. 
            IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)_workspace;

            IFeatureClass fcTower = pFeatureWorkspace.OpenFeatureClass("Tower");

            //get the tower feature by id 
            IQueryFilter pQFilter = new QueryFilter();
            pQFilter.WhereClause = "TOWERID = '" + towerid + "'";

            IFeatureCursor pFCursor = fcTower.Search(pQFilter, true);

            IFeature pTowerFeature = pFCursor.NextFeature();

            if (pTowerFeature == null)
                return null;

            Tower tower = new Tower();
            tower.ID = towerid;
            tower.NetworkBand = pTowerFeature.get_Value(pTowerFeature.Fields.FindField("NETWORKBAND"));
            tower.TowerType = pTowerFeature.get_Value(pTowerFeature.Fields.FindField("TOWERTYPE"));

            return tower;
            //get the tower type, and then query the tower details table to get the rest of the data...


        }

    }
}
