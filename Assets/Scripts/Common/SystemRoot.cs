/****************************************************
    文件：SystemRoot.cs
	作者：Plane
    邮箱: 1785275942@qq.com
    日期：2018/12/4 5:45:48
	功能：业务系统基类
*****************************************************/

using UnityEngine;

public class SystemRoot : MonoBehaviour {
    protected ResSvc resSvc;
    protected AudioService AudioService;
    protected NetService NetService;
    protected TimerService TimerService;

    public virtual void InitSys() {
        resSvc = ResSvc.Instance;
        AudioService = AudioService.Instance;
        NetService = NetService.Instance;
        TimerService = TimerService.Instance;
    }
}