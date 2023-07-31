using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

//temporarily not interested in integrating other versions.Just read xml
public class ReadXml
{
     
    public void ReadAllXml(DirectoryInfo dir)
    {
         
         
        if(dir.GetDirectories().Length > 0)
        {
            foreach(DirectoryInfo Childdir in dir.GetDirectories())
            {
                ReadAllXml(Childdir);
            }
        }
        ReadFileXml(dir);
    }
    public void ReadFileXml(DirectoryInfo dir)
    {
        foreach(FileInfo file in dir.GetFiles("*xml"))
        {
            if(file.Extension!=".xml")
            {
                return;
            }
            string xmlContent = File.ReadAllText(file.FullName);
           
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmlContent);
                XmlNode xmlRoot = xml.DocumentElement;
                ReadNode(xmlRoot);
           
        }
    }

    private void ReturnNodes(XmlNodeList list)
    {
        foreach(XmlNode node in list)
        {
            ReadNode(node);
        }
    }
    private void ReadNode(XmlNode node)
    {
        if (node is XmlComment)
        {
            return;
        }
        if(node.HasChildNodes)
        {
            ReturnNodes( node.ChildNodes);
        }

        if(node.Name == "Sprite")
        {
            string atlasName = node.Attributes["atlasName"].Value;
            string name = node.Attributes["name"].Value;
            int x =   int.Parse( node.Attributes["x"].Value );
            int y =   int.Parse( node.Attributes["y"].Value );
            int w =   int.Parse( node.Attributes["w"].Value );
            int h =   int.Parse( node.Attributes["h"].Value );
            int pixelPerUnit = int.Parse( node.Attributes["pixelPerUnit"].Value );
            Controller.SpriteManager.LoadSprite(atlasName,name,new Rect(x,y,w,h),pixelPerUnit);
        }
    } 

    public Dictionary<string,string> Storage = new Dictionary<string, string>(); 
}
