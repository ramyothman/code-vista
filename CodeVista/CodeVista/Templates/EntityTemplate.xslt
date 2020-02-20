<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="/">

      

      <xsl:for-each select="Tables/Table">
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Text;
        using System.ComponentModel;
        using Qiyas.BusinessLogicLayer.Entity;
        
        public namespace <xsl:value-of select="NameSpace" />
        {
          [DataObject(true)]
          [Serializable]
          public class <xsl:value-of select="SingularName" />
          {

            #region Constructors
            public <xsl:value-of select="SingularName" />()
            {
                this.entity = new Qiyas.DataAccessLayer.<xsl:value-of select="Name" />();
                isNew = true;
            }
            #endregion
            #region Methods
            internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
            {
            if (isNew)
            {
            context.<xsl:value-of select="PluralName" />.InsertOnSubmit(this.entity);
                }
                else
                {
                
                }
                if (commit)
                    try
                    {
                        context.SubmitChanges();
                        isNew = false;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        lastException = ex;
                        return false;
                    }
                else
                    return null;

            }
            internal override bool? Delete(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
            {
                if (!isNew)
                    context.<xsl:value-of select="PluralName" />.DeleteOnSubmit(this.entity);
                else
                    return false;
                if (commit)
                    try
                    {
                        context.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        lastException = ex;
                        return false;
                    }
                else
                    return null;
            }
            public bool Save()
            {
                return Save(context, true).Value;
            }
            public bool Delete()
            {
                return Delete(context, true).Value;
            }
            #endregion
          }
        }
      </xsl:for-each>
        
        
    </xsl:template>
</xsl:stylesheet>
