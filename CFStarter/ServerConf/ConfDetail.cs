using System.Collections.Generic;
using System.Xml.Serialization;

namespace CFStarter.ServerConf
{

    [XmlRoot(ElementName = "Listener")]
        public class Listener
        {
            [XmlAttribute(AttributeName = "className")]
            public string ClassName { get; set; }
            [XmlAttribute(AttributeName = "SSLEngine")]
            public string SSLEngine { get; set; }
        }

        [XmlRoot(ElementName = "Resource")]
        public class Resource
        {
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
            [XmlAttribute(AttributeName = "auth")]
            public string Auth { get; set; }
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
            [XmlAttribute(AttributeName = "description")]
            public string Description { get; set; }
            [XmlAttribute(AttributeName = "factory")]
            public string Factory { get; set; }
            [XmlAttribute(AttributeName = "pathname")]
            public string Pathname { get; set; }
        }

        [XmlRoot(ElementName = "GlobalNamingResources")]
        public class GlobalNamingResources
        {
            [XmlElement(ElementName = "Resource")]
            public Resource Resource { get; set; }
        }

        [XmlRoot(ElementName = "Connector")]
        public class Connector
        {
            [XmlAttribute(AttributeName = "port")]
            public string Port { get; set; }
            [XmlAttribute(AttributeName = "protocol")]
            public string Protocol { get; set; }
            [XmlAttribute(AttributeName = "connectionTimeout")]
            public string ConnectionTimeout { get; set; }
            [XmlAttribute(AttributeName = "redirectPort")]
            public string RedirectPort { get; set; }
            [XmlAttribute(AttributeName = "packetSize")]
            public string PacketSize { get; set; }
            [XmlAttribute(AttributeName = "tomcatAuthentication")]
            public string TomcatAuthentication { get; set; }
            [XmlAttribute(AttributeName = "maxThreads")]
            public string MaxThreads { get; set; }
        }

        [XmlRoot(ElementName = "Realm")]
        public class Realm
        {
            [XmlAttribute(AttributeName = "className")]
            public string ClassName { get; set; }
            [XmlAttribute(AttributeName = "resourceName")]
            public string ResourceName { get; set; }
        }

        [XmlRoot(ElementName = "PreResources")]
        public class PreResources
        {
            [XmlAttribute(AttributeName = "className")]
            public string ClassName { get; set; }
            [XmlAttribute(AttributeName = "base")]
            public string Base { get; set; }
            [XmlAttribute(AttributeName = "webAppMount")]
            public string WebAppMount { get; set; }
        }

        [XmlRoot(ElementName = "Resources")]
        public class Resources
        {
            [XmlElement(ElementName = "PreResources")]
            public List<PreResources> PreResources { get; set; }
        }

        [XmlRoot(ElementName = "Context")]
        public class Context
        {
            [XmlElement(ElementName = "Resources")]
            public Resources Resources { get; set; }
            [XmlAttribute(AttributeName = "path")]
            public string Path { get; set; }
            [XmlAttribute(AttributeName = "docBase")]
            public string DocBase { get; set; }
            [XmlAttribute(AttributeName = "workDir")]
            public string WorkDir { get; set; }
        }

        [XmlRoot(ElementName = "Host")]
        public class Host
        {
            [XmlElement(ElementName = "Context")]
            public Context Context { get; set; }
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
            [XmlAttribute(AttributeName = "appBase")]
            public string AppBase { get; set; }
            [XmlAttribute(AttributeName = "unpackWARs")]
            public string UnpackWARs { get; set; }
            [XmlAttribute(AttributeName = "autoDeploy")]
            public string AutoDeploy { get; set; }
        }

        [XmlRoot(ElementName = "Engine")]
        public class Engine
        {
            [XmlElement(ElementName = "Realm")]
            public Realm Realm { get; set; }
            [XmlElement(ElementName = "Host")]
            public Host Host { get; set; }
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
            [XmlAttribute(AttributeName = "defaultHost")]
            public string DefaultHost { get; set; }
            [XmlAttribute(AttributeName = "jvmRoute")]
            public string JvmRoute { get; set; }
        }

        [XmlRoot(ElementName = "Service")]
        public class Service
        {
            [XmlElement(ElementName = "Connector")]
            public List<Connector> Connector { get; set; }
            [XmlElement(ElementName = "Engine")]
            public Engine Engine { get; set; }
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
        }

        [XmlRoot(ElementName = "Server")]
        public class Server
        {
            [XmlElement(ElementName = "Listener")]
            public List<Listener> Listener { get; set; }
            [XmlElement(ElementName = "GlobalNamingResources")]
            public GlobalNamingResources GlobalNamingResources { get; set; }
            [XmlElement(ElementName = "Service")]
            public Service Service { get; set; }
            [XmlAttribute(AttributeName = "port")]
            public string Port { get; set; }
            [XmlAttribute(AttributeName = "shutdown")]
            public string Shutdown { get; set; }
        }

}
