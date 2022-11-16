using UnityEngine;
using System.Collections;
using Photon.Bolt;

public class MapInfoToken : IProtocolToken
{
    public byte[] mapInfos;
    int cnt;

    public MapInfoToken()
    {
        cnt = 0;
        mapInfos = new byte[1500];
    }

    public void add(int x,int z)
    {
        mapInfos[cnt * 2] = (byte)x;
        mapInfos[cnt * 2 + 1] = (byte)z;
        ++cnt;
    }

    public void Write(UdpKit.UdpPacket packet)
    {
        packet.WriteByteArray(mapInfos);
        
        //바이트 2개 붙이는거.
    }

    public void Read(UdpKit.UdpPacket packet)
    {
        mapInfos = packet.ReadByteArray(1500);
    }
}
