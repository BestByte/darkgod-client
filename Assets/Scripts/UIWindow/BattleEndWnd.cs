/****************************************************
    文件：BattleEndWnd.cs
	作者：Plane
    邮箱: 1785275942@qq.com
    日期：2019/5/3 19:30:0
	功能：战斗结算界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;


public class BattleEndWnd : WindowRoot {
    #region UI Define
    public Transform rewardTrans;
    public Button btnClose;
    public Button btnExit;
    public Button btnSure;
    public Text txtTime;
    public Text txtRestHP;
    public Text txtReward;
    public Animation ani;
    #endregion

    private FBEndType endType = FBEndType.None;

    protected override void InitWnd() {
        base.InitWnd();

        RefreshUI();
    }

    private void RefreshUI() {
        switch (endType) {
            case FBEndType.Pause:
                SetActive(rewardTrans, false);
                SetActive(btnExit.gameObject);
                SetActive(btnClose.gameObject);
                break;
            case FBEndType.Win:
                SetActive(rewardTrans, false);
                SetActive(btnExit.gameObject, false);
                SetActive(btnClose.gameObject, false);

                MapCfg cfg = ResService.GetMapCfg(fbid);
                int min = costtime / 60;
                int sec = costtime % 60;
                int coin = cfg.coin;
                int exp = cfg.exp;
                int crystal = cfg.crystal;
                SetText(txtTime, "通关时间：" + min + ":" + sec);
                SetText(txtRestHP, "剩余血量：" + resthp);
                SetText(txtReward, "关卡奖励：" + Constants.Color(coin + "金币", TxtColor.Green) + Constants.Color(exp + "经验", TxtColor.Yellow) + Constants.Color(crystal + "水晶", TxtColor.Blue));

                TimerService.AddTimeTask((int tid) => {
                    SetActive(rewardTrans);
                    ani.Play();
                    TimerService.AddTimeTask((int tid1) => {
                        AudioService.PlayUIAudio(Constants.FBItemEnter);
                        TimerService.AddTimeTask((int tid2) => {
                            AudioService.PlayUIAudio(Constants.FBItemEnter);
                            TimerService.AddTimeTask((int tid3) => {
                                AudioService.PlayUIAudio(Constants.FBItemEnter);
                                TimerService.AddTimeTask((int tid5) => {
                                    AudioService.PlayUIAudio(Constants.FBLogoEnter);
                                }, 300);
                            }, 270);
                        }, 270);
                    }, 325);
                }, 1000);
                break;
            case FBEndType.Lose:
                SetActive(rewardTrans, false);
                SetActive(btnExit.gameObject);
                SetActive(btnClose.gameObject, false);
                AudioService.PlayUIAudio(Constants.FBLose);
                break;
        }
    }

    public void ClickClose() {
        AudioService.PlayUIAudio(Constants.UIClickBtn);
        BattleSystem.Instance.battleMgr.isPauseGame = false;
        SetWndState(false);
    }

    public void ClickExitBtn() {
        AudioService.PlayUIAudio(Constants.UIClickBtn);
        //进入主城，销毁当前战斗
        MainCitySystem.Instance.EnterMainCity();
        BattleSystem.Instance.DestroyBattle();
    }

    public void ClickSureBtn() {
        AudioService.PlayUIAudio(Constants.UIClickBtn);
        //进入主城，销毁当前战斗
        MainCitySystem.Instance.EnterMainCity();
        BattleSystem.Instance.DestroyBattle();
        //打开副本界面
        FubenSystem.Instance.EnterFuben();
    }


    public void SetWndType(FBEndType endType) {
        this.endType = endType;
    }


    private int fbid;
    private int costtime;
    private int resthp;
    public void SetBattleEndData(int fbid, int costtime, int resthp) {
        this.fbid = fbid;
        this.costtime = costtime;
        this.resthp = resthp;
    }
}

public enum FBEndType {
    None,
    Pause,
    Win,
    Lose
}
