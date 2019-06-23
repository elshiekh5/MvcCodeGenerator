using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace SMOSampleInConsol
{
    class CustomColumn
    {

        private Column _dbObject;
        private string _ProgramatlyName;
        private bool _HasDefaultValue;
        private string _TableName;
        private string _TableProgramatlyName;
        private Type _DotNetType; 
        private string _DotNetTypeAlias;
        private SqlDbType _SqlDbType;
        private System.ComponentModel.DataAnnotations.DataType _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Text;

        public Column dbObject{get { return _dbObject; }}

        public string NameInDatabase { get { return _dbObject.Name; } }

        public string ProgramatlyName{ get { return _ProgramatlyName; }}

        public Microsoft.SqlServer.Management.Smo.DataType SmoDataType { get { return _dbObject.DataType; } }

        public bool HasDefaultValue { get { return _HasDefaultValue; } }

        public bool IsComputed { get { return _dbObject.Computed; } }

        public bool IsIdentity { get { return _dbObject.Identity; } }

        public bool InPrimaryKey { get { return _dbObject.InPrimaryKey; } }

        public bool IsForeignKey { get { return _dbObject.IsForeignKey; } }

        public bool IsPersisted { get { return _dbObject.IsPersisted;  } }

        public bool IsNullable { get { return _dbObject.Nullable; } }

        public string TableName { get { return _TableName; } }

        public string TableProgramatlyName { get { return _TableProgramatlyName; } }


        public Type DotNetType { get { return _DotNetType; } }

        public string DotNetTypeString { get { return _DotNetType.ToString(); } }

        public string DotNetTypeAlias { get { return _DotNetTypeAlias; } }

        public SqlDbType SqlDbType { get { return _SqlDbType; } }
        public string SqlDbTypeString { get { return _SqlDbType.ToString(); } }

        public SqlDataType SqlDataType { get { return _dbObject.DataType.SqlDataType; } }

        public System.ComponentModel.DataAnnotations.DataType DataTypeAttr { get { return _DataTypeAttr; } }

        public int MaxLength { get { return _dbObject.DataType.MaximumLength; } }

        public bool IsOutoutIninsert { get { return (InPrimaryKey && IsIdentity); } }

        public bool NotPassedToSql { 
            get { 
                return (IsComputed);
            } 
        }

        public CustomColumn(Column c, string tableName, string tableProgramatlyName)
        {
            _dbObject = c;
            _TableName = tableName;
            _TableProgramatlyName = tableProgramatlyName;
            _ProgramatlyName = Globals.GetProgramatlyName(NameInDatabase);
            if (c.DefaultConstraint!=null)
            {
                _HasDefaultValue = true;
            }

            SetDataTypes();
        }


        public void SetDataTypes()
        {
            switch (SmoDataType.SqlDataType)
            {
                case SqlDataType.BigInt:
                    _SqlDbType = SqlDbType.BigInt;
                    _DotNetType = typeof(Int64);
                    _DotNetTypeAlias = "long";
                    break;
                case SqlDataType.Binary:
                    _SqlDbType = SqlDbType.Binary;
                    _DotNetType = typeof(Byte[]);
                    _DotNetTypeAlias = "byte[]";
                    break;
                case SqlDataType.Bit:
                    _SqlDbType = SqlDbType.Bit;
                    _DotNetType = typeof(Boolean);
                    _DotNetTypeAlias = "bool";
                    break;
                case SqlDataType.Char:
                    _SqlDbType = SqlDbType.Char;
                    _DotNetType = typeof(String);
                    _DotNetTypeAlias = "string";
                    SetStringDataType();
                    break;
                case SqlDataType.Date:
                    _SqlDbType = SqlDbType.Date;
                    _DotNetType = typeof(DateTime);
                    _DotNetTypeAlias = "DateTime";
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Date;
                    break;
                case SqlDataType.DateTime:
                    _SqlDbType = SqlDbType.DateTime;
                    _DotNetType = typeof(DateTime);
                    _DotNetTypeAlias = "DateTime";
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.DateTime;

                    break;
                case SqlDataType.DateTime2:
                    _SqlDbType = SqlDbType.DateTime2;
                    _DotNetType = typeof(DateTime);
                    _DotNetTypeAlias = "DateTime";
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.DateTime;
                    break;
                case SqlDataType.DateTimeOffset:
                    _SqlDbType = SqlDbType.DateTimeOffset;
                    _DotNetType = typeof(DateTimeOffset);
                    _DotNetTypeAlias = "DateTimeOffset";
                    break;
                case SqlDataType.Decimal:
                    _SqlDbType = SqlDbType.Decimal;
                    _DotNetType = typeof(Decimal);
                    _DotNetTypeAlias = "decimal";

                    break;
                case SqlDataType.Float:
                    _SqlDbType = SqlDbType.Float;
                    _DotNetType = typeof(Double);
                    _DotNetTypeAlias = "double";
                    break;
                case SqlDataType.Image:
                    _SqlDbType = SqlDbType.Image;
                    _DotNetType = typeof(Byte[]);
                    _DotNetTypeAlias = "byte[]";
                    break;
                case SqlDataType.Int:
                    _SqlDbType = SqlDbType.Int;
                    _DotNetType = typeof(Int32);
                    _DotNetTypeAlias = "int";
                    break;
                case SqlDataType.Money:
                    _SqlDbType = SqlDbType.Money;
                    _DotNetType = typeof(Decimal);
                    _DotNetTypeAlias = "decimal";
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Currency;

                    break;
                case SqlDataType.NChar:
                    _SqlDbType = SqlDbType.NChar;
                    _DotNetType = typeof(String);
                    _DotNetTypeAlias = "string";
                    SetStringDataType();

                    break;
                case SqlDataType.NText:
                    _SqlDbType = SqlDbType.NText;
                    _DotNetType = typeof(String);
                    _DotNetTypeAlias = "string";
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.MultilineText;

                    break;
                case SqlDataType.NVarChar:
                    _SqlDbType = SqlDbType.NVarChar;
                    _DotNetType = typeof(String);
                    _DotNetTypeAlias = "string";
                    SetStringDataType();

                    break;
                case SqlDataType.Real:
                    _SqlDbType = SqlDbType.Real;
                    _DotNetType = typeof(Single);
                    _DotNetTypeAlias = "Single";
                    break;
                case SqlDataType.SmallDateTime:
                    _SqlDbType = SqlDbType.SmallDateTime;
                    _DotNetType = typeof(DateTime);
                    _DotNetTypeAlias = "DateTime";
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.DateTime;
                    break;
                case SqlDataType.SmallInt:
                    _SqlDbType = SqlDbType.SmallInt;
                    _DotNetType = typeof(Int16);
                    _DotNetTypeAlias = "short";
                    break;
                case SqlDataType.SmallMoney:
                    _SqlDbType = SqlDbType.SmallMoney;
                    _DotNetType = typeof(Decimal);
                    _DotNetTypeAlias = "decimal";
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Currency;
                    break;

                case SqlDataType.Text:
                    _SqlDbType = SqlDbType.Text;
                    _DotNetType = typeof(String);
                    _DotNetTypeAlias = "string";
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.MultilineText;

                    break;
                case SqlDataType.Time:
                    _SqlDbType = SqlDbType.Time;
                    _DotNetType = typeof(TimeSpan);
                    _DotNetTypeAlias = "TimeSpan";
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Time;
                    break;
                case SqlDataType.Timestamp:
                    _SqlDbType = SqlDbType.Timestamp;
                    _DotNetType = typeof(Byte[]);
                    _DotNetTypeAlias = "byte[]";
                    break;
                case SqlDataType.TinyInt:
                    _SqlDbType = SqlDbType.TinyInt;
                    _DotNetType = typeof(Byte);
                    _DotNetTypeAlias = "byte";
                    break;
                case SqlDataType.UserDefinedDataType:
                    _SqlDbType = SqlDbType.UniqueIdentifier;
                    _DotNetType = typeof(Object);
                    _DotNetTypeAlias = "object";
                    break;
                case SqlDataType.UniqueIdentifier:
                    _DotNetType = typeof(Guid);
                    _DotNetTypeAlias = "Guid";
                    break;
                case SqlDataType.VarBinary:
                    _SqlDbType = SqlDbType.VarBinary;
                    _DotNetType = typeof(Byte[]);
                    _DotNetTypeAlias = "byte[]";
                    break;
                case SqlDataType.VarChar:
                    _SqlDbType = SqlDbType.VarChar;
                    _DotNetType = typeof(String);
                    _DotNetTypeAlias = "string";
                    SetStringDataType();

                    break;
                case SqlDataType.Variant:
                    _SqlDbType = SqlDbType.Variant;
                    _DotNetType = typeof(Object);
                    _DotNetTypeAlias = "object";
                    break;
                case SqlDataType.Xml:
                    _SqlDbType = SqlDbType.Xml;
                    _DotNetType = typeof(System.Xml.XmlDocument);
                    _DotNetTypeAlias = "XmlDocument";
                    break;
                case SqlDataType.Geography:
                    //    _SqlDbType = SqlDbType.Geography;
                    _DotNetType = typeof(Object);
                    _DotNetTypeAlias = "object";
                    break;
                case SqlDataType.Geometry:
                    //    _SqlDbType = SqlDbType.Geometry;
                    _DotNetType = typeof(Object);
                    _DotNetTypeAlias = "object";
                    break;
                case SqlDataType.HierarchyId:
                    //    _SqlDbType = SqlDbType.HierarchyId;
                    _DotNetType = typeof(Object);
                    _DotNetTypeAlias = "object";
                    break;
                case SqlDataType.None:
                    //    _SqlDbType = SqlDbType.None;
                    _DotNetType = typeof(Object);
                    _DotNetTypeAlias = "object";
                    break;
                case SqlDataType.Numeric:
                    //    _SqlDbType = SqlDbType.Numeric;
                    _DotNetType = typeof(Decimal);
                    _DotNetTypeAlias = "decimal";
                    break;

                case SqlDataType.NVarCharMax:
                    _SqlDbType = SqlDbType.NVarChar;
                    _DotNetType = typeof(String);
                    _DotNetTypeAlias = "string";
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.MultilineText;

                    break;
                case SqlDataType.SysName:
                    //    _SqlDbType = SqlDbType.SysName;
                    _DotNetType = typeof(Object);
                    _DotNetTypeAlias = "object";
                    break;
                case SqlDataType.UserDefinedTableType:
                    _SqlDbType = SqlDbType.Udt;
                    _DotNetType = typeof(Object);
                    _DotNetTypeAlias = "object";
                    break;
                case SqlDataType.UserDefinedType:
                    _SqlDbType = SqlDbType.Udt;
                    _DotNetType = typeof(Object);
                    _DotNetTypeAlias = "object";
                    break;
                case SqlDataType.VarBinaryMax:
                    _SqlDbType = SqlDbType.VarBinary;
                    _DotNetType = typeof(Byte[]);
                    _DotNetTypeAlias = "byte[]";
                    break;
                case SqlDataType.VarCharMax:
                    _SqlDbType = SqlDbType.VarChar;
                    _DotNetType = typeof(String);
                    _DotNetTypeAlias = "string";
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.MultilineText;

                    break;
                default:
                    _DotNetType = typeof(String);
                    _DotNetTypeAlias = "string";
                    break;
            }
        }
        //-------------------------------------------------------------------
        public void SetStringDataType()
        {
            if (_DotNetType == typeof(String))
            {
                if (MaxLength >= 200)
                {
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.MultilineText;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_Url, this))
                {
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Url;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_Email, this))
                {
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.EmailAddress;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_File, this))
                {
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Upload;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_Password, this))
                {
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Password;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_Phone, this))
                {
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.PhoneNumber;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_PostalCode, this))
                {
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.PostalCode;
                }
                else if (Globals.IsMatchIdentifier(Globals.Identifier_CreditCard, this))
                {
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.CreditCard;
                }
                else
                {
                    _DataTypeAttr = System.ComponentModel.DataAnnotations.DataType.Text;
                }
            }
            
        }
    }
}
