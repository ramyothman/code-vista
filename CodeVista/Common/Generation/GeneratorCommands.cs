using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Generation
{

    public class GeneratorCommands
    {
        #region Main Commands
        /************************ Main Commands ****************************/
        public const string NameSpaceCommand = "NameSpace";
        public const string ClassNameCommand = "ClassName";
        public const string TableNameCommand = "TableName";
        public const string NewLineCommand = "NewLine";
        public const string TableColumnParamCommand = "TableColumnParam";
        public const string TableColumnQueryCommand = "TableColumnQuery";
        public const string TableColumnCommand = "TableColumn";
        public const string ForeignTableColumnCommand = "ForeignTableColumn";
        public const string ParentTableColumnCommand = "ParentTableColumn";
        public const string AuthorNameCommand = "AuthorName";
        public const string CurrentDateCommand = "CurrentDate";
        public const string DataObjectProperty = "DataObjectProperty";
        public const string NewLineValue = "\n";
        public const string IFCondition = "IFCondition";
        public const string ParentRelation = "ParentRelation";
        public const string ChildRelation = "ChildRelation";
        public const string SchemaName = "SchemaName";
        /************************* End Main Commands **************************/
        #endregion

        #region Second Commands
        /************************* Start Second Commands **************************/
        public const string DataTypeCommand = "DataType";
        public const string NameCommand = "Name";
        public const string LibraryTypeCommand = "LibraryType";
        public const string DataTypeLengthCommand = "Length";
        public const string IsPrimaryKey = "IsPrimaryKey";
        public const string IsForeignKey = "IsForeignKey";
        public const string IsIdentity = "IsIdentity";
        public const string IsComputed = "IsComputed";
        public const string ColumnName = "ColumnName";
        public const string DataTypeName = "DataTypeName";
        /************************* End Second Commands ***************************/
        #endregion

        #region Command Option
        /************************* Start Options **************************/
        public const string PrimaryKeyOption = @"\(PK\)";
        public const string ForeignKeyOption = @"\(FK\)";
        public const string LowerCaseOption = @"\(L\)";
        public const string UpperCaseOption = @"\(U\)";
        public const string FirstLowerOption = @"\(FL\)";
        public const string FirstUpperOption = @"\(FU\)";
        public const string TrueOption = @"\(true\)";
        public const string FalseOption = @"\(false\)";
        public const string AttributeValue = @"\(*\)";
        /************************* End Options **************************/
        #endregion

        public static string ReplaceCommand(string input, string command, string replaceString)
        {
            string result = input;
            Regex regInternal = new Regex(String.Format(@"<%\$\$\s*{0}\s*\$\$%>", command), RegexOptions.IgnoreCase);
            Match matchInternal = regInternal.Match(result);
            if (matchInternal.Success)
            {
                string value = matchInternal.Groups[0].Value;
                result = result.Replace(value, replaceString);
            }
            return result;
        }

        public static StringBuilder ReplaceCommandBuilder(StringBuilder input, string command, string replaceString)
        {
            StringBuilder result = new StringBuilder(input.ToString());
            Regex regInternal = new Regex(String.Format(@"<%\$\$\s*{0}\s*\$\$%>", command), RegexOptions.IgnoreCase);
            Match matchInternal = regInternal.Match(result.ToString());
            if (matchInternal.Success)
            {
                string value = matchInternal.Groups[0].Value;
                result = result.Replace(value, replaceString);
            }
            return result;
        }


        public static string ReplaceCommand(string input, string commandPart1,string commandPart2,string commandOption1,string commandOption2, string replaceString)
        {
            string result = input;
            

            string regExpString = "";
            if (!string.IsNullOrEmpty(commandOption1))
                commandOption1 = commandOption1 + @"\s*";
            if (!string.IsNullOrEmpty(commandOption2))
                commandOption2 = @"\s*" + commandOption2;
            regExpString = String.Format(@"{0}\s*{1},\s*{2}{3}", commandPart1, commandOption1, commandPart2, commandOption2);
            Regex regInternal = new Regex(String.Format(@"<%\$\$\s*{0}\s*\$\$%>", regExpString), RegexOptions.IgnoreCase);
            result = regInternal.Replace(input, replaceString);
            return result;
        }

        
    }
}
