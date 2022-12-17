/****************************************************
	文件：FubenSystem.cs
	作者：Plane
	邮箱: 1785275942@qq.com
	日期：2019/03/01 4:01   	
	功能：副本业务系统
*****************************************************/

using PEProtocol;

public class FubenSystem : SystemRoot {
    public static FubenSystem Instance = null;

    public FubenWnd fubenWnd;

    public override void InitSys() {
        base.InitSys();

        Instance = this;
        PECommon.Log("Init FubenSystem...");
    }

    public void EnterFuben() {
        SetFubenWndState();
    }

    #region Fuben Wnd
    public void SetFubenWndState(bool isActive = true) {
        fubenWnd.SetWndState(isActive);
    }
    #endregion

    public void RspFBFight(GameMsg msg) {
        GameRoot.Instance.SetPlayerDataByFBStart(msg.rspFBFight);
        MainCitySys.Instance.maincityWnd.SetWndState(false);
        SetFubenWndState(false);
        BattleSys.Instance.StartBattle(msg.rspFBFight.fbid);
    }

}
