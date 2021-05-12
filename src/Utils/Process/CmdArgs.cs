namespace Utils.Process
{
    public struct CmdArgs
    {
        public struct Steam
        {
            public const string AnonLogin = "+login anonymous";
            public const string ForceInstallDir = " +force_install_dir ";
            public const string DownloadWorkshopItem = " +workshop_download_item";
            public const string RimAppId = " 294100 ";
            public const string Quit = " +quit";
        }

        public struct Weidu
        {
            public const string InstallHighResPatch = "--nogame Files/HighRes.tp2 --yes --reinstall";
            public const string UninstallHighResPatch = "--nogame Files/HighRes.tp2 --uninstall";
        }
    }
}