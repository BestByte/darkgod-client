/****************************************************
    文件：GameRoot.cs
	作者：Plane
    邮箱: 1785275942@qq.com
    日期：2018/12/3 5:30:21
	功能：游戏启动入口
*****************************************************/

using PEProtocol;
using UnityEngine;
/// <summary>
/// 游戏启动入口
/// </summary>
public class GameRoot : MonoBehaviour {
    public static GameRoot Instance = null;

    public LoadingWnd loadingWnd;
    public DynamicWnd dynamicWnd;

    private void Start() {
        Instance = this;
        DontDestroyOnLoad(this);
        PECommon.Log("Game Start...");

        ClearUIRoot();

        Init();
    }
	/// <summary>
	/// 在GameRoot里面新增加两个方法，一个是AddTips用来实现tips的添加，一个是ClearUIRoot用来把所有的Wnd都隐藏
	/// </summary>
	private void ClearUIRoot() {
        Transform canvas = transform.Find("Canvas");
        for (int i = 0; i < canvas.childCount; i++) {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Init() {

        //服务模块初始化
        NetService net = GetComponent<NetService>();
        net.InitSvc();

        ResService res = GetComponent<ResService>();
        res.InitSvc();
        AudioService audio = GetComponent<AudioService>();
        audio.InitSvc();
        TimerService timer = GetComponent<TimerService>();
        timer.InitSvc();


        //业务系统初始化
        LoginSystem login = GetComponent<LoginSystem>();
        login.InitSys();

        MainCitySystem maincity = GetComponent<MainCitySystem>();
        maincity.InitSys();
        FubenSystem fuben = GetComponent<FubenSystem>();
        fuben.InitSys();
        BattleSystem battle = GetComponent<BattleSystem>();
        battle.InitSys();

        dynamicWnd.SetWndState();


        //进入登录场景并加载相应UI
        login.EnterLogin();
    }

    public static void AddTips(string tips) {
        Instance.dynamicWnd.AddTips(tips);
    }

    private PlayerData playerData = null;
    public PlayerData PlayerData {
        get {
            return playerData;
        }
    }
    public void SetPlayerData(RspLogin data) {
        playerData = data.playerData;
    }

    public void SetPlayerName(string name) {
        PlayerData.name = name;
    }

    public void SetPlayerDataByGuide(RspGuide data) {
        PlayerData.coin = data.coin;
        PlayerData.lv = data.lv;
        PlayerData.exp = data.exp;
        PlayerData.guideid = data.guideid;
    }

    public void SetPlayerDataByStrong(RspStrong data) {
        PlayerData.coin = data.coin;
        PlayerData.crystal = data.crystal;
        PlayerData.hp = data.hp;
        PlayerData.ad = data.ad;
        PlayerData.ap = data.ap;
        PlayerData.addef = data.addef;
        PlayerData.apdef = data.apdef;

        PlayerData.strongArr = data.strongArr;
    }

    public void SetPlayerDataByBuy(RspBuy data) {
        PlayerData.diamond = data.dimond;
        PlayerData.coin = data.coin;
        PlayerData.power = data.power;
    }
    public void SetPlayerDataByPower(PshPower data) {
        PlayerData.power = data.power;
    }
    public void SetPlayerDataByTask(RspTakeTaskReward data) {
        PlayerData.coin = data.coin;
        PlayerData.lv = data.lv;
        PlayerData.exp = data.exp;
        PlayerData.taskArr = data.taskArr;
    }
    public void SetPlayerDataByTaskPsh(PshTaskPrgs data) {
        PlayerData.taskArr = data.taskArr;
    }
    public void SetPlayerDataByFBStart(RspFBFight data) {
        PlayerData.power = data.power;
    }
    public void SetPlayerDataByFBEnd(RspFBFightEnd data) {
        PlayerData.coin = data.coin;
        PlayerData.lv = data.lv;
        PlayerData.exp = data.exp;
        PlayerData.crystal = data.crystal;
        PlayerData.fuben = data.fuben;
    }
}