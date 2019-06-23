using System;
using System.Data;
namespace CmsTeamSmallLibrary.SitesInfomations
{
    public class SiteEntity
    {
        #region --------------ID--------------
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        //------------------------------------------
        #endregion

        #region --------------Name--------------
        private string _Name = "";
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        //------------------------------------------
        #endregion

        #region --------------OneJournalModel--------------
        private bool _OneJournalModel;
        public bool OneJournalModel
        {
            get { return _OneJournalModel; }
            set { _OneJournalModel = value; }
        }
        //------------------------------------------
        #endregion

        #region --------------MainOwnerID--------------
        private int _MainOwnerID;
        public int MainOwnerID
        {
            get { return _MainOwnerID; }
            set { _MainOwnerID = value; }
        }
        //------------------------------------------
        #endregion

        #region --------------JounalFolderString--------------
        private string _JounalFolderString = "";
        public string JounalFolderString
        {
            get { return _JounalFolderString; }
            set { _JounalFolderString = value; }
        }
        //------------------------------------------
        #endregion

        #region --------------BucketName--------------
        private string _BucketName = "";
        public string BucketName
        {
            get { return _BucketName; }
            set { _BucketName = value; }
        }
        //------------------------------------------
        #endregion

    }
}