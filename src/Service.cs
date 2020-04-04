using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace EnvironmentControl {
    public class Service : IService {
        const int HWND_BROADCAST = 0xffff;
        const uint WM_SETTINGCHANGE = 0x001a;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SendNotifyMessage(IntPtr hWnd, uint Msg,
            UIntPtr wParam, string lParam);

        public void SetVariable(string value) {
            using (var envKey = Registry.LocalMachine.OpenSubKey(
                @"SYSTEM\CurrentControlSet\Control\Session Manager\Service",
                true)) {
                envKey.SetValue("MY_VAR", value);
                SendNotifyMessage((IntPtr)HWND_BROADCAST, WM_SETTINGCHANGE,
                    (UIntPtr)0, "Service");
            }
        }

        public string GetVariable() {
            return string.Empty;
        }

        public async Task<VariableValue[]> LoadItems() {
            try {
                var json = await File.ReadAllTextAsync("db.json");
                return JsonConvert.DeserializeObject<Db>(json).Values;
            }
            catch (FileNotFoundException) {
                return new VariableValue[0];
            }
        }
    }
}
