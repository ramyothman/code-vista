using System;
using System.Collections.Generic;
using System.Text;

namespace <%$$ NameSpace $$%>
{
    public class <%$$ TableName $$%>
    {
        #region Declarations
        <%$$ TableColumns,Var $$%>  //Could be also Var,Property it's like passing display parameter using comma
        <%$$ TableColumns,Property $$%>
        <%$$ TableColumns,Set $$%>
        <%$$ TableColumns,Get $$%>
        #endregion

        public <%$$ TableName $$%>()
        {

        }

        public void Insert([$$<%$$ TableColumn,DataType $$%> <%$$ TableColumns,Name $$%>$$])
        {
            Insert(<%$$ TableColumns,ParamVal $$%>,Original_<%$$ TableColumns,ParamValKeys $$%>)
        }
    }
}


//Code Generation KeyWords
//<%$$ FileName $$%> "single" Parameters(TableName,ClassName,"Prefix","Suffix")
//<%$$ NameSpace $$%>  "single"
//<%$$ ClassName $$%> "single"
//<%$$ TableName $$%> "single"
//<%$$ TableColumns $$%> "plural" Parameters (Var,Property,Set,Get,ParamVal,ParamValKey,ParamKeys)
//<%$$ LibraryType $$%> "single"
//[$$
