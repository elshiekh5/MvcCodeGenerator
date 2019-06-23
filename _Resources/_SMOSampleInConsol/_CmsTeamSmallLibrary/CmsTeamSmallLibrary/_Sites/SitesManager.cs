using System;
using System.Xml;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace CmsTeamSmallLibrary.SitesInfomations
{
    public class SitesManager
    {
        
        private Hashtable _SiteList;
        public Hashtable SiteList
        {
            get
            {
                if (_SiteList == null)
                    _SiteList = new Hashtable();
                return _SiteList;
            }
            set { _SiteList = value; }
        }
        private XmlDocument XmlDoc;
        //-----------------------------------------------------------------
        public string sitesConfigpath = AppDomain.CurrentDomain.BaseDirectory + "Sites.xml";

          #region -----------------Instance-----------------
        //-----------------------------------------------------------------
        public static SitesManager _Instance;
        public static SitesManager Instance
        {
            get
            {
                _Instance = new SitesManager();
                _Instance.LoadAllSites();

                return _Instance;
            }
        }
        #endregion
        //-----------------------------------------------------------------



        #region -----------------GetConfig-----------------
        //-----------------------------------------------------------------
        private void LoadConfig(string configPath)
        {
            XmlDoc = new XmlDocument();
            XmlDoc.PreserveWhitespace = true;
            XmlDoc.Load(configPath);

        }
        #endregion
        //-----------------------------------------------------------------
        #region -----------------LoadAllSites-----------------
        //-----------------------------------------------------------------
        public void LoadAllSites()
        {
            SiteList.Clear();
            LoadConfig(sitesConfigpath);
            XmlNodeList sitesXml = XmlDoc.SelectNodes("/sites/Site");
            SiteEntity tempSite;
            foreach (XmlNode site in sitesXml)
            {
                tempSite = PopulateSiteFromXmlNode(site);
                SiteList[tempSite.ID.ToString()] = tempSite;
            }
        }
        #endregion

        public SiteEntity GetSite(int siteID)
        {
            object site = SiteList[siteID.ToString()];
            if (site != null)
                return (SiteEntity)site;
            return new SiteEntity();
        }
        //-----------------------------------------------------------------
        #region -----------------PopulateSiteFromXmlNode-----------------
        //-----------------------------------------------------------------
        private SiteEntity PopulateSiteFromXmlNode(XmlNode node)
        {
            SiteEntity site = new SiteEntity();
            /****************************************************/
            //find all the public properties of list Type using reflection
            PropertyInfo[] piT = typeof(SiteEntity).GetProperties();
            // Get the Type object corresponding to MyClass.
            Type myType = typeof(SiteEntity);
            PropertyInfo myPropInfo;
            string exceptions = "";
            foreach (XmlAttribute attr in node.Attributes)
            {
                try
                {
                    myPropInfo = myType.GetProperty(attr.Name);
                    if (myPropInfo.CanWrite)
                    {
                        if (myPropInfo.PropertyType.BaseType == typeof(System.Enum))
                        {
                            //int intVal = Convert.ToInt32(attr.Value);
                            myPropInfo.SetValue(site, Enum.Parse(myPropInfo.PropertyType, attr.Value), null);
                            //Enum.Parse(typeof(myPropInfo.), "FirstName");   
                        }
                        else
                        {
                            myPropInfo.SetValue(site, Convert.ChangeType(attr.Value, myPropInfo.PropertyType), null);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(attr.Name);
                }
            }
            return site;
        }
        #endregion
        //-----------------------------------------------------------------
        #region -----------------PopulateXmlNodeFromSite-----------------
        //-----------------------------------------------------------------
        //PopulateXmlNodeFromSite
        //-----------------------------------------------------------------
        private XmlElement PopulateXmlNodeFromSite(SiteEntity site, XmlElement node)
        {
            //-----------------------------------------------------------------
            SiteEntity defaultSite = new SiteEntity();
            //-----------------------------------------------------------------
            Type myType = typeof(SiteEntity);
            PropertyInfo[] piT = myType.GetProperties();
            object moduleValue;
            object defaultValue;
            foreach (PropertyInfo myPropInfo in piT)
            {
                if (myPropInfo.CanWrite)
                {
                    moduleValue = myPropInfo.GetValue(site, null);
                    defaultValue = myPropInfo.GetValue(defaultSite, null);
                    if (moduleValue.ToString() != defaultValue.ToString())
                        AddAttribute(node, myPropInfo.Name, moduleValue);
                }
            }
            return node;

        }
        //-----------------------------------------------------------------
        #endregion


        //-----------------------------------------------------------------
        //-----------------------------------------------------------------
        #region -----------------AddAttribute-----------------
        //-----------------------------------------------------------------
        private static void AddAttribute(XmlElement element, string name, object value)
        {
            XmlAttribute att = element.SetAttributeNode(name, "");
            att.InnerText = value.ToString();
            element.Attributes.Append(att);
        }
        #endregion
        //-----------------------------------------------------------------
        public bool SaveSite(SiteEntity site)
        {
            XmlNodeList nodeList = XmlDoc.SelectNodes("/sites/Site[@ID='" + site.ID + "']");
            if (nodeList.Count == 0)
            {
                return AddSite(site);
            }
            else
                return UpdateSite(site);
        }
        public bool AddSite(SiteEntity site)
        {
            bool res = false;
            XmlNodeList nodeList = XmlDoc.SelectNodes("/sites/Site[@ID='" + site.ID + "']");
            if (nodeList.Count == 0)
            {
                //-----------------------------------------------------------------
                XmlElement xmlNewSite = XmlDoc.CreateElement("Site");
                xmlNewSite = PopulateXmlNodeFromSite(site, xmlNewSite);
                XmlNode commonParent = XmlDoc.SelectSingleNode("/Sites");
                commonParent.AppendChild(xmlNewSite);
                XmlDoc.Save(sitesConfigpath);
                ////-----------------------------------------------------------------
                res = true;
            }
            return res;
        }
        //------------------------------------------------------------------
        public bool UpdateSite(SiteEntity site)
        {
            bool res = false;
            XmlNodeList nodesList = XmlDoc.SelectNodes("/sites/Site[@ID='" + site.ID + "']");
            if (nodesList.Count == 1)
            {

                XmlElement oldSiteNode = (XmlElement)nodesList[0];
                oldSiteNode.Attributes.RemoveAll();
                PopulateXmlNodeFromSite(site, oldSiteNode);
                XmlDoc.Save(sitesConfigpath);
                res = true;
            }
            return res;
        }
        //------------------------------------------------------------------
        public bool DeleteSite(int ID)
        {
            bool res = false;
            XmlNodeList nodesList = XmlDoc.SelectNodes("/sites/Site[@ID='" + ID + "']");
            foreach (XmlNode module in nodesList)
            {
                XmlNode parentnode = module.ParentNode;
                parentnode.RemoveChild(module);
            }
            XmlDoc.Save(sitesConfigpath);
            res = true;
            return res;
        }
        //------------------------------------------------------------------
    }
}
