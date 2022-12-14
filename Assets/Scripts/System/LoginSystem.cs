/****************************************************
    文件：LoginSystem.cs
	作者：Plane
    邮箱: 1785275942@qq.com
    日期：2018/12/3 5:31:49
	功能：登录注册业务系统
*****************************************************/

using PEProtocol;

public class LoginSystem : SystemRoot {
    public static LoginSystem Instance = null;

	
	public LoginWnd loginWnd;
    public CreateWnd createWnd;

    public override void InitSys() {
        base.InitSys();

        Instance = this;
        PECommon.Log("Init LoginSystem...");
    }

    /// <summary>
    /// 进入登录场景
    /// </summary>
    public void EnterLogin() {
		//异步的加载登录场景
		//并显示加载的进度
		//LoginSystem的的EnterLogin方法中进行调用
		ResService.AsyncLoadScene(Constants.SceneLogin, () => {
            //加载完成以后再打开注册登录界面
            loginWnd.SetWndState();
            AudioService.PlayBGMusic(Constants.BGLogin);
        });
    }
	/// <summary>
	/// 在LoginSystem里面，分析登录验证逻辑和回应客户端
	/// </summary>
	/// <param name="msg"></param>
	public void RspLogin(GameMsg msg) {
        GameRoot.AddTips("登录成功");
        GameRoot.Instance.SetPlayerData(msg.rspLogin);

        if (msg.rspLogin.playerData.name == "") {
            createWnd.SetWndState();
        }
        else {
            MainCitySystem.Instance.EnterMainCity();
        }
        //关闭登录界面
        loginWnd.SetWndState(false);
    }

    public void RspRename(GameMsg msg) {
        GameRoot.Instance.SetPlayerName(msg.rspRename.name);

        //跳转场景进入主城
        MainCitySystem.Instance.EnterMainCity();
        //关闭创建界面
        createWnd.SetWndState(false);
    }
}