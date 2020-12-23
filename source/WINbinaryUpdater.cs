using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Diagnostics;

namespace CheesyGame.Updater
{
    public class WINbinaryUpdater
    {
        int newval=0;
        private string dpath="";
        private void updater(){
            string tempPath = System.IO.Path.GetTempPath();
            string path = Path.Combine(new string[]{tempPath, "sharkbyte", "cheesygame", "updater"});
            if(!Directory.Exists(path)){
                Directory.CreateDirectory(path);
            }
            path=Path.Combine(path, "cheesygameSetup.exe");
            dpath = path;
            WebClient client = new WebClient();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            client.DownloadFile(new Uri("https://github.com/Sharkbyteprojects/Cheesy-Game/raw/master/builds%20to%20download/cheesygameSetup.exe"), path);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = dpath;
            try{
                Process exeProcess = Process.Start(startInfo);
                if (exeProcess != null && exeProcess.HasExited != true)
                {
                    Console.WriteLine("Start Installer\nWait for Exit");
                    exeProcess.WaitForExit();
                    if(exeProcess.ExitCode==0){
                        File.WriteAllText("./version.txt", newval.ToString());
                    }
                }
            }catch(Exception er){
                Console.WriteLine(er.Message);
            }
        }
        public static void Main ()
        {
            WINbinaryUpdater updater= new WINbinaryUpdater();
            Console.WriteLine("<c> Sharkbyteprojects\nCheesyGame Updater");
            try{
                WebClient client = new WebClient();
                Stream data = client.OpenRead(@"https://raw.githubusercontent.com/Sharkbyteprojects/Cheesy-Game/master/version.txt");
                StreamReader reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                data.Close();
                reader.Close();
                string numtoparse=Regex.Replace(s, @"[^0-9.]", string.Empty);
                int version=0;
                int xversion=0;
                Console.WriteLine("Disconnected...");
                string toparse="0.0";
                if(File.Exists("./version.txt")){
                    toparse=File.ReadAllText("./version.txt");
                }
                if(int.TryParse(numtoparse, out version)&&int.TryParse(toparse, out xversion)){
                    updater.newval=version;
                    if(version > xversion){
                        Console.WriteLine("Update available!\nTry Update");
                        updater.updater();
                    }else{
                        Console.WriteLine("Skip Update");
                    }
                }else{
                    Console.WriteLine("404 - Not Found");
                }
            }catch(Exception e){
                 Console.WriteLine(string.Format("Some Error Occured!\n{0}", e.Message));
            }
        }
    }
}