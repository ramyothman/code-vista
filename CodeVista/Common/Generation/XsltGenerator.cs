using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace Common.Generation
{
    public struct XsltParam
    {
        public string Name;
        public string Value;
    }
    public class XsltGenerator
    {
        public static void GenerateViaXSLT(string xsltFileName, XmlDocument xmlMetaData, string outputFile, params XsltParam[] paramsList)
        {
            System.Xml.Xsl.XslTransform xslt = new System.Xml.Xsl.XslTransform();
            //System.Xml.Xsl.XslCompiledTransform compiledTransform = new System.Xml.Xsl.XslCompiledTransform();
            
            System.Xml.XPath.XPathNavigator xNav;
            System.IO.StreamWriter streamWriter = null;

            System.Xml.Xsl.XsltArgumentList args = new System.Xml.Xsl.XsltArgumentList();
            
            
            try
            {
                if(xmlMetaData == null)
                {
                    xmlMetaData = new XmlDocument();
                }

                foreach(var param in paramsList)
                {
                    args.AddParam(param.Name, "", param.Value);
                }
                xNav = xmlMetaData.CreateNavigator();
                streamWriter = new System.IO.StreamWriter(outputFile);
                xslt.Load(xsltFileName);
                xslt.Transform(xNav, args, streamWriter, null);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                if(streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
        }
    }
}
