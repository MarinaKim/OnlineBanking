using OnlineBankungCustomer.Lib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OnlineBankungCustomer.Lib
{
  public  class ServiceXmlDocument
    {
        public ServiceXmlDocument() { }
        public ServiceXmlDocument(string pathDocument)
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
                            XmlElement root = doc.CreateElement("user");
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
                        XmlElement root = doc.CreateElement("user");
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
                        XmlElement root = doc.CreateElement("user");
                        doc.AppendChild(root);
                    }
                    doc.Save(pathDocument);
                    return doc;
                }
            }
            else
                throw new FileNotFoundException();
        }

        public void CreateUser (User user)
        {
            string guidUser = Guid.NewGuid().ToString();
            pathDocument = pathDocument + @"\" + guidUser + ".xml";
            XmlDocument doc = GetDocument();

            XmlElement xUser = doc.CreateElement("user");

            XmlElement xUserId = doc.CreateElement("userId");
            xUserId.InnerText = guidUser;
            xUser.AppendChild(xUserId);

            XmlElement xEmail = doc.CreateElement("email");
            xEmail.InnerText = user.email;
            xUser.AppendChild(xEmail);

            XmlElement xLogin = doc.CreateElement("login");
            xLogin.InnerText = user.login;
            xUser.AppendChild(xLogin);

            XmlElement xPassword = doc.CreateElement("password");
            xPassword.InnerText =user.Password;
            xUser.AppendChild(xPassword);

            doc.DocumentElement.AppendChild(xUser);
            doc.Save(pathDocument);

        }
    }
}
