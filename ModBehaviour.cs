using System.Collections.Generic;
using Duckov.Modding;
using UnityEngine;

// 测试ModSetting的保存功能
namespace ModSettingTest1 {
    public class ModBehaviour : Duckov.Modding.ModBehaviour{
        private void OnEnable() {
            ModManager.OnModActivated += ModManager_OnModActivated;
        }
        private void OnDisable() {
            ModManager.OnModActivated -= ModManager_OnModActivated;
            Setting.Clear();
        }
        private void AddUI() {
            ModSettingAPI.AddDropdownList("D1", "下拉列表1",
                new List<string> { "选项1", "选项2", "选项3" }, 
                Setting.Dropdown1, Setting.SetDropdown1);
            ModSettingAPI.AddDropdownList("D2", "下拉列表2", 
                new List<string> { "选项7", "选项8", "选项9" },
                Setting.Dropdown2, Setting.SetDropdown2);
            ModSettingAPI.AddToggle("T1", "按钮1", Setting.Toggle1, Setting.SetToggle1);
            ModSettingAPI.AddToggle("T2", "按钮2", Setting.Toggle2, Setting.SetToggle2);
            ModSettingAPI.AddSlider("S1", "滑块1", Setting.Slider1, new Vector2(0, 100), Setting.SetSlider1);
            ModSettingAPI.AddSlider("S2", "滑块2", Setting.Slider2, new Vector2(0, 1000), Setting.SetSlider2,2);
            ModSettingAPI.AddSlider("S3", "滑块3", 60, new Vector2(0, 1000), null,3,8);
            ModSettingAPI.AddSlider("S4", "滑块4", 50,0,200,value=>{ Debug.Log("滑块4:"+value);});
            ModSettingAPI.AddInput("I1", "输入框1", Setting.Input1, 40, Setting.SetInput1);
            ModSettingAPI.AddInput("I2", "输入框2", Setting.Input2, 50, Setting.SetInput2);
            ModSettingAPI.AddKeybinding("K1", "按键绑定1", Setting.Keybinding1, Setting.SetKeybinding1);
            ModSettingAPI.AddKeybinding("K2", "按键绑定2", Setting.Keybinding2, Setting.SetKeybinding2);
            
            ModSettingAPI.AddToggle("T3", "点击移除S2", false, value => {
                ModSettingAPI.RemoveUI("S2", result => { Debug.Log($"移除{(result?"成功":"失败")}");
                });
            });
            
        }
        private void ModManager_OnModActivated(ModInfo arg1, Duckov.Modding.ModBehaviour arg2) {
            if (arg1.name != ModSettingAPI.MOD_NAME || !ModSettingAPI.Init(info)) return;
            //(触发时机:此mod在ModSetting之前启用)检查启用的mod是否是ModSetting,是进行初始化
            //先从ModSetting中读取配置
            Setting.Init();
            AddUI();
        }
        protected override void OnAfterSetup() {
            //(触发时机:ModSetting在此mod之前启用)此mod，Setup后,尝试进行初始化
            if (!ModSettingAPI.Init(info)) return;
            //先从ModSetting中读取配置
            Setting.Init();
            AddUI();
        }
    }
}