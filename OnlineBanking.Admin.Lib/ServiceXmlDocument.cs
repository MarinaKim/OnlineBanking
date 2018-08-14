using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using OnlineBanking.Admin.Lib.Model;

namespace OnlineBanking.Admin.Lib
{
    public class ServiceXmlDoument
    {
        public ServiceXmlDoument() { }
        public ServiceXmlDoument(string pathDocument)
        {
            this.pathDocument = pathDocument;
        }
        private string pathDocument { get; set; }

        public XmlDocument GetDocument()
        {
            XmlDocument doc = new XmlDocument();

            if (!string.IsNullOrEmpty(pathDocument))
            {
                FileInfo file = new FileInfo(pathDocument);
                if (file.Exists)
                {
                    try
                    {
                        doc.Load(pathDocument);
                    }
                    catch (XmlException ex)
                    {
                        if (ex.HResult == -2146232000)
                        {
                            XmlElement root = doc.CreateElement("operators");
                            doc.AppendChild(root);
                            //<operators></operators>
                            doc.Save(pathDocument);
                        }
                    }


                    //if (doc.HasChildNodes)
                    if (doc.DocumentElement != null)
                        return doc;
                    else
                    {
                        XmlElement root = doc.CreateElement("operators");
                        doc.AppendChild(root);
                        //<operators></operators>
                        doc.Save(pathDocument);
                        return doc;
                    }
                }
                else
                {
                    using (Stream stream = file.Create())
                    {
                        XmlElement root = doc.CreateElement("operators");
                        doc.AppendChild(root);
                    }
                    doc.Save(pathDocument);
                    return doc;
                }
            }
            else
                throw new FileNotFoundException();
        }

        public void CreateOperator(Operator oper)
        {
            //1. получить документ
            XmlDocument doc = GetDocument();

            if (ExistsOperator(oper.name) == false)
            {
                #region 2.  создфть xml для нового оператора
                XmlElement xOper = doc.CreateElement("operator");
                //<operator></operator>
                XmlElement xName = doc.CreateElement("name");
                xName.InnerText = oper.name;
                xOper.AppendChild(xName);
                //<name> Beeline</name>

                XmlElement xPercent = doc.CreateElement("percent");
                xPercent.InnerText = oper.percent.ToString();
                xOper.AppendChild(xPercent);

                XmlElement xLogo = doc.CreateElement("logo");
                xLogo.InnerText = oper.Logo;
                xOper.AppendChild(xLogo);

                XmlElement xPrefixes = doc.CreateElement("prefixes");
                foreach (Prefix pref in oper.prefixes)
                {
                    if (ExistsPrefixOperator(pref.Pref) == false)
                    {
                        XmlElement xPrefix = doc.CreateElement("prefix");
                        xPrefix.InnerText = pref.Pref.ToString();
                        xPrefixes.AppendChild(xPrefix);
                        //<prefixes <prefix>777</prefix></prefixes>
                    }
                    else
                        Console.WriteLine("Префикс "+pref.Pref+" уже существует");
                }
                xOper.AppendChild(xPrefixes);
                #endregion

                //3. добавить  xml с новым оператором в документ
                doc.DocumentElement.AppendChild(xOper);
                //4. сохранить документ
                doc.Save(pathDocument);
            }
            else throw new Exception("Operator " + oper.name + " уже сууществует");
        }

        private bool ExistsOperator(string name)
        {
            //1.получить документ

            XmlDocument doc = GetDocument();
            //2. вытащить все наименования опреаторов
            XmlNodeList operators=doc.SelectNodes("operators/operator/name");
            foreach (XmlNode item in operators)
            {
                if (item.InnerText.ToUpper() == name.ToUpper())
                    return true;
            }

            /*
             
             */
            return false;
            //3. проверить
        }

        private bool ExistsPrefixOperator(int prefix)
        {
            //1.получить документ
            XmlDocument doc = GetDocument();

            //2. вытащить все префиксы опреаторов
            XmlNodeList operators = doc.SelectNodes("operators/operator/prefixes/prefix");
            foreach (XmlNode item in operators)
            {
                if (item.InnerText == prefix.ToString())
                    return true;
            }
             return false;          
        }
    }
}
