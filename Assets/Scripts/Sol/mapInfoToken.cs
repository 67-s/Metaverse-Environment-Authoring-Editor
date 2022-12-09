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
        mapInfos = new byte[7980]; //9976
    }

    public void add(float x)
    {
        byte[] bytes = System.BitConverter.GetBytes(x);
        for(int i=0; i<4; ++i)
        {
            mapInfos[cnt++] = bytes[i];
        }
        Debug.Log(cnt);
    }

    public void addByte(byte x)
    {
        mapInfos[cnt++] = x;
    }

    public void Write(UdpKit.UdpPacket packet)
    {
        packet.WriteByteArray(mapInfos);
        //바이트 2개 붙이는거.
    }

    public void Read(UdpKit.UdpPacket packet)
    {
        mapInfos = packet.ReadByteArray(7980);
    }
}
