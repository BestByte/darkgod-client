/****************************************************
	文件：TimerService.cs
	作者：Plane
	邮箱: 1785275942@qq.com
	日期：2019/02/24 5:56   	
	功能：计时服务
*****************************************************/

using System;

using PEProtocol;

public class TimerService : SystemRoot {
    public static TimerService Instance = null;

    private PETimer pt;

    public void InitSvc() {
        Instance = this;
        pt = new PETimer();

        //设置日志输出
        pt.SetLog((string info) => {
            PECommon.Log(info);
        });

        PECommon.Log("Init TimerService...");
    }

    public void Update() {
        pt.Update();
    }

    public int AddTimeTask(Action<int> callback, double delay, PETimeUnit timeUnit = PETimeUnit.Millisecond, int count = 1) {
        return pt.AddTimeTask(callback, delay, timeUnit, count);
    }

    public double GetNowTime() {
        return pt.GetMillisecondsTime();
    }

    public void DelTask(int tid) {
        pt.DeleteTimeTask(tid);
    }

}