using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomModels.FlexigridModels
{

    public class FlexigridColumn 
    {
        #region --------------Display--------------
        //------------------------------------------------------------------------------------------------------
        //Display
        //--------------------------------------------------------------------
        private string _Display;
        /// <summary>
        /// Gets or sets entity Display. 
        /// </summary>
        public string Display
        {
            get { return _Display; }
            set { _Display = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Name--------------
        //------------------------------------------------------------------------------------------------------
        //Name
        //--------------------------------------------------------------------
        private string _Name;
        /// <summary>
        /// Gets or sets entity Name. 
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Width--------------
        //------------------------------------------------------------------------------------------------------
        //Width
        //--------------------------------------------------------------------
        private int _Width;
        /// <summary>
        /// Gets or sets entity Width. 
        /// </summary>
        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion


        #region --------------Sortable--------------
        //------------------------------------------------------------------------------------------------------
        //Sortable
        //--------------------- -----------------------------------------------
        private bool _Sortable;
        /// <summary>
        /// Gets or sets entity Sortable. 
        /// </summary>
        public bool Sortable
        {
            get { return _Sortable; }
            set { _Sortable = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Align--------------
        //------------------------------------------------------------------------------------------------------
        //Align
        //--------------------------------------------------------------------
        private string _Align;
        /// <summary>
        /// Gets or sets entity Align. 
        /// </summary>
        public string Align
        {
            get { return _Align; }
            set { _Align = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Searchable--------------
        //------------------------------------------------------------------------------------------------------
        //Searchable
        //--------------------------------------------------------------------
        private bool _Searchable;
        /// <summary>
        /// Gets or sets entity Searchable. 
        /// </summary>
        public bool Searchable
        {
            get { return _Searchable; }
            set { _Searchable = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------IsTheDefaultInSearch--------------
        //------------------------------------------------------------------------------------------------------
        //IsTheDefaultInSearch
        //--------------------------------------------------------------------
        private bool _IsTheDefaultInSearch;
        /// <summary>
        /// Gets or sets entity IsTheDefaultInSearch. 
        /// </summary>
        public bool IsTheDefaultInSearch
        {
            get { return _IsTheDefaultInSearch; }
            set { _IsTheDefaultInSearch = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        public FlexigridColumn() { 
        }

        public FlexigridColumn(string display,string name,int width,bool sortable,string align,bool searchable,bool isTheDefaultInSearch)
        {
            this.Display = display;
            this.Name = name;
            this.Width = width;
            this.Sortable = sortable;
            this.Align = align;
            this.Searchable = searchable;
            this.IsTheDefaultInSearch = isTheDefaultInSearch;
        }
        public string GetPropString(object val)
        {
            if (val is bool)
            {
                return val.ToString().ToLower();
            }

            return val.ToString().ToLower();
        }

        public string GetPropString(bool val)
        {
            return val.ToString().ToLower();
        }
    }

    public class FlexigridButton
    {

        #region --------------Name--------------
        //------------------------------------------------------------------------------------------------------
        //Name
        //--------------------------------------------------------------------
        private string _Name;
        /// <summary>
        /// Gets or sets entity Name. 
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion


        #region --------------Bclass--------------
        //------------------------------------------------------------------------------------------------------
        //Bclass
        //--------------------------------------------------------------------
        private string _Bclass;
        /// <summary>
        /// Gets or sets entity Bclass. 
        /// </summary>
        public string Bclass
        {
            get { return _Bclass; }
            set { _Bclass = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------OnPress--------------
        //------------------------------------------------------------------------------------------------------
        //OnPress
        //--------------------------------------------------------------------
        private string _OnPress;
        /// <summary>
        /// Gets or sets entity OnPress. 
        /// </summary>
        public string OnPress
        {
            get { return _OnPress; }
            set { _OnPress = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

		public FlexigridButton() { 
        }

        public FlexigridButton(string name, string bclass, string onPress)
        {
            this.Name = name;
            this.Bclass = bclass;
            this.OnPress = onPress;
        }


    }
		
    public class FlexigridViewModel
    {
        #region --------------ID--------------
        //------------------------------------------------------------------------------------------------------
        //ID
        //--------------------------------------------------------------------
        private string _ID="Flexigrid";
        /// <summary>
        /// Gets or sets entity ID. 
        /// </summary>
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
		
        #region --------------Url--------------
        //------------------------------------------------------------------------------------------------------
        //Url
        //--------------------------------------------------------------------
        private string _Url;
        /// <summary>
        /// Gets or sets entity Url. 
        /// </summary>
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------DataType--------------
        //------------------------------------------------------------------------------------------------------
        //DataType
        //--------------------------------------------------------------------
        private string _DataType="json";
        /// <summary>
        /// Gets or sets entity DataType. 
        /// </summary>
        public string DataType
        {
            get { return _DataType; }
            set { _DataType = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
		

        #region --------------SortName--------------
        //------------------------------------------------------------------------------------------------------
        //SortName
        //--------------------------------------------------------------------
        private string _SortName;
        /// <summary>
        /// Gets or sets entity SortName. 
        /// </summary>
        public string SortName
        {
            get { return _SortName; }
            set { _SortName = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------SortOrder--------------
        //------------------------------------------------------------------------------------------------------
        //SortOrder
        //--------------------------------------------------------------------
        private string _SortOrder = "asc";
        /// <summary>
        /// Gets or sets entity SortOrder. 
        /// </summary>
        public string SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion


        #region --------------UsePager--------------
        //------------------------------------------------------------------------------------------------------
        //UsePager
        //--------------------------------------------------------------------
        private bool _UsePager = true;
        /// <summary>
        /// Gets or sets entity UsePager. 
        /// </summary>
        public bool UsePager
        {
            get { return _UsePager; }
            set { _UsePager = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Title--------------
        //------------------------------------------------------------------------------------------------------
        //Title
        //--------------------------------------------------------------------
        private string _Title;
        /// <summary>
        /// Gets or sets entity Title. 
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------UseRp--------------
        //------------------------------------------------------------------------------------------------------
        //UseRp
        //--------------------------------------------------------------------
        private bool _UseRp=true;
        /// <summary>
        /// Gets or sets entity UseRp. 
        /// </summary>
        public bool UseRp
        {
            get { return _UseRp; }
            set { _UseRp = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Rp--------------
        //------------------------------------------------------------------------------------------------------
        //Rp
        //--------------------------------------------------------------------
        private int _Rp=15;
        /// <summary>
        /// Gets or sets entity Rp. 
        /// </summary>
        public int Rp
        {
            get { return _Rp; }
            set { _Rp = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion


        #region --------------ShowTableToggleBtn--------------
        //------------------------------------------------------------------------------------------------------
        //ShowTableToggleBtn
        //--------------------------------------------------------------------
        private bool _ShowTableToggleBtn=true;
        /// <summary>
        /// Gets or sets entity ShowTableToggleBtn. 
        /// </summary>
        public bool ShowTableToggleBtn
        {
            get { return _ShowTableToggleBtn; }
            set { _ShowTableToggleBtn = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Width--------------
        //------------------------------------------------------------------------------------------------------
        //Width
        //--------------------------------------------------------------------
        private int _Width=750;
        /// <summary>
        /// Gets or sets entity Width. 
        /// </summary>
        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Height--------------
        //------------------------------------------------------------------------------------------------------
        //Height
        //--------------------------------------------------------------------
        private int _Height=500;
        /// <summary>
        /// Gets or sets entity Height. 
        /// </summary>
        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        public string jsForm { get; set; }
        public string jsObject { get; set; }

        public List<FlexigridColumn> Columns { get; set; }
        public List<FlexigridButton> Buttons { get; set; }
        public int DefaultSearchIndex { get; set; }

        public bool SetButtonsSerator { get; set; }

        #region --------------SearchColumns--------------
        //------------------------------------------------------------------------------------------------------
        //SearchColumns
        //--------------------------------------------------------------------
        private List<int>  _SearchColumns=null;
        /// <summary>
        /// Gets or sets entity SearchColumns. 
        /// </summary>
        public List<int> SearchColumns
        {
            get
            {
                if (_SearchColumns == null)
                {
                    _SearchColumns = new List<int>();
                    int i = 0;
                    foreach (FlexigridColumn c in Columns)
                    {
                        if (c.Searchable)
                        {
                            _SearchColumns.Add(i);
                            if (c.IsTheDefaultInSearch)
                                DefaultSearchIndex = i;
                        }
                        i++;
                    }

                }
                return _SearchColumns;
            }
            //set { _SearchColumns = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        /*public List<int> SearchColumns { get 
        {
            
            for (int i = 0; i < Columns.Count; i++)
			{
                c=Columns[i];
                if(c.Searchable)

			}
            foreach (FlexigridColumn c in Columns)
            {
            }
        }*/
        #region --------------DialogBoxId--------------
        //------------------------------------------------------------------------------------------------------
        //DialogBoxId
        //--------------------------------------------------------------------
        private string _DialogBoxId;
        /// <summary>
        /// Gets or sets entity DialogBoxId. 
        /// </summary>
        public string DialogBoxId
        {
            get { return _DialogBoxId; }
            set { _DialogBoxId = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------DialogBoxWidth--------------
        //------------------------------------------------------------------------------------------------------
        //DialogBoxWidth
        //--------------------------------------------------------------------
        private int _DialogBoxWidth;
        /// <summary>
        /// Gets or sets entity DialogBoxWidth. 
        /// </summary>
        public int DialogBoxWidth
        {
            get { return _DialogBoxWidth; }
            set { _DialogBoxWidth = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------DialogBoxHeight--------------
        //------------------------------------------------------------------------------------------------------
        //DialogBoxHeight
        //--------------------------------------------------------------------
        private int _DialogBoxHeight;
        /// <summary>
        /// Gets or sets entity DialogBoxHeight. 
        /// </summary>
        public int DialogBoxHeight
        {
            get { return _DialogBoxHeight; }
            set { _DialogBoxHeight = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
		
        public FlexigridViewModel()
        { }
        public FlexigridViewModel(string id,string jsform,string jsobject, string url, string dataType, string sortName, string sortOrder, bool usePager, string title, bool useRp, int rp, bool showTableToggleBtn, int width, int height)
        {
            this.ID = id;
            this.jsForm = jsform;
            this.jsObject = jsobject;
            this.Url = url;
            this.DataType = dataType;
            this.SortName = sortName;
            this.SortOrder = sortOrder;
            this.UsePager =usePager ;
            this.Title = title;
            this.UseRp = useRp;
            this.Rp = rp;
            this.ShowTableToggleBtn = showTableToggleBtn;
            this.Width = width;
            this.Height = Height;
        }

        public string GetPropString(object val)
        {
            if (val is bool)
            {
                return val.ToString().ToLower();
            }

            return val.ToString().ToLower();
        }

        public string GetPropString(bool val)
        {
            return val.ToString().ToLower();
        }

    }
}