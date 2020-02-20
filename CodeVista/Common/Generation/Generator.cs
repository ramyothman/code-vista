using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Linq.Expressions;
namespace Common.Generation
{
    public class Generator
    {
        #region Properties & Declarations
        private CodeType CurrentCodeType = CodeType.Other;
        private string parameterSeperator = ",";
        public string ParameterSeperator
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    parameterSeperator = value;
            }
            get { return parameterSeperator; }
        }

        private string lengthPrefix = "(";
        public string LengthPrefix
        {
            set { lengthPrefix = value; }
            get { return lengthPrefix; }
        }

        private string lengthSuffix = ")";
        public string LengthSuffix
        {
            set { lengthSuffix = value; }
            get { return lengthSuffix; }
        }
        #endregion
        
        
        public Generator(CodeType currentCodeType)
        {
            this.CurrentCodeType = currentCodeType;
        }

        public string GetCode(string input, string namespaceName, string className, Entities.MetaDataSchema.Table table)
        {
            return GetCode(input, namespaceName, className,"", table);
        }
        public string GetCode(string input,string namespaceName,string className,string authorName,Entities.MetaDataSchema.Table table)
        {
            StringBuilder result = new StringBuilder(input);
            result = GeneratorCommands.ReplaceCommandBuilder(result, GeneratorCommands.NameSpaceCommand, namespaceName);
            result = GeneratorCommands.ReplaceCommandBuilder(result, GeneratorCommands.ClassNameCommand, className);
            result = GeneratorCommands.ReplaceCommandBuilder(result, GeneratorCommands.TableNameCommand, table.Name);
            result = GeneratorCommands.ReplaceCommandBuilder(result, GeneratorCommands.SchemaName, table.Schema);
            result = GeneratorCommands.ReplaceCommandBuilder(result, GeneratorCommands.AuthorNameCommand, authorName);
            result = GeneratorCommands.ReplaceCommandBuilder(result, GeneratorCommands.CurrentDateCommand, DateTime.Now.ToShortDateString());
            result = new StringBuilder(GetCodeConditioned(result.ToString(), namespaceName, className, authorName, table));
            Regex reg = new Regex(@"(\[\$\$.*?\$\$\])", RegexOptions.Singleline);
            Match match; //= reg.Match(input);
            string value = "";
            for (match = reg.Match(input); match.Success; match = match.NextMatch())
            {
                value = match.Groups[0].Value;
                #region Parameters

                Regex regInternal = new Regex(String.Format(@"<%\$\$\s*{0}\s*{1}", GeneratorCommands.TableColumnParamCommand, GeneratorCommands.PrimaryKeyOption), RegexOptions.IgnoreCase);
                Match matchInternal = regInternal.Match(value);
                bool isNormal = true;
                if (matchInternal.Success)
                {
                    string currentValue = GetPrimaryKeyParameters(value, table);
                    result = result.Replace(value, currentValue);
                    isNormal = false;
                    continue;
                }
                regInternal = new Regex(String.Format(@"<%\$\$\s*{0}\s*{1}", GeneratorCommands.TableColumnParamCommand, GeneratorCommands.ForeignKeyOption), RegexOptions.IgnoreCase);
                matchInternal = regInternal.Match(value);
                if (matchInternal.Success)
                {
                    string currentValue = GetForeignKeyParameters(value, table);
                    result = result.Replace(value, currentValue);
                    isNormal = false;
                    continue;
                }
                if(isNormal)
                {
                    regInternal = new Regex(@"<%\$\$\s*" + GeneratorCommands.TableColumnParamCommand, RegexOptions.IgnoreCase);
                    matchInternal = regInternal.Match(value);
                    if (matchInternal.Success)
                    {
                        string currentValue = GetNormalParameters(value, table);
                        result = result.Replace(value, currentValue);
                        continue;
                    }
                }
                #endregion 

                #region Normal Columns
                regInternal = new Regex(String.Format(@"<%\$\$\s*{0}\s*{1}", GeneratorCommands.TableColumnCommand, GeneratorCommands.PrimaryKeyOption), RegexOptions.IgnoreCase);
                matchInternal = regInternal.Match(value);
                isNormal = true;
                if (matchInternal.Success)
                {
                    string currentValue = GetPrimaryKeyTableColumnRow(value, table);
                    result = result.Replace(value, currentValue);
                    isNormal = false;
                    continue;
                }

                regInternal = new Regex(String.Format(@"<%\$\$\s*{0}\s*{1}", GeneratorCommands.TableColumnCommand, GeneratorCommands.ForeignKeyOption), RegexOptions.IgnoreCase);
                matchInternal = regInternal.Match(value);
                if (matchInternal.Success)
                {
                    string currentValue = GetForeignKeyTableColumnRow(value, table);
                    result = result.Replace(value, currentValue);
                    isNormal = false;
                    continue;
                }

                if(isNormal)
                {
                    regInternal = new Regex(@"<%\$\$\s*" + GeneratorCommands.TableColumnCommand, RegexOptions.IgnoreCase);
                    matchInternal = regInternal.Match(value);
                    if (matchInternal.Success)
                    {
                        string currentValue = GetNormalTableColumnRow(value, table);
                        result = result.Replace(value, currentValue);
                        continue;
                    }
                    
                }

                regInternal = new Regex(String.Format(@"<%\$\$\s*{0}\s*{1}", GeneratorCommands.ParentTableColumnCommand, GeneratorCommands.ForeignKeyOption), RegexOptions.IgnoreCase);
                matchInternal = regInternal.Match(value);
                if (matchInternal.Success)
                {
                    string currentValue = GetForeignKeyTableColumnRow(value, table);
                    result = result.Replace(value, currentValue);
                    isNormal = false;
                    continue;
                }
                #endregion

            }
            return result.ToString();
        }
        public string GetCodeConditioned(string input, string namespaceName, string className, string authorName, Entities.MetaDataSchema.Table table)
        {
            StringBuilder result = new StringBuilder(input);
            Regex reg = new Regex(String.Format(@"(\[\$\${0}.*?\$\$\])", GeneratorCommands.IFCondition), RegexOptions.Singleline);
            Match match; //= reg.Match(input);
            string value = "";
            for (match = reg.Match(input); match.Success; match = match.NextMatch())
            {
                value = match.Groups[0].Value;
                Regex regInternal = new Regex(String.Format(@"\[\$\${0}(\s*{1}|{2}|{3}|{4}|{5}\s*[(]\s*\w*\W*\s*[)])", GeneratorCommands.IFCondition, GeneratorCommands.ColumnName,GeneratorCommands.DataTypeName,GeneratorCommands.IsComputed,GeneratorCommands.IsIdentity,GeneratorCommands.IsPrimaryKey), RegexOptions.IgnoreCase);
                Match matchInternal = regInternal.Match(value);
                if (matchInternal.Success)
                {
                    string currentCondition = matchInternal.Groups[0].Value;
                    
                    //if(currentCondition.ToLower())
                }
            }
            return result.ToString();
        }

        #region Helper Methods

        #region Table Column Data Handling
        private string GetPrimaryKeyParameters(string input, Entities.MetaDataSchema.Table table)
        {
            StringBuilder result = new StringBuilder();
            int index = 0;
            foreach (Entities.MetaDataSchema.Column col in table.Columns)
            {
                if (col.IsPrimary)
                {
                    string tempValue = input;
                    string dataType = GetDataType(col);
                    

                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.DataTypeCommand, GeneratorCommands.PrimaryKeyOption, "", dataType);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.LibraryTypeCommand, GeneratorCommands.PrimaryKeyOption, "", col.ColumnDataType.LibraryType);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.DataTypeLengthCommand, GeneratorCommands.PrimaryKeyOption, "", GetDataTypeLength(col));
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand, GeneratorCommands.PrimaryKeyOption, "", col.Name);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand, GeneratorCommands.PrimaryKeyOption, GeneratorCommands.LowerCaseOption, col.Name.ToLower());
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand, GeneratorCommands.PrimaryKeyOption, GeneratorCommands.UpperCaseOption, col.Name.ToUpper());
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand, GeneratorCommands.PrimaryKeyOption, GeneratorCommands.FirstLowerOption, FirstLetterLower(col.Name));
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand, GeneratorCommands.PrimaryKeyOption, GeneratorCommands.FirstUpperOption, FirstLetterUpper(col.Name));

                    
                  
                    if (index < (table.PrimaryColumnCount - 1))
                    {
                        tempValue += parameterSeperator;
                    }
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.NewLineCommand, GeneratorCommands.NewLineValue);
                    tempValue = RemoveEnvelope(tempValue);
                    result.Append(tempValue);
                    index++;
                }
            }
            return result.ToString();
        }

        private string GetForeignKeyParameters(string input, Entities.MetaDataSchema.Table table)
        {
            StringBuilder result = new StringBuilder();
            int index = 0;
            foreach (Entities.MetaDataSchema.Column col in table.Columns)
            {
                if (col.IsForeign)
                {
                    string tempValue = input;
                    string dataType = GetDataType(col);


                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.DataTypeCommand, GeneratorCommands.ForeignKeyOption, "", dataType);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.LibraryTypeCommand, GeneratorCommands.ForeignKeyOption, "", col.ColumnDataType.LibraryType);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.DataTypeLengthCommand, GeneratorCommands.ForeignKeyOption, "", GetDataTypeLength(col));
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand, GeneratorCommands.ForeignKeyOption, "", col.Name);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand, GeneratorCommands.ForeignKeyOption, GeneratorCommands.LowerCaseOption, col.Name.ToLower());
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand, GeneratorCommands.ForeignKeyOption, GeneratorCommands.UpperCaseOption, col.Name.ToUpper());
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand, GeneratorCommands.ForeignKeyOption, GeneratorCommands.FirstLowerOption, FirstLetterLower(col.Name));
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand, GeneratorCommands.ForeignKeyOption, GeneratorCommands.FirstUpperOption, FirstLetterUpper(col.Name));
                    var fkParent = (from x in col.ParentTable.ParentRelationShips where x.ChildColumn.Name == col.Name && x.ChildTable.Name == col.ParentTable.Name select x).FirstOrDefault();
                    if (fkParent != null)
                    {
                        tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.ParentTableColumnCommand, GeneratorCommands.NameCommand, GeneratorCommands.ForeignKeyOption, "", fkParent.MasterTable.Name);
                        tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.ParentTableColumnCommand, GeneratorCommands.NameCommand, "", GeneratorCommands.FirstUpperOption, FirstLetterUpper(fkParent.MasterTable.Name));
                        tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.ParentTableColumnCommand, GeneratorCommands.NameCommand, "", GeneratorCommands.FirstLowerOption, FirstLetterLower(fkParent.MasterTable.Name));
                    }


                    if (index < (table.ForeignColumnCount - 1))
                    {
                        tempValue += parameterSeperator;
                        //result.Append(ParameterSeperator);
                    }
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.NewLineCommand, GeneratorCommands.NewLineValue);
                    tempValue = RemoveEnvelope(tempValue);
                    result.Append(tempValue);
                    index++;
                }
            }
            return result.ToString();
        }

        private string GetNormalParameters(string input, Entities.MetaDataSchema.Table table)
        {
            StringBuilder result = new StringBuilder();
            int index = 0;
            foreach (Entities.MetaDataSchema.Column col in table.Columns)
            {
                string tempValue = input;
                
                string dataType = GetDataType(col);

                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.DataTypeCommand, "", "", dataType);
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand, "", "", col.Name);
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand,"", GeneratorCommands.LowerCaseOption, col.Name.ToLower());
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand,"", GeneratorCommands.UpperCaseOption, col.Name.ToUpper());
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand,"", GeneratorCommands.FirstLowerOption,  FirstLetterLower(col.Name));
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.NameCommand,"", GeneratorCommands.FirstUpperOption, FirstLetterUpper(col.Name));

                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.LibraryTypeCommand,"", "", col.ColumnDataType.LibraryType);
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnParamCommand, GeneratorCommands.DataTypeLengthCommand, "", "", GetDataTypeLength(col));

                
                if (index < (table.Columns.Count - 1))
                {
                    tempValue += parameterSeperator;
                }
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.NewLineCommand, GeneratorCommands.NewLineValue);
                tempValue = RemoveEnvelope(tempValue);
                result.Append(tempValue);
                index++;
            }
            return result.ToString();
        }

        private string GetNormalTableColumnRow(string input, Entities.MetaDataSchema.Table table)
        {
            StringBuilder result = new StringBuilder();
            int index = 0;
            foreach (Entities.MetaDataSchema.Column col in table.Columns)
            {
                string tempValue = input;
                string dataType = GetDataType(col);
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.DataTypeCommand, "", "", dataType);
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand, "", "", col.Name);
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand,"", GeneratorCommands.LowerCaseOption, col.Name.ToLower());
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand,"", GeneratorCommands.UpperCaseOption, col.Name.ToUpper());
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand,"", GeneratorCommands.FirstLowerOption, FirstLetterLower(col.Name));
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand,"", GeneratorCommands.FirstUpperOption, FirstLetterUpper(col.Name));

                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.LibraryTypeCommand, "", "", col.ColumnDataType.LibraryType);
                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.DataTypeLengthCommand, "", "", GetDataTypeLength(col));

                

                tempValue = RemoveEnvelope(tempValue);

                tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.NewLineCommand, GeneratorCommands.NewLineValue);
                result.Append(tempValue);
                index++;
            }
            return result.ToString();
        }

        private string GetPrimaryKeyTableColumnRow(string input, Entities.MetaDataSchema.Table table)
        {
            StringBuilder result = new StringBuilder();
            int index = 0;
            foreach (Entities.MetaDataSchema.Column col in table.Columns)
            {
                if (col.IsPrimary)
                {
                    string tempValue = input;
                    string dataType = GetDataType(col);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.DataTypeCommand, GeneratorCommands.PrimaryKeyOption, "", dataType);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.LibraryTypeCommand, GeneratorCommands.PrimaryKeyOption, "", col.ColumnDataType.LibraryType);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.DataTypeLengthCommand, GeneratorCommands.PrimaryKeyOption, "", GetDataTypeLength(col));
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand, GeneratorCommands.PrimaryKeyOption, "", col.Name);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand, GeneratorCommands.PrimaryKeyOption, GeneratorCommands.LowerCaseOption, col.Name.ToLower());
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand, GeneratorCommands.PrimaryKeyOption, GeneratorCommands.UpperCaseOption, col.Name.ToUpper());
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand, GeneratorCommands.PrimaryKeyOption, GeneratorCommands.FirstLowerOption, FirstLetterLower(col.Name));
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand, GeneratorCommands.PrimaryKeyOption, GeneratorCommands.FirstUpperOption, FirstLetterUpper(col.Name));

                    tempValue = RemoveEnvelope(tempValue);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.NewLineCommand, GeneratorCommands.NewLineValue);
                    result.Append(tempValue);
                    index++;
                }
            }
            return result.ToString();
        }

        private string GetForeignKeyTableColumnRow(string input, Entities.MetaDataSchema.Table table)
        {
            StringBuilder result = new StringBuilder();
            int index = 0;
            foreach (Entities.MetaDataSchema.Column col in table.Columns)
            {
                if (col.IsForeign)
                {
                    string tempValue = input;
                    string dataType = GetDataType(col);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.DataTypeCommand, GeneratorCommands.ForeignKeyOption, "", dataType);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.LibraryTypeCommand, GeneratorCommands.ForeignKeyOption, "", col.ColumnDataType.LibraryType);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.DataTypeLengthCommand, GeneratorCommands.ForeignKeyOption, "", GetDataTypeLength(col));
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand, GeneratorCommands.ForeignKeyOption, "", col.Name);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand, GeneratorCommands.ForeignKeyOption, GeneratorCommands.LowerCaseOption, col.Name.ToLower());
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand, GeneratorCommands.ForeignKeyOption, GeneratorCommands.UpperCaseOption, col.Name.ToUpper());
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand, GeneratorCommands.ForeignKeyOption, GeneratorCommands.FirstLowerOption, FirstLetterLower(col.Name));
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.TableColumnCommand, GeneratorCommands.NameCommand, GeneratorCommands.ForeignKeyOption, GeneratorCommands.FirstUpperOption, FirstLetterUpper(col.Name));

                    var fkParent = (from x in col.ParentTable.ParentRelationShips where x.ChildColumn.Name == col.Name && x.ChildTable.Name == col.ParentTable.Name select x).FirstOrDefault();
                    if (fkParent != null)
                    {
                        tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.ParentTableColumnCommand, GeneratorCommands.NameCommand, "", GeneratorCommands.FirstUpperOption, FirstLetterUpper(fkParent.MasterTable.Name));
                        tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.ParentTableColumnCommand, GeneratorCommands.NameCommand, "", GeneratorCommands.FirstLowerOption, FirstLetterLower(fkParent.MasterTable.Name));
                    }

                    tempValue = RemoveEnvelope(tempValue);
                    tempValue = GeneratorCommands.ReplaceCommand(tempValue, GeneratorCommands.NewLineCommand, GeneratorCommands.NewLineValue);
                    result.Append(tempValue);
                    index++;
                }
            }
            return result.ToString();
        }
        #endregion

        #region General Helpers
        private string RemoveEnvelope(string input)
        {
            string tempValue = input.Replace("[$$", "");
            tempValue = tempValue.Replace("$$]", "");
            return tempValue;
        }
        private string GetDataTypeLength(Entities.MetaDataSchema.Column col)
        {
            string result = "";
            if (col.ColumnDataType.CalculateLength)
            {
                result = LengthPrefix + col.Length.ToString() + LengthSuffix;
            }
            return result;
        }
        private string GetDataType(Entities.MetaDataSchema.Column col)
        {
            string result = col.ColumnDataType.SQLType;
            switch (CurrentCodeType)
            {
                case CodeType.CSharp:
                    result = col.ColumnDataType.CSharpType;
                    break;
                case CodeType.DotNet:
                    result = col.ColumnDataType.DotNetType;
                    break;
                case CodeType.Java:
                    result = col.ColumnDataType.JavaType;
                    break;
                case CodeType.Other:
                    result = col.ColumnDataType.LanguageSpecificType;
                    break;
                case CodeType.VisualBasicDotNet:
                    result = col.ColumnDataType.VBType;
                    break;
                case CodeType.Sql:
                    result = col.ColumnDataType.SQLType;
                    break;
            }
            return result;
        }
        private string FirstLetterLower(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string subString = input.Substring(0, 1);
                input = input.Remove(0, 1);
                input = subString.ToLower() + input;
                return input;
            }
            return input;
        }

        private string FirstLetterUpper(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string subString = input.Substring(0, 1);
                input = input.Remove(0, 1);
                input = subString.ToUpper() + input;
                return input;
            }
            return input;
        }
        #endregion
        #endregion
    }
}
