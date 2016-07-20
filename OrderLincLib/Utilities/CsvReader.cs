using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading;
using System.Text.RegularExpressions;

namespace OrderLinc.Utilities
{
    public class CsvReader
    {
        string _csvContent;
        List<CsvRow> _rows;
        public event EventHandler<CsvReaderRowArgs> RowReadEvent;

        public CsvReader(string csvData)
        {
            Option = new CsvOption();

            Fields = new List<CsvField>();
            _rows = new List<CsvRow>();
            Rows = new ReadOnlyCollection<CsvRow>(_rows);
            _csvContent = csvData;
            IsValid = true;
            ErrorText = string.Empty;
        }

        public CsvOption Option { get; set; }

        public List<CsvField> Fields { get; set; }

        public ReadOnlyCollection<CsvRow> Rows { get; private set; }

        private void OnRowRead(CsvReaderRowArgs e)
        {
            if (RowReadEvent != null) RowReadEvent(this, e);
        }

        public bool IsValid { get; private set; }

        public string ErrorText { get; private set; }

        public void Read()
        {
            if (_csvContent == null) throw new ArgumentNullException("csvData");
            if (_csvContent.Length == 0) throw new ArgumentNullException("empty");
            if (Option == null) throw new ArgumentNullException("option");

            try
            {

                Parse(_csvContent);
            }
            catch
            {
                throw;
            }


        }

        private void ParseHeader(string headerRow)
        {
            try
            {
                bool isValid; string errorText = string.Empty;

                string[] data = ParseLineData(headerRow, out isValid, out errorText);

                IsValid = isValid;
                ErrorText = errorText;

                if (IsValid == false) return;

                if (Fields.Count > 0)
                {
                    if (Fields.Count != data.Length)
                    {
                        IsValid = false;
                        ErrorText = "Invalid format. Number of columns does not match.";
                    }
                }
                else
                {
                    foreach (string f in data)
                    {
                        Fields.Add(new CsvField()
                        {
                            Name = f.Trim(),
                            DataType = typeof(string)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string[] ParseLineData(string lineData, out bool isValid, out string error)
        {
            bool begin = false;
            isValid = true;
            error = string.Empty;

            List<string> list = new List<string>();
            StringBuilder blr = new StringBuilder(100);

            string prv = string.Empty;
            int i = 0;
            string cleanData = Regex.Replace(lineData, @"[^\x20-\x7F]", "");

            foreach (char c in cleanData)
            {
                i++;
                if (c.ToString() == "\"" && begin == false)
                {
                    begin = true;
                    prv = c.ToString();
                    continue;
                }
                else if ((c.ToString() == "\"" && begin == true))
                {
                    list.Add(blr.ToString().Trim());
                    blr.Clear();
                    begin = false;
                    prv = c.ToString();
                    continue;
                }
                else if (begin == false && c.ToString() == "," && prv == "\"" && i != lineData.Length) // the expected char is comma, if the previous char is " and not index i = lineData.Length then this a separator
                {
                    prv = c.ToString();
                    continue;
                }
                else if (begin == false && c.ToString() == "," && prv == ",") // consider as empty fields.
                {
                    prv = c.ToString();
                    list.Add(string.Empty);
                    if (cleanData.Length == i)
                        list.Add(string.Empty); // add empty field value
                }
                else if (begin == false && c.ToString() == "," && prv == "\"") // consider as empty fields.
                {
                    prv = c.ToString();
                    list.Add(string.Empty);

                    if (cleanData.Length == i)
                        list.Add(string.Empty); // add empty field value
                }
                else if (begin == false)
                {
                    isValid = false;
                    error = "Invalid format. Extra character has detected.";
                    return list.ToArray();
                }

                blr.Append(c);
            }

            return list.ToArray();
        }

        private void Parse(string csv)
        {
            try
            {

                string[] lines = csv.Split(new string[] { this.Option.LineTerminator }, StringSplitOptions.RemoveEmptyEntries);

                int startRow = this.Option.SkipRows;

                if (this.Option.FirstRowIsHeader)
                {
                    ParseHeader(lines[startRow]);
                    startRow += 1;

                }
                int index = 0;
                if (IsValid == false) return;

                for (int i = startRow; i < lines.Length; i++)
                {

                    string strData = lines[i];
                    bool isValid; string errorText = string.Empty;

                    string[] data = ParseLineData(strData, out isValid, out errorText);

                    CsvRow row = new CsvRow(this.Fields, strData, data, index, i + 1, isValid, errorText);
                    index += 1;

                    _rows.Add(row);
                    OnRowRead(new CsvReaderRowArgs(row));
                }

            }
            catch
            {
                throw;
            }
        }
    }

    public class CsvReaderArgs : EventArgs
    {
        public bool IsError { get; private set; }


    }

    public class CsvReaderRowArgs : EventArgs
    {
        public CsvReaderRowArgs(CsvRow row)
        {
            Row = row;
        }

        public CsvRow Row { get; private set; }
    }

    public class CsvOption
    {
        public CsvOption()
        {
            LineTerminator = "\r\n";
            FieldValueQualifier = "\"";
            FieldTerminator = ",";

            RemoveHiddenChars = true;
            Format = CsvFormat.Delimited;
            SkipRows = 0;
            FirstRowIsHeader = true;
        }

        public string LineTerminator { get; set; }

        public string FieldTerminator { get; set; }

        public string FieldValueQualifier { get; set; }

        public CsvFormat Format { get; set; }

        public int SkipRows { get; set; }

        public bool FirstRowIsHeader { get; set; }

        public bool RemoveHiddenChars { get; set; }
    }

    public enum CsvFormat
    {
        Delimited = 1,
        FixedWidth = 2,

    }

    public class CsvField
    {

        /// <summary>
        /// Source format string
        /// </summary>
        public string Format { get; set; }

        public string Name
        {
            get;
            set;
        }

        public Type DataType { get; set; }

        public bool IsRequired { get; set; }
    }

    public class CsvRow
    {
        private string[] data;
        private Dictionary<string, object> keyValue;
        string _strData;

        public CsvRow(List<CsvField> fields, string[] fieldData, int index, int lineNo, bool isValid, string errorText)
            : this(fields, null, fieldData, index, lineNo, isValid, errorText)
        { }

        public CsvRow(List<CsvField> fields, string strData, string[] fieldData, int index, int lineNo, bool isValid, string errorText)
        {
            IsValid = isValid;
            ValidateMessage = errorText;

            _strData = strData;
            Fields = fields;
            data = fieldData;
            Index = index;
            LineNo = lineNo;

            ParseData();
        }


        public string LineData
        {
            get
            {
                if (string.IsNullOrEmpty(_strData))
                    _strData = string.Join(",", data);

                return _strData;
            }
        }

        private void ParseData()
        {
            keyValue = new Dictionary<string, object>();
            StringBuilder blr = new StringBuilder(100);
            if (IsValid)
            {
                if (Fields.Count != data.Length)
                {
                    IsValid = false;
                    blr.Append("Data field count does not match with the defined field count.");
                }
            }

            for (int i = 0; i < Fields.Count; i++)
            {
                if (i > data.Length - 1) // field does not match
                    break;

                var fld = Fields[i];
                var value = data[i].Trim();

                if (string.IsNullOrEmpty(value) && fld.IsRequired)
                {
                    IsValid = false;
                    blr.Append(string.Format("Missing value for {0}. ", fld.Name));
                }

                if (typeof(string) == fld.DataType)
                    keyValue.Add(fld.Name, value);
                else if (typeof(int) == fld.DataType || typeof(long) == fld.DataType)
                {
                    if (string.IsNullOrEmpty(value))
                        keyValue.Add(fld.Name, 0);
                    else
                    {
                        if (typeof(int) == fld.DataType)
                        {
                            int p = 0;
                            if (int.TryParse(value, out p))
                            {
                                keyValue.Add(fld.Name, p);
                            }
                            else
                            {
                                keyValue.Add(fld.Name, 0);
                                IsValid = false;
                                blr.Append(string.Format("Invalid value for {0}. ", fld.Name));
                            }
                        }
                        else
                        {
                            long p = 0;
                            if (long.TryParse(value, out p))
                            {
                                keyValue.Add(fld.Name, p);
                            }
                            else
                            {
                                keyValue.Add(fld.Name, 0);
                                IsValid = false;
                                blr.Append(string.Format("Invalid value for {0}. ", fld.Name));
                            }

                        }
                    }
                }
                else if (typeof(double) == fld.DataType)
                {
                    if (string.IsNullOrEmpty(value))
                        keyValue.Add(fld.Name, 0);
                    else
                    {
                        double p = 0;
                        if (double.TryParse(value, out p))
                        {
                            keyValue.Add(fld.Name, p);
                        }
                        else
                        {
                            keyValue.Add(fld.Name, 0);
                            IsValid = false;
                            blr.Append(string.Format("Invalid value for {0}. ", fld.Name));
                        }
                    }
                }
                else if (typeof(float) == fld.DataType)
                {
                    if (string.IsNullOrEmpty(value))
                        keyValue.Add(fld.Name, 0);
                    else
                    {
                        float p = 0;
                        if (float.TryParse(value, out p))
                        {
                            keyValue.Add(fld.Name, p);
                        }
                        else
                        {
                            keyValue.Add(fld.Name, 0);
                            IsValid = false;
                            blr.Append(string.Format("Invalid value for {0}. ", fld.Name));
                        }
                    }
                }
                else if (typeof(float) == fld.DataType)
                {
                    if (string.IsNullOrEmpty(value))
                        keyValue.Add(fld.Name, 0);
                    else
                    {
                        float p = 0;
                        if (float.TryParse(value, out p))
                        {
                            keyValue.Add(fld.Name, p);
                        }
                        else
                        {
                            keyValue.Add(fld.Name, 0);
                            IsValid = false;
                            blr.Append(string.Format("Invalid value for {0}. ", fld.Name));
                        }
                    }
                }
                else if (typeof(DateTime) == fld.DataType || typeof(DateTime?) == fld.DataType)
                {
                    if (string.IsNullOrEmpty(value))
                        keyValue.Add(fld.Name, null);
                    else
                    {
                        DateTime p;

                        if (DateTime.TryParseExact(value, fld.Format, Thread.CurrentThread.CurrentCulture, System.Globalization.DateTimeStyles.None, out p))
                        {
                            keyValue.Add(fld.Name, p);
                        }
                        else
                        {
                            keyValue.Add(fld.Name, null);
                            IsValid = false;
                            blr.Append(string.Format("Invalid value ({0}) or date format for '{1}', expected format '{2}'. ", value, fld.Name, fld.Format));
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(ValidateMessage))
                ValidateMessage = blr.ToString();
            else
                ValidateMessage += " " + blr.ToString();
        }

        public string ValidateMessage
        {
            get;
            private set;
        }

        public bool IsValid
        {
            get;
            private set;
        }
        /// <summary>
        /// Row index
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Line number in csv file
        /// </summary>
        public int LineNo { get; private set; }

        public List<CsvField> Fields { get; private set; }



        public string this[string fieldName]
        {
            get
            {
                int i = GetIndex(fieldName);

                if (i == -1) throw new ArgumentOutOfRangeException("fieldName");

                return data[i];
            }
        }

        public T GetValue<T>(string fieldName)
        {
            if (keyValue.ContainsKey(fieldName) == false)
            {
                throw new ArgumentOutOfRangeException(string.Format("Invalid field '{0}'", fieldName));
            }
            object value = keyValue[fieldName];

            if (value == null) return default(T);

            Type t = typeof(T);

            t = Nullable.GetUnderlyingType(t) ?? t;

            return (T)Convert.ChangeType(keyValue[fieldName], t);
        }

        private int GetIndex(string fieldName)
        {

            for (int j = 0; j < Fields.Count; j++)
            {
                if (Fields[j].Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
                    return j;
            }

            return -1;
        }

    }
}
