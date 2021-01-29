using System;
using System.Collections.Generic;
using System.Xml;

namespace Devices
{
    public class ZoneGroupTopology
    {
        //private ZonePlayers players;
        ZoneGroupTopology zoneGroupTopology;

        //        public ZoneGroupTopology(ref ZonePlayers zonePlayers)
        //public ZoneGroupTopology()
        //{
            //zonePlayers = zonePlayers;
            //string playerIpAddress = zonePlayers.ZonePlayersList[0].PlayerIpAddress;
            //getZoneGroupTopology(playerIpAddress);
        //}

        private List<ZoneGroup> zoneGroupList = new List<ZoneGroup>();

        public List<ZoneGroup> ZoneGroupList
        {
            get => zoneGroupList;
            set => zoneGroupList = value;
        }

        private StereoPairs stereoPairs;
        
        public StereoPairs StereoPairs
        { 
            get => stereoPairs; 
            set => stereoPairs = value; 
        }

        //private string soapBody = string.Empty;
        //private string action = string.Empty;
        //private string endpoint = string.Empty;

        //private XmlDocument doc = new XmlDocument();
        //private XmlNamespaceManager ns;

        //private void getZoneGroupTopology(string playerIpAddress)
        //{
        //    soapBody = Commss.Properties.SonosSettings.Default.GET_ZONE_GROUP_STATE_BODY;
        //    action = Comms.Properties.SonosSettings.Default.GET_ZONE_GROUP_STATE_ACTION;
        //    endpoint = Comms.Properties.SonosSettings.Default.ZONE_GROUP_ENDPOINT;

        //    SoapUtils su = new SoapUtils();

        //    string response = su.invokeService(playerIpAddress, soapBody, endpoint, action);
        //    if (response != string.Empty)
        //    {
        //        doc.LoadXml(response);

        //        try
        //        {
        //            ns = new XmlNamespaceManager(doc.NameTable);
        //            ns.AddNamespace("u", "urn:schemas-upnp-org:service:ZoneGroupTopology:1");
        //            XmlNode zoneGroupState = doc.SelectSingleNode("//*[local-name()='ZoneGroupState']");
        //            XmlDocument zoneGS = new XmlDocument(ns.NameTable);
        //            zoneGS.LoadXml(zoneGroupState.InnerText);

        //            getZoneGroups(zoneGS);
        //            getStereoPairs(zoneGS);
        //        }
        //        catch (Exception ex)
        //        {
        //            //MessageBox.Show(ex.Message);
        //        }
        //    }

            //void getZoneGroups(XmlDocument doc)
            //{
            //    XmlNodeList zoneGroups = doc.SelectNodes("//*[local-name()='ZoneGroup']");

            //    if (zoneGroups.Count > 0)
            //    {
            //        foreach (XmlNode node in zoneGroups)
            //        {
            //            ZoneGroup zoneGroup = new ZoneGroup();
            //            zoneGroup.ZoneGroupCoordinator = node.Attributes.GetNamedItem("Coordinator").Value;
            //            zoneGroup.ZoneGroupId = node.Attributes.GetNamedItem("ID").Value;

            //            getGroupMembers(zoneGroup, node);

            //            zoneGroup.ZoneGroupName = setCurrentGroupName(zoneGroup, node);

            //            if (zoneGroup.ZoneGroupName != string.Empty)
            //            {
            //                zoneGroupList.Add(zoneGroup);
            //            }
            //        }
            //    }
            //}

            //void getStereoPairs(XmlDocument doc)
            //{
            //    XmlNodeList stereoPairs = doc.SelectNodes("//*[local-name()='ZoneGroup']/*[local-name()='ZoneGroupMember'][@ChannelMapSet]");

            //    if (stereoPairs.Count > 0)
            //    {
            //        StereoPairs = new StereoPairs();

            //        XmlNodeList channelMapSets = doc.SelectNodes("//*[local-name()='ZoneGroup']/*[local-name()='ZoneGroupMember']/@ChannelMapSet[not(. = preceding::*/@ChannelMapSet)]");

            //        foreach (XmlNode channelMapSet in channelMapSets)
            //        {
            //            XmlNodeList pairMembers = doc.SelectNodes("//*[local-name()='ZoneGroup']/*[local-name()='ZoneGroupMember'][@ChannelMapSet='" + channelMapSet.InnerText + "']");
            //            StereoPair sp = new StereoPair();
            //            sp.ChannelMapSet = channelMapSet.InnerText;
            //            sp.PairName = pairMembers.Item(0).Attributes.GetNamedItem("ZoneName").InnerText;
            //            foreach (XmlNode pairMemeber in pairMembers)
            //            {
            //                if (pairMemeber.SelectSingleNode("@Invisible") != null)
            //                {
            //                    sp.RightUUID = pairMemeber.SelectSingleNode("@UUID").InnerText;
            //                }
            //                else
            //                {
            //                    sp.LeftUUID = pairMemeber.SelectSingleNode("@UUID").InnerText;
            //                }
            //            }

            //            StereoPairs.StereoPairsList.Add(sp);
            //        }
            //    }
            //}

            //string setCurrentGroupName(ZoneGroup zoneGroup, XmlNode node) 
            //{
            //    string zoneGroupName = string.Empty;
            //    string coord = zoneGroup.ZoneGroupCoordinator;
            //    ZoneGroupMember coordMember = zoneGroup.ZoneGroupMemeberList.Find(x => x.UUID == coord);
            //    if (!coordMember.Invisible)
            //    {
            //        zoneGroupName = coordMember.ZoneName;
            //    }
            //    foreach (ZoneGroupMember member in zoneGroup.ZoneGroupMemeberList.FindAll(x => x.UUID != coord))
            //    {
            //        if (!member.Invisible)
            //        {
            //            if (zoneGroupName == string.Empty)
            //            {
            //                zoneGroupName = member.ZoneName;
            //            }
            //            else
            //            {
            //                zoneGroupName = zoneGroupName + Environment.NewLine + member.ZoneName;
            //            }
            //        }
            //    }
            //    return zoneGroupName;
            //}

            //void getGroupMembers(ZoneGroup zoneGroup, XmlNode node)
            //{
            //    XmlNodeList zoneGroupMembers = node.SelectNodes("*[local-name()='ZoneGroupMember']");
            //    if (zoneGroupMembers.Count > 0)
            //    {
            //        foreach (XmlNode memberNode in zoneGroupMembers)
            //        {
            //            ZoneGroupMember zoneGroupMember = new ZoneGroupMember();
            //            zoneGroupMember.UUID = memberNode.Attributes.GetNamedItem("UUID").Value;
            //            zoneGroupMember.ZoneName = memberNode.Attributes.GetNamedItem("ZoneName").Value;
            //            if (memberNode.Attributes.GetNamedItem("ChannelMapSet") != null)
            //            {
            //                zoneGroupMember.ChannelMapSet = memberNode.Attributes.GetNamedItem("ChannelMapSet").Value;
            //            }
            //            if (memberNode.Attributes.GetNamedItem("Invisible") != null && memberNode.Attributes.GetNamedItem("Invisible").Value == "1")
            //            {
            //                zoneGroupMember.Invisible = true;
            //            }
            //            zoneGroup.ZoneGroupMemeberList.Add(zoneGroupMember);
            //        }
            //    }
            //}
        }
   // }
}
