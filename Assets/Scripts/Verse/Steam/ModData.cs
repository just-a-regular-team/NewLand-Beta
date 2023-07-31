using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Newtonsoft.Json;
using UnityEngine;

public class ModData
{
    private PreviewData previewData = new PreviewData();
    [JsonIgnore]
    public DirectoryInfo rootDirInt{get;private set;}
    private Texture2D previewImage;
    private bool isLoadPreviewImage;
    
    protected ModContent<AudioClip> audioClips; 
	protected ModContent<Texture2D> textures; 
	protected ModContent<string> strings;

    private bool enable = true;//Err
    private bool isOfficial;
    public ModData(string localPath)
    {
        rootDirInt = new DirectoryInfo(localPath);

        this.audioClips = new ModContent<AudioClip>(this);
		this.textures = new ModContent<Texture2D>(this);
		this.strings = new ModContent<string>(this);

        previewData = ReadPreview();
          
        this.Init();
    }
    private PreviewData ReadPreview()// Read file as xml
    {
        PreviewData result = null;
        XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(File.ReadAllText(rootDirInt.FullName+"/Preview.xml"));
        XmlNode xmlRoot = xmlDocument.DocumentElement;

        result = new PreviewData();

        HashSet<string> hashSet = null;

        if (xmlRoot.ChildNodes.Count > 1)
		{
			hashSet = new HashSet<string>();
		}
        var fieldNames = result.GetType().GetFields()
                            .Select(field => field.Name)
                            .ToList();
        
		XmlNodeList childNodes = xmlRoot.ChildNodes;
		for (int i = 0; i < childNodes.Count; i++)
        {
            XmlNode xmlNode = childNodes[i];
            if (!(xmlNode is XmlComment))
            {
                if (childNodes.Count > 1 && !hashSet.Add(xmlNode.Name))
					{
						Debug.LogError(string.Concat(new object[]
						{
							"XML ",
							typeof(PreviewData),
							" defines the same field twice: ",
							xmlNode.Name,
							".\n\nField contents: ",
							xmlNode.InnerText,
							".\n\nWhole XML:\n\n",
							xmlRoot.OuterXml
						}));
					}
            }
            if(fieldNames.Contains(xmlNode.Name))
            {
                //Debug.Log(xmlNode.Name+" - "+xmlNode.InnerText);
                result.GetType().GetField(xmlNode.Name).SetValue(result,xmlNode.InnerText);
            }
            
        }

        return result;
    }

    void Init()//Load content in mod here
    {
        textures.ReloadAll(rootDirInt,new Func<string, bool>(ContentAssets<Texture2D>.IsAcceptableExtension));
        audioClips.ReloadAll(rootDirInt,new Func<string, bool>(ContentAssets<Texture2D>.IsAcceptableExtension));
        strings.ReloadAll(rootDirInt,new Func<string, bool>(ContentAssets<Texture2D>.IsAcceptableExtension));
    }
    public void ClearDestroy()
	{
		this.audioClips.ClearDestroy();
		this.textures.ClearDestroy();

	}
    public int numLoadOrder;
    public string Name {get{return previewData.name;}protected set{}}
    [JsonIgnore]
    public bool Enable{get{if(isOfficial){return true;}return enable;}}
     

    internal class PreviewData
    {
        public string shortname;
        public string name;
        public string author = "Anonymous";
        public string description;
        public string packageID;

        public List<string> supportedVersions;
        public List<string> loadBefore = new List<string>();
		public List<string> loadAfter = new List<string>();
		public List<string> incompatibleWith = new List<string>();
    }
}
